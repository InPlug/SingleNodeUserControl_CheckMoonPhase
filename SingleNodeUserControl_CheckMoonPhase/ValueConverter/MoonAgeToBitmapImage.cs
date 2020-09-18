using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections.Generic;

namespace Vishnu_UserModules.ValueConverter
{

    /// <summary>
    /// Setzt einen Integer für das Mondalter in Tagen
    /// in ein BitmapImage der zugehörigen Mondphase um.
    /// </summary>
    /// <remarks>
    /// Autor: Erik Nagel
    ///
    /// 05.04.2020 Erik Nagel: erstellt
    /// </remarks>
    public class MoonAgeToBitmapImage : IMultiValueConverter
    {
        private static Dictionary<int, string> _moonPaseToImageName
            = new Dictionary<int, string>() {
                  {0, "Neumond"}
                , {1, "Viertelmond_zunehmend" }
                , {2, "Halbmond_zunehmend" }
                , {3, "Dreiviertelmond_zunehmend" }
                , {4, "Vollmond" }
                , {5, "Dreiviertelmond_abnehmend" }
                , {6, "Halbmond_abnehmend" }
                , {7, "Viertelmond_abnehmend" }
        };

        /// <summary>
        /// Übersetzt eine Knoten-Zustand (Enum) in ein Bild.
        /// </summary>
        /// <param name="values">Array: [Mondalter in Tagen][ResourceDictionary mit BitmapImages].</param>
        /// <param name="targetType">Der Zieltyp (BitmapImage).</param>
        /// <param name="parameter">Wird nicht genutzt.</param>
        /// <param name="culture">Sprache, Sonderzeichen</param>
        /// <returns>BitmapImage.</returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return new BitmapImage(new Uri("./Media/Viertelmond_zunehmend.png", UriKind.Relative));
            }
            int moonAge = (int)values[0];
            ResourceDictionary targets = (ResourceDictionary)(values[1]);
            int moonPase = this.CalculateImageIndexFromMoonAge(moonAge);
            BitmapImage img = (BitmapImage)(targets[_moonPaseToImageName[moonPase]]);
            return img;
        }

        private int CalculateImageIndexFromMoonAge(int moonAge)
        {
            if (moonAge == 1 || moonAge == 30)
            {
                return 0; // Neumond
            }
            return (moonAge + 2) / 4;
        }

        /// <summary>
        /// Ist nicht implementiert.
        /// </summary>
        /// <param name="value">BitmapImage</param>
        /// <param name="targetTypes">Array: [Mondalter in Tagen][ResourceDictionary mit BitmapImages].</param>
        /// <param name="parameter">Wird nicht genutzt.</param>
        /// <param name="culture">Sprache, Sonderzeichen</param>
        /// <returns>Mondalter in Tagen als Object.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
