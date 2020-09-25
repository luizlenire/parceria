using System.Linq;

namespace App.Models
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public interface IRepository
    {
        #region --> Public properties. <--

        IQueryable<ControleParceria> listControleParceria { get; }

        #endregion --> Public properties. <--
    }
}
