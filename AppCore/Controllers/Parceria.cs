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
                                                   .Where(x => x.DataInicio >= (dateTime ?? Tools.GetDateTimeNow()) &&
                                                               x.DataTermino <= (dateTime ?? Tools.GetDateTimeNow()) &&
                                                               (search == default || x.Titulo.Contains(search)) &&
                                                               (codigo == default || x.Codigo == codigo))
                                                   .ToList();

            if (listvParceria.Count == default)
            {
                listvParceria = context.vParceria
                                       .Where(x => x.DataInicio >= (dateTime ?? Tools.GetDateTimeNow()) &&
                                                   x.DataTermino <= (dateTime ?? Tools.GetDateTimeNow()) &&
                                                   (search == default || x.Empresa.Contains(search)))
                                       .ToList();

            }

            iDbContextTransaction.Commit();

            return listvParceria;
        }

        public void Post(int codigo)
        {
            using Context context = new Context();
            context.Database.ExecuteSqlRaw("spParceriaLike", parameters: codigo);
        }

        #endregion --> Public methods. <--
    }
}
