using ParceriaAPI.SeveralFunctions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    /* --> † 24/09/2020 - Luiz Lenire. */

    [Table("impexplog")]
    public sealed class ParceriaLog
    {
        #region --> Public properties. <--

        [Column("identifier")]
        [Key]
        public long Identifier { get; set; }

        [Column("included")]
        public DateTime Included { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("descriptionexception")]
        public string DescriptionException { get; set; }

        [Column("descriptioninnerexception")]
        public string DescriptionInnerException { get; set; }

        #endregion --> Public properties. <--

        #region --> Constructors. <--   

        public ParceriaLog() { }

        public ParceriaLog(Exception ex)
        {
            Included = Tools.GetDateTimeNow();
            Description = ex.Message;
            DescriptionException = ex.StackTrace;

            if (ex.InnerException != default) DescriptionInnerException = ex.InnerException.ToString();
        }

        #endregion --> Constructors. <--
    }
}
