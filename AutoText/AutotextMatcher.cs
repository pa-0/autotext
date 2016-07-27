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
			if (_bufferString.Length >= _bufferString.Capacity)
			{
				_bufferString.Remove(0, 1);
			}


			AutotextRuleConfig config =
				_rules.SingleOrDefault(p => _bufferString.ToString().EndsWith(p.Abbreviation.AbbreviationText, !p.Abbreviation.CaseSensitive,null) );

			if (config != null)
			{
				AutotextRuleTrigger configMatchTrigger =
					config.Triggers.SingleOrDefault(p => string.Compare(p.Value, e.CapturedCharacter, !p.CaseSensitive) == 0);

				if (configMatchTrigger != null)
				{
					OnMatchFound(new AutotextMatchEventArgs(config, configMatchTrigger));
					_bufferString.Clear();
					return;

				}
				else
				{
					foreach (string capturedKey in e.CapturedKeys)
					{
						foreach (AutotextRuleTrigger ruleTrigger in config.Triggers)
						{
							if ("{" + capturedKey + "}" == ruleTrigger.Value)
							{
								OnMatchFound(new AutotextMatchEventArgs(config, ruleTrigger));
								_bufferString.Clear();
								return;
							}
						}
					}

				}
			}


			_bufferString.Append(e.CapturedCharacter);
		}

		public void ClearBuffer()
		{
			_bufferString.Clear();
		}

		public void EraseLastBufferedSymbol()
		{
			if (_bufferString.Length != 0)
			{
				_bufferString.Remove(_bufferString.Length - 1, 1);
			}
		}
	}
}
