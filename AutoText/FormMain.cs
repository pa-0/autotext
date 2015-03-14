using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using WindowsInput;
using AutoText.Helpers;
using AutoText.Helpers.Configuration;

namespace AutoText
{
	[XmlRootAttribute("PurchaseOrder", Namespace = "http://www.cpandl.com",IsNullable = false)]
	public class PurchaseOrder
	{
		public Address ShipTo;
		public string OrderDate;
		// The XmlArrayAttribute changes the XML element name
		// from the default of "OrderedItems" to "Items".
		[XmlArrayAttribute("Items")]
		public OrderedItem[] OrderedItems;
		public decimal SubTotal;
		public decimal ShipCost;
		public decimal TotalCost;
	}

	public class Address
	{
		// The XmlAttribute instructs the XmlSerializer to serialize the 
		// Name field as an XML attribute instead of an XML element (the 
		// default behavior).
		[XmlAttribute]
		public string Name;
		public string Line1;

		// Setting the IsNullable property to false instructs the 
		// XmlSerializer that the XML attribute will not appear if 
		// the City field is set to a null reference.
		[XmlElementAttribute(IsNullable = false)]
		public string City;
		public string State;
		public string Zip;
	}

	public class OrderedItem
	{
		[XmlElement("ItemName")]
		public string ItemNameField;
		public string Description;
		public decimal UnitPrice;
		public int Quantity;
		public decimal LineTotal;

		// Calculate is a custom method that calculates the price per item
		// and stores the value in a field.
		public void Calculate()
		{
			LineTotal = UnitPrice * Quantity;
		}
	}




	public class SomeListItem
	{
		[XmlAttribute("name")]
		public string SomeField { get; set; }
	}


	public class SomeField
	{
		public string SomeFieldAttr { get; set; }
	}

	public class TestDataObj
	{
		//[XmlAttribute("someField")]
		//public string SomeField { get; set; }

		//List<SomeListItem> ListOfItems{ get; set; }

		[XmlAttribute("someAttr")] public string SomeAttribute;
	}

	[XmlRootAttribute("rootElem", Namespace = "http://www.cpandl.com", IsNullable = false)]
	public class TestRoot
	{
		[XmlElement("someField")]
		public SomeField SomeFieldVal { get; set; }

		[XmlArray("someList")]
		[XmlArrayItem("listItem")]
		public List<SomeListItem> ListItems { get; set; }
	}

	[Serializable]
	public class SerTest
	{
		public decimal NumTest { get; set; }
		public List<SomeField> StringListTest { get; set; }

		public SerTest()
		{
		}

		public SerTest(decimal dectest, List<SomeField> stringListTest)
		{
			NumTest = dectest;
			StringListTest = stringListTest;
		}
	}


	public partial class FormMain : Form
	{
		private List<AutotextRuleConfig> _rules;
		private Dictionary<string, int> _macrosChars;
		private AutotextMatcher _matcher;
		private string _textBoxText;
		private KeyLogger _keylogger = new KeyLogger();

