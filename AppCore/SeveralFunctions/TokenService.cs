using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParceriaAPI.SeveralFunctions
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class TokenService
    {
        #region --> Public static properties. <--

        public const string Teste = "8e6f6f815b50f474cf0dc22d4f400725";     

        #endregion --> Public static properties. <--

        #region --> Private properties. <--

        public static string Secret = "921130442a68c485734fcaf4ca9dddad";

        #endregion --> Private properties. <--

        #region --> Public methods. <--

        public string GenerateToken(string userName, string role)
        {
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(GlobalAtributtes.TokenExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }     

        #endregion --> Public methods. <--   
    }
}
