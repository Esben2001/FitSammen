namespace FitSammenDekstopClient
{
    partial class FitSammen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            listBoxClasses = new ListBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBoxClasses);
            groupBox1.Location = new Point(12, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(458, 371);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "TræningsHold";
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(476, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(312, 433);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Hold Informationer";
            // 
            // listBoxClasses
            // 
            listBoxClasses.FormattingEnabled = true;
            listBoxClasses.ItemHeight = 15;
            listBoxClasses.Location = new Point(6, 61);
            listBoxClasses.Name = "listBoxClasses";
            listBoxClasses.Size = new Size(446, 304);
            listBoxClasses.TabIndex = 1;
            // 
            // FitSammen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FitSammen";
            Text = "FitSammen";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ListBox listBoxClasses;
        private GroupBox groupBox2;
    }
}
