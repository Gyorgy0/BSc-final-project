namespace Szakdolgozat.HelpForms
{
    public partial class OperationsInfo : Form
    {
        public OperationsInfo()
        {
            InitializeComponent();
            this.ActiveControl = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi A transzponálás során egy kiválasztott mátrix sorait és oszlopait felcseréljük. Az m*n-es mátrix transzponáltja n*m-es lesz. \line Amennyiben a mátrix sorainak és oszlopainak száma megegyezik, abban az esetben a transzponált mátrix az eredeti mátrix átlója szerint lesz tükrözve.\line \line Egy mátrix szimmetrikus, ha a transzponáltja önmaga, szimmetrikus mátrix pedig csak négyzetes mátrix lehet (sorainak és oszlopainak száma megegyezik).\line\line\line Példa:\line\line		[ 1 2 3 ]       =>       [ 1 4 ]\line		[ 4 5 6 ]       =>       [ 2 5 ]\line				[ 3 6 ] }";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi A függőleges tükrözés során a kiválasztott mátrix elemeit felcseréljük olyan módon, hogy amely az 1. sorba volt, azt átrakjuk az m. sorba, amelyik a 2. sorba, azt pedig az (m-1). sorba.\line\line\line Példa:\line\line		[ 1 2 3 ]       =>       [ 4 5 6 ]\line		[ 4 5 6 ]       =>       [ 1 2 3 ] \line \line \line A vízszintes tükrözés során a kiválasztott mátrix elemeit felcseréljük olyan módon, hogy amely az 1. oszlopba volt, azt átrakjuk az m. oszlopba, amelyik a 2. oszlopba, azt pedig az (m-1). oszlopba.\line\line\line Példa:\line\line		[ 1 2 3 ]       =>       [ 3 2 1 ]\line		[ 4 5 6 ]       =>       [ 6 5 4 ] }";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Csak azonos dimenziójú mátrixok adhatóak össze. Tehát a két mátrixnak ugyanannyi sorának és oszlopának kell lennie. \line\line\line Példa:\line\line		[ 1 2 3 ]       +       [ 1 2 3 ]       =>      [ 1+1 2+2 3+3 ]      =>      [ 2 4 6 ]\line		[ 1 1 1 ]       +       [ 2 2 2 ]       =>      [ 1+2 1+2 1+2 ]      =>      [ 3 3 3 ]}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Két mátrix csak akkor szorozható össze, ha a bal oldali mátrix oszlopainak száma megegyezik a jobb oldali mátrix sorainak számával. \line\line\line Példa:\line\line		[ 1 2 ]       *       [ 1 ]       =>      [ (1*1) + (2*2) ]      =>      [ 5 ]\line			 *       [ 2 ]       =>			  =>}";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi A kiválasztott mátrix összes elemét megszorozzuk egy számmal. \line\line\line Példa:\line\line		(-2)       *       [ 1 2 ]       =>      [ (1*(-2)) + (2*(-2)) ]      =>      [ (-2) (-4) ]}";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Megkeressük a kiválasztott mátrixban található legnagyobb értékű számot. \line\line\line Példa:\line\line		[ 7 2 4 ] \line		[ 6 7 3 ] \line		[ 7 8 \b 9 \b0 ] \line \line A legnagyobb értékű szám a mátrixban a \b 9 \b0 .}";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Megkeressük a kiválasztott mátrixban található legkisebb értékű számot. \line\line\line Példa:\line\line		[ 7 \b 2 \b0 4 ] \line		[ 6 7 3 ] \line		[ 7 8 9 ] \line \line A legkisebb értékű szám a mátrixban a \b 2 \b0 .}";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Megkeressük a kiválasztott mátrixban található páros számokat, majd ezeket összeszámoljuk. \line Páros számoknak tekintünk minden olyan egész számot, amely 2-vel osztva nem ad maradékot. \line\line\line Példa:\line\line		[ 7 \b 2 4 \b0 ] \line		[ \b 6 \b0 7 3 ] \line		[ 7 \b 8 \b0 9 ] \line \line A mátrixban \b 4 \b0 páros szám található.}";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Megkeressük a kiválasztott mátrixban található páros számokat, majd ezeket összeszámoljuk. \line Páros számoknak tekintünk minden olyan egész számot, amely 2-vel osztva ad maradékot. \line\line\line Példa:\line\line		[ \b 7 \b0 2 4 ] \line		[ 6 \b 7 3 \b0 ] \line		[ \b 7 \b0 8 \b 9 \b0 ] \line \line A mátrixban \b 5 \b0 páratlan szám található.}";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi Megkeressük a kiválasztott mátrixban a megadott számot, majd összeszámoljuk, hogy mennyi van belőle. \line Megadott szám: \b 7 \b0 \line\line\line Példa:\line\line		[ \b 7 \b0 2 4 ] \line		[ 6 \b 7 \b0 3 ] \line		[ \b 7 \b0 8 9 ] \line \line A mátrixban \b 3 \b0-szor fordul elő a megadott szám.}";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button Sender = sender as Button;
            label1.Text = Sender.Text;
            richTextBox1.Rtf = @"{\rtf\ansi A kiválasztott mátrix sorait rendezzük növekvő sorrendbe: \line\line\line Példa:\line\line		[ 7 2 4 ]       =>      [ 2 4 7 ] \line		[ 6 7 3 ]       =>      [ 3 6 7 ] \line		[ 7 8 9 ]       =>      [ 7 8 9 ] \line \line A kiválasztott mátrix sorait rendezzük csökkenő sorrendbe: \line\line\line Példa:\line\line		[ 7 2 4 ]       =>      [ 7 4 2 ] \line		[ 6 7 3 ]       =>      [ 7 6 3 ] \line		[ 7 8 9 ]       =>      [ 9 8 7 ]}";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            {
                Button Sender = sender as Button;
                label1.Text = Sender.Text;
                richTextBox1.Rtf = @"{\rtf\ansi A kiválasztott mátrix oszlopait rendezzük növekvő sorrendbe: \line\line\line Példa:\line\line		[ 7 2 4 ]       =>      [ 6 2 3 ] \line		[ 6 7 3 ]       =>      [ 7 7 4 ] \line		[ 7 8 9 ]       =>      [ 7 8 9 ] \line \line A kiválasztott mátrix oszlopait rendezzük csökkenő sorrendbe: \line\line\line Példa:\line\line		[ 7 2 4 ]       =>      [ 7 8 9 ] \line		[ 6 7 3 ]       =>      [ 7 7 4 ] \line		[ 7 8 9 ]       =>      [ 6 2 3 ]}";
            }
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
