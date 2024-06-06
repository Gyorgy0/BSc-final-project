namespace Szakdolgozat.HelpForms
{
    public partial class ControlsInfo : Form
    {
        public ControlsInfo()
        {
            InitializeComponent();
            richTextBox1.Rtf = @"{\rtf\ansi\qc \b R \b0 + \b Backspace \b0}";
            richTextBox2.Rtf = @"{\rtf\ansi\qc \b R \b0 + ( \b + \b0 / \b - \b0 / \b Egérgörgő \b0 )}";
            richTextBox3.Rtf = @"{\rtf\ansi\qc \b C \b0 + \b Backspace \b0}";
            richTextBox4.Rtf = @"{\rtf\ansi\qc \b C \b0 + ( \b + \b0 / \b - \b0 / \b Egérgörgő \b0 )}";
            richTextBox5.Rtf = @"{\rtf\ansi\qc \b A \b0  / \b \u8592\ \b0 + \b Shift \b0}";
            richTextBox6.Rtf = @"{\rtf\ansi\qc \b W \b0  / \b \u8593\ \b0 + \b Shift \b0}";
            richTextBox7.Rtf = @"{\rtf\ansi\qc \b S \b0  / \b \u8595\ \b0 + \b Shift \b0}";
            richTextBox8.Rtf = @"{\rtf\ansi\qc \b D \b0  / \b \u8594\ \b0 + \b Shift \b0}";
            richTextBox9.Rtf = @"{\rtf\ansi\qc \b Space \b0}";
            richTextBox10.Rtf = @"{\rtf\ansi\qc \b Enter \b0}";
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void richTextBox20_MouseMove(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }
    }
}
