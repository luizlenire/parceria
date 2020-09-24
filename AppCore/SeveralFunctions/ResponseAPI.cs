using AppCore.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace ParceriaAPI.SeveralFunctions
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public class ResponseAPI : ControllerBase
    {
        #region --> Public methods. <--

        public dynamic ProcessException(dynamic serviceResponse, Exception ex)
        {
            if (Tools.IsPropertyExist(serviceResponse, "obj")) serviceResponse.obj = default(dynamic);
            else if (Tools.IsPropertyExist(serviceResponse, "Object")) serviceResponse.Object = default(dynamic);

            if (Tools.IsPropertyExist(serviceResponse, "message"))
            {
                serviceResponse.message = "Ocorreu uma falha grave ao executar esta ação.";
                serviceResponse.message += "M¹: " + ex.Message + " | S²: " + ex.StackTrace;

                if (ex.InnerException != default)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    GetInnerException(ex, stringBuilder, 0);

                    serviceResponse.message += " | I³: " + stringBuilder;
                }

                try
                {
                    Monitoring.exception = ex;
                    serviceResponse.message += " | --> EXCEÇÃO ARMAZENADA NA TABELA DE LOG.";
                }
                catch { serviceResponse.message += "| --> NÃO FOI POSSÍVEL ARMAZENAR ESTA EXCEÇÃO NA TABELA DE LOG, PROVAVELMENTE A APLICAÇÃO ESTÁ SEM ACESSO O BANCO DE DADOS."; }
            }
            else if (Tools.IsPropertyExist(serviceResponse, "mensagem"))
            {
                serviceResponse.mensagem = "Ocorreu uma falha grave ao executar esta ação.";
                serviceResponse.mensagem += "M¹: " + ex.Message + " | S²: " + ex.StackTrace;

                if (ex.InnerException != default)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    GetInnerException(ex, stringBuilder, 0);

                    serviceResponse.mensagem += " | I³: " + stringBuilder;
                }

                try
                {
                    Monitoring.exception = ex;
                    serviceResponse.mensagem += " | --> EXCEÇÃO ARMAZENADA NA TABELA DE LOG.";
                }
                catch { serviceResponse.mensagem += "| --> NÃO FOI POSSÍVEL ARMAZENAR ESTA EXCEÇÃO NA TABELA DE LOG, PROVAVELMENTE A APLICAÇÃO ESTÁ SEM ACESSO O BANCO DE DADOS."; }
            }

            return serviceResponse;
        }

        public IActionResult Process(dynamic serviceResponse)
        {
            if (serviceResponse != default(dynamic))
            {
                if (Tools.IsPropertyExist(serviceResponse, "success") &&
                    !serviceResponse.success) return BadRequest(serviceResponse);
                else if (Tools.IsPropertyExist(serviceResponse, "resultado") &&
                  !serviceResponse.resultado) return BadRequest(serviceResponse);
                else return Ok(serviceResponse);
            }
            else if (serviceResponse == default(dynamic)) return BadRequest(serviceResponse);
            else return Ok(serviceResponse);
        }

        #endregion --> Public methods. <--

        #region --> Private methods. <--

        private void GetInnerException(Exception exception, StringBuilder stringBuilder, int level)
        {
            string indent = new string(' ', level);

            if (level > 0) stringBuilder.AppendLine(indent + "=== INNER EXCEPTION ===");

            append("Message");
            append("HResult");
            append("HelpLink");
            append("Source");
            append("StackTrace");
            append("TargetSite");

            foreach (DictionaryEntry item in exception.Data) stringBuilder.AppendFormat("{0} {1} = {2}{3}", indent, item.Key, item.Value, Environment.NewLine);

            if (exception.InnerException != default) GetInnerException(exception.InnerException, stringBuilder, ++level);

            #region --> Sub-methods. <--

            void append(string prop)
            {
                PropertyInfo propertyInfo = exception.GetType().GetProperty(prop);
                object obj = propertyInfo.GetValue(exception);

                if (obj != default) stringBuilder.AppendFormat("{0}{1}: {2}{3}", indent, prop, obj.ToString(), Environment.NewLine);
            }

            #endregion --> Sub-methods. <--
        }

        #endregion --> Private methods. <--
    }
}
