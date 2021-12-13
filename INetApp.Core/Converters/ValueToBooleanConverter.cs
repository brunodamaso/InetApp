using System;
using System.Collections;
using System.Globalization;
using INetApp.Extensions;
using Xamarin.Forms;

namespace INetApp.Converters
{
    /// <summary>
    /// Value to boolean converter.
    /// </summary>
    public class ValueToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified value, targetType, parameter and culture.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool valBool)
            {
                return Converter(valBool, parameter);
            }
            else if (value is int valInt)
            {
                return Converter(GetBool(valInt, parameter), parameter);
            }
            else if (value is double valDouble)
            {
                return Converter(GetBool((int)valDouble, parameter), parameter);
            }
            else if (value is float valfloat)
            {
                return Converter(GetBool((int)valfloat, parameter), parameter);
            }
            else if (value is float valSingle)
            {
                return Converter(GetBool((int)valSingle, parameter), parameter);
            }
            else if (value is string valString)
            {
                return Converter(!valString.IsNullOrEmpty(), parameter);
            }
            else if (value is IEnumerable valEnumerable)
            {   // poner ienumerable despues de string, ya que un string es una lista de caracteres..
                return Converter(valEnumerable.HasElements(), parameter);
            }

            return Converter(value != null, parameter);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <returns>The back.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert the specified value.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="value">Value.</param>
        public bool Convert(object value)
        {
            return (Convert(value, null, null, CultureInfo.CurrentCulture) as bool?) ?? false;
        }

        #region Private 

        private object Converter(bool value, object parameter)
        {
            return "NOT".Equals(parameter.ToString().ToUpper()) ? !value : (object)value;
        }

        private bool GetBool(int valInt, object parameter)
        {
            bool valor = false;
            string str = $"{parameter?.ToString()}";
            if (int.TryParse(str, out int numero))
            {
                valor = (valInt) == numero;
            }
            else
            {
                if (str == "0")
                {
                    valor = (valInt) == 0;
                }
                else if (str.Contains(">"))
                {
                    valor = (valInt) > int.Parse(str.Substring(1));
                }
                else if (str.Contains("<"))
                {
                    valor = (valInt) < int.Parse(str.Substring(1));
                }
                else if (str.Contains("!="))
                {
                    valor = (valInt) != int.Parse(str.Substring(2));
                }
                else
                {
                    valor = (valInt) > 0;
                }
            }
            return valor;
        }

        #endregion
    }
}
