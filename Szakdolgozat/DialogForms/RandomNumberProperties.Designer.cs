namespace Szakdolgozat
{
    partial class RandomNumberProperties
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
            MinValue = new TextBox();
            MaxValue = new TextBox();
            DecimalPlaces = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // MinValue
            // 
            MinValue.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            MinValue.Location = new Point(5, 48);
            MinValue.Name = "MinValue";
            MinValue.Size = new Size(266, 35);
            MinValue.TabIndex = 0;
            MinValue.Text = "1";
            MinValue.TextAlign = HorizontalAlignment.Right;
            MinValue.KeyPress += textBox1_KeyPress;
            // 
            // MaxValue
            // 
            MaxValue.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            MaxValue.Location = new Point(5, 129);
            MaxValue.Name = "MaxValue";
            MaxValue.Size = new Size(266, 35);
            MaxValue.TabIndex = 1;
            MaxValue.Text = "1";
            MaxValue.TextAlign = HorizontalAlignment.Right;
            MaxValue.KeyPress += textBox1_KeyPress;
            // 
            // DecimalPlaces
            // 
            DecimalPlaces.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            DecimalPlaces.Location = new Point(5, 206);
            DecimalPlaces.Name = "DecimalPlaces";
            DecimalPlaces.Size = new Size(266, 35);
            DecimalPlaces.TabIndex = 2;
            DecimalPlaces.Text = "1";
            DecimalPlaces.TextAlign = HorizontalAlignment.Right;
            DecimalPlaces.KeyPress += textBox3_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(5, 9);
            label1.Name = "label1";
            label1.Size = new Size(195, 36);
            label1.TabIndex = 3;
            label1.Text = "Minimális érték:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(5, 90);
            label2.Name = "label2";
            label2.Size = new Size(199, 36);
            label2.TabIndex = 4;
            label2.Text = "Maximális érték:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(5, 167);
            label3.Name = "label3";
            label3.Size = new Size(260, 36);
            label3.TabIndex = 5;
            label3.Text = "Tizedesjegyek száma:";
            // 
            // button2
            // 
            button2.Location = new Point(115, 247);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(196, 247);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Mégse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // RandomNumberProperties
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(283, 280);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DecimalPlaces);
            Controls.Add(MaxValue);
            Controls.Add(MinValue);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RandomNumberProperties";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Véletlen szám beállítása...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MinValue;
        private TextBox MaxValue;
        private TextBox DecimalPlaces;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button2;
        private Button button1;
    }
}