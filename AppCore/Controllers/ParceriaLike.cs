using AppCore.Database;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Controllers
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public sealed class ParceriaLike
    {
        #region --> Public methods. <--    

        public void Post(int codigo)
        {
            using Context context = new Context();
            context.Database.ExecuteSqlRaw("spParceriaLike @p0", parameters: codigo);
        }

        #endregion --> Public methods. <--
    }
}
