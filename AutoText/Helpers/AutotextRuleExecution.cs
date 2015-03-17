using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoText.Helpers.Configuration;

namespace AutoText.Helpers
{
	public class Input
	{
		public InputType Type { get; set; }
		public InputActionType ActionType { get; set; }
		public char CharToInput { get; set; }
		public int KeyCodeValueToInput{ get; set; }
	}

	public enum InputActionType
	{
		KeyDown,
		KeyUp,
		Press
	}

	public enum InputType
	{
		UnicideChar,
		KeyCode
	}

	public static class AutotextRuleExecution
	{
		public static void ProcessRule(AutotextRuleConfig rule)
		{
			AutotextPhrase phrase = new AutotextPhrase(rule.Phrase);
			{ }
		}
	}
}
