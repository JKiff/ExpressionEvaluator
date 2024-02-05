namespace Calculator
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.expressionText = new System.Windows.Forms.TextBox();
			this.evalBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// expressionText
			// 
			this.expressionText.Location = new System.Drawing.Point(13, 13);
			this.expressionText.Name = "expressionText";
			this.expressionText.Size = new System.Drawing.Size(170, 20);
			this.expressionText.TabIndex = 0;
			this.expressionText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// evalBtn
			// 
			this.evalBtn.Location = new System.Drawing.Point(189, 11);
			this.evalBtn.Name = "evalBtn";
			this.evalBtn.Size = new System.Drawing.Size(75, 25);
			this.evalBtn.TabIndex = 1;
			this.evalBtn.Text = "Evaluate";
			this.evalBtn.UseVisualStyleBackColor = true;
			this.evalBtn.Click += new System.EventHandler(this.evalBtn_Click);
			// 
			// Form1
			// 
			this.AcceptButton = this.evalBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(295, 53);
			this.Controls.Add(this.evalBtn);
			this.Controls.Add(this.expressionText);
			this.Name = "Form1";
			this.Text = "Expression Calculator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox expressionText;
		private System.Windows.Forms.Button evalBtn;
	}
}

