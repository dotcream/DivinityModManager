﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DivinityModManager
{
	public static class KeyExtensions
	{
		private static readonly Dictionary<Key, string> KeyToName = new Dictionary<Key, string>
		{
			{Key.Add, "+"},
			{Key.D0, "0"},
			{Key.D1, "1"},
			{Key.D2, "2"},
			{Key.D3, "3"},
			{Key.D4, "4"},
			{Key.D5, "5"},
			{Key.D6, "6"},
			{Key.D7, "7"},
			{Key.D8, "8"},
			{Key.D9, "9"},
			{Key.Decimal, "."},
			{Key.Divide, " / "},
			{Key.Multiply, "*"},
			{Key.Oem1, ";"},
			{Key.Oem5, "\\"},
			{Key.Oem6, "]"},
			{Key.Oem7, "'"},
			{Key.OemBackslash, "\\"},
			{Key.OemComma, ","},
			{Key.OemMinus, "-"},
			{Key.OemOpenBrackets, "["},
			{Key.OemPeriod, "."},
			{Key.OemPlus, "="},
			{Key.OemQuestion, "/"},
			{Key.OemTilde, "`"},
			{Key.Subtract, "-"}
		};

		public static string GetKeyName(this Key key)
		{
			if(KeyToName.TryGetValue(key, out string name))
			{
				return name;
			}
			return key.ToString();
		}
	}
}