		public FormMain()
		{

			InitializeComponent();

			try
			{

				SerTest st = new SerTest(1.2m,new List<SomeField>(){new SomeField(){SomeFieldAttr = "attr 1"},new SomeField(){SomeFieldAttr = "attr 2"}});

				List<string> stList = new List<string>(){"asdasd","asdasd"};

				XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
				TextWriter writer = new StreamWriter(@"d:\Downloads\serTest.xml");
				serializer.Serialize(writer, stList);
				writer.Flush();
				writer.Close();
				{ }

				// Creates an instance of the XmlSerializer class;
				// specifies the type of object to be deserialized.
				// If the XML document has been altered with unknown 
				// nodes or attributes, handles them with the 
				// UnknownNode and UnknownAttribute events.
				serializer.UnknownNode += (sender, args) =>
				{
					{ }
				};
				serializer.UnknownAttribute += (sender, args) =>
				{
					{ }
				};

				// A FileStream is needed to read the XML document.
				FileStream fs = new FileStream(@"d:\Downloads\po.xml", FileMode.Open);
				// Declares an object variable of the type to be deserialized.
				PurchaseOrder po;
				// Uses the Deserialize method to restore the object's state 
				// with data from the XML document. */
				po = (PurchaseOrder)serializer.Deserialize(fs);
				{ }
			}
			catch (Exception ex)
			{
				{ }
			}


			try
			{
				FileStream fs = new FileStream(@"d:\Downloads\text.xml", FileMode.Open);
				XmlSerializer deserializer = new XmlSerializer(typeof(TestRoot));
				TestRoot objects = (TestRoot)deserializer.Deserialize(fs);
				fs.Flush();
				fs.Close();

				string attrVal = objects.SomeFieldVal.SomeFieldAttr;

				{ }
			}
			catch (Exception ex)
			{
				{ }
			}



			try
			{
				XmlRootAttribute xRoot = new XmlRootAttribute();
				xRoot.ElementName = "autotextRules";

				XmlSerializer deserializer = new XmlSerializer(typeof(AutotextRulesRoot), xRoot);
				Stream textReader = new FileStream(@"AutotextRules.xml", FileMode.Open, FileAccess.Read);
				AutotextRulesRoot rules = (AutotextRulesRoot)deserializer.Deserialize(textReader);
				textReader.Flush();
				textReader.Close();


				{ }

			}
			catch (Exception ex)
			{
				{ }
			}


			_macrosChars = ConfigHelper.GetMacrosCharacters(@"MacrosCharacters.xml");
			_rules = ConfigHelper.GetAutotextRules("AutotextRules.xml");
			_matcher = new AutotextMatcher(_keylogger, _rules);
			_matcher.MatchFound += _matcher_MatchFound;

			_keylogger.KeyCaptured += _testKeylogger_KeyCaptured;
			_keylogger.StartCapture();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			/*
			Dictionary<string, int> keysEmbeddedValues = new Dictionary<string, int>();
			Dictionary<string, int> keysOtherValues = new Dictionary<string, int>();

			string[] names = Enum.GetNames(typeof(Keys));
			int[] values = (int[])Enum.GetValues(typeof(Keys));

			for (int i = 0; i < names.Length; i++)
			{
				keysEmbeddedValues.Add(names[i], values[i]);
			}

			string[] names1 = Enum.GetNames(typeof(Keys));
			ushort[] values1 = (ushort[])Enum.GetValues(typeof(Keys));

			for (int i = 0; i < names1.Length; i++)
			{
				keysOtherValues.Add(names1[i], values1[i]);
			}

			string otherStr = string.Join("\r\n", keysOtherValues.Select(p => p.Value + " " + p.Key));

			*/

			/*
			try
			{
				List<string> keysNames = Enum.GetNames(typeof(Keys)).ToList();
				List<int> keysValues = ((int[])Enum.GetValues(typeof(Keys))).ToList();

				int engKeybLayout = 67699721;
				//int ebgLayout =	WinAPI.GetKeyboardLayout(WinAPI.GetWindowThreadProcessId(WinAPI.GetForegroundWindow(), IntPtr.Zero));


				string res = "\"Index\";\"Name\";\"Value\";\"Char\";\"Unicode str\";\"Is control\";\"Is digit\";\"Is letter\";\"Is number\";\"Is punct\";\"Is separator\";\"Is whitespace\";\"Is symbol\";\"Match Regex\"\r\n";
				string nonPrintableKeys = "";

				for (int i = 0; i < keysNames.Count; i++)
				{
					string unicodeStr = TextHelper.GetCharsFromKeys(keysValues[i], false, false, engKeybLayout);

					if (!Regex.IsMatch(unicodeStr, @"[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}"))
					{
						nonPrintableKeys += keysValues[i] + "\t" + keysNames[i] + "\r\n";
					}

					char charTOSave = 'E';

					if (keysValues[i] <= short.MaxValue && keysValues[i] >= short.MinValue)
					{
						charTOSave = Convert.ToChar(keysValues[i]);
					}

					bool isControl = Char.IsControl(charTOSave);
					bool isDigit = Char.IsDigit(charTOSave);
					bool isLetter = Char.IsLetter(charTOSave);
					bool isNumber = Char.IsNumber(charTOSave);
					bool isPunctuation = Char.IsPunctuation(charTOSave);
					bool isSeparator = Char.IsSeparator(charTOSave);
					bool isWhitespace = Char.IsWhiteSpace(charTOSave);
					bool isSymbol = Char.IsSymbol(charTOSave);
					bool matchRegex = Regex.IsMatch(unicodeStr, @"[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}");//[\p{L}\p{M}\p{N}\p{P}\p{S}]{1}

					res += string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{12}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{13}\"\r\n", 
						i, 
						keysNames[i], 
						keysValues[i], 
						charTOSave, 
						isControl,
						isDigit,
						isLetter,
						isNumber,
						isPunctuation,
						isSeparator,
						isWhitespace,
						isSymbol,
						unicodeStr,
						matchRegex);
				}
				//File.WriteAllText(@"d:\Downloads\all keys.csv", res,Encoding.UTF8);

			}
			catch (Exception ex)
			{
				{ }
			}
			*/
		}

		void _testKeylogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			textBox1.Invoke(new Action(() =>
			{
				textBox1.Text += (string.IsNullOrEmpty(e.CapturedCharacter) ? "\"\"" : e.CapturedCharacter) + "\r\n" + string.Join(" | ", e.CapturedKeys) + "\r\n\r\n";
				textBox1.Select(textBox1.Text.Length, 0);
				textBox1.ScrollToCaret();

			}));
		}

		void _matcher_MatchFound(object sender, AutotextMatchEventArgs e)
		{
			//SendKeys.SendWait(e.MatchedRule.Phrase);
			//InputSimulator.SimulateTextEntry(e.MatchedRule.Phrase);


			//InputSimulator.SimulateKeyDown(Keys.ControlKey);
			//InputSimulator.SimulateKeyPress(Keys.A);
			//Thread.Sleep(200);
			InputSimulator.SimulateKeyPress('З');
			//Thread.Sleep(200);
			//InputSimulator.SimulateKeyUp(Keys.ControlKey);

			//InputSimulator.SimulateKeyUp(Keys.Menu);



			/*
			for (int i = 0; i < e.MatchedRule.Phrase.Length; i++)
			{
				SendKeys.SendWait(e.MatchedRule.Phrase[i].ToString());
				Thread.Sleep(200);
			}
			*/


			/*
			_textBoxText += "Match found\r\n";
			textBox1.SetPropertyThreadSafe(() => textBox1.Text, _textBoxText);
			 */
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}
	}
}
