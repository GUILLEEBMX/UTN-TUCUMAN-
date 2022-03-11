using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiVentas.DTOs;

namespace WebApiVentas.Controllers.V1
{
   
    [ApiController]
    [Route("api/v1/cuentas")]
    public class CuentasController : ControllerBase
    {


        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;


        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration,SignInManager<IdentityUser>signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(CredencialUsuarioDTO credencialUsuarioDTO)
        {
            var usuario = new IdentityUser { UserName = credencialUsuarioDTO.email,
                Email = credencialUsuarioDTO.email };

            var resultado = await userManager.CreateAsync(usuario, credencialUsuarioDTO.password);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialUsuarioDTO);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
            


        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialUsuarioDTO credencialUsuarioDTO)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialUsuarioDTO.email,
                credencialUsuarioDTO.password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialUsuarioDTO);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var credencialUsuario = new CredencialUsuarioDTO()
            {
                email = email
            };
            return await ConstruirToken(credencialUsuario);

        }

        private async Task<ActionResult<RespuestaAutenticacionDTO>> ConstruirToken(CredencialUsuarioDTO credencialUsuarioDTO)
        {
            var claims = new List<Claim>()
            { new Claim("email", credencialUsuarioDTO.email) 
            };

            var usuario = await userManager.FindByEmailAsync(credencialUsuarioDTO.email);

            var claimDB= await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));

            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securitytoken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacionDTO() { Token = new JwtSecurityTokenHandler().WriteToken(securitytoken),Expiracion= expiracion};
        }

        [HttpPost("HacerAdmin")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {

            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email); ;
            await userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();
        }

        [HttpPost("RemoverAdmin")]
        public async Task<ActionResult> RemoveAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);
            await userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();
        }

    }
}
