using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public class AutotextMatcher
	{
		private const string AcceptablePrintableCharsRegex = @"[\p{L}\p{M}\p{N}\p{P}\p{S} ]{1}";

		public event EventHandler<AutotextMatchEventArgs> MatchFound;


		private List<AutotextRuleConfig> _rules;
		public List<AutotextRuleConfig> Rules
		{
			get
			{
				return _rules;
			}

			set
			{
				_rules = value;
			}
		}

		private readonly StringBuilder _bufferString = new StringBuilder(100);
		public string BufferContents
		{
			get
			{
				return _bufferString.ToString();
			}
		}



		protected virtual void OnMatchFound(AutotextMatchEventArgs e)
		{
			var handler = MatchFound;
			if (handler != null) handler(this, e);
		}

		public AutotextMatcher(List<AutotextRuleConfig> rules)
		{
			_rules = rules;
		}

		public void CaptureSymbol(KeyCapturedEventArgs e)
		{
			_bufferString.Append(e.CapturedCharacter);

			if (_bufferString.Length >= _bufferString.Capacity)
			{
				_bufferString.Remove(0, 1);
			}

			if (e.CapturedCharacter != "\b")
			{
				foreach (AutotextRuleConfig ruleConfig in _rules)
				{
					foreach (AutotextRuleTrigger ruleTrigger in ruleConfig.Triggers)
					{
						if (string.Compare(e.CapturedCharacter, ruleTrigger.Value, !ruleTrigger.CaseSensitive) == 0)
						{
							if (_bufferString.ToString().EndsWith(ruleConfig.Abbreviation.AbbreviationText, !ruleConfig.Abbreviation.CaseSensitive,null))
							{
								OnMatchFound(new AutotextMatchEventArgs(ruleConfig, ruleTrigger));
								return;
							}
						}
					}
				}


				foreach (AutotextRuleConfig ruleConfig in _rules)
				{
					//Check if abbreviatiuon matches
					if (ruleConfig.Triggers.Any(p => e.CapturedKeys.Where(j => "{" + j + "}" == p.Value).Count() > 0))
					{
						if (_bufferString.ToString().EndsWith(ruleConfig.Abbreviation.AbbreviationText))
						{
							OnMatchFound(new AutotextMatchEventArgs(ruleConfig, ruleConfig.Triggers.Single(p => e.CapturedKeys.Where(j => "{" + j + "}" == p.Value).Any())));
						}
					}
				}

			}
		}

		public void ClearBuffer()
		{
			_bufferString.Clear();
		}

		public void EraseLastBufferSymbol()
		{
			if (_bufferString.Length != 0)
			{
				_bufferString.Remove(_bufferString.Length - 1, 1);
			}
		}
	}
}
