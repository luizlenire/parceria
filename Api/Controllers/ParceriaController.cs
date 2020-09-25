using Api.Models;
using AppCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParceriaAPI.SeveralFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class ParceriaController
    {
        #region --> Private properties. <--

        private const string roles = TokenService.Fiap;

        private Stopwatch stopwatch { get; set; }

        struct RetornaListaPesquisaParceria
        {
            public string TituloDaParceria { get; set; }

            public string NomeDaEmpresa { get; set; }

            public DateTime DataDeTermino { get; set; }

            public int QtdLikes { get; set; }

            public string Url { get; set; }
        }

        struct RetornaParceria
        {
            public string TituloDaParceria { get; set; }

            public string DescricaoDaParceria { get; set; }

            public string NomeDaEmpresa { get; set; }

            public DateTime DataDeTermino { get; set; }

            public string Url { get; set; }
        }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public ParceriaController()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        /// <summary>
        /// Retornará todas as parcerias cadastradas no controle de parcerias e que a data atual esteja entre a data de início e término.        
        /// </summary>
        /// <param name="dateTime">Data atual para pesquisa de parcerias.</param>
        /// <returns>Retorno de objeto com parcerias.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno virá com as parcerias.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o problema.</response>    
        [HttpGet]
        [Route("retorna-lista")]
        [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult RetornaLista(DateTime? dateTime)
        {
            ServiceResponse<List<RetornaListaPesquisaParceria>> serviceResponse = new ServiceResponse<List<RetornaListaPesquisaParceria>>();

            try
            {
                AppCore.Controllers.Parceria parceria = new AppCore.Controllers.Parceria();
                List<vParceria> listvParceria = parceria.Get(default, default, (dateTime ?? Tools.GetDateTimeNow()));

                if (listvParceria.Count != default)
                {
                    serviceResponse.success = true;
                    serviceResponse.message = "Lista de parcerias obtidas com sucesso";
                    serviceResponse.obj = new List<RetornaListaPesquisaParceria>();

                    foreach (vParceria item in listvParceria)
                    {
                        serviceResponse.obj.Add(new RetornaListaPesquisaParceria()
                        {
                            TituloDaParceria = item.Titulo,
                            NomeDaEmpresa = item.Empresa,
                            DataDeTermino = item.DataTermino,
                            QtdLikes = item.QtdLikes,
                            Url = item.URLPagina
                        });
                    }
                }
                else serviceResponse.message = "Não foi possível localizar parcerias na data informada.";
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally
            {
                stopwatch.Stop();
                serviceResponse.message += " | Trafegados " + Tools.GetSize(serviceResponse.obj) + " em " + Tools.GetTime(stopwatch.Elapsed);

                GC.Collect();
            }

            return Startup.responseAPI.Process(serviceResponse);
        }


        /// <summary>
        /// Irá receber um termo para ser pesquisado em qualquer parte dos campos: título e nome da empresa, deve também respeitar a regra, se a data atual esteja entre a data de início e término.
        /// </summary>
        /// <param name="search">Termo para busca entre título da parceria ou empresa</param>
        /// <param name="dateTime">Data atual para pesquisa de parcerias.</param>
        /// <returns>Retorno de objeto com parcerias.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno virá com as parcerias.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o problema.</response>    
        [HttpGet]
        [Route("pesquisa-parceria")]
        [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult PesquisaParceria(string search, DateTime? dateTime)
        {
            ServiceResponse<List<RetornaListaPesquisaParceria>> serviceResponse = new ServiceResponse<List<RetornaListaPesquisaParceria>>();

            try
            {
                AppCore.Controllers.Parceria parceria = new AppCore.Controllers.Parceria();
                List<vParceria> listvParceria = parceria.Get(default, search, (dateTime ?? Tools.GetDateTimeNow()));

                if (listvParceria.Count != default)
                {
                    serviceResponse.success = true;
                    serviceResponse.message = "Lista de parcerias obtidas com sucesso";
                    serviceResponse.obj = new List<RetornaListaPesquisaParceria>();

                    foreach (vParceria item in listvParceria)
                    {
                        serviceResponse.obj.Add(new RetornaListaPesquisaParceria()
                        {
                            TituloDaParceria = item.Titulo,
                            NomeDaEmpresa = item.Empresa,
                            DataDeTermino = item.DataTermino,
                            QtdLikes = item.QtdLikes,
                            Url = item.URLPagina
                        });
                    }
                }
                else serviceResponse.message = "Não foi possível localizar parcerias nos parâmetros informados.";
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally
            {
                stopwatch.Stop();
                serviceResponse.message += " | Trafegados " + Tools.GetSize(serviceResponse.obj) + " em " + Tools.GetTime(stopwatch.Elapsed);

                GC.Collect();
            }

            return Startup.responseAPI.Process(serviceResponse);
        }

        /// <summary>
        /// O parâmetro será o código da parceria e deve trazer os campos abaixo em JSON.      
        /// </summary>
        /// <param name="codigo">Código da parceria.</param>
        /// <returns>Retorno de objeto com parcerias.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno virá com as parcerias.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o problema.</response>    
        [HttpGet]
        [Route("retorna-parceria")]
        [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult RetornaLista(int codigo)
        {
            ServiceResponse<List<RetornaParceria>> serviceResponse = new ServiceResponse<List<RetornaParceria>>();

            try
            {
                AppCore.Controllers.Parceria parceria = new AppCore.Controllers.Parceria();
                List<vParceria> listvParceria = parceria.Get(codigo, default, Tools.GetDateTimeNow());

                if (listvParceria.Count != default)
                {
                    serviceResponse.success = true;
                    serviceResponse.message = "Parceria obtidas com sucesso";
                    serviceResponse.obj = new List<RetornaParceria>();

                    foreach (vParceria item in listvParceria)
                    {
                        serviceResponse.obj.Add(new RetornaParceria()
                        {
                            TituloDaParceria = item.Titulo,
                            DescricaoDaParceria = item.Descricao,
                            NomeDaEmpresa = item.Empresa,
                            DataDeTermino = item.DataTermino,
                            Url = item.URLPagina
                        });
                    }
                }
                else serviceResponse.message = "Não foi possível localizar parceria com o código informado.";
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally
            {
                stopwatch.Stop();
                serviceResponse.message += " | Trafegados " + Tools.GetSize(serviceResponse.obj) + " em " + Tools.GetTime(stopwatch.Elapsed);

                GC.Collect();
            }

            return Startup.responseAPI.Process(serviceResponse);
        }


        /// <summary>
        /// Vai receber o código da parceira e deve fazer o cadastro na tabela “ParceriaLike”.    
        /// </summary>
        /// <param name="codigo">Código da parceria.</param>
        /// <returns>Retorno de objeto com parcerias.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno virá com as parcerias.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o problema.</response>    
        [HttpPost]
        [Route("cadastrar-like")]
        [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CadastrarLike([FromBody] int codigo)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                if (codigo != default)
                {
                    AppCore.Controllers.ParceriaLike parceriaLike = new AppCore.Controllers.ParceriaLike();
                    parceriaLike.Post(codigo);

                    serviceResponse.success = true;
                    serviceResponse.message = "Requisição processada com sucesso.";
                }
                else serviceResponse.message = "É necessário informar um código válido.";
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally
            {
                stopwatch.Stop();
                serviceResponse.message += " | Trafegados " + Tools.GetSize(serviceResponse.obj) + " em " + Tools.GetTime(stopwatch.Elapsed);

                GC.Collect();
            }

            return Startup.responseAPI.Process(serviceResponse);
        }

        #endregion --> Public methods. <--
    }
}
