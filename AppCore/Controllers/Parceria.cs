using AppCore.Database;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ParceriaAPI.SeveralFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppCore.Controllers
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class Parceria
    {
        #region --> Public methods. <--

        public List<vParceria> Get(int? codigo, string search, DateTime? dateTime)
        {
            using Context context = new Context();
            using IDbContextTransaction iDbContextTransaction = context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);

            List<vParceria> listvParceria = context.vParceria
                                                   .Where(x => (dateTime == default || x.DataInicio <= dateTime) &&
                                                               (dateTime == default || x.DataTermino >= dateTime) &&
                                                               (search == default || x.Titulo.Contains(search)) &&
                                                               (codigo == default || x.Codigo == codigo))
                                                   .ToList();

            if (listvParceria.Count == default)
            {
                listvParceria = context.vParceria
                                       .Where(x => x.DataInicio <= (dateTime ?? Tools.GetDateTimeNow()) &&
                                                   x.DataTermino >= (dateTime ?? Tools.GetDateTimeNow()) &&
                                                   (search == default || x.Empresa.Contains(search)))
                                       .ToList();

            }

            iDbContextTransaction.Commit();

            return listvParceria;
        }

        public void Post(Models.Parceria parceria)
        {
            using Context context = new Context();
            context.Database.ExecuteSqlRaw("spParceria @p0 @p1 @p2 @p3 @p4 @p5 @p6 @p7", 1, parceria.Titulo, parceria.Descricao, parceria.URLPagina, parceria.Empresa, parceria.DataInicio, parceria.DataTermino, parceria.QtdLikes);
        }

        public void Put(Models.Parceria parceria)
        {
            using Context context = new Context();
            context.Database.ExecuteSqlRaw("spParceria @p0 @p1 @p2 @p3 @p4 @p5 @p6 @p7 @p8", 2, parceria.Codigo, parceria.Titulo, parceria.Descricao, parceria.URLPagina, parceria.Empresa, parceria.DataInicio, parceria.DataTermino, parceria.QtdLikes);
        }

        public void Delete(Models.Parceria parceria)
        {
            using Context context = new Context();
            context.Database.ExecuteSqlRaw("spParceria @p0 @p1", 3, parceria.Codigo);
        }

        #endregion --> Public methods. <--
    }
}
