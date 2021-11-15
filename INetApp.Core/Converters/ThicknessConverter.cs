using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace INetApp.Converters
{
    public class ThicknessConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness retorno = new Thickness();
            try
            {
                if (parameter is null)
                {
                    double[] valores = new double[4];
                    int posicion = 0;
                    foreach (var value in values)
                    {
                        if (value is double _double)
                            valores[posicion] = _double;
                        else if (value is int _int)
                            valores[posicion] = _int;
                        else if (value is string _string)
                            valores[posicion] = double.Parse(_string);
                        posicion++;
                    }
                    switch (posicion)
                    {
                        case 1:
                            retorno = new Thickness(valores[0]);
                            break;
                        case 2:
                            retorno = new Thickness(valores[0], valores[1]);
                            break;
                        case 4:
                            retorno = new Thickness(valores[0], valores[1], valores[2], valores[3]);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }

            return retorno;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
