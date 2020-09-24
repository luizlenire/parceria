namespace Api.Models
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class ServiceResponse<T> : ServiceResult
    {
        #region --> Public properties. <--

        public T obj { get; set; }

        #endregion --> Public properties. <--
    }
}
