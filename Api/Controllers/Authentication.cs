using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParceriaAPI.SeveralFunctions;
using System;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    [ApiController]
    [Route("[controller]")]
    public sealed class Authentication
    {
        #region --> Private properties. <--

        private const string roles = TokenService.Fiap;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public Authentication()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        /// <summary>
        /// Método para gerar o token de acesso aos métodos da API.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///{
        ///"Username":"userfiap",
        ///"Password":"passfiap"
        ///}
        /// </remarks>
        /// <returns>Retornará um token para ser informado em cada método da API, sem ele, o acesso será negado.</returns>
        /// <response code="200">Token gerado com sucesso.</response>
        /// <response code="400">Usuário/senha inválidos.</response>          
        [HttpPost]
        [Route("token/generate")]
        [AllowAnonymous]
        public ActionResult<dynamic> GenerateToken([FromBody] string userName, string password)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                TokenService tokenService = new TokenService();

                if (!tokenService.Get(userName, password)) serviceResponse.message = "Usuário/senha inválidos.";
                else
                {
                    serviceResponse.obj = tokenService.GenerateToken(userName, roles);
                    serviceResponse.success = true;
                    serviceResponse.message = "Token gerado com sucesso.";
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally
            {
                stopwatch.Stop();
                serviceResponse.message += " | Trafegados " + Tools.GetSize(serviceResponse.obj) + " em " + Tools.GetTime(stopwatch.Elapsed);

                GC.Collect();
            }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
