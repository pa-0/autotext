namespace AutoText.Engine
{
	public class AutotextExpressionParameter
	{
		public string Name { get; private set; }
		public string Value { get; private set; }
		public int RelativeStartIndex { get;  set; }
		public int Length { get; set; }

		public AutotextExpressionParameter(string name, string value, int startIndex, int length)
		{
			Name = name;
			Value = value;
			RelativeStartIndex = startIndex;
			Length = length;
		}
	}
}