using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace INetApp.Converters
{
    public class GridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength retorno = GridLength.Auto;
            if (value is string _string)
            {
                if (_string == "Auto")
                {
                    retorno = GridLength.Auto;
                }
                else if (_string.Contains("*"))
                {
                    _string = _string.Replace("*", "");
                    if (double.TryParse(_string, out double valor))
                    {
                        retorno = new GridLength(valor, GridUnitType.Star);
                    }
                    else
                    {
                        retorno = GridLength.Star;
                    }
                }
                else if (double.TryParse(_string, out double valor))
                {
                    retorno = new GridLength(valor);
                }
            }
            else if (value is double _double)
            {
                retorno = new GridLength(_double);
            }
            else if (value is int _int)
            {
                retorno = new GridLength(_int);
            }

            return retorno;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
