using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace INetApp.Extensions
{
    /// <summary>
    /// Check InjectSql
    /// </summary>
    [Flags]
    public enum CheckInjectSQLEnum
    {
        /// <summary>
        /// All checks
        /// </summary>
        All = CheckInjectSQLEnum.Comments | CheckInjectSQLEnum.Dashs | CheckInjectSQLEnum.Point | CheckInjectSQLEnum.QuotationMarks | CheckInjectSQLEnum.Semicolon | CheckInjectSQLEnum.StoredProcedure,
        /// <summary>
        /// Check only ;
        /// </summary>
        Semicolon = 2, // ; Delimitador de consultas.
        /// <summary>
        /// Check only "
        /// </summary>
        QuotationMarks = 4, // "	Delimitador de cadenas de datos de caracteres.
        /// <summary>
        /// Check only --
        /// </summary>
        Dashs = 8, // --	Delimitador de cadenas de datos de caracteres.
        /// <summary>
        /// Check only .
        /// </summary>
        Point = 32, // .
        /// <summary>
        /// Check only /* or *\
        /// </summary>
        Comments = 64, // /* ... */	Delimitadores de comentarios. El servidor no evalúa el texto incluido entre /* y */ .
        /// <summary>
        /// Check only xp_
        /// </summary>
        StoredProcedure = 128, //  xp_

    }

    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Ises the Null or Empty.
        /// </summary>
        /// <returns><c>true</c>, if null or empty was ised, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        public static bool IsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }

        /// <summary>
        /// Equals the ignore case.
        /// </summary>
        /// <returns><c>true</c>, if ignore case was equaled, <c>false</c> otherwise.</returns>
        /// <param name="str">String.</param>
        /// <param name="val">Value.</param>
        public static bool EqualIgnoreCase(this string str, string val)
        {
            if (str != null && val != null)
                return string.Equals(val, str, StringComparison.OrdinalIgnoreCase);
            else
                return false;
        }

        /// <summary>
        /// Ises the same leng.
        /// </summary>
        /// <returns><c>true</c>, if same leng was ised, <c>false</c> otherwise.</returns>
        /// <param name="str">String.</param>
        /// <param name="str2">Str2.</param>
        public static bool IsSameLeng(this string str, string str2)
        {
            var leng1 = str?.Length ?? 0;
            var leng2 = str2?.Length ?? 0;

            return leng1 == leng2;
        }

        /// <summary>
        /// Reverse the specified text.
        /// </summary>
        /// <returns>The reverse.</returns>
        /// <param name="text">Text.</param>
        public static string Reverse(this string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

        /// <summary>
        /// To the encode URL.
        /// </summary>
        /// <returns>The encode URL.</returns>
        /// <param name="str">String.</param>
        public static string ToEncodeUrl(this string str)
        {
            if (!str.IsNullOrEmpty())
                return System.Net.WebUtility.UrlEncode(str);

            return "";
        }

        /// <summary>
        /// Convert to the date time.
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="val">Value.</param>
        /// <param name="format">Format.</param>
        public static DateTime? ToDateTime(this string val, string format)
        {
            DateTime? result = null;

            if (format != null && format.StartsWith("{", StringComparison.CurrentCulture) && format.EndsWith("}", StringComparison.CurrentCulture))
            {
                format = format.Replace("{0:", "").Replace("}", "");
            }

            if (!val.IsNullOrEmpty() && DateTime.TryParseExact(val, format, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime current))
            {
                result = current;
            }

            return result;
        }

        /// <summary>
        /// Tos the date time.
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="val">Value.</param>
        /// <param name="format">Format.</param>
        /// <param name="defaultDate">Default date.</param>
        public static DateTime ToDateTime(this string val, string format, DateTime defaultDate)
        {
            var result = val.ToDateTime(format);

            return result ?? defaultDate;
        }

        /// <summary>
        /// Create list of character from string.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IList<String> ToArray(this string val)
        {
            var result = new List<string>();

            if (!val.IsNullOrEmpty())
                result.AddAll(val.ToCharArray().Select(c => c.ToString()));

            return result;
        }

        /// <summary>
        /// Create object from Full Qualified Type Name of Class
        /// </summary>
        /// <param name="str"></param>
        /// <returns>if Ok return new class else return null</returns>
        public static object CreateFromFullyQualifiedTypeName(this string str)
        {
            try
            {
                var type = str.GetTypeFromFullyQualifiedTypeName();
                var myObject = Activator.CreateInstance(type);
                return myObject;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Err {ex}");
                Trace.WriteLine("Err " + ex?.ToString() ?? "");
            }

            return null;
        }

        /// <summary>
        /// Get type of Full Qualified Type Name class
        /// </summary>
        /// <param name="str"></param>
        /// <returns>if Ok return type of class else return null</returns>
        public static Type GetTypeFromFullyQualifiedTypeName(this string str)
        {
            try
            {
                var type = Type.GetType(str);
                return type;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Err {ex}");
                Trace.WriteLine("Err " + ex?.ToString() ?? "");
            }

            return null;
        }

        /// <summary>
        /// Convert for TypeCode
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type">type o cast</param>
        /// <param name="format">Formating string</param>
        /// <param name="defaultValue">return default value if not null</param>
        /// <param name="culture">For formating</param>
        /// <param name="safeDecimals">Intercambiate point for coma</param>
        /// <returns>Object of Type if convert is ok else default(Type).</returns>
        public static object ConvertToTypeFormat(this string str, Type type, CultureInfo culture = null, string format = "", object defaultValue = null, bool safeDecimals = false)
        {
            var currentCulture = culture ?? CultureInfo.CurrentCulture;

            if (str != null && type != null)
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        if ("true".EqualIgnoreCase(str) || "false".EqualIgnoreCase(str))
                            return "true".EqualIgnoreCase(str);
                        break;

                    case TypeCode.Byte:
                        if (Byte.TryParse(str, NumberStyles.Any, currentCulture, out var inte))
                            return inte;
                        break;

                    case TypeCode.Char:
                        if (Char.TryParse(str, out var chr))
                            return chr;
                        break;

                    case TypeCode.DateTime:
                        if (str.ToDateTime(format) is DateTime val)
                            return val;
                        break;

                    case TypeCode.DBNull:
                        return str.IsNullOrEmpty() ? DBNull.Value : defaultValue;

                    case TypeCode.Decimal:
                        str = safeDecimals ? SafeDecimals(str, culture) : str;
                        if (Decimal.TryParse(str, NumberStyles.Any, currentCulture, out var dec))
                            return dec;
                        break;

                    case TypeCode.Double:
                        str = safeDecimals ? SafeDecimals(str, culture) : str;
                        if (Double.TryParse(str, NumberStyles.Any, currentCulture, out var dou))
                            return dou;
                        break;

                    case TypeCode.Empty:
                        return str;

                    case TypeCode.Int16:
                        if (Int16.TryParse(str, NumberStyles.Any, currentCulture, out var int16))
                            return int16;
                        break;

                    case TypeCode.Int32:
                        if (Int32.TryParse(str, NumberStyles.Any, currentCulture, out var int32))
                            return int32;
                        break;

                    case TypeCode.Int64:
                        if (Int64.TryParse(str, NumberStyles.Any, currentCulture, out var int64))
                            return int64;
                        break;

                    case TypeCode.Object:
                        return str;

                    case TypeCode.SByte:
                        if (SByte.TryParse(str, NumberStyles.Any, currentCulture, out var _byte))
                            return _byte;
                        break;

                    case TypeCode.Single:
                        str = safeDecimals ? SafeDecimals(str, culture) : str;
                        if (Single.TryParse(str, NumberStyles.Any, currentCulture, out var single))
                            return single;
                        break;

                    case TypeCode.String:
                        return str;

                    case TypeCode.UInt16:
                        if (UInt16.TryParse(str, NumberStyles.Any, currentCulture, out var u16))
                            return u16;
                        break;

                    case TypeCode.UInt32:
                        if (UInt32.TryParse(str, NumberStyles.Any, currentCulture, out var u32))
                            return u32;
                        break;

                    case TypeCode.UInt64:
                        if (UInt64.TryParse(str, NumberStyles.Any, currentCulture, out var u64))
                            return u64;
                        break;
                }

            return defaultValue;
        }

        /// <summary>
        /// Convert for Generic Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="format">Formating string</param>
        /// <param name="defaultValue">return default value if not null</param>
        /// <param name="culture">For formating</param>
        /// <param name="safeDecimals">intercambiate point for coma</param>
        /// <returns>T if convert is ok else default(T).</returns>
        public static T ConvertToTypeFormat<T>(this string str, CultureInfo culture = null, string format = "", T defaultValue = default(T), bool safeDecimals = false)
        {
            if (str.ConvertToTypeFormat(typeof(T), culture, format, defaultValue, safeDecimals) is T result)
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Mask out characters of a string
        /// </summary>
        /// <returns>Masked string</returns>
        /// <param name="source">String to mask.</param>
        /// <param name="start">Start of the mask.</param>
        /// <param name="maskLength">Mask length.</param>
        /// <param name="maskCharacter">Mask character.</param>
        public static string Mask(this string source, int start, int maskLength, char maskCharacter)
        {
            if (start > source.Length - 1)
            {
                throw new ArgumentException("Start position is greater than string length");
            }

            if (maskLength > source.Length)
            {
                throw new ArgumentException("Mask length is greater than string length");
            }

            if (start + maskLength > source.Length)
            {
                throw new ArgumentException("Start position and mask length imply more characters than are present");
            }

            string mask = new string(maskCharacter, maskLength);
            string unMaskStart = source.Substring(0, start);
            string unMaskEnd = source.Substring(start + maskLength);

            return unMaskStart + mask + unMaskEnd;
        }

        /// <summary>
        /// A fixed length string "leng" is obtained by adding n characters "character" to the left
        /// </summary> 
        /// <param name="leng"></param>
        /// <param name="character"></param>
        /// <param name="val"></param>
        /// <returns>string "leng" characters</returns>
        public static string ToPadLeft(this string val, int leng, char character)
        {
            var padding = new string(character, leng);
            var cad = $"{padding}{val}";
            var result = cad.Right(leng);
            return result;
        }

        /// <summary>
        /// A fixed length string "leng" is obtained by adding n characters "character" to the right
        /// </summary> 
        /// <param name="leng"></param>
        /// <param name="character"></param>
        /// <param name="val"></param>
        /// <returns>string "leng" characters</returns>
        public static string ToPadRight(this string val, int leng, char character)
        {
            var padding = new string(character, leng);
            var cad = $"{val}{padding}";
            var result = cad.Left(leng);
            return result;
        }

        /// <summary>
        /// Get left n characters or minor
        /// </summary> 
        /// <param name="length"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            if (str.IsNullOrEmpty())
                return "";

            return str.Substring(0, Math.Min(length, str.Length));
        }

        /// <summary>
        /// Get right n characters or minor.
        /// </summary> 
        /// <param name="length"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Right(this string str, int length)
        {
            if (str.IsNullOrEmpty())
                return "";

            return str.Substring(str.Length - Math.Min(length, str.Length));
        }

        /// <summary>
        /// Check if string contains inject SQL
        /// </summary>
        /// <param name="str"></param>
        /// <param name="check">To check, for default All</param>
        /// <returns></returns>
        public static bool IsInjectSQL(this string str, CheckInjectSQLEnum check = CheckInjectSQLEnum.All)
        {
            if (str.IsNullOrEmpty())
                return false;

            if (check.HasFlag(CheckInjectSQLEnum.Comments) && (str.Contains("/*") || str.Contains("*\\")))
                return true;

            if (check.HasFlag(CheckInjectSQLEnum.Dashs) && str.Contains("--"))
                return true;

            if (check.HasFlag(CheckInjectSQLEnum.Point) && str.Contains("."))
                return true;

            if (check.HasFlag(CheckInjectSQLEnum.QuotationMarks) && str.Contains('"'))
                return true;

            if (check.HasFlag(CheckInjectSQLEnum.Semicolon) && str.Contains(";"))
                return true;

            if (check.HasFlag(CheckInjectSQLEnum.StoredProcedure) && str.Contains("xp_"))
                return true;

            return false;
        }

        /// <summary>
        /// Remove inject SQL in string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="check">To clean, for default All</param>
        /// <returns></returns>
        public static string CleanInjectSQL(this string str, CheckInjectSQLEnum check = CheckInjectSQLEnum.All)
        {
            if (str.IsNullOrEmpty())
                return str;

            var result = new StringBuilder(str);

            if (check.HasFlag(CheckInjectSQLEnum.Comments))
                result.Replace("/*", "").Replace("*\\", "");

            if (check.HasFlag(CheckInjectSQLEnum.Dashs))
                result.Replace("--", "");

            if (check.HasFlag(CheckInjectSQLEnum.Point))
                result.Replace(".", "");

            if (check.HasFlag(CheckInjectSQLEnum.QuotationMarks))
                result.Replace('"'.ToString(), "");

            if (check.HasFlag(CheckInjectSQLEnum.Semicolon))
                result.Replace(";", "");

            if (check.HasFlag(CheckInjectSQLEnum.StoredProcedure))
                result.Replace("xp_", "");

            return result.ToString();
        }

        #region Private 

        private static string SafeDecimals(string val, CultureInfo culture)
        {
            if (GetSeparator(culture) is string separator)
            {
                if (",".EqualIgnoreCase(separator))
                    val = val.Replace(".", ",");
                else if (".".EqualIgnoreCase(separator))
                    val = val.Replace(",", ".");
            }

            return val;
        }

        private static string GetSeparator(CultureInfo culture)
        {
            string result = null;

            if ((culture ?? CultureInfo.CurrentCulture) is CultureInfo current && current?.NumberFormat?.NumberDecimalSeparator is string val)
            {
                result = val;
            }

            return result;
        }

        #endregion
    }
}
