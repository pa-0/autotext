using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoText
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			/*
			List<string> my = File.ReadAllText(@"c:\Documents and Settings\k304148\Desktop\Downloads\CU pages.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
			List<string> alexey = File.ReadAllText(@"c:\Documents and Settings\k304148\Desktop\Downloads\Alexey links.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
			List<string> vpids = File.ReadAllText(@"c:\Documents and Settings\k304148\Desktop\Downloads\vpids.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

			List<string> res = my.Where(p => !vpids.Contains(p)).ToList();
			res.Clear();
			res.AddRange(alexey.Where(p => !vpids.Contains(p)).Distinct().ToList());

			string resStr = string.Join("\r\n", res);
			*/
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
		}
	}
}
