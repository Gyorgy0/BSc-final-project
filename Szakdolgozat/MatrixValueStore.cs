namespace Szakdolgozat
{
    public partial class Form1 : Form
    {
        bool RowNav;
        bool ColNav;
        bool NoSpam = true;

        TextBox SelectedTxtbox;
        private void Keys_Down(object? sender, KeyEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            Matrix? SelectedMatrix = (Matrix)Txtbox.Tag;

            string[] Pos = Txtbox.Name.Split('_');
            RowPos = Convert.ToInt32(Pos[0]);
            ColPos = Convert.ToInt32(Pos[1]);

            if (e.KeyCode == Keys.R)
            {
                RowNav = true;
            }
            if (e.KeyCode == Keys.C)
            {
                ColNav = true;
            }
            if (e.KeyCode == Keys.Space && NoSpam && SelectedMatrix.ActualCols + 1 < 10000)
            {
                MatrixPanelResize(SelectedMatrix, SelectedMatrix.ActualRows, SelectedMatrix.ActualCols + 1);

                NoSpam = false;
            }
            if (e.KeyCode == Keys.Back && ColNav && SelectedMatrix.ActualCols > 1)
            {
                MatrixPanelResize(SelectedMatrix, SelectedMatrix.ActualRows, SelectedMatrix.ActualCols - 1);
                NoSpam = false;

            }
            if (e.KeyCode == Keys.Enter && NoSpam && SelectedMatrix.ActualRows + 1 < 10000)
            {
                MatrixPanelResize(SelectedMatrix, SelectedMatrix.ActualRows + 1, SelectedMatrix.ActualCols);
                NoSpam = false;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Back && RowNav && SelectedMatrix.ActualRows > 1)
            {
                MatrixPanelResize(SelectedMatrix, SelectedMatrix.ActualRows - 1, SelectedMatrix.ActualCols);
                NoSpam = false;
            }
            if ((e.KeyCode == Keys.Left && e.Shift) || e.KeyCode == Keys.A)
            {
                if (ColPos > 0)
                {
                    StoreValue(SelectedMatrix, Txtbox);
                    ColPos--;
                    SelectedTxtbox.Location = new Point(((ColPos * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((RowPos * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
                    SelectedTxtbox.Name = RowPos.ToString() + "_" + ColPos.ToString();
                    SelectedTxtbox.Text = ActualSelectedMatrix.ContentsArray[RowPos, ColPos].ToString();
                    SelectedTxtbox.Select(0, SelectedTxtbox.Text.Length);
                }

            }
            if ((e.KeyCode == Keys.Right && e.Shift) || e.KeyCode == Keys.D)
            {
                if (ColPos < SelectedMatrix.ActualCols - 1)
                {
                    StoreValue(SelectedMatrix, Txtbox);
                    ColPos++;
                    SelectedTxtbox.Location = new Point(((ColPos * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((RowPos * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
                    SelectedTxtbox.Name = RowPos.ToString() + "_" + ColPos.ToString();
                    SelectedTxtbox.Text = ActualSelectedMatrix.ContentsArray[RowPos, ColPos].ToString();
                    SelectedTxtbox.Select(0, SelectedTxtbox.Text.Length);
                }

            }
            if ((e.KeyCode == Keys.Up && e.Shift) || e.KeyCode == Keys.W)
            {
                if (RowPos > 0)
                {
                    StoreValue(SelectedMatrix, Txtbox);
                    RowPos--;
                    SelectedTxtbox.Location = new Point(((ColPos * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((RowPos * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
                    SelectedTxtbox.Name = RowPos.ToString() + "_" + ColPos.ToString();
                    SelectedTxtbox.Text = ActualSelectedMatrix.ContentsArray[RowPos, ColPos].ToString();
                    SelectedTxtbox.Select(0, SelectedTxtbox.Text.Length);
                }

            }
            if ((e.KeyCode == Keys.Down && e.Shift) || e.KeyCode == Keys.S)
            {
                if (RowPos < SelectedMatrix.ActualRows - 1)
                {
                    StoreValue(SelectedMatrix, Txtbox);
                    RowPos++;
                    SelectedTxtbox.Location = new Point(((ColPos * TxtboxWidth) + (TxtboxWidth - SelectedMatrix.HorizontalScrollBar.Value) + 1), ((RowPos * TxtboxHeight) + (TxtboxHeight - SelectedMatrix.VerticalScrollBar.Value) + 1));
                    SelectedTxtbox.Name = RowPos.ToString() + "_" + ColPos.ToString();
                    SelectedTxtbox.Text = ActualSelectedMatrix.ContentsArray[RowPos, ColPos].ToString();
                    SelectedTxtbox.Select(0, SelectedTxtbox.Text.Length);
                }

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
                NoSpam = true;
            }
            if (e.KeyCode == Keys.C)
            {
                ColNav = false;
                NoSpam = true;
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                NoSpam = true;
                e.SuppressKeyPress = true;
            }
        }
        private void textBox1_KeyPress(object? sender, KeyPressEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            Matrix? SelectedMatrix = (Matrix)Txtbox.Tag;

            string[] Pos = Txtbox.Name.Split('_');
            RowPos = Convert.ToInt32(Pos[0]);
            ColPos = Convert.ToInt32(Pos[1]);

            if (e.KeyChar == '\r' && !NoSpam)
            {
                e.Handled = true;
                return;
            }

            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == ',') || (e.KeyChar == '.') || (e.KeyChar == '\b' && NoSpam)))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (char.IsDigit(e.KeyChar))
            {
                if (Txtbox.Text.StartsWith('-') && Txtbox.SelectionStart == 0)
                {
                    Txtbox.SelectionStart = 1;
                }
            }

            if (e.KeyChar == '+' && !RowNav && !ColNav)
            {
                if (Txtbox.Text.StartsWith('-'))
                {
                    Txtbox.Text = Txtbox.Text.Remove(0, 1);
                }
            }

            if (e.KeyChar == '-' && !RowNav && !ColNav)
            {
                if (!(Txtbox.Text.StartsWith('-')))
                {
                    Txtbox.Text = e.KeyChar.ToString() + Txtbox.Text;
                }

            }

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

                SelectedTxtbox.Font = TxtboxFont;
            }
        }

        private void SelectedTxtbox_MouseWheel(object? sender, MouseEventArgs e)
        {
            TextBox Sender = (TextBox)sender;
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

                matrix1.PanelPicturebox.Size = new Size(TxtboxWidth + matrix1.MatrixPanel.Width, TxtboxHeight + matrix1.MatrixPanel.Height);
                matrix2.PanelPicturebox.Size = new Size(TxtboxWidth + matrix2.MatrixPanel.Width, TxtboxHeight + matrix2.MatrixPanel.Height);
                matrix3.PanelPicturebox.Size = new Size(TxtboxWidth + matrix3.MatrixPanel.Width, TxtboxHeight + matrix3.MatrixPanel.Height);
                SelectedTxtbox.Font = TxtboxFont;
            }
        }
        private void StoreValue(Matrix SelectedMatrix, TextBox Txtbox)
        {
            int[] SelectedTxtboxLocation = new int[2];
            string[] SelectedTxtboxName = SelectedTxtbox.Name.Split('_', 2);
            SelectedTxtboxLocation[0] = Convert.ToInt32(SelectedTxtboxName[0]);
            SelectedTxtboxLocation[1] = Convert.ToInt32(SelectedTxtboxName[1]);
            if (Double.TryParse(Txtbox.Text.ToString(), out double value) == true)
            {
                if (value == -0)
                {
                    value = 0;
                }
                SelectedTxtbox.Text = Convert.ToString(value);
                SelectedMatrix.ContentsArray[SelectedTxtboxLocation[0], SelectedTxtboxLocation[1]] = Convert.ToDouble(value);
            }
            else
            {
                SelectedTxtbox.Text = "0";
                SelectedMatrix.ContentsArray[SelectedTxtboxLocation[0], SelectedTxtboxLocation[1]] = Convert.ToDouble(SelectedTxtbox.Text);
                MessageBox.Show("A beírt szám érvénytelen!", "Hiba");
            }
        }

        private void UpdateValue()
        {
            int[] SelectedTxtboxLocation = new int[2];
            string[] SelectedTxtboxName = SelectedTxtbox.Name.Split('_', 2);
            SelectedTxtboxLocation[0] = Convert.ToInt32(SelectedTxtboxName[0]);
            SelectedTxtboxLocation[1] = Convert.ToInt32(SelectedTxtboxName[1]);
            if (Double.TryParse(SelectedTxtbox.Text.ToString(), out double value) == true)
            {
                ActualSelectedMatrix.ContentsArray[SelectedTxtboxLocation[0], SelectedTxtboxLocation[1]] = value;
            }
            else
            {
                SelectedTxtbox.Text = "0";
                ActualSelectedMatrix.ContentsArray[SelectedTxtboxLocation[0], SelectedTxtboxLocation[1]] = Convert.ToDouble(SelectedTxtbox.Text);
                MessageBox.Show("A beírt szám érvénytelen volt!", "Hiba");
            }
        }
    }
}