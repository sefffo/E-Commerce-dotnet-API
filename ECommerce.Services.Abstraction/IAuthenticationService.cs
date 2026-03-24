using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.SharedLibirary.CommonResult;
using ECommerce.SharedLibirary.DTO_s.IdentityDTOs;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Services.Abstraction
{
    public interface IAuthenticationService
    {
        //login 
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);



        //register 

        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);

        //generating JWT Token


        Task<string> GenerateJWTToken(AppUser appUser);



        //check email 

        Task<bool> CheckEmailAsync(string Email); //==> get it from teh token 

        //get current user address
        Task<Result<AddressDTO>> GetUserAddressAsync(string email);

        Task<Result<AddressDTO>> UpdateUserAddressAsync(string email , AddressDTO NewAddress); //take the email from the token and the new addressDto ==> return updated address

        //get the current user 
        //get the mail from the token and return the user name and email

        Task<Result<UserDTO>> GetCurrentUserAsync(string email , string Token);

    }
}
