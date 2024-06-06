namespace Szakdolgozat.DialogForms
{
    public partial class MatrixImport : Form
    {
        public MatrixImport()
        {
            InitializeComponent();
        }
        public string ImportMatrix;
        public string ImportFilePath;

        private void MatrixImport_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void MatrixImport_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportMatrix = textBox1.Text;
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox? Txtbox = sender as TextBox;
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '\r' || e.KeyChar == ' ' || e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == '+' || e.KeyChar == '-'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog Import = new OpenFileDialog();
            Import.Title = "Mátrix kiválasztása...";
            Import.DefaultExt = "txt";
            Import.RestoreDirectory = true;
            Import.Filter = "szöveges fájlok (*.txt)|*.txt";
            Import.CheckPathExists = true;
            Import.CheckFileExists = true;
            if (Import.ShowDialog() == DialogResult.OK)
            {
                ImportFilePath = Import.FileName;
                DialogResult = DialogResult.Continue;
            }
        }
    }
}
