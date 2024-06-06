namespace Szakdolgozat
{
    partial class MatrixSizeSelector
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
            RowSize = new TextBox();
            ColSize = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // RowSize
            // 
            RowSize.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            RowSize.Location = new Point(12, 48);
            RowSize.Name = "RowSize";
            RowSize.Size = new Size(266, 35);
            RowSize.TabIndex = 0;
            RowSize.Text = "1";
            RowSize.TextAlign = HorizontalAlignment.Right;
            RowSize.KeyPress += textBox1_KeyPress;
            // 
            // ColSize
            // 
            ColSize.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            ColSize.Location = new Point(12, 129);
            ColSize.Name = "ColSize";
            ColSize.Size = new Size(266, 35);
            ColSize.TabIndex = 1;
            ColSize.Text = "1";
            ColSize.TextAlign = HorizontalAlignment.Right;
            ColSize.KeyPress += textBox1_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(5, 9);
            label1.Name = "label1";
            label1.Size = new Size(273, 36);
            label1.TabIndex = 2;
            label1.Text = "Mátrix sorainak száma:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(5, 90);
            label2.Name = "label2";
            label2.Size = new Size(312, 36);
            label2.TabIndex = 3;
            label2.Text = "Mátrix oszlopainak száma:";
            // 
            // button2
            // 
            button2.Location = new Point(168, 170);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(249, 170);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Mégse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MatrixSizeSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(332, 203);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ColSize);
            Controls.Add(RowSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MatrixSizeSelector";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mátrix méretének megadása...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox RowSize;
        private TextBox ColSize;
        private Label label1;
        private Label label2;
        private Button button2;
        private Button button1;
    }
}