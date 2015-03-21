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


		public List<Input> GetInput()
		{
			List<List<Input>> nestedExpressionsInput = new List<List<Input>>();

			for (int i = 0; i < NestedExpressions.Count; i++)
			{
				nestedExpressionsInput.Add(NestedExpressions[i].GetInput());
			}

			StringBuilder sbExpressionText = new StringBuilder(ExpressionText);

			for (int i = 0; i < nestedExpressionsInput.Count; i++)
			{
				AutotextExpression exp = NestedExpressions[i];
				sbExpressionText.Remove(exp.StartIndex, exp.Length );
				sbExpressionText.Insert(exp.StartIndex, string.Concat(nestedExpressionsInput[i].Select(p => p.CharToInput)));
			}

			Macros macros = Macros.Parse(sbExpressionText.ToString());
			List<Input> res = macros.GetInput();
			return res;
		}
	}
}
