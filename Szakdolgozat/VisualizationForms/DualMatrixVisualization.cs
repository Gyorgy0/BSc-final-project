namespace Szakdolgozat
{
    public partial class DualMatrixVisualization : Form
    {
        public DualMatrixVisualization(Matrix matrix1, Matrix matrix2, long StepCounter)
        {
            InitializeComponent();
            this.matrix1Contents = matrix1;
            this.matrix2Contents = matrix2;
            this.matrix1 = new Matrix(matrix1.ActualRows, matrix1.ActualCols, MatrixPicturebox1, Matrixpanel1Rows, Matrixpanel1Collumns);
            this.matrix2 = new Matrix(matrix2.ActualRows, matrix2.ActualCols, MatrixPicturebox2, Matrixpanel2Rows, Matrixpanel2Collumns);
            this.StepCounter = StepCounter;
            this.matrix3 = new Matrix(0, 0, MatrixPicturebox3, Matrixpanel3Rows, Matrixpanel3Collumns);
            timer1.Interval = TimerInterval;

            TxtboxFont = new Font(this.Font, FontStyle.Regular);
        }

        Matrix matrix1Contents, matrix2Contents;

        Matrix matrix1, matrix2, matrix3;
        long StepCounter;
        long ActualStep;

        int TimerInterval = 1000;

        public decimal Multiplicator;

        TextBox SelectedTxtbox;
        int TxtboxWidth = 33;
        int TxtboxHeight = 26;
        Font TxtboxFont;

        bool RowNav;
        bool ColNav;

        int RowPos;
        int ColPos = -1;

        Dictionary<string, Color> CellColors1 = new Dictionary<string, Color>();
        Dictionary<string, Color> CellColors2 = new Dictionary<string, Color>();

        public EventHandler ActualOperation;
        bool IsPlaying;
        bool Reverse;

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        private void DualMatrixVisualization_Load(object sender, EventArgs e)
        {
            Initialization(sender, e);
            if (Convert.ToInt32(StepCounter) <= 1024)
            {
                trackBar1.Maximum = Convert.ToInt32(StepCounter);
                trackBar1.Minimum = 0;
                trackBar1.Value = 0;
                trackBar1.TickFrequency = 1;
                trackBar1.TickStyle = TickStyle.Both;
            }
            else
            {
                trackBar1.Dispose();
            }
            ActualStep = 0;

            StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
            for (int i = 0; i < matrix1.ActualRows; i++)
            {
                for (int j = 0; j < matrix1.ActualCols; j++)
                {
                    matrix1.ContentsArray[i, j] = matrix1Contents.ContentsArray[i, j];
                }
            }
            SelectedTxtbox = new TextBox
            {
                Tag = matrix1,
                Name = "0_0",
                Text = matrix1.ContentsArray[0, 0].ToString(),
                TextAlign = HorizontalAlignment.Right,
                Size = new Size(TxtboxWidth - 3, TxtboxHeight - 3),
                Location = new Point(((0 * TxtboxWidth) + (TxtboxWidth - matrix1.HorizontalScrollBar.Value) + 1), ((0 * TxtboxHeight) + (TxtboxHeight - matrix1.VerticalScrollBar.Value) + 1)),
                ReadOnly = true,
            };
            SelectedTxtbox.KeyDown += Keys_Down;
            SelectedTxtbox.KeyUp += Keys_Up;
            SelectedTxtbox.KeyPress += textBox1_KeyPress;
            matrix1.PanelPicturebox.Controls.Add(SelectedTxtbox);
            for (int i = 0; i < matrix2.ActualRows; i++)
            {
                for (int j = 0; j < matrix2.ActualCols; j++)
                {
                    matrix2.ContentsArray[i, j] = matrix2Contents.ContentsArray[i, j];
                }
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix2);
            MatrixPanelRefresher(matrix3);
        }

        private void MatrixPanelRefresher(Matrix SelectedMatrix)
        {
            SelectedMatrix.PanelPicturebox.Location = new Point(-TxtboxWidth, -TxtboxHeight);
            SelectedMatrix.PanelPicturebox.Size = new Size(TxtboxWidth + SelectedMatrix.MatrixPanel.Width, TxtboxHeight + SelectedMatrix.MatrixPanel.Height);

            SelectedMatrix.ColPicturebox.Location = new Point(-TxtboxWidth, 0);
            SelectedMatrix.ColPicturebox.Size = new Size(TxtboxWidth + SelectedMatrix.MatrixPanel.Width, 26);

            SelectedMatrix.RowPicturebox.Location = new Point(0, -TxtboxHeight);
            SelectedMatrix.RowPicturebox.Size = new Size(33, TxtboxHeight + SelectedMatrix.MatrixPanel.Height);

            if ((SelectedMatrix.ActualRows * TxtboxHeight) < SelectedMatrix.VerticalScrollBar.Height || SelectedMatrix.ActualRows == 0)
            {
                SelectedMatrix.VerticalScrollBar.Value = 0;
                SelectedMatrix.VerticalScrollBar.Hide();
            }
            else
            {
                SelectedMatrix.VerticalScrollBar.Show();
                SelectedMatrix.VerticalScrollBar.Maximum = (SelectedMatrix.ActualRows * TxtboxHeight) - (SelectedMatrix.RowScrollbar.Height) + 10;
                SelectedMatrix.VerticalScrollBar.SmallChange = TxtboxHeight;
            }
            if ((SelectedMatrix.ActualCols * TxtboxWidth) < SelectedMatrix.HorizontalScrollBar.Width || SelectedMatrix.ActualCols == 0)
            {
                SelectedMatrix.HorizontalScrollBar.Value = 0;
                SelectedMatrix.HorizontalScrollBar.Hide();
            }
            else
            {
                SelectedMatrix.HorizontalScrollBar.Show();
                SelectedMatrix.HorizontalScrollBar.Maximum = (SelectedMatrix.ActualCols * TxtboxWidth) - (SelectedMatrix.ColScrollbar.Width) + 10;
                SelectedMatrix.HorizontalScrollBar.SmallChange = TxtboxWidth;
            }
            SelectedMatrix.RowScrollbar.Refresh();
            SelectedMatrix.ColScrollbar.Refresh();
            SelectedMatrix.PanelPicturebox.Refresh();
        }

        private void Keys_Down(object? sender, KeyEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            Matrix? SelectedMatrix = (Matrix)Txtbox.Tag;
            if (e.KeyCode == Keys.R)
            {
                RowNav = true;
            }
            if (e.KeyCode == Keys.C)
            {
                ColNav = true;
            }
        }

        private void Keys_Up(object? sender, KeyEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            SelectedTxtbox = Txtbox;
            Matrix? SelectedMatrix = (Matrix)Txtbox.Tag;

            if (e.KeyCode == Keys.R)
            {
                RowNav = false;
            }
            if (e.KeyCode == Keys.C)
            {
                ColNav = false;
            }
        }

        private void textBox1_KeyPress(object? sender, KeyPressEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            Matrix? SelectedMatrix = (Matrix)Txtbox.Tag;
            if (RowNav || ColNav)
            {
                int increment = 0;
                e.Handled = true;
                if (e.KeyChar == '+')
                {
                    e.Handled = true;
                    increment = 1;
                }
                if (e.KeyChar == '-')
                {
                    e.Handled = true;
                    increment = -1;
                }
                if (TxtboxWidth > (20 - increment) && TxtboxWidth < (250 - increment) && ColNav)
                {
                    TxtboxWidth += increment;
                    SelectedTxtbox.Size = new Size(TxtboxWidth - 3, TxtboxHeight);
                }
                if (TxtboxHeight > (20 - increment) && TxtboxHeight < (85 - increment) && RowNav)
                {
                    TxtboxFont = new Font(this.Font.FontFamily, (TxtboxFont.Size + (increment * 0.25F)));
                    SelectedTxtbox.Font = TxtboxFont;
                    MatrixPanelRefresher(SelectedMatrix);
                    TxtboxHeight = SelectedTxtbox.Height + 3;
                    SelectedTxtbox.Size = new Size(TxtboxWidth - 3, SelectedTxtbox.Height);
                }
                MatrixPanelRefresher(matrix1);
                MatrixPanelRefresher(matrix2);
                MatrixPanelRefresher(matrix3);

                SelectedTxtbox.Font = TxtboxFont;
            }
        }

        // Next step button
        private void NextStepButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            ActualOperation(sender, e);
        }

        // Previous step button
        private void PreviousStepButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Reverse = true;
            ActualOperation(sender, e);
            Reverse = false;
        }

        // Play / pause button
        private void PlayButton_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;

            if (!IsPlaying)
            {
                Sender.Text = "Megállítás";
                timer1.Start();
                IsPlaying = true;
                return;
            }
            if (IsPlaying)
            {
                TimerStop();
                return;
            }
        }

        // Restart button
        private void RestartButton_Click(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                TimerStop();
            }
            Reverse = true;
            while (0 < ActualStep)
            {
                ActualOperation(sender, e);
            }
            Reverse = false;
        }
        // Fast forward button
        private void FastForwardButton_Click(object sender, EventArgs e)
        {
            Button Sender = (Button)sender;
            if (timer1.Interval != 1000 && button5.Text != "Lassítás")
            {
                timer1.Interval = 1000;
                button5.Text = "Lassítás";
            }
            if (timer1.Interval < TimerInterval / 8)
            {
                timer1.Interval = 1000;
                Sender.Text = "Gyorsítás";
            }
            else
            {
                if (Sender.Text == "Gyorsítás")
                {
                    Sender.Text = "";
                }
                Sender.Text += ">";
                timer1.Interval = timer1.Interval / 2;
            }
        }

        // Fast backward button
        private void FastBackwardButton_Click(object sender, EventArgs e)
        {
            Button Sender = (Button)sender;
            if (timer1.Interval != 1000 && button4.Text != "Gyorsítás")
            {
                timer1.Interval = 1000;
                button4.Text = "Gyorsítás";
            }
            if (timer1.Interval > TimerInterval * 8)
            {
                timer1.Interval = 1000;
                Sender.Text = "Lassítás";
            }
            else
            {
                if (Sender.Text == "Lassítás")
                {
                    Sender.Text = "";
                }
                Sender.Text += "<";
                timer1.Interval = timer1.Interval * 2;
            }
        }

        // This method is for navigating between steps
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                TimerStop();
                return;
            }
            while (trackBar1.Value > ActualStep)
            {
                ActualOperation(sender, e);
            }
            Reverse = true;
            while (trackBar1.Value < ActualStep)
            {
                ActualOperation(sender, e);
            }
            Reverse = false;
        }

        // Initializes the Scrollbars for Matrixpanel3
        private void Initialization(object? sender, EventArgs e)
        {
            timer1.Tick += ActualOperation;
            Reverse = true;
            ActualOperation(sender, e);
            Reverse = false;
        }

        private void TimerStop()
        {
            timer1.Stop();
            button3.Text = "Lejátszás";
            IsPlaying = false;
        }

        // Scrollbar Paint methods
        void Collumn_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox Sender = sender as PictureBox;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            Pen Lines = new Pen(Color.Black);
            Brush ColColor = new SolidBrush(Color.FromArgb(200, 200, 200));
            Brush TextBrush = new SolidBrush(Color.Black);
            StringFormat TextStyle = new StringFormat();
            TextStyle.Alignment = StringAlignment.Center;
            TextStyle.LineAlignment = StringAlignment.Center;
            TextStyle.Trimming = StringTrimming.Character;
            for (int i = 0; i < SelectedMatrix.ActualCols; i++)
            {
                if (i >= SelectedMatrix.HorizontalScrollBar.Value / TxtboxWidth && i <= ((SelectedMatrix.HorizontalScrollBar.Value + SelectedMatrix.MatrixPanel.Width) / TxtboxWidth) + 1 && SelectedMatrix != matrix3 || (ActualStep != 0 && SelectedMatrix == matrix3))
                {
                    String Number = (i + 1).ToString();
                    Rectangle Collumn = new Rectangle(((i * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value)), 0, TxtboxWidth, Sender.Height - 1);
                    Rectangle CollumnFill = new Rectangle(((i * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value)) + 1, 1, TxtboxWidth - 1, Sender.Height - 2);
                    RectangleF ValueField = new RectangleF(CollumnFill.X, CollumnFill.Y + ((CollumnFill.Height - this.Font.Height) / 2), CollumnFill.Width, this.Font.Height);
                    e.Graphics.DrawRectangle(Lines, Collumn);
                    e.Graphics.FillRectangle(ColColor, CollumnFill);
                    e.Graphics.DrawString(Number, this.Font, TextBrush, ValueField, TextStyle);
                }
            }
        }

        void Row_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox Sender = sender as PictureBox;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            Pen Lines = new Pen(Color.Black);
            Brush ColColor = new SolidBrush(Color.FromArgb(200, 200, 200));
            Brush TextBrush = new SolidBrush(Color.Black);
            StringFormat TextStyle = new StringFormat();
            TextStyle.Alignment = StringAlignment.Center;
            TextStyle.LineAlignment = StringAlignment.Center;
            TextStyle.Trimming = StringTrimming.Character;
            for (int i = 0; i < SelectedMatrix.ActualRows; i++)
            {
                if (i >= SelectedMatrix.VerticalScrollBar.Value / TxtboxHeight && i <= ((SelectedMatrix.VerticalScrollBar.Value + SelectedMatrix.MatrixPanel.Height) / TxtboxHeight) + 1 && SelectedMatrix != matrix3 || (ActualStep != 0 && SelectedMatrix == matrix3))
                {
                    String Number = (i + 1).ToString();
                    Rectangle Collumn = new Rectangle(0, ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value)), Sender.Width - 1, TxtboxHeight);
                    Rectangle CollumnFill = new Rectangle(1, ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value)) + 1, Sender.Width - 2, TxtboxHeight - 1);
                    RectangleF ValueField = new RectangleF(CollumnFill.X, CollumnFill.Y + ((CollumnFill.Height - this.Font.Height) / 2), CollumnFill.Width, this.Font.Height);
                    e.Graphics.DrawRectangle(Lines, Collumn);
                    e.Graphics.FillRectangle(ColColor, CollumnFill);
                    e.Graphics.DrawString(Number, this.Font, TextBrush, ValueField, TextStyle);
                }
            }
        }

        private void MatrixPicturebox3_Paint(object sender, PaintEventArgs e)
        {
            PictureBox Sender = (PictureBox)sender;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            Pen Border = new Pen(Color.Gray);
            Brush BackColor = new SolidBrush(Color.White);
            Brush TextBrush = new SolidBrush(Color.Black);
            StringFormat TextStyle = new StringFormat();
            TextStyle.Alignment = StringAlignment.Far;
            TextStyle.LineAlignment = StringAlignment.Center;


            if ((Matrix)SelectedTxtbox.Tag == SelectedMatrix)
            {
                int[] SelectedTxtboxLocation = new int[2];
                string[] SelectedTxtboxName = SelectedTxtbox.Name.Split('_', 2);
                SelectedTxtboxLocation[0] = Convert.ToInt32(SelectedTxtboxName[0]);
                SelectedTxtboxLocation[1] = Convert.ToInt32(SelectedTxtboxName[1]);

                if (SelectedMatrix == matrix1 && CellColors1.ContainsKey(SelectedTxtbox.Name))
                {
                    SelectedTxtbox.BackColor = (Color)CellColors1[SelectedTxtbox.Name];
                }
                else if (SelectedMatrix == matrix2 && CellColors2.ContainsKey(SelectedTxtbox.Name))
                {
                    SelectedTxtbox.BackColor = (Color)CellColors2[SelectedTxtbox.Name];
                }
                else
                {
                    SelectedTxtbox.BackColor = Color.White;
                }

                SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[1] * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[0] * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
            }
            for (int i = SelectedMatrix.VerticalScrollBar.Value / TxtboxHeight; i < ((SelectedMatrix.VerticalScrollBar.Value + SelectedMatrix.MatrixPanel.Height) / TxtboxHeight) + 1; i++)
            {
                for (int j = SelectedMatrix.HorizontalScrollBar.Value / TxtboxWidth; j < ((SelectedMatrix.HorizontalScrollBar.Value + SelectedMatrix.MatrixPanel.Width) / TxtboxWidth) + 1; j++)
                {
                    if (i < SelectedMatrix.ActualRows && j < SelectedMatrix.ActualCols)
                    {
                        if (SelectedMatrix == matrix1 && CellColors1.ContainsKey(i.ToString() + "_" + j.ToString()))
                        {
                            BackColor = new SolidBrush((Color)(CellColors1[i.ToString() + "_" + j.ToString()]));
                        }
                        else if (SelectedMatrix == matrix2 && CellColors2.ContainsKey(i.ToString() + "_" + j.ToString()))
                        {
                            BackColor = new SolidBrush((Color)(CellColors2[i.ToString() + "_" + j.ToString()]));
                        }
                        else
                        {
                            BackColor = new SolidBrush(Color.White);
                        }
                        Rectangle Cell = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1), TxtboxWidth - 3, TxtboxHeight - 3);
                        Rectangle CellFill = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 2), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 2), TxtboxWidth - 4, TxtboxHeight - 4);
                        RectangleF ValueField = new RectangleF(CellFill.X, CellFill.Y + ((CellFill.Height - TxtboxFont.Height) / 2), CellFill.Width, TxtboxFont.Height);
                        if (SelectedMatrix == matrix3 && (((i * matrix3.ActualCols) + j) <= ((RowPos * matrix3.ActualCols) + ColPos)))
                        {
                            e.Graphics.DrawRectangle(Border, Cell);
                            e.Graphics.FillRectangle(BackColor, CellFill);
                            e.Graphics.DrawString(matrix3.ContentsArray[i, j].ToString(), TxtboxFont, TextBrush, ValueField, TextStyle);
                        }
                        else if (SelectedMatrix != matrix3)
                        {
                            e.Graphics.DrawRectangle(Border, Cell);
                            e.Graphics.FillRectangle(BackColor, CellFill);
                            e.Graphics.DrawString(SelectedMatrix.ContentsArray[i, j].ToString(), TxtboxFont, TextBrush, ValueField, TextStyle);
                        }
                    }
                }
            }
        }
        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            VScrollBar Sender = sender as VScrollBar;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            SelectedMatrix.RowScrollbar.Refresh();
            SelectedMatrix.PanelPicturebox.Refresh();
        }

        private void MatrixColScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            HScrollBar Sender = sender as HScrollBar;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            SelectedMatrix.ColScrollbar.Refresh();
            SelectedMatrix.PanelPicturebox.Refresh();
        }

        private void Matrixpanel1_MouseWheel(object sender, MouseEventArgs e)
        {
            Panel Sender = (Panel)sender;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;
            if (SelectedMatrix.VerticalScrollBar.Visible && SelectedMatrix.VerticalScrollBar.Value - (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.VerticalScrollBar.SmallChange) <= SelectedMatrix.VerticalScrollBar.Maximum && SelectedMatrix.VerticalScrollBar.Value >= (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.VerticalScrollBar.SmallChange))
            {
                SelectedMatrix.VerticalScrollBar.Value += -(e.Delta / Math.Abs(e.Delta) * SelectedMatrix.VerticalScrollBar.SmallChange);
            }
            if (SelectedMatrix.HorizontalScrollBar.Visible && !SelectedMatrix.VerticalScrollBar.Visible && SelectedMatrix.HorizontalScrollBar.Value - (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.HorizontalScrollBar.SmallChange) <= SelectedMatrix.HorizontalScrollBar.Maximum && SelectedMatrix.HorizontalScrollBar.Value >= (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.HorizontalScrollBar.SmallChange))
            {
                SelectedMatrix.HorizontalScrollBar.Value += -(e.Delta / Math.Abs(e.Delta) * SelectedMatrix.HorizontalScrollBar.SmallChange);
            }
            if (RowNav || ColNav)
            {
                if (TxtboxWidth > 20 - (-(e.Delta / Math.Abs(e.Delta))) && TxtboxWidth < 250 - (-(e.Delta / Math.Abs(e.Delta))) && ColNav)
                {
                    TxtboxWidth += -(e.Delta / Math.Abs(e.Delta));
                    SelectedTxtbox.Size = new Size(TxtboxWidth - 3, TxtboxHeight);
                }
                if (TxtboxHeight > 20 - (-(e.Delta / Math.Abs(e.Delta))) && TxtboxHeight < 85 - (-(e.Delta / Math.Abs(e.Delta))) && RowNav)
                {
                    TxtboxFont = new Font(this.Font.FontFamily, (TxtboxFont.Size + (-(e.Delta / Math.Abs(e.Delta)) * 0.25F)));
                    SelectedTxtbox.Font = TxtboxFont;
                    MatrixPanelRefresher(SelectedMatrix);
                    TxtboxHeight = SelectedTxtbox.Height + 3;
                    SelectedTxtbox.Size = new Size(TxtboxWidth - 3, SelectedTxtbox.Height);
                }
                MatrixPanelRefresher(matrix1);
                MatrixPanelRefresher(matrix2);
                MatrixPanelRefresher(matrix3);

                SelectedTxtbox.Font = TxtboxFont;
            }
        }

        private void MatrixPicturebox1_Click(object sender, EventArgs e)
        {
            PictureBox Sender = (PictureBox)sender;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            int[] SelectedTxtboxLocation = new int[2];
            string[] SelectedTxtboxName = SelectedTxtbox.Name.Split('_', 2);
            SelectedTxtboxLocation[0] = Convert.ToInt32(SelectedTxtboxName[0]);
            SelectedTxtboxLocation[1] = Convert.ToInt32(SelectedTxtboxName[1]);

            if ((SelectedMatrix != (Matrix)SelectedTxtbox.Tag && SelectedMatrix != matrix3) || (ActualStep != 0 && ((RowPos * matrix3.ActualCols) + ColPos) > (-1) && SelectedMatrix == matrix3))
            {
                ((Matrix)SelectedTxtbox.Tag).PanelPicturebox.Controls.Remove(SelectedTxtbox);

                SelectedMatrix.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = SelectedMatrix;
                SelectedTxtbox.Focus();
            }
            if (SelectedMatrix != matrix3 && SelectedMatrix.ActualRows != 0 && SelectedMatrix.ActualCols != 0)
            {
                SelectedTxtboxLocation = new int[2];
                if (SelectedMatrix != matrix3 && ((MousePosition.X - this.Location.X - SelectedMatrix.MatrixPanel.Location.X - (this.Width - this.ClientRectangle.Width) + 8) + SelectedMatrix.HorizontalScrollBar.Value) / (TxtboxWidth) < SelectedMatrix.ActualCols && ((MousePosition.Y - this.Location.Y - SelectedMatrix.MatrixPanel.Location.Y - (this.Height - this.ClientRectangle.Height) + 8) + SelectedMatrix.VerticalScrollBar.Value) / (TxtboxHeight) < SelectedMatrix.ActualRows)
                {
                    SelectedTxtboxLocation[0] = ((MousePosition.X - this.Location.X - SelectedMatrix.MatrixPanel.Location.X - (this.Width - this.ClientRectangle.Width) + 8) + SelectedMatrix.HorizontalScrollBar.Value) / (TxtboxWidth);
                    SelectedTxtboxLocation[1] = ((MousePosition.Y - this.Location.Y - SelectedMatrix.MatrixPanel.Location.Y - (this.Height - this.ClientRectangle.Height) + 8) + SelectedMatrix.VerticalScrollBar.Value) / (TxtboxHeight);
                }
            }
            else if (SelectedMatrix == matrix3 && SelectedMatrix.ActualRows != 0 && SelectedMatrix.ActualCols != 0)
            {
                SelectedTxtboxLocation = new int[2];
                if (((((((MousePosition.Y - this.Location.Y - SelectedMatrix.MatrixPanel.Location.Y - (this.Height - this.ClientRectangle.Height) + 8) + SelectedMatrix.VerticalScrollBar.Value) / (TxtboxHeight)) * matrix3.ActualCols) + (((MousePosition.X - this.Location.X - SelectedMatrix.MatrixPanel.Location.X - (this.Width - this.ClientRectangle.Width) + 8) + SelectedMatrix.HorizontalScrollBar.Value) / (TxtboxWidth))) <= ((RowPos * matrix3.ActualCols) + ColPos)))
                {
                    SelectedTxtboxLocation[0] = ((MousePosition.X - this.Location.X - SelectedMatrix.MatrixPanel.Location.X - (this.Width - this.ClientRectangle.Width) + 8) + SelectedMatrix.HorizontalScrollBar.Value) / (TxtboxWidth);
                    SelectedTxtboxLocation[1] = ((MousePosition.Y - this.Location.Y - SelectedMatrix.MatrixPanel.Location.Y - (this.Height - this.ClientRectangle.Height) + 8) + SelectedMatrix.VerticalScrollBar.Value) / (TxtboxHeight);
                }
            }
            SelectedTxtbox.Name = SelectedTxtboxLocation[1].ToString() + "_" + SelectedTxtboxLocation[0].ToString();
            SelectedTxtbox.Text = SelectedMatrix.ContentsArray[SelectedTxtboxLocation[1], SelectedTxtboxLocation[0]].ToString();
            SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[0] * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[1] * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
            SelectedTxtbox.Focus();
        }

        // Matrix addition step
        public void AdditionStep(object sender, EventArgs e)
        {
            matrix3.ContentsArray = matrix3.ResizeArray(matrix1.ActualRows, matrix1.ActualCols);

            if (SelectedTxtbox != null && (Matrix)SelectedTxtbox.Tag == matrix3)
            {
                ((Matrix)SelectedTxtbox.Tag).PanelPicturebox.Controls.Remove(SelectedTxtbox);
                int[] SelectedTxtboxLocation = new int[2];
                matrix1.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = matrix1;
                SelectedTxtbox.Name = SelectedTxtboxLocation[1].ToString() + "_" + SelectedTxtboxLocation[0].ToString();
                SelectedTxtbox.Text = matrix1.ContentsArray[SelectedTxtboxLocation[1], SelectedTxtboxLocation[0]].ToString();
                SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[0] * TxtboxWidth) + (TxtboxWidth - matrix1.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[1] * TxtboxHeight) + (TxtboxHeight - matrix1.VerticalScrollBar.Value) + 1));
                SelectedTxtbox.Focus();
            }
            if (ActualStep < StepCounter && !Reverse)
            {
                int Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                CellColors1.Add(TxtBoxName, Color.LightYellow);
                CellColors2.Add(TxtBoxName, Color.LightYellow);
                double Value;
                try
                {
                    Value = Convert.ToDouble(Convert.ToDecimal((decimal)matrix1.ContentsArray[Row, Col] + (decimal)matrix2.ContentsArray[Row, Col]));
                }
                catch (Exception)
                {
                    Value = matrix1.ContentsArray[Row, Col] + matrix2.ContentsArray[Row, Col];
                }
                if (Value == -0)
                {
                    Value = 0;
                }
                matrix3.ContentsArray[Row, Col] = Value;
                RowPos = Row;
                ColPos = Col;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                StepDisplay.Rtf = "{" + String.Format(@"\rtf\ansi\pard\ql Az első és a második mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopából kiválasztjuk az értékeket, majd összeadjuk őket az alábbi módon: \par\line\pard\qc\b {2} + {3} = {4} \b0\line\line\par\ql Ezután beírjuk az összeadás eredményét az eredmény mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopába.", (Row + 1), (Col + 1), matrix1.ContentsArray[Row, Col], matrix2.ContentsArray[Row, Col], Value) + "}";
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                int Row;
                int Col;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                string TxtBoxName;
                if (ActualStep > 0)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix1.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix1.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    double Value;
                    try
                    {
                        Value = Convert.ToDouble(Convert.ToDecimal((decimal)matrix1.ContentsArray[Row, Col] + (decimal)matrix2.ContentsArray[Row, Col]));
                    }
                    catch (Exception)
                    {
                        Value = matrix1.ContentsArray[Row, Col] + matrix2.ContentsArray[Row, Col];
                    }
                    if (Value == -0)
                    {
                        Value = 0;
                    }
                    matrix3.ContentsArray[Row, Col] = Value;
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql Az első és a második mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopából kiválasztjuk az értékeket, majd összeadjuk őket az alábbi módon: \par\line\pard\qc\b {2} + {3} = {4} \b0\line\line\par\ql Ezután beírjuk az összeadás eredményét az eredmény mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopába.", (Row + 1), (Col + 1), matrix1.ContentsArray[Row, Col], matrix2.ContentsArray[Row, Col], Value), "}");
                }
                else if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    ColPos = -1;
                }
                Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                TxtBoxName = String.Concat(Row, '_', Col);
                CellColors1.Remove(TxtBoxName);
                CellColors2.Remove(TxtBoxName);
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix2);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
            CellColors2.TrimExcess();
        }

        int RowCounter = 0;
        int ColCounter = 0;
        int LevelCounter = 0;
        int SumCounter = 0;
        double CellResult;

        // Matrix multiplication step
        public void MultiplicationByMatrixStep(object sender, EventArgs e)
        {
            matrix3.ContentsArray = matrix3.ResizeArray(matrix1.ActualRows, matrix2.ActualCols);

            if (SelectedTxtbox != null && (Matrix)SelectedTxtbox.Tag == matrix3)
            {
                ((Matrix)SelectedTxtbox.Tag).PanelPicturebox.Controls.Remove(SelectedTxtbox);
                int[] SelectedTxtboxLocation = new int[2];
                matrix1.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = matrix1;
                SelectedTxtbox.Name = SelectedTxtboxLocation[1].ToString() + "_" + SelectedTxtboxLocation[0].ToString();
                SelectedTxtbox.Text = matrix1.ContentsArray[SelectedTxtboxLocation[1], SelectedTxtboxLocation[0]].ToString();
                SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[0] * TxtboxWidth) + (TxtboxWidth - matrix1.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[1] * TxtboxHeight) + (TxtboxHeight - matrix1.VerticalScrollBar.Value) + 1));
                SelectedTxtbox.Focus();
            }
            if (ActualStep < StepCounter && !Reverse)
            {
                if (LevelCounter == matrix1.ActualCols && SumCounter == 0)
                {
                    SumCounter = 1;
                    for (int i = 0; i < matrix1.ActualCols; i++)
                    {
                        CellColors1.Remove(RowCounter.ToString() + "_" + i.ToString());
                        CellColors2.Remove(i.ToString() + "_" + ColCounter.ToString());
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc {0} \par\line\pard\ql A fenti művelet elvégzése után az eredményt beírjuk az eredmény mátrix \b {1}. \b0 sorának \b {2}. \b0 oszlopába.\par", VisualizeSum((LevelCounter + 1), ((LevelCounter + 1) == (matrix1.ActualCols + 1))), (RowCounter + 1), (ColCounter + 1)), "}");
                    matrix3.ContentsArray[RowCounter, ColCounter] = CellResult;
                    RowPos = ((RowCounter * matrix3.ActualCols) + ColCounter) / matrix3.ActualCols;
                    ColPos = ((RowCounter * matrix3.ActualCols) + ColCounter) % matrix3.ActualCols;
                }
                else if (SumCounter == 1)
                {
                    SumCounter = 0;
                    ColCounter++;
                    LevelCounter = 0;
                    CellResult = 0;
                }
                if ((ColCounter) == matrix2.ActualCols)
                {
                    RowCounter++;
                    ColCounter = 0;
                }
                if (SumCounter == 0)
                {
                    CellColors1.Add(RowCounter.ToString() + "_" + LevelCounter.ToString(), Color.LightYellow);
                    CellColors2.Add(LevelCounter.ToString() + "_" + ColCounter.ToString(), Color.LightYellow);
                    try
                    {
                        CellResult = Convert.ToDouble(Convert.ToDecimal((decimal)CellResult + ((decimal)matrix1.ContentsArray[RowCounter, LevelCounter] * (decimal)matrix2.ContentsArray[LevelCounter, ColCounter])));
                    }
                    catch (Exception)
                    {
                        CellResult = CellResult + (matrix1.ContentsArray[RowCounter, LevelCounter] * matrix2.ContentsArray[LevelCounter, ColCounter]);
                    }
                    if (CellResult == -0)
                    {
                        CellResult = 0;
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql Az első mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopában található cella értékét beszorozzuk a második mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopában található cella értékével.\line\line\pard\qc {4} \par", (RowCounter + 1), (LevelCounter + 1), (LevelCounter + 1), (ColCounter + 1), VisualizeSum((LevelCounter + 1), ((LevelCounter + 1) == (matrix1.ActualCols + 1)))), "}");
                }
                LevelCounter++;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                LevelCounter--;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (LevelCounter > 0 && LevelCounter < matrix1.ActualCols + 1)
                {
                    // Normal step back
                    if (LevelCounter < matrix1.ActualCols)
                    {
                        CellColors1.Remove(RowCounter.ToString() + "_" + LevelCounter.ToString());
                        CellColors2.Remove(LevelCounter.ToString() + "_" + ColCounter.ToString());
                        try
                        {
                            CellResult = Convert.ToDouble(Convert.ToDecimal((decimal)CellResult - ((decimal)matrix1.ContentsArray[RowCounter, LevelCounter] * (decimal)matrix2.ContentsArray[LevelCounter, ColCounter])));
                        }
                        catch (Exception)
                        {
                            CellResult = CellResult - (matrix1.ContentsArray[RowCounter, LevelCounter] * matrix2.ContentsArray[LevelCounter, ColCounter]);
                        }
                        if (CellResult == -0)
                        {
                            CellResult = 0;
                        }
                    }
                    else
                    {
                        CellResult = matrix3.ContentsArray[RowCounter, ColCounter];
                        for (int i = 0; i < matrix1.ActualCols; i++)
                        {
                            CellColors1.Add(RowCounter.ToString() + "_" + i.ToString(), Color.LightYellow);
                            CellColors2.Add(i.ToString() + "_" + ColCounter.ToString(), Color.LightYellow);
                        }
                        RowPos = ((RowCounter * matrix3.ActualCols) + ColCounter - 1) / matrix3.ActualCols;
                        ColPos = ((RowCounter * matrix3.ActualCols) + ColCounter - 1) % matrix3.ActualCols;
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql Az első mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopában található cella értékét beszorozzuk a második mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopában található cella értékével.\line\line\pard\qc {4} \par", (RowCounter + 1), LevelCounter, LevelCounter, (ColCounter + 1), VisualizeSum(LevelCounter, (LevelCounter == (matrix1.ActualCols + 1)))), "}");
                    SumCounter = 0;
                }
                if (LevelCounter == 0 && ActualStep != 0 && SumCounter == 0)
                {
                    CellColors1.Remove(RowCounter.ToString() + "_" + 0.ToString());
                    CellColors2.Remove(0.ToString() + "_" + ColCounter.ToString());

                    LevelCounter = matrix1.ActualCols + 1;
                    SumCounter = 1;
                    // Pause
                }
                if (SumCounter == 1 && ColCounter > 0)
                {
                    ColCounter--;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc {0} \par\line\pard\ql A fenti művelet elvégzése után az eredményt beírjuk az eredmény mátrix \b {1}. \b0 sorának \b {2}. \b0 oszlopába.\par", VisualizeSum(LevelCounter, (LevelCounter == matrix1.ActualCols + 1)), (RowCounter + 1), (ColCounter + 1)), "}");
                    // Previous Cell
                }
                else if (SumCounter == 1 && ColCounter == 0 && RowCounter > 0)
                {
                    ColCounter = matrix2.ActualCols - 1;
                    RowCounter--;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc {0} \par\line\pard\ql A fenti művelet elvégzése után az eredményt beírjuk az eredmény mátrix \b {1}. \b0 sorának \b {2}. \b0 oszlopába.\par", VisualizeSum(LevelCounter, (LevelCounter == matrix1.ActualCols + 1)), (RowCounter + 1), (ColCounter + 1)), "}");
                    // Previous row
                }
                else if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    // 0th step
                    CellColors1.Remove(RowCounter.ToString() + "_" + LevelCounter.ToString());
                    CellColors2.Remove(LevelCounter.ToString() + "_" + ColCounter.ToString());
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix2);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
            CellColors2.TrimExcess();
        }

        // This function visualizes the addition of matrix cell products
        private System.Text.StringBuilder VisualizeSum(int LevelCounter, bool SumComplete)
        {
            System.Text.StringBuilder SumVisualization = new System.Text.StringBuilder("");
            if (SumComplete)
            {
                LevelCounter--;
            }
            for (int i = 0; i < LevelCounter; i++)
            {
                if (i == 0 && i != LevelCounter - 1)
                {
                    SumVisualization.Append(String.Format(@"(( \b {0} \b0 ) * ( \b {1} \b0 )) +", matrix1.ContentsArray[RowCounter, i], matrix2.ContentsArray[i, ColCounter]));
                }
                else if (i > 0 && i + 1 != matrix1.ActualCols)
                {
                    SumVisualization.Append(String.Format(@" (( \b {0} \b0 ) * ( \b {1} \b0 )) +", matrix1.ContentsArray[RowCounter, i], matrix2.ContentsArray[i, ColCounter]));
                }
                else
                {
                    SumVisualization.Append(String.Format(@" (( \b {0} \b0 ) * ( \b {1} \b0 ))", matrix1.ContentsArray[RowCounter, i], matrix2.ContentsArray[i, ColCounter]));
                }
            }
            if (SumComplete)
            {
                SumVisualization.Append(String.Format(@" = \b {0} \b0 ", CellResult));
            }
            return SumVisualization;
        }
        private void TrackBarUpdate()
        {
            try
            {
                trackBar1.Value = Convert.ToInt32(ActualStep);
            }
            catch (Exception)
            {
            }
        }
    }
}
