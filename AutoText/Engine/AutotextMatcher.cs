/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

AutoText is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Model.Configuration;

namespace AutoText.Engine
{
	public class AutotextMatcher
	{
		public event EventHandler<AutotextMatchEventArgs> MatchFound;


		private List<AutotextRuleConfiguration> _rules;
		public List<AutotextRuleConfiguration> Rules
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

		public AutotextMatcher(List<AutotextRuleConfiguration> rules)
		{
			_rules = rules;
		}

		public void CaptureSymbol(KeyCapturedEventArgs symbol)
		{
			if (_bufferString.Length >= _bufferString.Capacity)
			{
				_bufferString.Remove(0, 1);
			}


			AutotextRuleConfiguration config =
				_rules.SingleOrDefault(p => _bufferString.ToString().EndsWith(p.Abbreviation.AbbreviationText, !p.Abbreviation.CaseSensitive, null));

			if (config != null)
			{
				AutotextRuleTrigger configMatchTrigger =
					config.Triggers.SingleOrDefault(p => string.Compare(p.Value, symbol.CapturedCharacter, !p.CaseSensitive) == 0);

				if (configMatchTrigger != null)
				{
					OnMatchFound(new AutotextMatchEventArgs(config, configMatchTrigger));
					_bufferString.Clear();
					return;

				}
				else
				{
					foreach (string capturedKey in symbol.CapturedKeys)
					{
						foreach (AutotextRuleTrigger ruleTrigger in config.Triggers)
						{
							if (capturedKey == ruleTrigger.Value)
							{
								OnMatchFound(new AutotextMatchEventArgs(config, ruleTrigger));
								_bufferString.Clear();
								return;
							}
						}
					}

				}
			}


			_bufferString.Append(symbol.CapturedCharacter);
		}

		public bool TestForMatch(KeyCapturedEventArgs symbol)
		{

			AutotextRuleConfiguration config =
				_rules.SingleOrDefault(p => _bufferString.ToString().EndsWith(p.Abbreviation.AbbreviationText, !p.Abbreviation.CaseSensitive, null));

			if (config != null)
			{
				AutotextRuleTrigger configMatchTrigger =
					config.Triggers.SingleOrDefault(p => string.Compare(p.Value, symbol.CapturedCharacter, !p.CaseSensitive) == 0);

				if (configMatchTrigger != null)
				{
					return true;
				}
				else
				{
					foreach (string capturedKey in symbol.CapturedKeys)
					{
						foreach (AutotextRuleTrigger ruleTrigger in config.Triggers)
						{
							if (capturedKey == ruleTrigger.Value)
							{
								return true;
							}
						}
					}

				}
			}

			return false;
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
