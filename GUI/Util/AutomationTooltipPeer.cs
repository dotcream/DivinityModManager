﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace DivinityModManager.Util
{
	/// <summary>
	/// This simply disables the automation stuff for the tooltip, to prevent the ElementNotAvailableException from happening.
	/// </summary>
	public class AutomationTooltipPeer : ToolTipAutomationPeer
	{
		public AutomationTooltipPeer(ToolTip owner) : base(owner) { }

		protected override string GetNameCore()
		{
			return "AutomationTooltipPeer";
		}

		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.ToolTip;
		}

		protected override List<AutomationPeer> GetChildrenCore()
		{
			return new List<AutomationPeer>();
		}
	}
}
