namespace Szakdolgozat
{
    public partial class Form1 : Form
    {
        Matrix ActualSelectedMatrix;
        EventHandler? ActualVisualizationOperation;
        long StepCounter;
        private void MatrixPanelComponentsUpdate()
        {
            StepCounter = 0;
            matrix3.PanelPicturebox.Click -= new EventHandler(MatrixPicturebox1_Click);
            if ((Matrix)SelectedTxtbox.Tag == matrix3)
            {
                Matrix PreviousMatrix = (Matrix)SelectedTxtbox.Tag;
                matrix1.MatrixPanel.BorderStyle = BorderStyle.None;
                matrix2.MatrixPanel.BorderStyle = BorderStyle.None;

                PreviousMatrix.PanelPicturebox.Controls.Remove(SelectedTxtbox);

                ActualSelectedMatrix.MatrixPanel.BorderStyle = BorderStyle.FixedSingle;
                ActualSelectedMatrix.PanelPicturebox.Controls.Add(SelectedTxtbox);
                SelectedTxtbox.Tag = ActualSelectedMatrix;
                SelectedTxtbox.ReadOnly = false;
                SelectedTxtbox.KeyPress += textBox1_KeyPress;
                SelectedTxtbox.KeyUp += Keys_Up;
                SelectedTxtbox.KeyDown += Keys_Down;
                SelectedTxtbox.Focus();


                SelectedTxtbox.Name = "0_0";
                SelectedTxtbox.Text = ActualSelectedMatrix.ContentsArray[0, 0].ToString();
                SelectedTxtbox.Location = new Point(((0 * TxtboxWidth) + (TxtboxWidth - ActualSelectedMatrix.HorizontalScrollBar.Value) + 1), ((0 * TxtboxHeight) + (TxtboxHeight - ActualSelectedMatrix.VerticalScrollBar.Value) + 1));
            }
            if ((Matrix)SelectedTxtbox.Tag != matrix3)
            {
                UpdateValue();
            }

            if (ActualVisualizationOperation != null)
            {
                button1.Click -= ActualVisualizationOperation;
            }
            button1.Enabled = true;
        }

        private void MatrixPanelRefresher()
        {
            matrix3.PanelPicturebox.Click += new EventHandler(MatrixPicturebox1_Click);
            matrix3.RowScrollbar.Refresh();
            matrix3.ColScrollbar.Refresh();
            matrix3.PanelPicturebox.Refresh();
            label6.Text = "Sorok száma: " + matrix3.ActualRows.ToString();
            label5.Text = "Oszlopok száma: " + matrix3.ActualCols.ToString();

            if ((matrix3.ActualRows * TxtboxHeight) < matrix3.VerticalScrollBar.Height || matrix3.ActualRows == 0)
            {
                matrix3.VerticalScrollBar.Value = 0;
                matrix3.VerticalScrollBar.Hide();
            }
            else
            {
                matrix3.VerticalScrollBar.Show();
                matrix3.VerticalScrollBar.Maximum = (matrix3.ActualRows * TxtboxHeight) - (matrix3.RowScrollbar.Height) + 10;
                matrix3.VerticalScrollBar.SmallChange = TxtboxHeight;
            }
            if ((matrix3.ActualCols * TxtboxWidth) < matrix3.HorizontalScrollBar.Width || matrix3.ActualCols == 0)
            {
                matrix3.HorizontalScrollBar.Value = 0;
                matrix3.HorizontalScrollBar.Hide();
            }
            else
            {
                matrix3.HorizontalScrollBar.Show();
                matrix3.HorizontalScrollBar.Maximum = (matrix3.ActualCols * TxtboxWidth) - (matrix3.ColScrollbar.Width) + 10;
                matrix3.HorizontalScrollBar.SmallChange = TxtboxWidth;
            }
        }

