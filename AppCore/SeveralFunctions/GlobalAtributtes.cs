namespace ParceriaAPI.SeveralFunctions
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class GlobalAtributtes
    {
        #region --> Public properties. <--            

        public static double TokenExpire { get; set; }

        public static int Timeout { get; set; }

        public static GlobalEnum.DatabaseType DatabaseType { get; set; }

        public static string Server { get; set; }

        public static string Database { get; set; }

        public static string User { get; set; }

        public static string Password { get; set; }

        public static string Connectionstring
        {
            get { return "Server=" + Server + ";Database=" + Database + " ;User Id=" + User + ";password=" + Password + ";"; }
        }

        #endregion --> Public properties. <--              
    }
}
