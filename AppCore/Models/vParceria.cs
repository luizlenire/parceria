using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    /* --> † 24/09/2020 - Luiz Lenire. */

    [Table("vparceria")]
    public sealed class vParceria
    {
        #region --> Public properties. <--

        [Column("codigo")]
        [Key]
        public int Codigo { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("urlpagina")]
        public string URLPagina { get; set; }

        [Column("empresa")]
        public string Empresa { get; set; }

        [Column("datainicio")]
        public DateTime DataInicio { get; set; }

        [Column("datatermino")]
        public DateTime DataTermino { get; set; }

        [Column("qtdlikes")]
        public int QtdLikes { get; set; }

        [Column("datahoracadastro")]
        public DateTime DataHoraCadastro { get; set; }

        #endregion --> Public properties. <--
    }
}
