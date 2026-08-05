using BankApp_Api.Core.DTO.Auth;
using BankApp_Api.Core.Repositories;
using BankApp_Api.Core.Services;
using BankApp_Api.Repository.Entities;
using BankApp_Api.Service.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Service.Services
{
    public class AuthService : IAuthService
    {
       private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;



        public AuthService(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            
        }
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto, CancellationToken cancellationToken = default)
        {
            //Find the user by username
            var users = await _userRepository.WhereAsync(u => u.Username == dto.Username && !u.IsDeleted, cancellationToken);
            var user = users.FirstOrDefault();

            if(user == null)
            {
                throw new Exception("Invalid username or password");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            //Verify the password
            if (!isPasswordValid)
            {
                throw new Exception("Invalid username or password");
            }
            //Generate JWT token
            var token = JwtHelper.GenerateJwtToken(user, _configuration);
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpireMinutes"] ?? "60"));

            return new LoginResponseDTO
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Expiration = expiration
            };
        }

        public async Task RegisterAsync(RegisterDTO dto, CancellationToken cancellationToken = default)
        {
            bool exists = await _userRepository.AnyAsync(u => u.Username == dto.Username && !u.IsDeleted, cancellationToken);
            if (exists)
            {
                throw new Exception("Username already exists");
            }
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
