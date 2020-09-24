using AppCore.Database;
using AppCore.Models;
using System;

namespace AppCore.Controllers
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class Monitoring
    {
        #region --> Public properties. <--

        public static Exception exception
        {
            set
            {
                using Context contextIntegration = new Context();
                contextIntegration.Post(new ParceriaLog(value));
            }
        }

        #endregion --> Public properties. <--
    }
}
