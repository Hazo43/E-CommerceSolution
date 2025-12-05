using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (User is null)
                return Error.InvalidCrendentials("User.InvalidCrendentials");

            var IsPasswordValid = _userManager.CheckPasswordAsync(User, loginDTO.Password);
            if(IsPasswordValid is null) // false يعني لو ب
                return Error.InvalidCrendentials("User.InvalidCrendentials");
            // true ب  IsPasswordValid لو ال
            return new UserDTO(User.Email!, User.DisplayName, "Token");

        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName,
            };

            var identityResult = await _userManager.CreateAsync(User, registerDTO.Password);
            if (identityResult.Succeeded == true)
                return new UserDTO(registerDTO.Email, registerDTO.DisplayName, "Token");

            return identityResult.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList();

        }
    }
}
