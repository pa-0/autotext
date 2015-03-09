using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	class AutotextMatcher
	{
		private const string AcceptablePrintableCharsRegex = @"[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}";
		private const string NonPrintableCharsRegex = @"{([\w\d]+)}";

		public event EventHandler<AutotextMatchEventArgs> MatchFound;

		private KeyLogger _keyLogger;
		public KeyLogger KeyLogger
		{
			get
			{
				return _keyLogger;
			}

			set
			{
				SetKeyLogger(value);
			}
		}

		private List<AutotextRule> _rules;
		public List<AutotextRule> Rules
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

		private StringBuilder _bufferString = new StringBuilder(100);
		private int _abbrMaxLength;
		private AutotextRule _matchedRule;

		Regex _acceptablePrintableCharsRegex = new Regex(AcceptablePrintableCharsRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		Regex _nonPrintableCharsRegex = new Regex(NonPrintableCharsRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		private void SetKeyLogger(KeyLogger keyLogger)
		{
			_keyLogger = keyLogger;
			_keyLogger.KeyCaptured += KeyLogger_KeyCaptured;
			_keyLogger.StartCapture();
		}

		public AutotextMatcher(KeyLogger keyLogger, List<AutotextRule> rules)
		{
			_rules = rules;
			_abbrMaxLength = rules.Max(p => p.Abbreviation.Length);
			KeyLogger = keyLogger;
		}

		void KeyLogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			if (e.CapturedKeys[0] == Keys.Back)
			{
				if (_bufferString.Length > 0)
				{
					_bufferString.Remove(_bufferString.Length - 1, 1);
				}
			}
			else
			{
				//Search for triggers
				if (_matchedRule != null)
				{
					//If captured char is a simple printable character
					if (_acceptablePrintableCharsRegex.IsMatch(e.CapturedCharacter))
					{
						List<AutotextRuleTrigger> simpleChars = _matchedRule.Triggers.Where(p => _acceptablePrintableCharsRegex.IsMatch(p.Value)).ToList();

						if (simpleChars.Count(p => p.Value == e.CapturedCharacter) == 1)
						{
							OnMatchFound(new AutotextMatchEventArgs(_matchedRule));
						}

					}//match for non printable chars
					else
					{
						//get non printable chars from triggers
						List<AutotextRuleTrigger> nonPrintableCharsFromRule = _matchedRule.Triggers.Where(p => _nonPrintableCharsRegex.IsMatch(p.Value)).ToList();
						List<string> nonPrintableFromCaptured = e.CapturedKeys.Select(p => string.Format("{{{0}}}", p)).ToList();

						if (nonPrintableCharsFromRule.Any(p => nonPrintableFromCaptured.Contains(p.Value)))
						{
							OnMatchFound(new AutotextMatchEventArgs(_matchedRule));
						}
					}

					_bufferString.Clear();
					_matchedRule = null;
				}
				else
				{
					if (_acceptablePrintableCharsRegex.IsMatch(e.CapturedCharacter))
					{
						_bufferString.Append(e.CapturedCharacter);

						if (_bufferString.Length > _abbrMaxLength)
						{
							_bufferString.Remove(0, 1);
						}

						string abbr = _bufferString.ToString();
						_matchedRule = _rules.SingleOrDefault(p => abbr.EndsWith(p.Abbreviation));
					}
				}
			}
		}

		protected virtual void OnMatchFound(AutotextMatchEventArgs e)
		{
			var handler = MatchFound;
			if (handler != null) handler(this, e);
		}
	}
}
