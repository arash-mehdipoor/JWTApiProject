using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiProject.Models.Services
{
    public interface ITokenValidator
    {
        Task Execute(TokenValidatedContext context);
    }

}
