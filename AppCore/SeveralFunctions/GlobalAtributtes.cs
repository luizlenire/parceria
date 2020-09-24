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
            get
            {
                if (DatabaseType == GlobalEnum.DatabaseType.SQLServer)
                {
                    //"Server=10.0.0.95;Database=SissIntegracaoBarueri;User Id=adm_saude;password=adm_saude;Trusted_Connection=False;MultipleActiveResultSets=true;"
                    return "Server=" + Server + ";Database=" + Database + " ;User Id=" + User + ";password=" + Password + ";";
                }
                else if (DatabaseType == GlobalEnum.DatabaseType.MySQL)
                {
                    //"Server=10.0.1.131;User Id=root;Password=apollo11;Database=sissintegracaobarueri"
                    return "Server=" + Server + " ;User Id=" + User + ";Password=" + Password + ";Database=" + Database;
                }
                else if (DatabaseType == GlobalEnum.DatabaseType.PostgreSQL)
                {
                    //"Host=nutty-custard-apple.db.elephantsql.com;Database=fjkkkcyf;Username=fjkkkcyf;Password=8pkXenzUZfgTAk95KWRLnNEgd6FlVl5V"
                    return "Host=" + Server + ";Database=" + Database + " ;Username=" + User + ";Password=" + Password;
                }
                else if (DatabaseType == GlobalEnum.DatabaseType.Oracle)
                {
                    return default;
                }
                else return default;
            }
        }    

        #endregion --> Public properties. <--              
    }
}
