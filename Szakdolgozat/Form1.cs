using System.Text;
using Szakdolgozat.DialogForms;
using Szakdolgozat.HelpForms;

namespace Szakdolgozat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            TxtboxFont = new Font(this.Font, FontStyle.Regular);
            InitializeComponent();
            this.DoubleBuffered = true;
            matrix1 = new Matrix(1, 1, MatrixPicturebox1, Matrixpanel1Rows, Matrixpanel1Collumns);
            matrix2 = new Matrix(1, 1, MatrixPicturebox2, Matrixpanel2Rows, Matrixpanel2Collumns);
            matrix3 = new Matrix(0, 0, MatrixPicturebox3, Matrixpanel3Rows, Matrixpanel3Collumns);

            matrix1_Import.Tag = matrix1;
            matrix2_Import.Tag = matrix2;

            matrix1_Export.Tag = matrix1;
            matrix2_Export.Tag = matrix2;
            matrix3_Export.Tag = matrix3;


        }
        Matrix matrix1;
        Matrix matrix2;
        Matrix matrix3;

        int TxtboxWidth = 33;
        int TxtboxHeight;
        Font TxtboxFont;

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectedTxtbox = new TextBox
            {
                Tag = (Matrix)matrix1,
                Name = "0_0",
                Text = matrix1.ContentsArray[0, 0].ToString(),
                TextAlign = HorizontalAlignment.Right,
                Size = new Size(TxtboxWidth - 3, 1),
            };
            SelectedTxtbox.KeyPress += textBox1_KeyPress;
            SelectedTxtbox.KeyDown += Keys_Down;
            SelectedTxtbox.KeyUp += Keys_Up;
            SelectedTxtbox.MouseWheel += SelectedTxtbox_MouseWheel;
            matrix1.PanelPicturebox.Controls.Add(SelectedTxtbox);
            TxtboxHeight = SelectedTxtbox.Height + 3;
            SelectedTxtbox.Location = new Point(((0 * TxtboxWidth) + (TxtboxWidth - matrix1.HorizontalScrollBar.Value) + 1), ((0 * TxtboxHeight) + (TxtboxHeight - matrix1.VerticalScrollBar.Value) + 1));

            MatrixPanelResize(matrix1, 1, 1);
            MatrixPanelResize(matrix2, 1, 1);
            MatrixPanelResize(matrix3, 0, 0);
            ActualSelectedMatrix = matrix1;
            ActualSelectedMatrix.MatrixPanel.BorderStyle = BorderStyle.FixedSingle;
        }
        private void MatrixPanelResize(Matrix SelectedMatrix, int NewRowSize, int NewColSize)
        {
            SelectedMatrix.RowPicturebox.Location = new Point(1, -TxtboxHeight);
            SelectedMatrix.ColPicturebox.Location = new Point(-TxtboxWidth, 1);
            int ActualRows = SelectedMatrix.ActualRows;
            int ActualCols = SelectedMatrix.ActualCols;

            if (SelectedMatrix == (Matrix)SelectedTxtbox.Tag)
            {
                int[] SelectedTxtboxLocation = new int[2];
                string[] SelectedTxtboxName = SelectedTxtbox.Name.Split('_', 2);
                SelectedTxtboxLocation[0] = 0;
                SelectedTxtboxLocation[1] = 0;

                SelectedTxtbox.Name = SelectedTxtboxLocation[1].ToString() + "_" + SelectedTxtboxLocation[0].ToString();
                SelectedTxtbox.Text = SelectedMatrix.ContentsArray[SelectedTxtboxLocation[1], SelectedTxtboxLocation[0]].ToString();
                SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[0] * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[1] * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
            }

            SelectedMatrix.ContentsArray = SelectedMatrix.ResizeArray(NewRowSize, NewColSize);

            if ((NewRowSize * TxtboxHeight) < SelectedMatrix.VerticalScrollBar.Height || NewRowSize == 0)
            {
                SelectedMatrix.VerticalScrollBar.Value = 0;
                SelectedMatrix.VerticalScrollBar.Hide();
            }
            else
            {
                SelectedMatrix.VerticalScrollBar.Show();
                SelectedMatrix.VerticalScrollBar.Maximum = (NewRowSize * TxtboxHeight) - (SelectedMatrix.RowScrollbar.Height) + 10;
                SelectedMatrix.VerticalScrollBar.SmallChange = TxtboxHeight;
            }
            if ((NewColSize * TxtboxWidth) < SelectedMatrix.HorizontalScrollBar.Width || NewColSize == 0)
            {
                SelectedMatrix.HorizontalScrollBar.Value = 0;
                SelectedMatrix.HorizontalScrollBar.Hide();
            }
            else
            {
                SelectedMatrix.HorizontalScrollBar.Show();
                SelectedMatrix.HorizontalScrollBar.Maximum = (NewColSize * TxtboxWidth) - (SelectedMatrix.ColScrollbar.Width) + 10;
                SelectedMatrix.HorizontalScrollBar.SmallChange = TxtboxWidth;
            }
            MatrixPanelRefresher(SelectedMatrix);
            GC.Collect();
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

            label2.Text = "Sorok száma: " + matrix1.ActualRows.ToString();
            label1.Text = "Oszlopok száma: " + matrix1.ActualCols.ToString();
            label4.Text = "Sorok száma: " + matrix2.ActualRows.ToString();
            label3.Text = "Oszlopok száma: " + matrix2.ActualCols.ToString();
        }

        int RowPos;
        int ColPos;

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

        private void MatrixPicturebox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox Sender = (PictureBox)sender;
            Matrix SelectedMatrix = (Matrix)Sender.Tag;

            Pen Border = new Pen(Color.Gray);
            Brush BackColor = new SolidBrush(Color.White);
            Brush TextBrush = new SolidBrush(Color.Black);
            StringFormat TextStyle = new StringFormat();
            TextStyle.Alignment = StringAlignment.Far;
            TextStyle.LineAlignment = StringAlignment.Center;
            TextStyle.Trimming = StringTrimming.Character;

            if ((Matrix)SelectedTxtbox.Tag == SelectedMatrix)
            {
                int[] SelectedTxtboxLocation = new int[2];
                string[] SelectedTxtboxName = SelectedTxtbox.Name.Split('_', 2);
                SelectedTxtboxLocation[0] = Convert.ToInt32(SelectedTxtboxName[0]);
                SelectedTxtboxLocation[1] = Convert.ToInt32(SelectedTxtboxName[1]);

                SelectedTxtbox.Location = new Point(((SelectedTxtboxLocation[1] * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((SelectedTxtboxLocation[0] * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
            }
            for (int i = SelectedMatrix.VerticalScrollBar.Value / TxtboxHeight; i < ((SelectedMatrix.VerticalScrollBar.Value + SelectedMatrix.MatrixPanel.Height) / TxtboxHeight) + 1; i++)
            {
                for (int j = SelectedMatrix.HorizontalScrollBar.Value / TxtboxWidth; j < ((SelectedMatrix.HorizontalScrollBar.Value + SelectedMatrix.MatrixPanel.Width) / TxtboxWidth) + 1; j++)
                {
                    if (i < SelectedMatrix.ActualRows && j < SelectedMatrix.ActualCols)
                    {
                        Rectangle Cell = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1), TxtboxWidth - 3, TxtboxHeight - 3);
                        Rectangle CellFill = new Rectangle(((j * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 2), ((i * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 2), TxtboxWidth - 4, TxtboxHeight - 4);
                        RectangleF ValueField = new RectangleF(Cell.X, Cell.Y + ((Cell.Height - TxtboxFont.Height) / 2), Cell.Width, TxtboxFont.Height);
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
            if (!RowNav && !ColNav && SelectedMatrix.VerticalScrollBar.Visible && SelectedMatrix.VerticalScrollBar.Value - (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.VerticalScrollBar.SmallChange) <= SelectedMatrix.VerticalScrollBar.Maximum && SelectedMatrix.VerticalScrollBar.Value >= (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.VerticalScrollBar.SmallChange))
            {
                SelectedMatrix.VerticalScrollBar.Value += -(e.Delta / Math.Abs(e.Delta) * SelectedMatrix.VerticalScrollBar.SmallChange);
            }
            if (!RowNav && !ColNav && SelectedMatrix.HorizontalScrollBar.Visible && !SelectedMatrix.VerticalScrollBar.Visible && SelectedMatrix.HorizontalScrollBar.Value - (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.HorizontalScrollBar.SmallChange) <= SelectedMatrix.HorizontalScrollBar.Maximum && SelectedMatrix.HorizontalScrollBar.Value >= (e.Delta / Math.Abs(e.Delta) * SelectedMatrix.HorizontalScrollBar.SmallChange))
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
            this.ActiveControl = SelectedTxtbox;

            if (SelectedMatrix != ActualSelectedMatrix && SelectedMatrix != matrix3 && (Matrix)SelectedTxtbox.Tag != matrix3)
            {
                Matrix PreviousMatrix = (Matrix)SelectedTxtbox.Tag;
                StoreValue(PreviousMatrix, SelectedTxtbox);
                PreviousMatrix.MatrixPanel.BorderStyle = BorderStyle.None;

                PreviousMatrix.PanelPicturebox.Controls.Remove(SelectedTxtbox);

                SelectedMatrix.MatrixPanel.BorderStyle = BorderStyle.FixedSingle;
                SelectedMatrix.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = SelectedMatrix;
                ActualSelectedMatrix = SelectedMatrix;
                SelectedTxtbox.Focus();

            }
            else if (ActualSelectedMatrix == SelectedMatrix && SelectedMatrix != matrix3 && (Matrix)SelectedTxtbox.Tag != matrix3)
            {
                StoreValue(SelectedMatrix, SelectedTxtbox);
            }
            else if (SelectedMatrix == matrix3 && (Matrix)SelectedTxtbox.Tag != matrix3 && matrix3.ActualRows != 0 && matrix3.ActualCols != 0)
            {
                Matrix PreviousMatrix = (Matrix)SelectedTxtbox.Tag;
                StoreValue(PreviousMatrix, SelectedTxtbox);

                PreviousMatrix.PanelPicturebox.Controls.Remove(SelectedTxtbox);

                SelectedMatrix.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = SelectedMatrix;
                SelectedTxtbox.ReadOnly = true;
                SelectedTxtbox.BackColor = Color.White;
                SelectedTxtbox.KeyPress -= textBox1_KeyPress;
                SelectedTxtbox.KeyUp -= Keys_Up;
                SelectedTxtbox.KeyDown -= Keys_Down;
                SelectedTxtbox.Focus();

            }
            else if ((Matrix)SelectedTxtbox.Tag == matrix3 && SelectedMatrix != matrix3)
            {
                Matrix PreviousMatrix = (Matrix)SelectedTxtbox.Tag;
                matrix1.MatrixPanel.BorderStyle = BorderStyle.None;
                matrix2.MatrixPanel.BorderStyle = BorderStyle.None;

                PreviousMatrix.PanelPicturebox.Controls.Remove(SelectedTxtbox);

                SelectedMatrix.MatrixPanel.BorderStyle = BorderStyle.FixedSingle;
                SelectedMatrix.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = SelectedMatrix;
                SelectedTxtbox.ReadOnly = false;
                SelectedTxtbox.KeyPress += textBox1_KeyPress;
                SelectedTxtbox.KeyUp += Keys_Up;
                SelectedTxtbox.KeyDown += Keys_Down;
                SelectedTxtbox.Focus();

                ActualSelectedMatrix = SelectedMatrix;
            }

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
            }
        }

        private async void MatrixClipboardImport(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            Matrix SelectedMatrix = (Matrix)(Sender.Tag);

            string ImportedMatrix;
            string[] matrixrows;
            string[] matrixcols;

            this.ActiveControl = null;
            SelectedMatrix.PanelPicturebox.Enabled = false;
            if (SelectedMatrix == matrix3)
            {
                this.Enabled = false;
            }

            MatrixImport matriximport = new MatrixImport();
            matriximport.ShowDialog();
            if (matriximport.DialogResult == DialogResult.OK)
            {
                ImportedMatrix = matriximport.ImportMatrix;
                matrixrows = ImportedMatrix.Split("\r\n");
                matrixcols = matrixrows[0].Split(" ");
                int Rows = matrixrows.GetLength(0);
                int Cols = matrixcols.GetLength(0);

                SelectedMatrix.ContentsArray = SelectedMatrix.ResizeArray(Rows, Cols);
                MatrixPanelResize(SelectedMatrix, Rows, Cols);
                for (int i = 0; i < Rows; i++)
                {
                    matrixcols = matrixrows[i].Split(" ");
                    for (int j = 0; j < Cols; j++)
                    {
                        try
                        {
                            SelectedMatrix.ContentsArray[i, j] = Double.Parse(matrixcols[j]);
                            if (SelectedMatrix.ContentsArray[i, j] == -0)
                            {
                                SelectedMatrix.ContentsArray[i, j] = 0;
                            }
                        }
                        catch (Exception)
                        {
                            SelectedMatrix.ContentsArray[i, j] = 0;
                        }
                    }
                }
                MessageBox.Show("Sikeresen be lett töltve a kiválasztott mátrix!", "Eredmény");
            }
            else if (matriximport.DialogResult == DialogResult.Continue)
            {
                using (StreamReader MatrixImport = new StreamReader(matriximport.ImportFilePath))
                {
                    ImportedMatrix = await MatrixImport.ReadLineAsync();
                    MatrixImport.DiscardBufferedData();
                    MatrixImport.BaseStream.Seek(0, SeekOrigin.Begin);

                    int Rows = 0;
                    int Cols = ImportedMatrix.Split(" ").GetLength(0);
                    while (await MatrixImport.ReadLineAsync() is not null)
                    {
                        Rows++;
                    }
                    SelectedMatrix.ContentsArray = SelectedMatrix.ResizeArray(Rows, Cols);
                    MatrixPanelResize(SelectedMatrix, Rows, Cols);
                    MatrixImport.DiscardBufferedData();
                    MatrixImport.BaseStream.Seek(0, SeekOrigin.Begin);
                    for (int i = 0; i < Rows; i++)
                    {
                        ImportedMatrix = await MatrixImport.ReadLineAsync();
                        matrixcols = ImportedMatrix.Split(" ");
                        Parallel.For(0, Cols, j =>
                        {
                            try
                            {
                                SelectedMatrix.ContentsArray[i, j] = Double.Parse(matrixcols[j]);
                            }
                            catch (Exception)
                            {
                                SelectedMatrix.ContentsArray[i, j] = 0;
                            }
                        });
                    }
                    MatrixImport.DiscardBufferedData();
                    MatrixImport.Dispose();
                    GC.Collect();

                }
                MessageBox.Show("Sikeresen be lett töltve a kiválasztott mátrix!", "Eredmény");
            }
            else
            {
                SelectedMatrix.PanelPicturebox.Enabled = true;
                if (SelectedMatrix == matrix3)
                {
                    this.Enabled = true;
                }
                return;
            }

            matriximport.Dispose();
            if ((Matrix)SelectedTxtbox.Tag == SelectedMatrix)
            {
                SelectedMatrix.PanelPicturebox.Controls.Remove(SelectedTxtbox);
            }
            if ((Matrix)SelectedTxtbox.Tag == SelectedMatrix)
            {
                SelectedMatrix.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Text = SelectedMatrix.ContentsArray[0, 0].ToString();
            }
            SelectedMatrix.PanelPicturebox.Refresh();
            SelectedMatrix.RowPicturebox.Refresh();
            SelectedMatrix.ColPicturebox.Refresh();
            SelectedMatrix.PanelPicturebox.Enabled = true;
            if (SelectedMatrix == matrix3)
            {
                this.Enabled = true;
            }

        }

        private async void MatrixClipboardExport(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            Matrix SelectedMatrix = (Matrix)(Sender.Tag);
            string[] ButtonName = Sender.Name.Split('_');
            SelectedMatrix.PanelPicturebox.Enabled = false;
            if (SelectedMatrix == matrix3)
            {
                this.Enabled = false;
            }
            this.ActiveControl = null;

            if (SelectedMatrix.ActualRows <= 10 && SelectedMatrix.ActualCols <= 10)
            {
                StringBuilder MatrixOutput = new StringBuilder("");
                for (int i = 0; i < SelectedMatrix.ActualRows; i++)
                {
                    for (int j = 0; j < SelectedMatrix.ActualCols; j++)
                    {
                        if (j < SelectedMatrix.ActualCols - 1)
                        {
                            MatrixOutput.Append(String.Format("{0} ", SelectedMatrix.ContentsArray[i, j]));
                        }
                        else if (j == SelectedMatrix.ActualCols - 1 && i < SelectedMatrix.ActualRows - 1)
                        {
                            MatrixOutput.Append(String.Format("{0}\r\n", SelectedMatrix.ContentsArray[i, j]));
                        }
                        else
                        {
                            MatrixOutput.Append(SelectedMatrix.ContentsArray[i, j].ToString());
                        }
                    }
                }
                try
                {
                    Clipboard.SetText(MatrixOutput.ToString());
                    MessageBox.Show("Ki lett másolva a " + ButtonName[0] + " mátrix a vágólapra!", "Eredmény");
                }
                catch (Exception)
                {
                    MessageBox.Show("A kimásolni kívánt " + ButtonName[0] + " mátrix üres,\nezért nem lehetett kimásolni", "Hiba");
                }
            }

            else
            {
                SaveFileDialog Output = new SaveFileDialog();
                Output.Title = "Mátrix mentése...";
                Output.DefaultExt = "txt";
                Output.RestoreDirectory = true;
                Output.Filter = "Szöveges fájlok (*.txt)|*.txt";
                Output.CheckPathExists = true;

                if (Output.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter MatrixExport = new StreamWriter(Output.FileName))
                    {
                        for (int i = 0; i < SelectedMatrix.ActualRows; i++)
                        {
                            for (int j = 0; j < SelectedMatrix.ActualCols; j++)
                            {
                                if (j < SelectedMatrix.ActualCols - 1)
                                {
                                    await MatrixExport.WriteAsync(String.Format("{0} ", SelectedMatrix.ContentsArray[i, j]));
                                }
                                else if (j == SelectedMatrix.ActualCols - 1 && i < SelectedMatrix.ActualRows - 1)
                                {
                                    await MatrixExport.WriteAsync(String.Format("{0}\r\n", SelectedMatrix.ContentsArray[i, j]));
                                }
                                else
                                {
                                    await MatrixExport.WriteAsync(SelectedMatrix.ContentsArray[i, j].ToString());
                                }
                            }
                        }
                    }
                    try
                    {
                        MessageBox.Show("Ki lett másolva a " + ButtonName[0] + " mátrix!", "Eredmény");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("A kimásolni kívánt " + ButtonName[0] + " mátrix üres,\nezért nem lehetett kimásolni", "Hiba");
                    }
                }
            }
            SelectedMatrix.PanelPicturebox.Enabled = true;
            if (SelectedMatrix == matrix3)
            {
                this.Enabled = true;
            }
        }

        private void MatrixPicturebox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Panel Sender = sender as Panel;
            Matrix SelectedMatrix = (Matrix)(Sender.Tag);
            int increment = 0;

            if (e.KeyValue == '+')
            {
                increment = 1;
            }
            else if (e.KeyValue == '-')
            {
                increment = -1;
            }

            if (RowNav || ColNav)
            {
                if (TxtboxWidth > (20 - increment) && TxtboxWidth < (250 - increment) && ColNav)
                {
                    TxtboxWidth += increment;
                    SelectedTxtbox.Size = new Size(TxtboxWidth - 3, TxtboxHeight);
                }
                if (TxtboxHeight > (20 - increment) && TxtboxHeight < (45 - increment) && RowNav)
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

                matrix1.PanelPicturebox.Size = new Size(TxtboxWidth + matrix1.MatrixPanel.Width, TxtboxHeight + matrix1.MatrixPanel.Height);
                matrix2.PanelPicturebox.Size = new Size(TxtboxWidth + matrix2.MatrixPanel.Width, TxtboxHeight + matrix2.MatrixPanel.Height);
                matrix3.PanelPicturebox.Size = new Size(TxtboxWidth + matrix3.MatrixPanel.Width, TxtboxHeight + matrix3.MatrixPanel.Height);
                SelectedTxtbox.Font = TxtboxFont;
            }
        }

        private void irányításToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlsInfo ControlsInfoForm = new ControlsInfo();
            ControlsInfoForm.Show();
        }

        private void műveletekToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OperationsInfo OperationsInfoForm = new OperationsInfo();
            OperationsInfoForm.Show();
        }
    }
}