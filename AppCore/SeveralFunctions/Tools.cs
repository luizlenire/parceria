using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace ParceriaAPI.SeveralFunctions
{
    /* --> † 24/09/2019 - Luiz Lenire. <--*/

    public sealed class Tools
    {
        #region --> Public static methods. <--

        public static IEnumerable<ValidationResult> ValidadeDataAnnotations(object obj)
        {
            List<ValidationResult> listValidationResult = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(obj, default, default);
            Validator.TryValidateObject(obj, validationContext, listValidationResult, true);

            return listValidationResult;
        }

        public static bool IsPropertyExist(dynamic obj, string name)
        {
            if (obj is ExpandoObject) return ((IDictionary<string, object>)obj).ContainsKey(name);
            else return obj.GetType().GetProperty(name) != null;
        }     

        public static string GetTime(TimeSpan timeSpan)
        {
            return string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        public static string GetSize(dynamic obj)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = default;
            int order = 0;

            try
            {
                len = JsonConvert.SerializeObject(obj).Length;

                while (len >= 1024 &&
                       order < sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }
            }
            catch { }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        public static DateTime GetDateTimeNow()
        {
            try
            {
                string val = Environment.GetEnvironmentVariable("GMT");

                if (val != default) return DateTime.Now.AddHours(int.Parse(Environment.GetEnvironmentVariable("GMT")));
                else return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            }
            catch { return DateTime.Now; }
        }  

        #endregion --> Public static methods. <--
    }
}
