using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	class AutotextMatcher
	{
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

		private List<AutotextRule> _buffer = new List<AutotextRule>(100);
		private string _abbreviation;

		private void SetKeyLogger(KeyLogger keyLogger)
		{
			_keyLogger = keyLogger;
			_keyLogger.KeyCaptured += KeyLogger_KeyCaptured;
			_keyLogger.StartCapture();
		}

		public AutotextMatcher(KeyLogger keyLogger, List<AutotextRule> rules)
		{
			_rules = rules;
			KeyLogger = keyLogger;
		}

		void KeyLogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			if (string.IsNullOrEmpty(_abbreviation))
			{
				foreach (AutotextRule rule in _rules)
				{
					if (rule.Abbreviation.StartsWith(e.CapturedCharacter))
					{
						_abbreviation = e.CapturedCharacter;
						_buffer.Add(rule);
					}
				}
			}
			else
			{
				_abbreviation += e.CapturedCharacter;
				_buffer.RemoveAll(p => !p.Abbreviation.StartsWith(_abbreviation));

				if (_buffer.Count == 1)
				{
					if (_buffer[0].Abbreviation.CompareTo(_abbreviation) == 0)
					{
						OnMatchFound(new AutotextMatchEventArgs(_buffer[0]));
						_buffer.Clear();
						_abbreviation = string.Empty;
					}
				}
				else if (_buffer.Count == 0)
				{
					_abbreviation = string.Empty;
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
