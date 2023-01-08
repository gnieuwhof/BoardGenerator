namespace BoardGenerator
{
    using System.Windows.Forms;

    public partial class LogFrm : Form
    {
        public LogFrm()
        {
            InitializeComponent();
        }


        public void Update(string log)
        {
            this.logTxt.Text = log;
            this.logTxt.SelectionStart = this.logTxt.TextLength;
            this.logTxt.ScrollToCaret();
        }
    }
}
