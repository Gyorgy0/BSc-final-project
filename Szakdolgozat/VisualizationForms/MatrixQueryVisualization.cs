namespace Szakdolgozat
{
    public partial class MatrixQueryVisualization : Form
    {
        public MatrixQueryVisualization(Matrix ActualSelectedMatrix, long StepCounter)
        {
            InitializeComponent();
            this.matrix3Contents = ActualSelectedMatrix;
            this.matrix3 = new Matrix(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols, MatrixPicturebox3, Matrixpanel3Rows, Matrixpanel3Collumns);
            this.StepCounter = StepCounter;
            timer1.Interval = TimerInterval;

            TxtboxFont = new Font(this.Font, FontStyle.Regular);
        }
        Matrix? matrix3Contents;
        Matrix? matrix3;
        long StepCounter;
        long ActualStep;

        int TimerInterval = 1000;

        public double SearchValue;
        double MaxValue;
        double MinValue;

        TextBox SelectedTxtbox;
        int TxtboxWidth = 33;
        int TxtboxHeight = 26;
        Font TxtboxFont;

        bool RowNav;
        bool ColNav;

        int OddCounter;
        int EvenCounter;
        int Counter;

        double[,] ComparisonValues;
        Dictionary<string, Color> CellColors = new Dictionary<string, Color>();

        public EventHandler? ActualOperation;
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

        private void MatrixQueryVisualization_Load(object sender, EventArgs e)
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
            for (int i = 0; i < matrix3.ActualRows; i++)
            {
                for (int j = 0; j < matrix3.ActualCols; j++)
                {
                    matrix3.ContentsArray[i, j] = matrix3Contents.ContentsArray[i, j];
                }
            }
            ComparisonValues = new double[matrix3.ActualRows, matrix3.ActualCols];
            SelectedTxtbox = new TextBox
            {
                Tag = matrix3,
                Name = "0_0",
                Text = matrix3.ContentsArray[0, 0].ToString(),
                TextAlign = HorizontalAlignment.Right,
                Size = new Size(TxtboxWidth - 3, TxtboxHeight - 3),
                Location = new Point(((0 * TxtboxWidth) + (TxtboxWidth - matrix3.HorizontalScrollBar.Value) + 1), ((0 * TxtboxHeight) + (TxtboxHeight - matrix3.VerticalScrollBar.Value) + 1)),
                ReadOnly = true,
            };
            SelectedTxtbox.KeyDown += Keys_Down;
            SelectedTxtbox.KeyUp += Keys_Up;
            SelectedTxtbox.KeyPress += textBox1_KeyPress;
            matrix3.PanelPicturebox.Controls.Add(SelectedTxtbox);
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
            Matrixpanel3Rows.Paint += Row_Paint;
            Matrixpanel3Collumns.Paint += Collumn_Paint;
            Matrixpanel3ColScrollbar.Refresh();
            Matrixpanel3RowScrollbar.Refresh();
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
                if (i >= SelectedMatrix.HorizontalScrollBar.Value / TxtboxWidth && i <= ((SelectedMatrix.HorizontalScrollBar.Value + SelectedMatrix.MatrixPanel.Width) / TxtboxWidth) + 1)
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
                if (i >= SelectedMatrix.VerticalScrollBar.Value / TxtboxHeight && i <= ((SelectedMatrix.VerticalScrollBar.Value + SelectedMatrix.MatrixPanel.Height) / TxtboxHeight) + 1)
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

                if (CellColors.ContainsKey(SelectedTxtbox.Name))
                {
                    SelectedTxtbox.BackColor = (Color)CellColors[SelectedTxtbox.Name];
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
                        if (CellColors.ContainsKey(i.ToString() + "_" + j.ToString()))
                        {
                            BackColor = new SolidBrush((Color)(CellColors[i.ToString() + "_" + j.ToString()]));
                        }
                        else
                        {
                            BackColor = new SolidBrush(Color.White);
                        }
                        Rectangle Cell = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1), TxtboxWidth - 3, TxtboxHeight - 3);
                        Rectangle CellFill = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 2), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 2), TxtboxWidth - 4, TxtboxHeight - 4);
                        RectangleF ValueField = new RectangleF(CellFill.X, CellFill.Y + ((CellFill.Height - TxtboxFont.Height) / 2), CellFill.Width, TxtboxFont.Height);
                        e.Graphics.DrawRectangle(Border, Cell);
                        e.Graphics.FillRectangle(BackColor, CellFill);
                        e.Graphics.DrawString(SelectedMatrix.ContentsArray[i, j].ToString(), TxtboxFont, TextBrush, ValueField, TextStyle);
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

            if (SelectedMatrix.ActualRows != 0 && SelectedMatrix.ActualCols != 0)
            {
                SelectedTxtboxLocation = new int[2];
                if (((MousePosition.X - this.Location.X - SelectedMatrix.MatrixPanel.Location.X - (this.Width - this.ClientRectangle.Width) + 8) + SelectedMatrix.HorizontalScrollBar.Value) / (TxtboxWidth) < SelectedMatrix.ActualCols && ((MousePosition.Y - this.Location.Y - SelectedMatrix.MatrixPanel.Location.Y - (this.Height - this.ClientRectangle.Height) + 8) + SelectedMatrix.VerticalScrollBar.Value) / (TxtboxHeight) < SelectedMatrix.ActualRows)
                {
                    SelectedTxtboxLocation[0] = ((MousePosition.X - this.Location.X - SelectedMatrix.MatrixPanel.Location.X - (this.Width - this.ClientRectangle.Width) + 8) + SelectedMatrix.HorizontalScrollBar.Value) / (TxtboxWidth);
                    SelectedTxtboxLocation[1] = ((MousePosition.Y - this.Location.Y - SelectedMatrix.MatrixPanel.Location.Y - (this.Height - this.ClientRectangle.Height) + 8) + SelectedMatrix.VerticalScrollBar.Value) / (TxtboxHeight);
                }

                SelectedTxtbox.Name = SelectedTxtboxLocation[1].ToString() + "_" + SelectedTxtboxLocation[0].ToString();
                SelectedTxtbox.Text = SelectedMatrix.ContentsArray[SelectedTxtboxLocation[1], SelectedTxtboxLocation[0]].ToString();
                SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[0] * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[1] * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
                SelectedTxtbox.Focus();
            }
        }

        public void MaxSearchStep(object sender, EventArgs e)
        {
            if (ActualStep < StepCounter && !Reverse)
            {
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                }
                else if (ActualStep == 1)
                {
                    MaxValue = matrix3.ContentsArray[Row, Col];
                    ComparisonValues[Row, Col] = MaxValue;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Mivel a kiválasztott elem ( \b {0} \b0 ) az első vizsgált szám, ezért a következő számot ehhez fogjuk hasonlítani.", MaxValue), "}");
                    CellColors.Add(TxtBoxName, Color.LightYellow);
                }
                else if (ActualStep > 1)
                {
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját megvizsgáljuk, hogy nagyobb-e, mint a/az \b {2} \b0\line\line", (Row + 1), (Col + 1), MaxValue), "}");
                    if (MaxValue < matrix3.ContentsArray[Row, Col])
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b nagyobb \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MaxValue), "}");

                        MaxValue = matrix3.ContentsArray[Row, Col];
                        CellColors.Add(TxtBoxName, Color.LightGreen);
                    }
                    else if (MaxValue >= matrix3.ContentsArray[Row, Col])
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b kisebb, vagy egyenlő \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MaxValue), "}");
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    ComparisonValues[Row, Col] = MaxValue;
                }
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                CellColors.Remove(TxtBoxName);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                }
                else if (ActualStep == 1)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    MaxValue = matrix3.ContentsArray[Row, Col];
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Mivel a kiválasztott elem ( \b {0} \b0 ) az első vizsgált szám, ezért a következő számot ehhez fogjuk hasonlítani.", MaxValue), "}");
                }
                else if (ActualStep > 1)
                {
                    Row = Convert.ToInt32((ActualStep - 2) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 2) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    MaxValue = ComparisonValues[Row, Col];
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját megvizsgáljuk, hogy nagyobb-e, mint a/az \b {2} \b0\line\line", (Row + 1), (Col + 1), MaxValue), "}");
                    if (MaxValue < matrix3.ContentsArray[Row, Col])
                    {
                        Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                        Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                        TxtBoxName = String.Concat(Row, '_', Col);
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b nagyobb \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MaxValue), "}");
                    }
                    else if (MaxValue >= matrix3.ContentsArray[Row, Col])
                    {
                        Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                        Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                        TxtBoxName = String.Concat(Row, '_', Col);
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b kisebb, vagy egyenlő \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MaxValue), "}");
                    }
                }
                Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                TxtBoxName = String.Concat(Row, '_', Col);
                if (Row >= 0 && Col >= 0)
                {
                    MaxValue = ComparisonValues[Row, Col];
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix3);
            CellColors.TrimExcess();
        }

        public void MinSearchStep(object sender, EventArgs e)
        {
            if (ActualStep < StepCounter && !Reverse)
            {
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                }
                else if (ActualStep == 1)
                {
                    MinValue = matrix3.ContentsArray[Row, Col];
                    ComparisonValues[Row, Col] = MinValue;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Mivel a kiválasztott elem ( \b {0} \b0 ) az első vizsgált szám, ezért a következő számot ehhez fogjuk hasonlítani.", MinValue), "}");
                    CellColors.Add(TxtBoxName, Color.LightYellow);
                }
                else if (ActualStep > 1)
                {
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját megvizsgáljuk, hogy nagyobb-e, mint a/az \b {2} \b0\line\line", (Row + 1), (Col + 1), MinValue), "}");
                    if (MinValue > matrix3.ContentsArray[Row, Col])
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b kisebb \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MinValue), "}");

                        MinValue = matrix3.ContentsArray[Row, Col];
                        CellColors.Add(TxtBoxName, Color.LightGreen);
                    }
                    else if (MinValue <= matrix3.ContentsArray[Row, Col])
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b nagyobb, vagy egyenlő \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MinValue), "}");
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    ComparisonValues[Row, Col] = MinValue;
                }
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                CellColors.Remove(TxtBoxName);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                }
                else if (ActualStep == 1)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    MinValue = Convert.ToDouble(matrix3.ContentsArray[Row, Col]);
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Mivel a kiválasztott elem ( \b {0} \b0 ) az első vizsgált szám, ezért a következő számot ehhez fogjuk hasonlítani.", MinValue), "}");
                }
                else if (ActualStep > 1)
                {
                    Row = Convert.ToInt32((ActualStep - 2) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 2) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    MinValue = ComparisonValues[Row, Col];
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját megvizsgáljuk, hogy nagyobb-e, mint a/az \b {2} \b0\line\line", (Row + 1), (Col + 1), MinValue), "}");
                    if (MinValue > matrix3.ContentsArray[Row, Col])
                    {
                        Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                        Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                        TxtBoxName = String.Concat(Row, '_', Col);
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b kisebb \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MinValue), "}");
                    }
                    else if (MinValue <= matrix3.ContentsArray[Row, Col])
                    {
                        Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                        Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                        TxtBoxName = String.Concat(Row, '_', Col);
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A vizsgált szám ( \b {0} \b0 ) \b nagyobb, vagy egyenlő \b0 , mint a/az {1}", matrix3.ContentsArray[Row, Col], MinValue), "}");
                    }
                }
                Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                TxtBoxName = String.Concat(Row, '_', Col);
                if (Row >= 0 && Col >= 0)
                {
                    MinValue = ComparisonValues[Row, Col];
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix3);
            CellColors.TrimExcess();
        }

        public void OddCounterStep(object sender, EventArgs e)
        {
            if (ActualStep < StepCounter && !Reverse)
            {
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    OddCounter = 0;

                }
                else if (ActualStep > 0)
                {
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 1 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        OddCounter++;
                        CellColors.Add(TxtBoxName, Color.LightGreen);
                    }
                    else if (matrix3.ContentsArray[Row, Col] % 1 != 0)
                    {
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    else
                    {
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Páratlan számok: {0} \line\line", OddCounter), "}");
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 1 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b páratlan \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else if (matrix3.ContentsArray[Row, Col] % 1 != 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem egész szám \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem páratlan \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                }
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                CellColors.Remove(TxtBoxName);
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    OddCounter = 0;
                }
                else if (ActualStep > 0)
                {
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 1 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        OddCounter--;
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Páratlan számok: {0} \line\line", OddCounter), "}");
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 1 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b páratlan \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else if (matrix3.ContentsArray[Row, Col] % 1 != 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem egész szám \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem páratlan \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix3);
            CellColors.TrimExcess();
        }

        public void EvenCounterStep(object sender, EventArgs e)
        {
            if (ActualStep < StepCounter && !Reverse)
            {
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    EvenCounter = 0;

                }
                else if (ActualStep > 0)
                {
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 0 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        EvenCounter++;
                        CellColors.Add(TxtBoxName, Color.LightGreen);
                    }
                    else if (matrix3.ContentsArray[Row, Col] % 1 != 0)
                    {
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    else
                    {
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Páros számok: {0} \line\line", EvenCounter), "}");
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 0 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b páros \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else if (matrix3.ContentsArray[Row, Col] % 1 != 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem egész szám \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem páros \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                }
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                CellColors.Remove(TxtBoxName);
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    EvenCounter = 0;
                }
                else if (ActualStep > 0)
                {
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 0 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        EvenCounter--;
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Páros számok: {0} \line\line", EvenCounter), "}");
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    if (Math.Abs(matrix3.ContentsArray[Row, Col] % 2) == 0 && matrix3.ContentsArray[Row, Col] % 1 == 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b páros \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else if (matrix3.ContentsArray[Row, Col] % 1 != 0)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem egész szám \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem páros \b0 !", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix3);
            CellColors.TrimExcess();
        }

        public void CustomNumberSearchStep(object sender, EventArgs e)
        {
            if (ActualStep < StepCounter && !Reverse)
            {
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    Counter = 0;

                }
                else if (ActualStep > 0)
                {
                    if (matrix3.ContentsArray[Row, Col] == SearchValue)
                    {
                        Counter++;
                        CellColors.Add(TxtBoxName, Color.LightGreen);
                    }
                    else
                    {
                        CellColors.Add(TxtBoxName, Color.LightYellow);
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Keresett szám ( {0} ) találatai a mátrixban: {1} \line\line", SearchValue, Counter), "}");
                    if (matrix3.ContentsArray[Row, Col] == SearchValue)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b megegyzik \b0 a keresett számmal!", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem egyezik meg \b0 a keresett számmal!", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                }
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix3.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix3.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                CellColors.Remove(TxtBoxName);
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep == 0)
                {
                    StepDisplay.Rtf = "";
                    Counter = 0;
                }
                else if (ActualStep > 0)
                {
                    if (matrix3.ContentsArray[Row, Col] == SearchValue)
                    {
                        Counter--;
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi Keresett szám ( {0} ) találatai a mátrixban: {1} \line\line", SearchValue, Counter), "}");
                    Row = Convert.ToInt32((ActualStep - 1) / matrix3.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix3.ActualCols);
                    TxtBoxName = String.Concat(Row, '_', Col);
                    if (matrix3.ContentsArray[Row, Col] == SearchValue)
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b megegyzik \b0 a keresett számmal!", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                    else
                    {
                        StepDisplay.Select(StepDisplay.TextLength, 0);
                        StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 cellájában található szám ( {2} ) \b nem egyezik meg \b0 a keresett számmal!", (Row + 1), (Col + 1), matrix3.ContentsArray[Row, Col]), "}");
                    }
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix3);
            CellColors.TrimExcess();
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
