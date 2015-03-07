using System.Configuration;

namespace AutoText.Helpers.Configuration
{
	[ConfigurationCollection(typeof(RuleElement))]
	public class AutotextRulesCollection : ConfigurationElementCollection
	{
		public RuleElement this[int index]
		{
			get { return (RuleElement)BaseGet(index); }

			set
			{
				if (BaseGet(index) != null)
					BaseRemoveAt(index);

				BaseAdd(index, value);
			}
		}


		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RuleElement)(element)).Trigger;
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new RuleElement();
		}
	}
}