namespace Szakdolgozat
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        private void mátrixÁtméretezéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateValue();
            MatrixSizeSelector matrixSizeSelector = new MatrixSizeSelector();
            int RowSize, ColSize;
            if (matrixSizeSelector.ShowDialog(this) == DialogResult.OK)
            {
                RowSize = Int32.Parse(matrixSizeSelector.Controls.Find("RowSize", false).FirstOrDefault().Text);
                ColSize = Int32.Parse(matrixSizeSelector.Controls.Find("ColSize", false).FirstOrDefault().Text);
            }
            else
            {
                return;
            }
            matrixSizeSelector.Dispose();

            int ActualRowSize = ActualSelectedMatrix.ActualRows;
            int ActualColSize = ActualSelectedMatrix.ActualCols;
            if (ActualRowSize != RowSize || ActualColSize != ColSize)
            {
                MatrixPanelResize(ActualSelectedMatrix, RowSize, ColSize);
            }
        }
        delegate void SetTextCallback(string text);
        // Updating Selectedtxtbox
        // Checks, whether a thread is trying to modify SelectedTxtBox's contents
        private void SetSelectedTxtboxText(string text)
        {
            if (this.SelectedTxtbox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetSelectedTxtboxText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.SelectedTxtbox.Text = text;
            }
        }

        private void mátrixFeltöltéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateValue();

            RandomNumberProperties randomNumberProperties = new RandomNumberProperties();

            double MinValue, MaxValue;
            int DecimalPlaces;

            if (randomNumberProperties.ShowDialog(this) == DialogResult.OK)
            {
                MinValue = Double.Parse(randomNumberProperties.Controls.Find("MinValue", false).FirstOrDefault().Text);
                MaxValue = Double.Parse(randomNumberProperties.Controls.Find("MaxValue", false).FirstOrDefault().Text);
                DecimalPlaces = Int32.Parse(randomNumberProperties.Controls.Find("DecimalPlaces", false).FirstOrDefault().Text);
            }
            else
            {
                return;
            }

            randomNumberProperties.Dispose();

            Parallel.For(0, ActualSelectedMatrix.ActualRows, i =>
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    string TxtboxName = i.ToString() + "_" + j.ToString();
                    double CellValue = Math.Round((((rnd.NextDouble() * (MaxValue - MinValue)) + MinValue)), DecimalPlaces);
                    if (CellValue == -0)
                    {
                        CellValue = 0;
                    }
                    ActualSelectedMatrix.ContentsArray[i, j] = CellValue;
                    if (SelectedTxtbox.Name == TxtboxName && ActualSelectedMatrix == (Matrix)SelectedTxtbox.Tag)
                    {
                        SetSelectedTxtboxText(ActualSelectedMatrix.ContentsArray[i, j].ToString());
                    }
                }
            });
            ActualSelectedMatrix.PanelPicturebox.Refresh();
            MessageBox.Show("A kiválasztott mátrix sikeresen fel lett töltve " + MinValue.ToString() + " és " + MaxValue.ToString() + " közötti elemekkel, amelyeknek " + DecimalPlaces.ToString() + " tizedesjegyük van.", "Eredmény");
        }

        private void mátrixNegálásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateValue();
            Parallel.For(0, ActualSelectedMatrix.ActualRows, i =>
            {
                for (int j = 0; j < ActualSelectedMatrix.ActualCols; j++)
                {
                    string TxtboxName = i.ToString() + "_" + j.ToString();
                    double CellValue = (-1) * ActualSelectedMatrix.ContentsArray[i, j];
                    if (CellValue == -0)
                    {
                        CellValue = 0;
                    }
                    ActualSelectedMatrix.ContentsArray[i, j] = CellValue;
                    if (SelectedTxtbox.Name == TxtboxName && ActualSelectedMatrix == (Matrix)SelectedTxtbox.Tag)
                    {
                        SetSelectedTxtboxText(ActualSelectedMatrix.ContentsArray[i, j].ToString());
                    }
                }
            });
            ActualSelectedMatrix.PanelPicturebox.Refresh();
            MessageBox.Show("A kiválasztott mátrix elemei sikeresen negálva lettek!", "Eredmény");
        }
    }
}
