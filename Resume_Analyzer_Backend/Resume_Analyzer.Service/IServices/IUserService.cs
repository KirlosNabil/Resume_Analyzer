using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Resume_Analyzer.Service.DTOs;

namespace Resume_Analyzer.Service.IServices
{
    public interface IUserService
    {
        public Task Register(RegisterDTO registerDto);
        public Task<TokenDTO> Login(LoginDTO loginDto);
    }
}
