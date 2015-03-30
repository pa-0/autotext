using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public class CursorPositionChangedEventArgs : EventArgs
	{
		public int OldCursorPosition { get; private set; }
		public int NewCursorPosition { get; private set; }

		public CursorPositionChangedEventArgs(int oldCursorPosition, int newCursorPosition)
		{
			OldCursorPosition = oldCursorPosition;
			NewCursorPosition = newCursorPosition;
		}
	}


	public class BufferContentsChangedEventArgs : EventArgs
	{
		public string OldValue { get; private set; }
		public string NewValue { get; private set; }
		public int CursorPosition { get; private set; }

		public BufferContentsChangedEventArgs(string oldValue, string newValue, int cursorPos)
		{
			OldValue = oldValue;
			NewValue = newValue;
			CursorPosition = cursorPos;
		}
	}


	public class AutotextMatcher
	{
		private const string AcceptablePrintableCharsRegex = @"[\p{L}\p{M}\p{N}\p{P}\p{S} ]{1}";
		private const string NonPrintableCharsRegex = @"{([\w\d]+)}";

		public event EventHandler<AutotextMatchEventArgs> MatchFound;
		public event EventHandler<CursorPositionChangedEventArgs> CursorpositionChanged;
		public event EventHandler<BufferContentsChangedEventArgs> BufferContentsChanged;

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
				return  _bufferString.ToString();
			}
		}
		private int _cursorPosition;

		private int _abbrMaxLength;
		private AutotextRuleConfig _matchedRule;

		readonly Regex _acceptablePrintableCharsRegex = new Regex(AcceptablePrintableCharsRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		readonly Regex _nonPrintableCharsRegex = new Regex(NonPrintableCharsRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		protected virtual void OnMatchFound(AutotextMatchEventArgs e)
		{
			var handler = MatchFound;
			if (handler != null) handler(this, e);
		}

		private void SetKeyLogger(KeyLogger keyLogger)
		{
			_keyLogger = keyLogger;
			_keyLogger.KeyCaptured += KeyLogger_KeyCaptured;
			_keyLogger.StartCapture();
		}

		public AutotextMatcher(KeyLogger keyLogger, List<AutotextRuleConfig> rules)
		{
			_rules = rules;
			_abbrMaxLength = rules.Max(p => p.Abbreviation.AbbreviationText.Length);
			int userDefinedMaxLenght = rules.Max(p => p.Abbreviation.LastCharsCount);

			if (userDefinedMaxLenght > _abbrMaxLength)
			{
				_abbrMaxLength = userDefinedMaxLenght;
			}

			KeyLogger = keyLogger;
		}

		void KeyLogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			if (e.CapturedKeys[0] == Keys.Back)
			{
				if (_bufferString.Length > 0)
				{
					string oldVal = _bufferString.ToString();
					_bufferString.Remove(_bufferString.Length - 1, 1);
					_cursorPosition--;
					string newVal = _bufferString.ToString();
					OnBufferContentsChanged(new BufferContentsChangedEventArgs(oldVal, newVal, _cursorPosition));
				}

				_matchedRule = null;
			}
			else if (e.CapturedKeys[0] == Keys.Left)
			{
				int oldPos = _cursorPosition;

				if (_cursorPosition > 0)
				{
					if (_bufferString.Length > 0)
					{
						_cursorPosition--;
						OnCursorpositionChanged(new CursorPositionChangedEventArgs(oldPos, _cursorPosition));
					}
				}

			}
			else if (e.CapturedKeys[0] == Keys.Right)
			{
				int oldPos = _cursorPosition;

				if (_cursorPosition < _bufferString.Length)
				{
					_cursorPosition++;
					OnCursorpositionChanged(new CursorPositionChangedEventArgs(oldPos, _cursorPosition));
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

						if (simpleChars.Count(p => string.Compare(p.Value , e.CapturedCharacter,StringComparison.CurrentCultureIgnoreCase) == 0 ) == 1)
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

					string oldVal = _bufferString.ToString();
					_bufferString.Clear();
					string newVal = _bufferString.ToString();
					OnBufferContentsChanged(new BufferContentsChangedEventArgs(oldVal, newVal,_cursorPosition));

					_matchedRule = null;
				}
				else
				{
					if (_acceptablePrintableCharsRegex.IsMatch(e.CapturedCharacter))
					{
						string oldVal = _bufferString.ToString();

						_bufferString.Append(e.CapturedCharacter);

						if (_bufferString.Length > _abbrMaxLength)
						{
							_bufferString.Remove(0, _bufferString.Length - _abbrMaxLength);
						}

						string newVal = _bufferString.ToString();
						OnBufferContentsChanged(new BufferContentsChangedEventArgs(oldVal, newVal,_cursorPosition));

						string abbr = _bufferString.ToString();

						foreach (AutotextRuleConfig rule in _rules)
						{
							if (rule.Abbreviation.Type == Abbriviationtype.Text)
							{
								if (abbr.EndsWith(rule.Abbreviation.AbbreviationText))
								{
									_matchedRule = rule;
									break;
								}
							}
							else if (rule.Abbreviation.Type == Abbriviationtype.Regex)
							{
								string stringToMatch = abbr;

								if (abbr.Length > rule.Abbreviation.LastCharsCount)
								{
									stringToMatch = abbr.Substring(abbr.Length - rule.Abbreviation.LastCharsCount);
								}

								MatchCollection matches = Regex.Matches(stringToMatch, rule.Abbreviation.AbbreviationText);

								if (matches.Count > 0)
								{
									_matchedRule = rule;
									_matchedRule.MatchedString = stringToMatch;
									break;
								}
							}
						}
					}
				}
			}
		}

		protected virtual void OnBufferContentsChanged(BufferContentsChangedEventArgs e)
		{
			var handler = BufferContentsChanged;
			if (handler != null) handler(this, e);
		}

		protected virtual void OnCursorpositionChanged(CursorPositionChangedEventArgs e)
		{
			var handler = CursorpositionChanged;
			if (handler != null) handler(this, e);
		}
	}
}
