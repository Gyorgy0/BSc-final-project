namespace Szakdolgozat
{
    public partial class SingleMatrixVisualization : Form
    {
        public SingleMatrixVisualization(Matrix ActualSelectedMatrix, long StepCounter)
        {
            InitializeComponent();
            this.matrix1Contents = ActualSelectedMatrix;
            this.matrix1 = new Matrix(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols, MatrixPicturebox1, Matrixpanel1Rows, Matrixpanel1Collumns);
            this.StepCounter = StepCounter;
            this.matrix3 = new Matrix(0, 0, MatrixPicturebox3, Matrixpanel3Rows, Matrixpanel3Collumns);
            SortedRowValues = new double[matrix1.ActualCols];
            OriginalRowValues = new double[matrix1.ActualCols];
            SortedColValues = new double[matrix1.ActualRows];
            OriginalColValues = new double[matrix1.ActualRows];
            timer1.Interval = TimerInterval;

            TxtboxFont = new Font(this.Font, FontStyle.Regular);
        }
        Matrix? matrix1Contents;
        Matrix? matrix1, matrix3;
        long StepCounter;
        long ActualStep;

        int TimerInterval = 1000;

        public double Multiplicator;

        double[] SortedRowValues;
        double[] OriginalRowValues;
        double[] SortedColValues;
        double[] OriginalColValues;

        TextBox SelectedTxtbox;
        int TxtboxWidth = 33;
        int TxtboxHeight = 26;
        Font TxtboxFont;

        bool RowNav;
        bool ColNav;

        public bool VerticalPainting; // false - default, true - for Collumn sorting

        int RowPos;
        int ColPos = -1;

        Dictionary<string, Color> CellColors1 = new Dictionary<string, Color>();

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

        private void SingleMatrixVisualization_Load(object sender, EventArgs e)
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
            MatrixPanelRefresher(matrix1);
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
                        else
                        {
                            BackColor = new SolidBrush(Color.White);
                        }
                        Rectangle Cell = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1), TxtboxWidth - 3, TxtboxHeight - 3);
                        Rectangle CellFill = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 2), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 2), TxtboxWidth - 4, TxtboxHeight - 4);
                        RectangleF ValueField = new RectangleF(CellFill.X, CellFill.Y + ((CellFill.Height - TxtboxFont.Height) / 2), CellFill.Width, TxtboxFont.Height);
                        // Default: left to right painting of matrix3 cells
                        if (SelectedMatrix == matrix3 && (((i * matrix3.ActualCols) + j) <= ((RowPos * matrix3.ActualCols) + ColPos)) && !VerticalPainting)
                        {
                            e.Graphics.DrawRectangle(Border, Cell);
                            e.Graphics.FillRectangle(BackColor, CellFill);
                            e.Graphics.DrawString(matrix3.ContentsArray[i, j].ToString(), TxtboxFont, TextBrush, ValueField, TextStyle);
                        }
                        // Top to bottom painting of matrix3 cells
                        else if (SelectedMatrix == matrix3 && (((j * matrix3.ActualRows) + i) <= ((ColPos * matrix3.ActualRows) + RowPos)) && VerticalPainting)
                        {
                            Cell = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1), TxtboxWidth - 3, TxtboxHeight - 3);
                            CellFill = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 2), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 2), TxtboxWidth - 4, TxtboxHeight - 4);
                            ValueField = new RectangleF(CellFill.X, CellFill.Y + ((CellFill.Height - TxtboxFont.Height) / 2), CellFill.Width, TxtboxFont.Height);
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

        // This is the matrix transpose method that displays how it is done step-by-step
        public void TransposeStep(object sender, EventArgs e)
        {
            matrix3.ContentsArray = matrix3.ResizeArray(matrix1.ActualCols, matrix1.ActualRows);

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
                int Row = Convert.ToInt32(ActualStep / matrix1.ActualRows);
                int Col = Convert.ToInt32(ActualStep % matrix1.ActualRows);
                string TxtBoxName = String.Concat(Col, '_', Row);
                CellColors1.Add(TxtBoxName, Color.LightGreen);
                matrix3.ContentsArray[Row, Col] = matrix1.ContentsArray[Col, Row];
                RowPos = Row;
                ColPos = Col;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját áthelyezzük az új mátrix \b {1}. \b0 sorának \b {0}. \b0 oszlopába.", (Row + 1), (Col + 1)), "}");
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix1.ActualRows);
                int Col = Convert.ToInt32(ActualStep % matrix1.ActualRows);
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep > 0)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix1.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját áthelyezzük az új mátrix \b {1}. \b0 sorának \b {0}. \b0 oszlopába.", (Row + 1), (Col + 1)), "}");
                    Row = Convert.ToInt32(ActualStep / matrix1.ActualRows);
                    Col = Convert.ToInt32(ActualStep % matrix1.ActualRows);
                    string TxtBoxName = String.Concat(Col, '_', Row);
                    CellColors1.Remove(TxtBoxName);
                }

                else if (ActualStep == 0)
                {
                    Row = Convert.ToInt32(ActualStep / matrix1.ActualRows);
                    Col = Convert.ToInt32(ActualStep % matrix1.ActualRows);
                    string TxtBoxName = String.Concat(Col, '_', Row);
                    ColPos = -1;
                    CellColors1.Remove(TxtBoxName);
                    StepDisplay.Rtf = "";
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }

        // This is the matrix horizontal mirroring method that displays how it is done step-by-step
        public void HorizontalMirroringStep(object sender, EventArgs e)
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
                CellColors1.Add(Row.ToString() + "_" + Math.Abs(Col - (matrix1.ActualCols - 1)).ToString(), Color.LightGreen);
                matrix3.ContentsArray[Row, Col] = matrix1.ContentsArray[Row, Math.Abs(Col - (matrix1.ActualCols - 1))];
                RowPos = Row;
                ColPos = Col;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", (Row + 1), (Math.Abs(Col - (matrix1.ActualCols - 1)) + 1), (Row + 1), (Col + 1)), "}");
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep > 0)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix1.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", (Row + 1), (Math.Abs(Col - (matrix1.ActualCols - 1)) + 1), (Row + 1), (Col + 1)), "}");
                }
                else if (ActualStep == 0)
                {
                    ColPos = -1;
                    StepDisplay.Rtf = "";
                }
                Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                CellColors1.Remove(Row.ToString() + "_" + Math.Abs(Col - (matrix1.ActualCols - 1)).ToString());
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }
        // This is the matrix vertical mirroring method that displays how it is done step-by-step
        public void VerticalMirroringStep(object sender, EventArgs e)
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
                CellColors1.Add(Math.Abs(Row - (matrix1.ActualRows - 1)).ToString() + "_" + Col.ToString(), Color.LightGreen);
                matrix3.ContentsArray[Row, Col] = matrix1.ContentsArray[Math.Abs(Row - (matrix1.ActualRows - 1)), Col];
                RowPos = Row;
                ColPos = Col;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", (Math.Abs(Row - (matrix1.ActualRows - 1)) + 1), (Col + 1), (Row + 1), (Col + 1)), "}");
            }
            if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep > 0)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix1.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", (Math.Abs(Row - (matrix1.ActualRows - 1)) + 1), (Col + 1), (Row + 1), (Col + 1)), "}");
                }
                else if (ActualStep == 0)
                {
                    ColPos = -1;
                    StepDisplay.Rtf = "";
                }
                Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                CellColors1.Remove(Math.Abs(Row - (matrix1.ActualRows - 1)).ToString() + "_" + Col.ToString());
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }
        int CellNavCounter = 0; // This is for counting the sort steps - after every sort we subtract the CellNavCounter from the ActualStep, so that the locations of rows and collumns doesn't drift
        int CellCounter = 0; // Counts the cells of the matrix
        int SortCounter = 0; // This is for counting sort steps - every second sort step must be skipped
        //This is the method, that sort a matrix by it's rows in ascending order
        public void RowSortingStep(object sender, EventArgs e)
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
            // StepCounter = (number of cells * 2) + number of rows (sorting step)
            if (ActualStep < StepCounter && !Reverse)
            {
                // Sorting
                if (SortCounter == 0 && CellCounter == matrix1.ActualCols)
                {
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalRowValues, matrix1.ActualCols - 1, false)), "}");
                    Array.Sort(SortedRowValues);
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit növekvő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedRowValues, matrix1.ActualCols - 1, false)), "}");
                    CellNavCounter++;
                    SortCounter++;
                    CellCounter = -1;
                }
                else if (SortCounter == 1 && CellCounter == matrix1.ActualCols)
                {
                    SortCounter = 0;
                    CellCounter = 0;
                }
                int Row = Convert.ToInt32(((ActualStep - CellNavCounter) / matrix1.ActualCols) - CellNavCounter);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                // matrix1 cell management
                if (SortCounter == 0 && CellCounter != -1)
                {
                    CellColors1.Add(TxtBoxName, Color.LightYellow);
                    SortedRowValues[Col] = matrix1.ContentsArray[Row, Col];
                    OriginalRowValues[Col] = matrix1.ContentsArray[Row, Col];
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalRowValues, Col, false)), "}");
                }
                // matrix3 cell management
                else if (SortCounter == 1 && CellCounter != -1)
                {
                    matrix3.ContentsArray[Row, Col] = SortedRowValues[Col];
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedRowValues, Math.Abs(Col - (matrix1.ActualCols - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                }
                CellCounter++;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                int Row = Convert.ToInt32(((ActualStep - CellNavCounter) / matrix1.ActualCols) - CellNavCounter);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                if (SortCounter == 1 && CellCounter == 1)
                {
                    Row = Convert.ToInt32(((ActualStep - CellNavCounter - 1) / matrix1.ActualCols) - CellNavCounter);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalRowValues, matrix1.ActualCols - 1, false)), "}");
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit növekvő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedRowValues, matrix1.ActualCols - 1, false)), "}");
                    CellCounter = 0;
                }
                else if (SortCounter == 1 && CellCounter == 0)
                {
                    CellNavCounter--;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1 + CellNavCounter), (Col + 1), (Col + 1), ArrayToString(OriginalRowValues, Col, false)), "}");
                    CellCounter = matrix1.ActualCols;
                    SortCounter = 0;
                }
                else if (SortCounter == 0 && CellCounter == 1)
                {
                    CellColors1.Remove(TxtBoxName);
                    Row = Convert.ToInt32(((ActualStep - CellNavCounter - 1) / matrix1.ActualCols) - CellNavCounter);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualCols);
                    for (int i = 0; i < matrix1.ActualCols; i++)
                    {
                        OriginalRowValues[i] = matrix1.ContentsArray[Row, i];
                        SortedRowValues[i] = matrix3.ContentsArray[Row, i];
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedRowValues, Math.Abs(Col - (matrix1.ActualCols - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter = matrix1.ActualCols;
                    SortCounter = 1;
                }
                else if (SortCounter == 0 && CellCounter > 0)
                {
                    // matrix1 cell management
                    CellColors1.Remove(TxtBoxName);
                    OriginalRowValues[Col] = matrix1.ContentsArray[Row, Col];
                    SortedRowValues[Col] = matrix3.ContentsArray[Row, Col];
                    Col -= 1;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalRowValues, Col, false)), "}");
                    CellCounter--;
                }
                else if (SortCounter == 1 && CellCounter > 1)
                {
                    // matrix3 cell management
                    Row = Convert.ToInt32(((ActualStep - CellNavCounter - 1) / matrix1.ActualCols) - CellNavCounter);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedRowValues, Math.Abs(Col - (matrix1.ActualCols - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter--;
                }
                if (ActualStep == 0)
                {
                    CellCounter = 0;
                    SortCounter = 0;
                    CellNavCounter = 0;
                    ColPos = -1;
                    StepDisplay.Rtf = "";
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }
        //This is the method, that sort a matrix by it's collumns in ascending order
        public void ColSortingStep(object sender, EventArgs e)
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
            // StepCounter = (number of cells * 2) + number of rows (sorting step)
            if (ActualStep < StepCounter && !Reverse)
            {
                // Sorting
                if (SortCounter == 0 && CellCounter == matrix1.ActualRows)
                {
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalColValues, matrix1.ActualRows - 1, false)), "}");
                    Array.Sort(SortedColValues);
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit növekvő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedColValues, matrix1.ActualRows - 1, false)), "}");
                    CellNavCounter++;
                    SortCounter++;
                    CellCounter = -1;
                }
                else if (SortCounter == 1 && CellCounter == matrix1.ActualRows)
                {
                    SortCounter = 0;
                    CellCounter = 0;
                }
                int Row = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualRows);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) / matrix1.ActualRows - CellNavCounter);
                string TxtBoxName = String.Concat(Row, '_', Col);
                // matrix1 cell management
                if (SortCounter == 0 && CellCounter != -1)
                {
                    CellColors1.Add(TxtBoxName, Color.LightYellow);
                    SortedColValues[Row] = matrix1.ContentsArray[Row, Col];
                    OriginalColValues[Row] = matrix1.ContentsArray[Row, Col];
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalColValues, Row, false)), "}");
                }
                // matrix3 cell management
                else if (SortCounter == 1 && CellCounter != -1)
                {
                    matrix3.ContentsArray[Row, Col] = SortedColValues[Row];
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedColValues, Math.Abs(Row - (matrix1.ActualRows - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                }
                CellCounter++;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                int Row = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualRows);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) / matrix1.ActualRows - CellNavCounter);
                string TxtBoxName = String.Concat(Row, '_', Col);
                if (SortCounter == 1 && CellCounter == 1)
                {
                    Row = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualRows);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) / matrix1.ActualRows - CellNavCounter);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalColValues, matrix1.ActualRows - 1, false)), "}");
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit növekvő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedColValues, matrix1.ActualRows - 1, false)), "}");
                    CellCounter = 0;
                }
                else if (SortCounter == 1 && CellCounter == 0)
                {
                    CellNavCounter--;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1 + CellNavCounter), (Col + 1), (Col + 1), ArrayToString(OriginalColValues, Row, false)), "}");
                    CellCounter = matrix1.ActualRows;
                    SortCounter = 0;
                }
                else if (SortCounter == 0 && CellCounter == 1)
                {
                    CellColors1.Remove(TxtBoxName);
                    Row = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualRows);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) / matrix1.ActualRows - CellNavCounter);
                    for (int i = 0; i < matrix1.ActualRows; i++)
                    {
                        OriginalColValues[i] = matrix1.ContentsArray[i, Col];
                        SortedColValues[i] = matrix3.ContentsArray[i, Col];
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedColValues, Math.Abs(Row - (matrix1.ActualRows - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter = matrix1.ActualRows;
                    SortCounter = 1;
                }
                else if (SortCounter == 0 && CellCounter > 0)
                {
                    // matrix1 cell management
                    CellColors1.Remove(TxtBoxName);
                    OriginalColValues[Row] = matrix1.ContentsArray[Row, Col];
                    SortedColValues[Row] = matrix3.ContentsArray[Row, Col];
                    Row -= 1;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalColValues, Row, false)), "}");
                    CellCounter--;
                }
                else if (SortCounter == 1 && CellCounter > 1)
                {
                    // matrix3 cell management
                    Row = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualRows);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) / matrix1.ActualRows - CellNavCounter);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedColValues, Math.Abs(Row - (matrix1.ActualRows - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter--;
                }
                if (ActualStep == 0)
                {
                    CellCounter = 0;
                    SortCounter = 0;
                    CellNavCounter = 0;
                    RowPos = -1;
                    StepDisplay.Rtf = "";
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }

        //This is the method, that sorts a matrix by it's rows in descending order
        public void ReverseRowSortingStep(object sender, EventArgs e)
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
            // StepCounter = (number of cells * 2) + number of rows (sorting step)
            if (ActualStep < StepCounter && !Reverse)
            {
                // Sorting
                if (SortCounter == 0 && CellCounter == matrix1.ActualCols)
                {
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalRowValues, matrix1.ActualCols - 1, false)), "}");
                    Array.Sort(SortedRowValues);
                    Array.Reverse(SortedRowValues);
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit csökkenő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedRowValues, matrix1.ActualCols - 1, false)), "}");
                    CellNavCounter++;
                    SortCounter++;
                    CellCounter = -1;
                }
                else if (SortCounter == 1 && CellCounter == matrix1.ActualCols)
                {
                    SortCounter = 0;
                    CellCounter = 0;
                }
                int Row = Convert.ToInt32(((ActualStep - CellNavCounter) / matrix1.ActualCols) - CellNavCounter);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                // matrix1 cell management
                if (SortCounter == 0 && CellCounter != -1)
                {
                    CellColors1.Add(TxtBoxName, Color.LightYellow);
                    SortedRowValues[Col] = matrix1.ContentsArray[Row, Col];
                    OriginalRowValues[Col] = matrix1.ContentsArray[Row, Col];
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalRowValues, Col, false)), "}");
                }
                // matrix3 cell management
                else if (SortCounter == 1 && CellCounter != -1)
                {
                    matrix3.ContentsArray[Row, Col] = SortedRowValues[Col];
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedRowValues, Math.Abs(Col - (matrix1.ActualCols - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                }
                CellCounter++;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                int Row = Convert.ToInt32(((ActualStep - CellNavCounter) / matrix1.ActualCols) - CellNavCounter);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                if (SortCounter == 1 && CellCounter == 1)
                {
                    Row = Convert.ToInt32(((ActualStep - CellNavCounter - 1) / matrix1.ActualCols) - CellNavCounter);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalRowValues, matrix1.ActualCols - 1, false)), "}");
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit csökkenő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedRowValues, matrix1.ActualCols - 1, false)), "}");
                    CellCounter = 0;
                }
                else if (SortCounter == 1 && CellCounter == 0)
                {
                    CellNavCounter--;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1 + CellNavCounter), (Col + 1), (Col + 1), ArrayToString(OriginalRowValues, Col, false)), "}");
                    CellCounter = matrix1.ActualCols;
                    SortCounter = 0;
                }
                else if (SortCounter == 0 && CellCounter == 1)
                {
                    CellColors1.Remove(TxtBoxName);
                    Row = Convert.ToInt32(((ActualStep - CellNavCounter - 1) / matrix1.ActualCols) - CellNavCounter);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualCols);
                    for (int i = 0; i < matrix1.ActualCols; i++)
                    {
                        OriginalRowValues[i] = matrix1.ContentsArray[Row, i];
                        SortedRowValues[i] = matrix3.ContentsArray[Row, i];
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedRowValues, Math.Abs(Col - (matrix1.ActualCols - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter = matrix1.ActualCols;
                    SortCounter = 1;
                }
                else if (SortCounter == 0 && CellCounter > 0)
                {
                    // matrix1 cell management
                    CellColors1.Remove(TxtBoxName);
                    OriginalRowValues[Col] = matrix1.ContentsArray[Row, Col];
                    SortedRowValues[Col] = matrix3.ContentsArray[Row, Col];
                    Col -= 1;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalRowValues, Col, false)), "}");
                    CellCounter--;
                }
                else if (SortCounter == 1 && CellCounter > 1)
                {
                    // matrix3 cell management
                    Row = Convert.ToInt32(((ActualStep - CellNavCounter - 1) / matrix1.ActualCols) - CellNavCounter);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedRowValues, Math.Abs(Col - (matrix1.ActualCols - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter--;
                }
                if (ActualStep == 0)
                {
                    CellCounter = 0;
                    SortCounter = 0;
                    CellNavCounter = 0;
                    ColPos = -1;
                    StepDisplay.Rtf = "";
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }
        //This is the method, that sorts a matrix by it's collumns in descending order
        public void ReverseColSortingStep(object sender, EventArgs e)
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
            // StepCounter = (number of cells * 2) + number of rows (sorting step)
            if (ActualStep < StepCounter && !Reverse)
            {
                // Sorting
                if (SortCounter == 0 && CellCounter == matrix1.ActualRows)
                {
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalColValues, matrix1.ActualRows - 1, false)), "}");
                    Array.Sort(SortedColValues);
                    Array.Reverse(SortedColValues);
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit csökkenő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedColValues, matrix1.ActualRows - 1, false)), "}");
                    CellNavCounter++;
                    SortCounter++;
                    CellCounter = -1;
                }
                else if (SortCounter == 1 && CellCounter == matrix1.ActualRows)
                {
                    SortCounter = 0;
                    CellCounter = 0;
                }
                int Row = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualRows);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) / matrix1.ActualRows - CellNavCounter);
                string TxtBoxName = String.Concat(Row, '_', Col);
                // matrix1 cell management
                if (SortCounter == 0 && CellCounter != -1)
                {
                    CellColors1.Add(TxtBoxName, Color.LightYellow);
                    SortedColValues[Row] = matrix1.ContentsArray[Row, Col];
                    OriginalColValues[Row] = matrix1.ContentsArray[Row, Col];
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalColValues, Row, false)), "}");
                }
                // matrix3 cell management
                else if (SortCounter == 1 && CellCounter != -1)
                {
                    matrix3.ContentsArray[Row, Col] = SortedColValues[Row];
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedColValues, Math.Abs(Row - (matrix1.ActualRows - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                }
                CellCounter++;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                int Row = Convert.ToInt32((ActualStep - CellNavCounter) % matrix1.ActualRows);
                int Col = Convert.ToInt32((ActualStep - CellNavCounter) / matrix1.ActualRows - CellNavCounter);
                string TxtBoxName = String.Concat(Row, '_', Col);
                if (SortCounter == 1 && CellCounter == 1)
                {
                    Row = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualRows);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) / matrix1.ActualRows - CellNavCounter);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par", ArrayToString(OriginalColValues, matrix1.ActualRows - 1, false)), "}");
                    StepDisplay.Select(StepDisplay.TextLength, 0);
                    StepDisplay.SelectedRtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\line\line\ql\b0 Ezután a fenti tömb elemeit csökkenő sorrendbe helyezzük, így az alábbi tömböt kapjuk: \line\line\pard\qc\b {0}\b0\par", ArrayToString(SortedColValues, matrix1.ActualRows - 1, false)), "}");
                    CellCounter = 0;
                }
                else if (SortCounter == 1 && CellCounter == 0)
                {
                    CellNavCounter--;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1 + CellNavCounter), (Col + 1), (Col + 1), ArrayToString(OriginalColValues, Row, false)), "}");
                    CellCounter = matrix1.ActualRows;
                    SortCounter = 0;
                }
                else if (SortCounter == 0 && CellCounter == 1)
                {
                    CellColors1.Remove(TxtBoxName);
                    Row = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualRows);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) / matrix1.ActualRows - CellNavCounter);
                    for (int i = 0; i < matrix1.ActualRows; i++)
                    {
                        OriginalColValues[i] = matrix1.ContentsArray[i, Col];
                        SortedColValues[i] = matrix3.ContentsArray[i, Col];
                    }
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedColValues, Math.Abs(Row - (matrix1.ActualRows - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter = matrix1.ActualRows;
                    SortCounter = 1;
                }
                else if (SortCounter == 0 && CellCounter > 0)
                {
                    // matrix1 cell management
                    CellColors1.Remove(TxtBoxName);
                    OriginalColValues[Row] = matrix1.ContentsArray[Row, Col];
                    SortedColValues[Row] = matrix3.ContentsArray[Row, Col];
                    Row -= 1;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\pard\ql A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját behelyezzük ideiglenesen az alábbi tömb \b {2}. \b0 rekeszébe:\par\line\pard\qc\b{3}\b0\par", (Row + 1), (Col + 1), (Col + 1), ArrayToString(OriginalColValues, Row, false)), "}");
                    CellCounter--;
                }
                else if (SortCounter == 1 && CellCounter > 1)
                {
                    // matrix3 cell management
                    Row = Convert.ToInt32((ActualStep - CellNavCounter - 1) % matrix1.ActualRows);
                    Col = Convert.ToInt32((ActualStep - CellNavCounter - 1) / matrix1.ActualRows - CellNavCounter);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi\line\pard\qc\b{0}\b0 \par\ql\line\line Az aktuális rendezett tömb \b {1}. \b0 rekeszéből áthelyezzük az új mátrix \b {2}. \b0 sorának \b {3}. \b0 oszlopába.", ArrayToString(SortedColValues, Math.Abs(Row - (matrix1.ActualRows - 1)), true), (Col + 1), (Row + 1), (Col + 1)), "}");
                    CellCounter--;
                }
                if (ActualStep == 0)
                {
                    CellCounter = 0;
                    SortCounter = 0;
                    CellNavCounter = 0;
                    RowPos = -1;
                    StepDisplay.Rtf = "";
                }
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }

        // Matrix multiplication by number step
        public void MultiplicationByNumberStep(object sender, EventArgs e)
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
                double Value;
                try
                {
                    Value = Convert.ToDouble(Convert.ToDecimal((Decimal)Multiplicator * (Decimal)matrix1.ContentsArray[Row, Col]));
                }
                catch (Exception)
                {
                    Value = Multiplicator * matrix1.ContentsArray[Row, Col];
                }
                if (Value == -0)
                {
                    Value = 0;
                }
                CellColors1.Add(TxtBoxName, Color.LightGreen);
                matrix3.ContentsArray[Row, Col] = Value;
                RowPos = Row;
                ColPos = Col;
                ActualStep++;
                TrackBarUpdate();
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját beszorozzuk a/az \b {2} \b0 értékkel, majd az így kapott értéket (  \b {3} \b0 ) berakjuk a másik mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopába.", (Row + 1), (Col + 1), Multiplicator, matrix3.ContentsArray[Row, Col]), "}");
            }
            else if (ActualStep > 0 && Reverse)
            {
                ActualStep--;
                TrackBarUpdate();
                int Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                int Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                StepDisplayTitle.Text = String.Format("Aktuális lépés ({0}/{1})", ActualStep, StepCounter);
                if (ActualStep > 0)
                {
                    Row = Convert.ToInt32((ActualStep - 1) / matrix1.ActualCols);
                    Col = Convert.ToInt32((ActualStep - 1) % matrix1.ActualCols);
                    RowPos = Row;
                    ColPos = Col;
                    StepDisplay.Rtf = String.Concat("{", String.Format(@"\rtf\ansi A kiválasztott mátrix \b {0}. \b0 sorának \b {1}. \b0 celláját beszorozzuk a/az \b {2} \b0 értékkel, majd az így kapott értéket (  \b {3} \b0 ) berakjuk a másik mátrix \b {0}. \b0 sorának \b {1}. \b0 oszlopába.", (Row + 1), (Col + 1), Multiplicator, matrix3.ContentsArray[Row, Col]), "}");
                }
                else if (ActualStep == 0)
                {
                    ColPos = -1;
                    StepDisplay.Rtf = "";
                }
                Row = Convert.ToInt32(ActualStep / matrix1.ActualCols);
                Col = Convert.ToInt32(ActualStep % matrix1.ActualCols);
                string TxtBoxName = String.Concat(Row, '_', Col);
                CellColors1.Remove(TxtBoxName);
            }
            else
            {
                TimerStop();
            }
            MatrixPanelRefresher(matrix1);
            MatrixPanelRefresher(matrix3);
            CellColors1.TrimExcess();
        }

        // This is a function that displays an array as a string, it can reveal each number one-by-one
        /* [ 1, _, _, _ ]
         * [ 1, 2, _, _ ]
         * [ 1, 2, 3, _ ]
         * [ 1, 2, 3, 4 ]
         * 
         * Reverse (same array):
         * [ _, _, _, 4 ]
         * [ _, _, 3, 4 ]
         * [ _, 2, 3, 4 ]
         * [ 1, 2, 3, 4 ]
         *
         */
        private System.Text.StringBuilder ArrayToString(double[] Array, int Col, bool isReverse)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder("[ ");
            if (!isReverse)
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    if (i == Array.Length - 1 && i != Col)
                    {
                        result.Append("_");
                    }
                    else if (i == Array.Length - 1 && i == Col)
                    {
                        result.Append(Array[i].ToString());
                    }
                    else if (i <= Col)
                    {
                        result.Append(String.Format("{0}; ", Array[i]));
                    }
                    else
                    {
                        result.Append("_; ");
                    }

                }
            }
            else if (isReverse)
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    if (i == Array.Length - 1 && Col == 0)
                    {
                        result.Append("_");
                    }
                    else if (i == Array.Length - 1 && Col > 0)
                    {
                        result.Append(Array[i].ToString());
                    }
                    else if (i > Math.Abs((Array.Length) - Col) - 1)
                    {
                        result.Append(String.Format("{0}; ", Array[i]));
                    }
                    else
                    {
                        result.Append("_; ");
                    }

                }
            }
            result.Append(" ]");
            return result;
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
