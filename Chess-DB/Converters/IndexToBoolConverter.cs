using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Chess_DB.Converters;

public class IndexToBoolConverter : IValueConverter
{
    // Convertit l'index (int) sélectionné dans le ViewModel vers un booléen pour le RadioButton
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // On vérifie si la valeur actuelle (SelectedResultIndex) est égale au paramètre du RadioButton
        if (value is int intValue && parameter is string paramString && int.TryParse(paramString, out int intParam))
        {
            return intValue == intParam;
        }
        return false;
    }

    // Convertit le booléen (RadioButton coché) vers l'index (int) pour le ViewModel
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is true && parameter is string paramString && int.TryParse(paramString, out int intParam))
        {
            return intParam;
        }
        return null; // On ne fait rien si décoché
    }
}