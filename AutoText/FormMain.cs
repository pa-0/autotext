using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	public partial class FormMain : Form
	{
		private List<AutotextRule> _rules;
		private AutotextMatcher _matcher;
		private string _textBoxText;

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			/*
			int[] _keysValues = (int[])Enum.GetValues(typeof(Keys));
			List<char> chars = new List<char>();
			char character = ' ';

			for (int i = 0; i < _keysValues.Length; i++)
			{
				int value = _keysValues[i];

				try
				{
					character = Convert.ToChar((uint)value);

					if (!Char.IsControl(character))
					{
						chars.Add(character);
						textBox1.Text += "(pos: "+ (i + 1) +")\t(value: " + value + ")\t" + character.ToString() + "\r\n";
					}
				}
				catch (Exception)
				{
				}

				//textBox1.Text += value.ToString() + "\r\n";
			}

			List<int> notSeq = new List<int>(200);
			for (int i = 0; i < _keysValues.Length; i++)
			{
				if (_keysValues[i] != i)
				{
					notSeq.Add(_keysValues[i]);
				}
			}
			*/
			_rules = ConfigHelper.GetAutotextRules("AutotextRules.xml");
			_matcher = new AutotextMatcher(new KeyLogger(), _rules);
			_matcher.MatchFound += _matcher_MatchFound;
		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			_textBoxText += "Match found\r\n";
			textBox1.SetPropertyThreadSafe(() => textBox1.Text, _textBoxText);
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}
	}
}
