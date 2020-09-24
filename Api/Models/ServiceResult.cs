namespace Api.Models
{
    /* --> † 24/09/2020 - Luiz Lenire. <--*/

    public abstract class ServiceResult
    {
        #region --> Public properties. <--

        public bool success { get; set; }

        public string message { get; set; }

        #endregion --> Public properties. <--
    }
}
