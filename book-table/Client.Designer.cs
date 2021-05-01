namespace booking
{
	partial class Clien
	{
		/// <summary> 
		/// Summary description for Form1. 
		/// </summary> 
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox firstNameBox;
		private System.Windows.Forms.TextBox guestNumberBox;
		private System.Windows.Forms.Button button1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.firstNameBox = new System.Windows.Forms.TextBox();
			this.guestNumberBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.datePicker = new System.Windows.Forms.DateTimePicker();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.lastNameBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.timePicker = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Restaurant";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(27, 113);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "First Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(27, 213);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Guests:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(27, 267);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Date:";
			// 
			// firstNameBox
			// 
			this.firstNameBox.Location = new System.Drawing.Point(152, 111);
			this.firstNameBox.Name = "firstNameBox";
			this.firstNameBox.Size = new System.Drawing.Size(128, 20);
			this.firstNameBox.TabIndex = 5;
			// 
			// guestNumberBox
			// 
			this.guestNumberBox.Location = new System.Drawing.Point(152, 211);
			this.guestNumberBox.Name = "guestNumberBox";
			this.guestNumberBox.Size = new System.Drawing.Size(128, 20);
			this.guestNumberBox.TabIndex = 6;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(27, 396);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(104, 41);
			this.button1.TabIndex = 8;
			this.button1.Text = "Request Booking";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label5.Location = new System.Drawing.Point(7, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(603, 39);
			this.label5.TabIndex = 9;
			this.label5.Text = "Restaurant Booking Intermediary (RBI)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// datePicker
			// 
			this.datePicker.CustomFormat = "M-d-yy";
			this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.datePicker.Location = new System.Drawing.Point(152, 264);
			this.datePicker.MinDate = new System.DateTime(2021, 4, 28, 0, 0, 0, 0);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new System.Drawing.Size(128, 20);
			this.datePicker.TabIndex = 11;
			this.datePicker.Value = new System.DateTime(2021, 4, 28, 0, 0, 0, 0);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
			"Restaurant 1",
			"Restaurant 2"});
			this.comboBox1.Location = new System.Drawing.Point(152, 55);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(128, 21);
			this.comboBox1.TabIndex = 12;
			// 
			// lastNameBox
			// 
			this.lastNameBox.Location = new System.Drawing.Point(152, 159);
			this.lastNameBox.Name = "lastNameBox";
			this.lastNameBox.Size = new System.Drawing.Size(128, 20);
			this.lastNameBox.TabIndex = 14;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(27, 161);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 20);
			this.label6.TabIndex = 13;
			this.label6.Text = "Last Name:";
			// 
			// timePicker
			// 
			this.timePicker.Checked = false;
			this.timePicker.CustomFormat = "h:mmtt";
			this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.timePicker.Location = new System.Drawing.Point(152, 325);
			this.timePicker.MinDate = new System.DateTime(2021, 4, 28, 0, 0, 0, 0);
			this.timePicker.Name = "timePicker";
			this.timePicker.ShowUpDown = true;
			this.timePicker.Size = new System.Drawing.Size(128, 20);
			this.timePicker.TabIndex = 16;
			this.timePicker.Value = new System.DateTime(2021, 4, 28, 0, 0, 0, 0);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(27, 328);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 20);
			this.label7.TabIndex = 15;
			this.label7.Text = "Time";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(629, 647);
			this.Controls.Add(this.timePicker);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.lastNameBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.datePicker);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.guestNumberBox);
			this.Controls.Add(this.firstNameBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Client";
			this.Text = "RBI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker datePicker;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox lastNameBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker timePicker;
		private System.Windows.Forms.Label label7;
	}
}

