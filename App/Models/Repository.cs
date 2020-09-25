using AppCore.Models;
using ParceriaAPI.SeveralFunctions;
using System.Collections.Generic;
using System.Linq;

namespace App.Models
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public sealed class Repository : IRepository
    {
        #region --> Public methods. <--

        public IQueryable<ControleParceria> listControleParceria => Dados.AsQueryable();

        #endregion --> Public methods. <--

        #region --> Private properties. <--

        private static ControleParceria[] Dados { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public Repository()
        {
            try
            {
                AppCore.Controllers.Parceria parceria = new AppCore.Controllers.Parceria();
                List<vParceria> listvParceria = parceria.Get(default, default, default);

                if (listvParceria.Count != default)
                {
                    Dados = new ControleParceria[listvParceria.Count];

                    for (int i = 0; i < listvParceria.Count; i++)
                    {
                        Dados[i] = new ControleParceria
                        {
                            CodParceria = listvParceria[i].Codigo,
                            Titulo = listvParceria[i].Titulo,
                            Empresa = listvParceria[i].Empresa,
                            DataInicio = listvParceria[i].DataInicio,
                            DataTermino = listvParceria[i].DataTermino,
                            QtdLikes = listvParceria[i].QtdLikes
                        };
                    }
                }
            }
            catch
            {
                Dados = new ControleParceria[]
                {
                    new ControleParceria { CodParceria = 1, Titulo = "Aluguel de carros", Empresa = "Movida", DataInicio = Tools.GetDateTimeNow().AddDays(-10), DataTermino = Tools.GetDateTimeNow().AddDays(10), QtdLikes = 100},
                    new ControleParceria { CodParceria = 2, Titulo = "Delivery de comida", Empresa = "Uber Eats", DataInicio = Tools.GetDateTimeNow().AddDays(-20), DataTermino = Tools.GetDateTimeNow().AddDays(20), QtdLikes = 200},
                    new ControleParceria { CodParceria = 3, Titulo = "Aplicativo de viagens", Empresa = "Uber", DataInicio = Tools.GetDateTimeNow().AddDays(-30), DataTermino = Tools.GetDateTimeNow().AddDays(30), QtdLikes = 300},
                    new ControleParceria { CodParceria = 4, Titulo = "Comércio Eletrônico", Empresa = "Mercado Livre", DataInicio = Tools.GetDateTimeNow().AddDays(-40), DataTermino = Tools.GetDateTimeNow().AddDays(40), QtdLikes = 400},
                    new ControleParceria { CodParceria = 5, Titulo = "Banco", Empresa = "Santander", DataInicio = Tools.GetDateTimeNow().AddDays(-50), DataTermino = Tools.GetDateTimeNow().AddDays(50), QtdLikes = 500},
                };
            }
        }

        #endregion --> Constructors. <--


    }
}
