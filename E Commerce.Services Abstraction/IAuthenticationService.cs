using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IAuthenticationService
    {
        // Login                   
        // email , password =>  UserDTO ( email , DisplayName , Token) 

        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

        // Register 
        // Email , password , UserName , PhoneNumber , DisplayName =>  UserDTO (  email , DisplayName , Token )

        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    }
}
