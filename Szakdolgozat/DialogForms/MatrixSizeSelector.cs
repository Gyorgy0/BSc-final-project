namespace Szakdolgozat
{
    public partial class MatrixSizeSelector : Form
    {
        public MatrixSizeSelector()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        bool ValidRows = false;
        bool ValidCols = false;
        private void button2_Click(object sender, EventArgs e)
        {
            ValidRows = Int32.TryParse(RowSize.Text.ToString(), out int rows);
            ValidCols = Int32.TryParse(ColSize.Text.ToString(), out int cols);
            if (ValidCols && cols > 0 && rows > 0 && cols < 10000 && rows < 10000 && ValidRows)
            {
                DialogResult = DialogResult.OK;
                return;
            }
            if ((rows > 9999 || rows < 1) && ValidRows)
            {
                MessageBox.Show("A megadott mátrix sorainak száma nem lehet kisebb 1-nél és nem lehet nagyobb 9999-nél!", "Figyelmeztetés");
            }
            if ((cols > 9999 || cols < 1) && ValidCols)
            {
                MessageBox.Show("A megadott mátrix oszlopainak száma nem lehet kisebb 1-nél és nem lehet nagyobb 9999-nél!", "Figyelmeztetés");
            }
            if (!ValidRows)
            {
                MessageBox.Show("A megadott mátrix sorainak száma érvénytelen!", "Figyelmeztetés");
            }
            if (!ValidCols)
            {
                MessageBox.Show("A megadott mátrix oszlopainak száma érvénytelen!", "Figyelmeztetés");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '\b')))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                ValidRows = Int32.TryParse(RowSize.Text.ToString(), out int rows);
                ValidCols = Int32.TryParse(ColSize.Text.ToString(), out int cols);
                if (ValidRows && Txtbox == RowSize && rows > 1 && rows < 10000)
                {
                    ColSize.Focus();
                    return;
                }
                if (ValidCols && cols > 0 && rows > 0 && cols < 10000 && rows < 10000 && ValidRows)
                {
                    DialogResult = DialogResult.OK;
                    return;
                }
                if ((rows > 9999 || rows < 1) && ValidRows)
                {
                    MessageBox.Show("A megadott mátrix sorainak száma nem lehet kisebb 1-nél és nem lehet nagyobb 9999-nél!", "Figyelmeztetés");
                }
                if ((cols > 9999 || cols < 1) && ValidCols)
                {
                    MessageBox.Show("A megadott mátrix oszlopainak száma nem lehet kisebb 1-nél és nem lehet nagyobb 9999-nél!", "Figyelmeztetés");
                }
                if (!ValidRows)
                {
                    MessageBox.Show("A megadott mátrix sorainak száma érvénytelen!", "Figyelmeztetés");
                }
                if (!ValidCols)
                {
                    MessageBox.Show("A megadott mátrix oszlopainak száma érvénytelen!", "Figyelmeztetés");
                }
            }
        }
    }
}
