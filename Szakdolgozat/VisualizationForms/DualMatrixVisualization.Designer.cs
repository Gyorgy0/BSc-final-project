namespace Szakdolgozat
{
    partial class DualMatrixVisualization
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
            StepDisplayTitle = new Label();
            StepDisplay = new RichTextBox();
            button6 = new Button();
            trackBar1 = new TrackBar();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            MatrixColScrollBar3 = new HScrollBar();
            MatrixColScrollBar2 = new HScrollBar();
            MatrixRowScrollBar3 = new VScrollBar();
            MatrixRowScrollBar2 = new VScrollBar();
            MatrixColScrollBar1 = new HScrollBar();
            MatrixRowScrollBar1 = new VScrollBar();
            Matrixpanel3RowScrollbar = new Panel();
            Matrixpanel3Rows = new PictureBox();
            Matrixpanel3 = new Panel();
            MatrixPicturebox3 = new PictureBox();
            Matrixpanel3ColScrollbar = new Panel();
            Matrixpanel3Collumns = new PictureBox();
            Matrixpanel2RowScrollbar = new Panel();
            Matrixpanel2Rows = new PictureBox();
            Matrixpanel2 = new Panel();
            MatrixPicturebox2 = new PictureBox();
            Matrixpanel2ColScrollbar = new Panel();
            Matrixpanel2Collumns = new PictureBox();
            Matrixpanel1RowScrollbar = new Panel();
            Matrixpanel1Rows = new PictureBox();
            Matrixpanel1 = new Panel();
            MatrixPicturebox1 = new PictureBox();
            Matrixpanel1ColScrollbar = new Panel();
            Matrixpanel1Collumns = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            Matrixpanel3RowScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Rows).BeginInit();
            Matrixpanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox3).BeginInit();
            Matrixpanel3ColScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Collumns).BeginInit();
            Matrixpanel2RowScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel2Rows).BeginInit();
            Matrixpanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox2).BeginInit();
            Matrixpanel2ColScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel2Collumns).BeginInit();
            Matrixpanel1RowScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Rows).BeginInit();
            Matrixpanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox1).BeginInit();
            Matrixpanel1ColScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Collumns).BeginInit();
            SuspendLayout();
            // 
            // StepDisplayTitle
            // 
            StepDisplayTitle.AutoSize = true;
            StepDisplayTitle.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            StepDisplayTitle.Location = new Point(12, 374);
            StepDisplayTitle.Name = "StepDisplayTitle";
            StepDisplayTitle.Size = new Size(204, 36);
            StepDisplayTitle.TabIndex = 53;
            StepDisplayTitle.Text = "Az aktuális lépés";
            // 
            // StepDisplay
            // 
            StepDisplay.Location = new Point(13, 413);
            StepDisplay.Name = "StepDisplay";
            StepDisplay.ReadOnly = true;
            StepDisplay.Size = new Size(564, 274);
            StepDisplay.TabIndex = 52;
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
            // button6
            // 
            button6.Location = new Point(1042, 613);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 60;
            button6.Text = "Újraindítás";
            button6.UseVisualStyleBackColor = true;
            button6.Click += RestartButton_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(583, 642);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(615, 45);
            trackBar1.TabIndex = 59;
            trackBar1.TickFrequency = 0;
            trackBar1.TickStyle = TickStyle.Both;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // button5
            // 
            button5.Location = new Point(880, 613);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 58;
            button5.Text = "Lassítás";
            button5.UseVisualStyleBackColor = true;
            button5.Click += FastBackwardButton_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1123, 613);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 57;
            button4.Text = "Gyorsítás";
            button4.UseVisualStyleBackColor = true;
            button4.Click += FastForwardButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(961, 613);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 56;
            button3.Text = "Lejátszás";
            button3.UseVisualStyleBackColor = true;
            button3.Click += PlayButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(583, 613);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 55;
            button2.Text = "Előző lépés";
            button2.UseVisualStyleBackColor = true;
            button2.Click += PreviousStepButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(664, 613);
            button1.Name = "button1";
            button1.Size = new Size(100, 23);
            button1.TabIndex = 54;
            button1.Text = "Következő lépés";
            button1.UseVisualStyleBackColor = true;
            button1.Click += NextStepButton_Click;
            // 
            // MatrixColScrollBar3
            // 
            MatrixColScrollBar3.Location = new Point(820, 357);
            MatrixColScrollBar3.Name = "MatrixColScrollBar3";
            MatrixColScrollBar3.Size = new Size(298, 17);
            MatrixColScrollBar3.TabIndex = 75;
            MatrixColScrollBar3.ValueChanged += MatrixColScrollBar1_ValueChanged;
            // 
            // MatrixColScrollBar2
            // 
            MatrixColScrollBar2.Location = new Point(432, 357);
            MatrixColScrollBar2.Name = "MatrixColScrollBar2";
            MatrixColScrollBar2.Size = new Size(298, 17);
            MatrixColScrollBar2.TabIndex = 74;
            MatrixColScrollBar2.ValueChanged += MatrixColScrollBar1_ValueChanged;
            // 
            // MatrixRowScrollBar3
            // 
            MatrixRowScrollBar3.Location = new Point(1122, 114);
            MatrixRowScrollBar3.Name = "MatrixRowScrollBar3";
            MatrixRowScrollBar3.Size = new Size(17, 239);
            MatrixRowScrollBar3.TabIndex = 73;
            MatrixRowScrollBar3.ValueChanged += vScrollBar1_ValueChanged;
            // 
            // MatrixRowScrollBar2
            // 
            MatrixRowScrollBar2.Location = new Point(733, 114);
            MatrixRowScrollBar2.Name = "MatrixRowScrollBar2";
            MatrixRowScrollBar2.Size = new Size(17, 239);
            MatrixRowScrollBar2.TabIndex = 72;
            MatrixRowScrollBar2.ValueChanged += vScrollBar1_ValueChanged;
            // 
            // MatrixColScrollBar1
            // 
            MatrixColScrollBar1.Location = new Point(49, 357);
            MatrixColScrollBar1.Name = "MatrixColScrollBar1";
            MatrixColScrollBar1.Size = new Size(299, 17);
            MatrixColScrollBar1.TabIndex = 71;
            MatrixColScrollBar1.ValueChanged += MatrixColScrollBar1_ValueChanged;
            // 
            // MatrixRowScrollBar1
            // 
            MatrixRowScrollBar1.Location = new Point(351, 114);
            MatrixRowScrollBar1.Name = "MatrixRowScrollBar1";
            MatrixRowScrollBar1.Size = new Size(17, 239);
            MatrixRowScrollBar1.TabIndex = 70;
            MatrixRowScrollBar1.ValueChanged += vScrollBar1_ValueChanged;
            // 
            // Matrixpanel3RowScrollbar
            // 
            Matrixpanel3RowScrollbar.Controls.Add(Matrixpanel3Rows);
            Matrixpanel3RowScrollbar.Location = new Point(783, 115);
            Matrixpanel3RowScrollbar.Name = "Matrixpanel3RowScrollbar";
            Matrixpanel3RowScrollbar.Size = new Size(37, 239);
            Matrixpanel3RowScrollbar.TabIndex = 69;
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
            Matrixpanel3.Location = new Point(820, 114);
            Matrixpanel3.Name = "Matrixpanel3";
            Matrixpanel3.Size = new Size(299, 239);
            Matrixpanel3.TabIndex = 63;
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
            Matrixpanel3ColScrollbar.Location = new Point(820, 78);
            Matrixpanel3ColScrollbar.Name = "Matrixpanel3ColScrollbar";
            Matrixpanel3ColScrollbar.Size = new Size(299, 37);
            Matrixpanel3ColScrollbar.TabIndex = 68;
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
            // Matrixpanel2RowScrollbar
            // 
            Matrixpanel2RowScrollbar.Controls.Add(Matrixpanel2Rows);
            Matrixpanel2RowScrollbar.Location = new Point(394, 114);
            Matrixpanel2RowScrollbar.Name = "Matrixpanel2RowScrollbar";
            Matrixpanel2RowScrollbar.Size = new Size(37, 239);
            Matrixpanel2RowScrollbar.TabIndex = 67;
            Matrixpanel2RowScrollbar.Tag = MatrixRowScrollBar2;
            // 
            // Matrixpanel2Rows
            // 
            Matrixpanel2Rows.Location = new Point(1, -26);
            Matrixpanel2Rows.Name = "Matrixpanel2Rows";
            Matrixpanel2Rows.Size = new Size(35, 266);
            Matrixpanel2Rows.TabIndex = 22;
            Matrixpanel2Rows.TabStop = false;
            Matrixpanel2Rows.Tag = Matrixpanel2;
            Matrixpanel2Rows.Paint += Row_Paint;
            // 
            // Matrixpanel2
            // 
            Matrixpanel2.BackColor = SystemColors.ControlLightLight;
            Matrixpanel2.Controls.Add(MatrixPicturebox2);
            Matrixpanel2.Location = new Point(431, 114);
            Matrixpanel2.Name = "Matrixpanel2";
            Matrixpanel2.Size = new Size(299, 239);
            Matrixpanel2.TabIndex = 62;
            Matrixpanel2.Tag = "matrix2";
            Matrixpanel2.MouseWheel += Matrixpanel1_MouseWheel;
            // 
            // MatrixPicturebox2
            // 
            MatrixPicturebox2.BackColor = Color.White;
            MatrixPicturebox2.Location = new Point(-33, -26);
            MatrixPicturebox2.Name = "MatrixPicturebox2";
            MatrixPicturebox2.Size = new Size(333, 266);
            MatrixPicturebox2.TabIndex = 33;
            MatrixPicturebox2.TabStop = false;
            MatrixPicturebox2.Click += MatrixPicturebox1_Click;
            MatrixPicturebox2.Paint += MatrixPicturebox3_Paint;
            // 
            // Matrixpanel2ColScrollbar
            // 
            Matrixpanel2ColScrollbar.Controls.Add(Matrixpanel2Collumns);
            Matrixpanel2ColScrollbar.Location = new Point(431, 77);
            Matrixpanel2ColScrollbar.Name = "Matrixpanel2ColScrollbar";
            Matrixpanel2ColScrollbar.Size = new Size(299, 37);
            Matrixpanel2ColScrollbar.TabIndex = 65;
            Matrixpanel2ColScrollbar.Tag = MatrixColScrollBar2;
            // 
            // Matrixpanel2Collumns
            // 
            Matrixpanel2Collumns.Location = new Point(-33, 1);
            Matrixpanel2Collumns.Name = "Matrixpanel2Collumns";
            Matrixpanel2Collumns.Size = new Size(333, 35);
            Matrixpanel2Collumns.TabIndex = 0;
            Matrixpanel2Collumns.TabStop = false;
            Matrixpanel2Collumns.Tag = Matrixpanel2;
            Matrixpanel2Collumns.Paint += Collumn_Paint;
            // 
            // Matrixpanel1RowScrollbar
            // 
            Matrixpanel1RowScrollbar.Controls.Add(Matrixpanel1Rows);
            Matrixpanel1RowScrollbar.Location = new Point(12, 114);
            Matrixpanel1RowScrollbar.Name = "Matrixpanel1RowScrollbar";
            Matrixpanel1RowScrollbar.Size = new Size(37, 239);
            Matrixpanel1RowScrollbar.TabIndex = 66;
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
            Matrixpanel1.Location = new Point(49, 115);
            Matrixpanel1.Name = "Matrixpanel1";
            Matrixpanel1.Size = new Size(299, 239);
            Matrixpanel1.TabIndex = 61;
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
            Matrixpanel1ColScrollbar.Location = new Point(49, 77);
            Matrixpanel1ColScrollbar.Name = "Matrixpanel1ColScrollbar";
            Matrixpanel1ColScrollbar.Size = new Size(299, 37);
            Matrixpanel1ColScrollbar.TabIndex = 64;
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
            // DualMatrixVisualization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1216, 708);
            Controls.Add(MatrixColScrollBar3);
            Controls.Add(MatrixColScrollBar2);
            Controls.Add(MatrixRowScrollBar3);
            Controls.Add(MatrixRowScrollBar2);
            Controls.Add(MatrixColScrollBar1);
            Controls.Add(MatrixRowScrollBar1);
            Controls.Add(Matrixpanel3RowScrollbar);
            Controls.Add(Matrixpanel3);
            Controls.Add(Matrixpanel3ColScrollbar);
            Controls.Add(Matrixpanel2RowScrollbar);
            Controls.Add(Matrixpanel2ColScrollbar);
            Controls.Add(Matrixpanel1RowScrollbar);
            Controls.Add(Matrixpanel1ColScrollbar);
            Controls.Add(Matrixpanel2);
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
            Name = "DualMatrixVisualization";
            Load += DualMatrixVisualization_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            Matrixpanel3RowScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Rows).EndInit();
            Matrixpanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox3).EndInit();
            Matrixpanel3ColScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Collumns).EndInit();
            Matrixpanel2RowScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel2Rows).EndInit();
            Matrixpanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox2).EndInit();
            Matrixpanel2ColScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel2Collumns).EndInit();
            Matrixpanel1RowScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Rows).EndInit();
            Matrixpanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox1).EndInit();
            Matrixpanel1ColScrollbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Matrixpanel1Collumns).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label StepDisplayTitle;
        private RichTextBox StepDisplay;
        private Button button6;
        private TrackBar trackBar1;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
        private HScrollBar MatrixColScrollBar3;
        private HScrollBar MatrixColScrollBar2;
        private VScrollBar MatrixRowScrollBar3;
        private VScrollBar MatrixRowScrollBar2;
        private HScrollBar MatrixColScrollBar1;
        private VScrollBar MatrixRowScrollBar1;
        private Panel Matrixpanel3RowScrollbar;
        private PictureBox Matrixpanel3Rows;
        private Panel Matrixpanel3;
        private PictureBox MatrixPicturebox3;
        private Panel Matrixpanel3ColScrollbar;
        private PictureBox Matrixpanel3Collumns;
        private Panel Matrixpanel2RowScrollbar;
        private PictureBox Matrixpanel2Rows;
        private Panel Matrixpanel2;
        private PictureBox MatrixPicturebox2;
        private Panel Matrixpanel2ColScrollbar;
        private PictureBox Matrixpanel2Collumns;
        private Panel Matrixpanel1RowScrollbar;
        private PictureBox Matrixpanel1Rows;
        private Panel Matrixpanel1;
        private PictureBox MatrixPicturebox1;
        private Panel Matrixpanel1ColScrollbar;
        private PictureBox Matrixpanel1Collumns;
    }
}