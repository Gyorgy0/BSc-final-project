namespace Szakdolgozat
{
    partial class SingleMatrixVisualization
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
            components = new System.ComponentModel.Container();
            StepDisplay = new RichTextBox();
            StepDisplayTitle = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            trackBar1 = new TrackBar();
            timer1 = new System.Windows.Forms.Timer(components);
            button6 = new Button();
            MatrixColScrollBar1 = new HScrollBar();
            MatrixRowScrollBar1 = new VScrollBar();
            Matrixpanel1RowScrollbar = new Panel();
            Matrixpanel1Rows = new PictureBox();
            Matrixpanel1 = new Panel();
            MatrixPicturebox1 = new PictureBox();
            Matrixpanel1ColScrollbar = new Panel();
            Matrixpanel1Collumns = new PictureBox();
            MatrixColScrollBar3 = new HScrollBar();
            MatrixRowScrollBar3 = new VScrollBar();
            Matrixpanel3RowScrollbar = new Panel();
            Matrixpanel3Rows = new PictureBox();
            Matrixpanel3 = new Panel();
            MatrixPicturebox3 = new PictureBox();
            Matrixpanel3ColScrollbar = new Panel();
            Matrixpanel3Collumns = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            Matrixpanel1RowScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Rows).BeginInit();
            Matrixpanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox1).BeginInit();
            Matrixpanel1ColScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Collumns).BeginInit();
            Matrixpanel3RowScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Rows).BeginInit();
            Matrixpanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox3).BeginInit();
            Matrixpanel3ColScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Collumns).BeginInit();
            SuspendLayout();
            // 
            // StepDisplay
            // 
            StepDisplay.Location = new Point(879, 98);
            StepDisplay.Name = "StepDisplay";
            StepDisplay.ReadOnly = true;
            StepDisplay.Size = new Size(280, 238);
            StepDisplay.TabIndex = 28;
            StepDisplay.Text = "";
            StepDisplay.Click += richTextBox1_Click;
            StepDisplay.MouseClick += richTextBox1_MouseMove;
            StepDisplay.DoubleClick += richTextBox1_Click;
            StepDisplay.MouseDoubleClick += richTextBox1_MouseMove;
            StepDisplay.MouseCaptureChanged += richTextBox1_Click;
            StepDisplay.MouseDown += richTextBox1_MouseMove;
            StepDisplay.MouseEnter += richTextBox1_Click;
            StepDisplay.MouseLeave += richTextBox1_Click;
            StepDisplay.MouseHover += richTextBox1_Click;
            StepDisplay.MouseMove += richTextBox1_MouseMove;
            StepDisplay.MouseUp += richTextBox1_MouseMove;
            // 
            // StepDisplayTitle
            // 
            StepDisplayTitle.AutoSize = true;
            StepDisplayTitle.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            StepDisplayTitle.Location = new Point(879, 59);
            StepDisplayTitle.Name = "StepDisplayTitle";
            StepDisplayTitle.Size = new Size(204, 36);
            StepDisplayTitle.TabIndex = 30;
            StepDisplayTitle.Text = "Az aktuális lépés";
            // 
            // button1
            // 
            button1.Location = new Point(618, 369);
            button1.Name = "button1";
            button1.Size = new Size(100, 23);
            button1.TabIndex = 31;
            button1.Text = "Következő lépés";
            button1.UseVisualStyleBackColor = true;
            button1.Click += NextStepButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(537, 369);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 32;
            button2.Text = "Előző lépés";
            button2.UseVisualStyleBackColor = true;
            button2.Click += PreviousStepButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(922, 369);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 33;
            button3.Text = "Lejátszás";
            button3.UseVisualStyleBackColor = true;
            button3.Click += PlayButton_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1084, 369);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 34;
            button4.Text = "Gyorsítás";
            button4.UseVisualStyleBackColor = true;
            button4.Click += FastForwardButton_Click;
            // 
            // button5
            // 
            button5.Location = new Point(841, 369);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 35;
            button5.Text = "Lassítás";
            button5.UseVisualStyleBackColor = true;
            button5.Click += FastBackwardButton_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(537, 398);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(622, 45);
            trackBar1.TabIndex = 36;
            trackBar1.TickFrequency = 0;
            trackBar1.TickStyle = TickStyle.Both;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            // 
            // button6
            // 
            button6.Location = new Point(1003, 369);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 37;
            button6.Text = "Újraindítás";
            button6.UseVisualStyleBackColor = true;
            button6.Click += RestartButton_Click;
            // 
            // MatrixColScrollBar1
            // 
            MatrixColScrollBar1.Location = new Point(49, 339);
            MatrixColScrollBar1.Name = "MatrixColScrollBar1";
            MatrixColScrollBar1.Size = new Size(299, 17);
            MatrixColScrollBar1.TabIndex = 76;
            MatrixColScrollBar1.ValueChanged += MatrixColScrollBar1_ValueChanged;
            // 
            // MatrixRowScrollBar1
            // 
            MatrixRowScrollBar1.Location = new Point(351, 96);
            MatrixRowScrollBar1.Name = "MatrixRowScrollBar1";
            MatrixRowScrollBar1.Size = new Size(17, 239);
            MatrixRowScrollBar1.TabIndex = 75;
            MatrixRowScrollBar1.ValueChanged += vScrollBar1_ValueChanged;
            // 
            // Matrixpanel1RowScrollbar
            // 
            Matrixpanel1RowScrollbar.Controls.Add(Matrixpanel1Rows);
            Matrixpanel1RowScrollbar.Location = new Point(12, 96);
            Matrixpanel1RowScrollbar.Name = "Matrixpanel1RowScrollbar";
            Matrixpanel1RowScrollbar.Size = new Size(37, 239);
            Matrixpanel1RowScrollbar.TabIndex = 74;
            Matrixpanel1RowScrollbar.Tag = MatrixRowScrollBar1;
            // 
            // Matrixpanel1Rows
            // 
            Matrixpanel1Rows.Location = new Point(1, -26);
            Matrixpanel1Rows.Name = "Matrixpanel1Rows";
            Matrixpanel1Rows.Size = new Size(35, 266);
            Matrixpanel1Rows.TabIndex = 21;
            Matrixpanel1Rows.TabStop = false;
            Matrixpanel1Rows.Tag = Matrixpanel1;
            Matrixpanel1Rows.Paint += Row_Paint;
            // 
            // Matrixpanel1
            // 
            Matrixpanel1.BackColor = SystemColors.ControlLightLight;
            Matrixpanel1.Controls.Add(MatrixPicturebox1);
            Matrixpanel1.Location = new Point(49, 97);
            Matrixpanel1.Name = "Matrixpanel1";
            Matrixpanel1.Size = new Size(299, 239);
            Matrixpanel1.TabIndex = 72;
            Matrixpanel1.Tag = "matrix1";
            Matrixpanel1.MouseWheel += Matrixpanel1_MouseWheel;
            // 
            // MatrixPicturebox1
            // 
            MatrixPicturebox1.BackColor = Color.White;
            MatrixPicturebox1.Location = new Point(-33, -26);
            MatrixPicturebox1.Name = "MatrixPicturebox1";
            MatrixPicturebox1.Size = new Size(333, 266);
            MatrixPicturebox1.TabIndex = 32;
            MatrixPicturebox1.TabStop = false;
            MatrixPicturebox1.Click += MatrixPicturebox1_Click;
            MatrixPicturebox1.Paint += MatrixPicturebox3_Paint;
            // 
            // Matrixpanel1ColScrollbar
            // 
            Matrixpanel1ColScrollbar.Controls.Add(Matrixpanel1Collumns);
            Matrixpanel1ColScrollbar.Location = new Point(49, 59);
            Matrixpanel1ColScrollbar.Name = "Matrixpanel1ColScrollbar";
            Matrixpanel1ColScrollbar.Size = new Size(299, 37);
            Matrixpanel1ColScrollbar.TabIndex = 73;
            Matrixpanel1ColScrollbar.Tag = MatrixColScrollBar1;
            // 
            // Matrixpanel1Collumns
            // 
            Matrixpanel1Collumns.BackColor = SystemColors.Control;
            Matrixpanel1Collumns.Location = new Point(-33, 1);
            Matrixpanel1Collumns.Name = "Matrixpanel1Collumns";
            Matrixpanel1Collumns.Size = new Size(333, 35);
            Matrixpanel1Collumns.TabIndex = 20;
            Matrixpanel1Collumns.TabStop = false;
            Matrixpanel1Collumns.Tag = Matrixpanel1;
            Matrixpanel1Collumns.Paint += Collumn_Paint;
            // 
            // MatrixColScrollBar3
            // 
            MatrixColScrollBar3.Location = new Point(557, 339);
            MatrixColScrollBar3.Name = "MatrixColScrollBar3";
            MatrixColScrollBar3.Size = new Size(298, 17);
            MatrixColScrollBar3.TabIndex = 81;
            MatrixColScrollBar3.ValueChanged += MatrixColScrollBar1_ValueChanged;
            // 
            // MatrixRowScrollBar3
            // 
            MatrixRowScrollBar3.Location = new Point(859, 96);
            MatrixRowScrollBar3.Name = "MatrixRowScrollBar3";
            MatrixRowScrollBar3.Size = new Size(17, 239);
            MatrixRowScrollBar3.TabIndex = 80;
            MatrixRowScrollBar3.ValueChanged += vScrollBar1_ValueChanged;
            // 
            // Matrixpanel3RowScrollbar
            // 
            Matrixpanel3RowScrollbar.Controls.Add(Matrixpanel3Rows);
            Matrixpanel3RowScrollbar.Location = new Point(520, 97);
            Matrixpanel3RowScrollbar.Name = "Matrixpanel3RowScrollbar";
            Matrixpanel3RowScrollbar.Size = new Size(37, 239);
            Matrixpanel3RowScrollbar.TabIndex = 79;
            Matrixpanel3RowScrollbar.Tag = MatrixRowScrollBar3;
            // 
            // Matrixpanel3Rows
            // 
            Matrixpanel3Rows.Location = new Point(1, -26);
            Matrixpanel3Rows.Name = "Matrixpanel3Rows";
            Matrixpanel3Rows.Size = new Size(35, 266);
            Matrixpanel3Rows.TabIndex = 22;
            Matrixpanel3Rows.TabStop = false;
            Matrixpanel3Rows.Tag = Matrixpanel3;
            Matrixpanel3Rows.Paint += Row_Paint;
            // 
            // Matrixpanel3
            // 
            Matrixpanel3.BackColor = SystemColors.ControlLightLight;
            Matrixpanel3.Controls.Add(MatrixPicturebox3);
            Matrixpanel3.Location = new Point(557, 96);
            Matrixpanel3.Name = "Matrixpanel3";
            Matrixpanel3.Size = new Size(299, 239);
            Matrixpanel3.TabIndex = 77;
            Matrixpanel3.Tag = "matrix3";
            Matrixpanel3.MouseWheel += Matrixpanel1_MouseWheel;
            // 
            // MatrixPicturebox3
            // 
            MatrixPicturebox3.BackColor = Color.White;
            MatrixPicturebox3.Location = new Point(-33, -26);
            MatrixPicturebox3.Name = "MatrixPicturebox3";
            MatrixPicturebox3.Size = new Size(333, 266);
            MatrixPicturebox3.TabIndex = 33;
            MatrixPicturebox3.TabStop = false;
            MatrixPicturebox3.Click += MatrixPicturebox1_Click;
            MatrixPicturebox3.Paint += MatrixPicturebox3_Paint;
            // 
            // Matrixpanel3ColScrollbar
            // 
            Matrixpanel3ColScrollbar.Controls.Add(Matrixpanel3Collumns);
            Matrixpanel3ColScrollbar.Location = new Point(557, 60);
            Matrixpanel3ColScrollbar.Name = "Matrixpanel3ColScrollbar";
            Matrixpanel3ColScrollbar.Size = new Size(299, 37);
            Matrixpanel3ColScrollbar.TabIndex = 78;
            Matrixpanel3ColScrollbar.Tag = MatrixColScrollBar3;
            // 
            // Matrixpanel3Collumns
            // 
            Matrixpanel3Collumns.Location = new Point(-33, 1);
            Matrixpanel3Collumns.Name = "Matrixpanel3Collumns";
            Matrixpanel3Collumns.Size = new Size(333, 35);
            Matrixpanel3Collumns.TabIndex = 0;
            Matrixpanel3Collumns.TabStop = false;
            Matrixpanel3Collumns.Tag = Matrixpanel3;
            Matrixpanel3Collumns.Paint += Collumn_Paint;
            // 
            // SingleMatrixVisualization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1183, 467);
            Controls.Add(MatrixColScrollBar3);
            Controls.Add(MatrixRowScrollBar3);
            Controls.Add(Matrixpanel3RowScrollbar);
            Controls.Add(Matrixpanel3);
            Controls.Add(Matrixpanel3ColScrollbar);
            Controls.Add(MatrixColScrollBar1);
            Controls.Add(MatrixRowScrollBar1);
            Controls.Add(Matrixpanel1RowScrollbar);
            Controls.Add(Matrixpanel1ColScrollbar);
            Controls.Add(Matrixpanel1);
            Controls.Add(button6);
            Controls.Add(trackBar1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(StepDisplayTitle);
            Controls.Add(StepDisplay);
            Name = "SingleMatrixVisualization";
            Load += SingleMatrixVisualization_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            Matrixpanel1RowScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Rows).EndInit();
            Matrixpanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox1).EndInit();
            Matrixpanel1ColScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Collumns).EndInit();
            Matrixpanel3RowScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Rows).EndInit();
            Matrixpanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox3).EndInit();
            Matrixpanel3ColScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Collumns).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox StepDisplay;
        private Label StepDisplayTitle;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private TrackBar trackBar1;
        private System.Windows.Forms.Timer timer1;
        private Button button6;
        private HScrollBar MatrixColScrollBar1;
        private VScrollBar MatrixRowScrollBar1;
        private Panel Matrixpanel1RowScrollbar;
        private PictureBox Matrixpanel1Rows;
        private Panel Matrixpanel1;
        private PictureBox MatrixPicturebox1;
        private Panel Matrixpanel1ColScrollbar;
        private PictureBox Matrixpanel1Collumns;
        private HScrollBar MatrixColScrollBar3;
        private VScrollBar MatrixRowScrollBar3;
        private Panel Matrixpanel3RowScrollbar;
        private PictureBox Matrixpanel3Rows;
        private Panel Matrixpanel3;
        private PictureBox MatrixPicturebox3;
        private Panel Matrixpanel3ColScrollbar;
        private PictureBox Matrixpanel3Collumns;
    }
}