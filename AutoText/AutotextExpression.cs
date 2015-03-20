using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoText.Helpers;

namespace AutoText
{
	public class AutotextExpression
	{
		public string ExpressionText { get; private set; }
		public int StartIndex { get; private set; }
		public int Length { get; private set; }
		public List<AutotextExpression> NestedExpressions { get; private set; }
		public int InputLength { get; set; }

		public AutotextExpression(string expressionText, int startIndex, int length)
		{
			ExpressionText = expressionText;
			StartIndex = startIndex;
			Length = length;
			NestedExpressions = new List<AutotextExpression>(100);
		}

		private List<Input> GenerateExpressionInput()
		{
			List<List<Input>> nestedExpressionsInput = new List<List<Input>>();

			for (int i = 0; i < NestedExpressions.Count; i++)
			{
				nestedExpressionsInput.Add(NestedExpressions[i].GenerateExpressionInput());
			}

			List<Input> result = new List<Input>(1000);


			throw new NotImplementedException();
		}

		public List<Input> GetInput()
		{
			{ }

			throw new NotImplementedException();
		}
	}
}
