namespace Szakdolgozat
{
    public partial class NumberProvideDialog : Form
    {
        public NumberProvideDialog()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == ',' || e.KeyChar == '.'))
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
                if (Double.TryParse(Number.Text.ToString(), out double ertek) == true)
                {
                    if (ertek == -0)
                    {
                        ertek = 0;
                    }
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("A beírt szám érvénytelen!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(Number.Text.ToString(), out double ertek) == true)
            {
                if (ertek == -0)
                {
                    ertek = 0;
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("A beírt szám érvénytelen!");
            }
        }
    }
}