        private void transzponálásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(TransposeVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualCols, ActualSelectedMatrix.ActualRows);
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    matrix3.ContentsArray[j, i] = ActualSelectedMatrix.ContentsArray[i, j];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void vízszintesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(HorizontalMirroringVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    matrix3.ContentsArray[i, j] = ActualSelectedMatrix.ContentsArray[i, Math.Abs(j - (ActualSelectedMatrix.ActualCols - 1))];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void függőlegesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(VerticalMirroringVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    matrix3.ContentsArray[i, j] = ActualSelectedMatrix.ContentsArray[Math.Abs(i - (ActualSelectedMatrix.ActualRows - 1)), j];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void sorokbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(MatrixRowSortingVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            double[] RowValues = new double[matrix3.ActualCols];

            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    RowValues[j] = CellValue;
                    StepCounter++;
                }
                Array.Sort(RowValues);
                StepCounter++;
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    matrix3.ContentsArray[i, j] = RowValues[j];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }
        private void oszlopokbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(MatrixCollumnSortingVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            double[] ColValues = new double[matrix3.ActualRows];

            for (int i = 0; i < ActualSelectedMatrix.ActualCols; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualRows; j++)
                {

                    double CellValue = ActualSelectedMatrix.ContentsArray[j, i];
                    ColValues[j] = CellValue;
                    StepCounter++;
                }
                Array.Sort(ColValues);
                StepCounter++;
                for (int j = 0; j < ActualSelectedMatrix.ActualRows; j++)
                {
                    matrix3.ContentsArray[j, i] = ColValues[j];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void sorokbanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(MatrixReverseRowSortingVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            double[] RowValues = new double[matrix3.ActualCols];

            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    RowValues[j] = CellValue;
                    StepCounter++;
                }
                Array.Sort(RowValues);
                Array.Reverse(RowValues);
                StepCounter++;
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    matrix3.ContentsArray[i, j] = RowValues[j];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }
        private void oszlopokbanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(MatrixReverseCollumnSortingVisualization);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            double[] ColValues = new double[matrix3.ActualRows];

            for (int i = 0; i < ActualSelectedMatrix.ActualCols; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualRows; j++)
                {

                    double CellValue = ActualSelectedMatrix.ContentsArray[j, i];
                    ColValues[j] = CellValue;
                    StepCounter++;
                }
                Array.Sort(ColValues);
                Array.Reverse(ColValues);
                StepCounter++;
                for (int j = 0; j < ActualSelectedMatrix.ActualRows; j++)
                {
                    matrix3.ContentsArray[j, i] = ColValues[j];
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        public double Multiplicator;
        private void szorzásSzámmalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberProvideDialog numberProvideDialog = new NumberProvideDialog();
            if (numberProvideDialog.ShowDialog(this) == DialogResult.OK)
            {
                Multiplicator = Double.Parse(numberProvideDialog.Controls.Find("Number", false).FirstOrDefault().Text);
            }
            else
            {
                return;
            }
            numberProvideDialog.Dispose();
            MatrixPanelComponentsUpdate();

            ActualVisualizationOperation = new EventHandler(MatrixMultiplicationByNumber);

            matrix3.ContentsArray = matrix3.ResizeArray(ActualSelectedMatrix.ActualRows, ActualSelectedMatrix.ActualCols);
            for (int i = 0; i < matrix3.ActualRows; i++)
            {
                for (int j = 0; j < matrix3.ActualCols; j++)
                {
                    double Value;
                    try
                    {
                        Value = Convert.ToDouble(Convert.ToDecimal((Decimal)Multiplicator * (Decimal)ActualSelectedMatrix.ContentsArray[i, j]));
                    }
                    catch (Exception)
                    {
                        Value = Multiplicator * ActualSelectedMatrix.ContentsArray[i, j];
                    }
                    if (Value == -0)
                    {
                        Value = 0;
                    }
                    matrix3.ContentsArray[i, j] = Value;
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void összeadásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (matrix1.ActualCols == matrix2.ActualCols && matrix1.ActualRows == matrix2.ActualRows && matrix1.PanelPicturebox.Enabled && matrix2.PanelPicturebox.Enabled)
            {
                MatrixPanelComponentsUpdate();

                ActualVisualizationOperation = new EventHandler(MatrixAddition);

                matrix3.ContentsArray = matrix3.ResizeArray(matrix1.ActualRows, matrix1.ActualCols);
                for (int i = 0; i < matrix3.ActualRows; i++)
                {
                    for (int j = 0; j < matrix3.ActualCols; j++)
                    {
                        double Value;
                        try
                        {
                            Value = Convert.ToDouble(Convert.ToDecimal((Decimal)matrix1.ContentsArray[i, j] + (Decimal)matrix2.ContentsArray[i, j]));
                        }
                        catch (Exception)
                        {
                            Value = matrix1.ContentsArray[i, j] + matrix2.ContentsArray[i, j];
                        }
                        if (Value == -0)
                        {
                            Value = 0;
                        }
                        matrix3.ContentsArray[i, j] = Value;
                        StepCounter++;
                    }
                }
                MatrixPanelRefresher();
                button1.Click += ActualVisualizationOperation;
            }
            else if (!matrix1.PanelPicturebox.Enabled || !matrix2.PanelPicturebox.Enabled)
            {
                MessageBox.Show("A kiválasztott művelet nem végezhető el, mivel az egyik mátrix éppen mentés, vagy importálás alatt áll.", "Hiba");
            }
            else
            {
                MessageBox.Show("A kiválasztott művelet nem végezhető el, mivel a két mátrix alakja nem egyezik meg.", "Hiba");
            }
        }

        private void mátrixokSzorzásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (matrix1.ActualCols == matrix2.ActualRows && matrix1.PanelPicturebox.Enabled && matrix2.PanelPicturebox.Enabled)
            {
                MatrixPanelComponentsUpdate();
                ActualVisualizationOperation = new EventHandler(MatrixMultiplicationByMatrix);

                double[,] CellResult = new double[matrix1.ActualRows, matrix2.ActualCols];
                matrix3.ContentsArray = matrix3.ResizeArray(matrix1.ActualRows, matrix2.ActualCols);
                Parallel.For(0, matrix1.ActualRows, i =>
                {
                    for (int j = 0; j < matrix2.ActualCols; j++)
                    {
                        CellResult[i, j] = 0;
                        for (int k = 0; k < matrix1.ActualCols; k++)
                        {
                            try
                            {
                                CellResult[i, j] += Convert.ToDouble(Convert.ToDecimal(((Decimal)matrix1.ContentsArray[i, k] * (Decimal)matrix2.ContentsArray[k, j])));
                            }
                            catch (Exception)
                            {
                                CellResult[i, j] += (matrix1.ContentsArray[i, k] * matrix2.ContentsArray[k, j]);
                            }
                            Interlocked.Increment(ref StepCounter);
                        }
                        if (CellResult[i, j] == -0)
                        {
                            CellResult[i, j] = 0;
                        }
                        matrix3.ContentsArray[i, j] = CellResult[i, j];
                        Interlocked.Increment(ref StepCounter);
                    }
                });
                MatrixPanelRefresher();
                button1.Click += ActualVisualizationOperation;
                CellResult = null;
                GC.Collect();
            }
            else if (!matrix1.PanelPicturebox.Enabled || !matrix2.PanelPicturebox.Enabled)
            {
                MessageBox.Show("A kiválasztott művelet nem végezhető el, mivel az egyik mátrix éppen mentés, vagy importálás alatt áll.", "Hiba");
            }
            else
            {
                MessageBox.Show("A kiválasztott művelet nem végezhető el, mivel az első mátrix oszlopainak száma nem egyezik meg a másik mátrix sorainak számával!");
            }
        }
        private void maximumKereséseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(MaxSearchVisualization);
            double MaxValue = ActualSelectedMatrix.ContentsArray[0, 0];
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    if (CellValue > MaxValue)
                    {
                        MaxValue = CellValue;
                    }
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            MessageBox.Show("A kiválasztott mátrix legnagyobb értéke a/az\n\r" + MaxValue.ToString(), "Eredmény");

            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void minimumKereséseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(MinSearchVisualization);
            double MinValue = ActualSelectedMatrix.ContentsArray[0, 0];
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    if (CellValue < MinValue)
                    {
                        MinValue = CellValue;
                    }
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            MessageBox.Show("A kiválasztott mátrix legkisebb értéke a/az\n\r" + MinValue.ToString(), "Eredmény");

            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void páratlanSzámokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(OddCounterVisualization);
            int OddCounter = 0;
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    if (CellValue % 1 == 0 && Math.Abs(CellValue % 2) == 1)
                    {
                        OddCounter++;
                    }
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            MessageBox.Show("A kiválasztott mátrixban " + OddCounter.ToString() + " páratlan szám található!", "Eredmény");

            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(EvenCounterVisualization);
            int EvenCounter = 0;
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    if (Math.Abs(CellValue % 2) == 0 && CellValue % 1 == 0)
                    {
                        EvenCounter++;
                    }
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            MessageBox.Show("A kiválasztott mátrixban " + EvenCounter.ToString() + " páros szám található!", "Eredmény");

            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }

        double ProvidedNumber;

        private void adottSzámMegszámlálásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumberProvideDialog numberProvideDialog = new NumberProvideDialog();
            int Counter = 0;
            if (numberProvideDialog.ShowDialog(this) == DialogResult.OK)
            {
                ProvidedNumber = Convert.ToDouble(numberProvideDialog.Controls.Find("Number", false).FirstOrDefault().Text);
            }
            else
            {
                return;
            }
            numberProvideDialog.Dispose();
            MatrixPanelComponentsUpdate();
            ActualVisualizationOperation = new EventHandler(CustomNumberSearchVisualization);
            for (int i = 0; i < ActualSelectedMatrix.ActualRows; i++)
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    double CellValue = ActualSelectedMatrix.ContentsArray[i, j];
                    if (CellValue == ProvidedNumber)
                    {
                        Counter++;
                    }
                    StepCounter++;
                }
            }
            MatrixPanelRefresher();
            MessageBox.Show("A megadott szám(" + ProvidedNumber.ToString() + ") összesen " + Counter.ToString() + " alkalommal fordul elő a mátrixban!", "Eredmény");

            button1.Click += ActualVisualizationOperation;
            VisualizedMatrix = ActualSelectedMatrix;
        }
    }
}
