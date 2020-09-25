namespace App.Models
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public sealed class ErrorViewModel
    {
        #region --> Public properties. <--

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion --> Public properties. <--
    }
}
