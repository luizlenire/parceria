using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    /* --> † 24/09/2020 - Luiz Lenire. */

    [Table("parcerialike")]
    public sealed class ParceriaLike
    {
        #region --> Public properties. <--

        [Column("codigo")]
        [Key]
        public int Codigo { get; set; }

        [Column("codigoparceria")]
        public int CodigoParceria { get; set; }

        [Column("datahoracadastro")]
        public DateTime DataHoraCadastro { get; set; }

        #endregion --> Public properties. <--
    }
}
