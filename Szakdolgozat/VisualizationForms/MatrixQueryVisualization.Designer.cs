namespace Szakdolgozat
{
    partial class MatrixQueryVisualization
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
            MatrixRowScrollBar3 = new VScrollBar();
            Matrixpanel3RowScrollbar = new Panel();
            Matrixpanel3Rows = new PictureBox();
            Matrixpanel3 = new Panel();
            MatrixPicturebox3 = new PictureBox();
            Matrixpanel3ColScrollbar = new Panel();
            Matrixpanel3Collumns = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            Matrixpanel3RowScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Rows).BeginInit();
            Matrixpanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixPicturebox3).BeginInit();
            Matrixpanel3ColScrollbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Matrixpanel3Collumns).BeginInit();
            SuspendLayout();
            // 
            // StepDisplayTitle
            // 
            StepDisplayTitle.AutoSize = true;
            StepDisplayTitle.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            StepDisplayTitle.Location = new Point(387, 72);
            StepDisplayTitle.Name = "StepDisplayTitle";
            StepDisplayTitle.Size = new Size(204, 36);
            StepDisplayTitle.TabIndex = 56;
            StepDisplayTitle.Text = "Az aktuális lépés";
            // 
            // StepDisplay
            // 
            StepDisplay.Location = new Point(387, 111);
            StepDisplay.Name = "StepDisplay";
            StepDisplay.ReadOnly = true;
            StepDisplay.ScrollBars = RichTextBoxScrollBars.Vertical;
            StepDisplay.Size = new Size(395, 240);
            StepDisplay.TabIndex = 55;
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
            button6.Location = new Point(626, 401);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 63;
            button6.Text = "Újraindítás";
            button6.UseVisualStyleBackColor = true;
            button6.Click += RestartButton_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(167, 430);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(615, 45);
            trackBar1.TabIndex = 62;
            trackBar1.TickFrequency = 0;
            trackBar1.TickStyle = TickStyle.Both;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // button5
            // 
            button5.Location = new Point(464, 401);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 61;
            button5.Text = "Lassítás";
            button5.UseVisualStyleBackColor = true;
            button5.Click += FastBackwardButton_Click;
            // 
            // button4
            // 
            button4.Location = new Point(707, 401);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 60;
            button4.Text = "Gyorsítás";
            button4.UseVisualStyleBackColor = true;
            button4.Click += FastForwardButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(545, 401);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 59;
            button3.Text = "Lejátszás";
            button3.UseVisualStyleBackColor = true;
            button3.Click += PlayButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(167, 401);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 58;
            button2.Text = "Előző lépés";
            button2.UseVisualStyleBackColor = true;
            button2.Click += PreviousStepButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(248, 401);
            button1.Name = "button1";
            button1.Size = new Size(100, 23);
            button1.TabIndex = 57;
            button1.Text = "Következő lépés";
            button1.UseVisualStyleBackColor = true;
            button1.Click += NextStepButton_Click;
            // 
            // MatrixColScrollBar3
            // 
            MatrixColScrollBar3.Location = new Point(49, 354);
            MatrixColScrollBar3.Name = "MatrixColScrollBar3";
            MatrixColScrollBar3.Size = new Size(298, 17);
            MatrixColScrollBar3.TabIndex = 68;
            MatrixColScrollBar3.ValueChanged += MatrixColScrollBar1_ValueChanged;
            // 
            // MatrixRowScrollBar3
            // 
            MatrixRowScrollBar3.Location = new Point(351, 111);
            MatrixRowScrollBar3.Name = "MatrixRowScrollBar3";
            MatrixRowScrollBar3.Size = new Size(17, 239);
            MatrixRowScrollBar3.TabIndex = 67;
            MatrixRowScrollBar3.ValueChanged += vScrollBar1_ValueChanged;
            // 
            // Matrixpanel3RowScrollbar
            // 
            Matrixpanel3RowScrollbar.Controls.Add(Matrixpanel3Rows);
            Matrixpanel3RowScrollbar.Location = new Point(12, 112);
            Matrixpanel3RowScrollbar.Name = "Matrixpanel3RowScrollbar";
            Matrixpanel3RowScrollbar.Size = new Size(37, 239);
            Matrixpanel3RowScrollbar.TabIndex = 66;
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
            // 
            // Matrixpanel3
            // 
            Matrixpanel3.BackColor = SystemColors.ControlLightLight;
            Matrixpanel3.Controls.Add(MatrixPicturebox3);
            Matrixpanel3.Location = new Point(49, 111);
            Matrixpanel3.Name = "Matrixpanel3";
            Matrixpanel3.Size = new Size(299, 239);
            Matrixpanel3.TabIndex = 64;
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
            Matrixpanel3ColScrollbar.Location = new Point(49, 75);
            Matrixpanel3ColScrollbar.Name = "Matrixpanel3ColScrollbar";
            Matrixpanel3ColScrollbar.Size = new Size(299, 37);
            Matrixpanel3ColScrollbar.TabIndex = 65;
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
            // 
            // MatrixQueryVisualization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(805, 497);
            Controls.Add(MatrixColScrollBar3);
            Controls.Add(MatrixRowScrollBar3);
            Controls.Add(Matrixpanel3RowScrollbar);
            Controls.Add(Matrixpanel3);
            Controls.Add(Matrixpanel3ColScrollbar);
            Controls.Add(button6);
            Controls.Add(trackBar1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(StepDisplayTitle);
            Controls.Add(StepDisplay);
            Name = "MatrixQueryVisualization";
            Load += MatrixQueryVisualization_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
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
        private VScrollBar MatrixRowScrollBar3;
        private Panel Matrixpanel3RowScrollbar;
        private PictureBox Matrixpanel3Rows;
        private Panel Matrixpanel3;
        private PictureBox MatrixPicturebox3;
        private Panel Matrixpanel3ColScrollbar;
        private PictureBox Matrixpanel3Collumns;
    }
}