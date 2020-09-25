using System;

namespace App.Models
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public sealed class ControleParceria
    {
        #region --> Public properties. <--

        public int CodParceria { get; set; }

        public string Titulo { get; set; }

        public string Empresa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataTermino { get; set; }

        public decimal QtdLikes { get; set; }

        #endregion --> Public properties. <--

        #region --> Constructors. <--

        public ControleParceria(int codParceria = default, string titulo = default, string empresa = default, DateTime dataInicio = default, DateTime dataTermino = default, decimal qtdLikes = default)
        {
            CodParceria = codParceria;
            Titulo = titulo;
            Empresa = empresa;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            QtdLikes = qtdLikes;
        }

        #endregion --> Constructors. <--
    }
}
