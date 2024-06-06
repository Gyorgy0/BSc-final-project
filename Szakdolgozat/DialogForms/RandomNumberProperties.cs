namespace Szakdolgozat
{
    public partial class RandomNumberProperties : Form
    {
        public RandomNumberProperties()
        {
            InitializeComponent();
        }

        bool ValidMinValue = false;
        bool ValidMaxValue = false;
        bool ValidDecimalPlaces = false;

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ValidMinValue = Double.TryParse(MinValue.Text.ToString(), out double minvalue);
            ValidMaxValue = Double.TryParse(MaxValue.Text.ToString(), out double maxvalue);
            ValidDecimalPlaces = Int32.TryParse(DecimalPlaces.Text.ToString(), out int decimalplaces);

            if (minvalue <= maxvalue && ValidMinValue && ValidMaxValue && ValidDecimalPlaces && decimalplaces >= 0 && decimalplaces <= 15)
            {
                DialogResult = DialogResult.OK;
                return;
            }
            if (minvalue > maxvalue)
            {
                MessageBox.Show("A minimális érték nem lehet kisebb a maximális értéknél!", "Figyelmeztetés");
            }
            if (!ValidMinValue)
            {
                MessageBox.Show("A minimális érték érvénytelen szám!", "Figyelmeztetés");
            }
            if (!ValidMaxValue)
            {
                MessageBox.Show("A maximális érték érvénytelen szám!", "Figyelmeztetés");
            }
            if (!ValidDecimalPlaces && !(decimalplaces >= 0 && decimalplaces <= 15))
            {
                MessageBox.Show("A tizedesjegyek számának 0 és 15 között kell lennie!", "Figyelmeztetés");
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox Txtbox = sender as TextBox;

            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '\b')))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (Txtbox.SelectionStart == 0 && !(Txtbox.Text.Contains(e.KeyChar)))
                {
                    Txtbox.Text = "0" + e.KeyChar.ToString() + Txtbox.Text;
                    Txtbox.SelectionStart = 1;
                }
                if (Txtbox.SelectionStart == Txtbox.Text.Length && !(Txtbox.Text.Contains(e.KeyChar)))
                {
                    Txtbox.Text = Txtbox.Text + e.KeyChar.ToString() + "0";
                    Txtbox.SelectionStart = Txtbox.Text.Length - 1;
                }
            }

            if (char.IsDigit(e.KeyChar))
            {
                if (Txtbox.Text.StartsWith('-') && Txtbox.SelectionStart == 0)
                {
                    Txtbox.SelectionStart = 1;
                }
            }

            if (e.KeyChar == '+')
            {
                if (Txtbox.Text.StartsWith('-'))
                {
                    Txtbox.Text = Txtbox.Text.Remove(0, 1);
                }
            }

            if (e.KeyChar == '-')
            {
                if (!(Txtbox.Text.StartsWith('-')))
                {
                    Txtbox.Text = e.KeyChar.ToString() + Txtbox.Text;
                }

            }

            if (e.KeyChar == '\r')
            {
                ValidMinValue = Double.TryParse(MinValue.Text.ToString(), out double minvalue);
                ValidMaxValue = Double.TryParse(MaxValue.Text.ToString(), out double maxvalue);
                if (ValidMinValue && Txtbox == MinValue)
                {
                    MaxValue.Focus();
                    return;
                }

                if (ValidMaxValue && Txtbox == MaxValue && minvalue <= maxvalue)
                {
                    DecimalPlaces.Focus();
                    return;
                }

                if (minvalue > maxvalue)
                {
                    MessageBox.Show("A minimális érték nem lehet kisebb a maximális értéknél!", "Figyelmeztetés");
                }
                if (!ValidMinValue)
                {
                    MessageBox.Show("A minimális érték érvénytelen szám!", "Figyelmeztetés");
                }
                if (!ValidMaxValue)
                {
                    MessageBox.Show("A maximális érték érvénytelen szám!", "Figyelmeztetés");
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '\b')))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                ValidMinValue = Double.TryParse(MinValue.Text.ToString(), out double minvalue);
                ValidMaxValue = Double.TryParse(MaxValue.Text.ToString(), out double maxvalue);
                ValidDecimalPlaces = Int32.TryParse(DecimalPlaces.Text.ToString(), out int decimalplaces);
                if (minvalue <= maxvalue && ValidMinValue && ValidMaxValue && ValidDecimalPlaces && decimalplaces >= 0 && decimalplaces <= 15)
                {
                    DialogResult = DialogResult.OK;
                    return;
                }
                if (minvalue > maxvalue)
                {
                    MessageBox.Show("A minimális érték nem lehet kisebb a maximális értéknél!", "Figyelmeztetés");
                }
                if (!ValidMinValue)
                {
                    MessageBox.Show("A minimális érték érvénytelen szám!", "Figyelmeztetés");
                }
                if (!ValidMaxValue)
                {
                    MessageBox.Show("A maximális érték érvénytelen szám!", "Figyelmeztetés");
                }
                if (!ValidDecimalPlaces || !(decimalplaces >= 0 && decimalplaces <= 15))
                {
                    MessageBox.Show("A tizedesjegyek számának 0 és 15 között kell lennie!", "Figyelmeztetés");
                }
            }
        }
    }
}
