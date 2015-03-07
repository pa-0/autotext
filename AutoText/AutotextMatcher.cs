using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoText
{
	class AutotextMatcher
	{
		public KeyLogger KeyLogger { get; private set; }

		public AutotextMatcher(KeyLogger keyLogger)
		{
			KeyLogger = keyLogger;
			KeyLogger.KeyCaptured += KeyLogger_KeyCaptured;
			KeyLogger.StartCapture();
		}

		void KeyLogger_KeyCaptured(object sender, KeyCapturedEventArgs e)
		{
			
		}
	}
}
