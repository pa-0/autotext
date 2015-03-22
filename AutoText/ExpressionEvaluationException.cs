using System;
using System.Runtime.Serialization;

namespace AutoText
{
	[Serializable]
	public class ExpressionEvaluationException : Exception
	{

		public ExpressionEvaluationException()
		{
		}

		public ExpressionEvaluationException(string message) : base(message)
		{
		}

		public ExpressionEvaluationException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ExpressionEvaluationException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}