using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void evalBtn_Click(object sender, EventArgs e)
		{
			string expText = expressionText.Text;
			Expression ex = new Expression(expText);
			expressionText.Text = ex.Solve().ToString();
		}
	}
}
