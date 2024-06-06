namespace Szakdolgozat.DialogForms
{
    partial class MatrixImport
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
            textBox1 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            button3 = new Button();
            label9 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.AcceptsReturn = true;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(12, 208);
            textBox1.MaxLength = 0;
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Mátrix megadása\nAz alábbi módon tudunk megadni egy 2 sorral\nés 4 oszloppal rendelkező mátrixot:\n\n\n        1 2 3 4\n        5 6 7 8";
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(487, 221);
            textBox1.TabIndex = 0;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // button2
            // 
            button2.Location = new Point(343, 435);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(424, 435);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Mégse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(237, 15);
            label1.TabIndex = 8;
            label1.Text = "A mátrixot az alábbi módon tudja megadni:";
            label1.Click += MatrixImport_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 24);
            label2.Name = "label2";
            label2.Size = new Size(196, 15);
            label2.TabIndex = 9;
            label2.Text = "- Szóköz: új oszlopot ad a mátrixhoz";
            label2.Click += MatrixImport_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 39);
            label3.Name = "label3";
            label3.Size = new Size(251, 15);
            label3.TabIndex = 10;
            label3.Text = "- Sortörés (Enter gomb): új sort ad a mátrixhoz";
            label3.Click += MatrixImport_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 115);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 11;
            label4.Text = "FONTOS:";
            label4.Click += MatrixImport_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 130);
            label5.Name = "label5";
            label5.Size = new Size(354, 15);
            label5.TabIndex = 12;
            label5.Text = "- Az első sor oszlopai határozzák meg a mátrix oszlopainak számát";
            label5.Click += MatrixImport_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 160);
            label6.Name = "label6";
            label6.Size = new Size(263, 15);
            label6.TabIndex = 13;
            label6.Text = "- Az üresen hagyott helyek 0-val lesznek feltöltve";
            label6.Click += MatrixImport_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 145);
            label7.Name = "label7";
            label7.Size = new Size(336, 15);
            label7.TabIndex = 14;
            label7.Text = "- Az első oszlop sorai határozzák meg a mátrix sorainak számát";
            label7.Click += MatrixImport_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 54);
            label8.Name = "label8";
            label8.Size = new Size(452, 15);
            label8.TabIndex = 15;
            label8.Text = "- A megadott számokat szóközökkel választjuk el, vagy pedig új sorba is írhatjuk őket";
            label8.Click += MatrixImport_Click;
            // 
            // button3
            // 
            button3.Location = new Point(212, 435);
            button3.Name = "button3";
            button3.Size = new Size(125, 23);
            button3.TabIndex = 16;
            button3.Text = "Mátrix feltöltése";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 69);
            label9.Name = "label9";
            label9.Size = new Size(431, 30);
            label9.TabIndex = 17;
            label9.Text = "- Vagy pedig a \"Mátrix feltöltése\" gombra kattintva kiválaszthat a számítógépéről\r\n egy elmentett mátrixot";
            // 
            // MatrixImport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(511, 470);
            Controls.Add(label9);
            Controls.Add(button3);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MatrixImport";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mátrix megadása...";
            Shown += MatrixImport_Click;
            Click += MatrixImport_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button button3;
        private Label label9;
    }
}