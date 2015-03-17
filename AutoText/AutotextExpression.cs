using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoText
{
	public class AutotextExpression
	{
		public string ExpressionText { get; set; }
		public int StartIndex { get; set; }
		public int Length { get; set; }
		public List<AutotextExpression> NestedExpressions { get; set; }

		public AutotextExpression()
		{
			NestedExpressions = new List<AutotextExpression>(100);
		}

		public AutotextExpression(string expressionText, int startIndex, int length)
		{
			ExpressionText = expressionText;
			StartIndex = startIndex;
			Length = length;
			NestedExpressions = new List<AutotextExpression>(100);
		}

		private List<Input> GenerateExpressionInput()
		{
			throw new NotImplementedException();
		}

		public List<Input> GetInput()
		{

			throw new NotImplementedException();
		}
	}
}
