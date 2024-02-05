using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

namespace Calculator
{
	internal class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());

			//string test = "(12.5+2*(5+6.15*8))";
			//Expression ex = new Expression(test);
			//Console.WriteLine(ex.Solve());
			//test = "45-68/6";
			//ex = new Expression(test);
			//Console.WriteLine(ex.Solve());
			//Console.ReadKey();
		}
	}
}
