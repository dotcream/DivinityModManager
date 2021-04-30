﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DivinityModManager.Converters
{
	public class StringNotEmptyToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool reverse = false;
			if (parameter != null)
			{
				if (parameter is int reverseInt)
				{
					reverse = reverseInt > 0;
				}
				else if (parameter is bool r)
				{
					reverse = r;
				}
			}

			if (value is string v)
			{
				if (!reverse)
				{
					return !String.IsNullOrWhiteSpace(v) ? Visibility.Visible : Visibility.Collapsed;
				}
				else
				{
					return String.IsNullOrWhiteSpace(v) ? Visibility.Visible : Visibility.Collapsed;
				}
			}
			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
