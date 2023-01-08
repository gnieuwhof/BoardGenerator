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

            this.ScrollToEnd();
        }

        private void ScrollToEnd()
        {
            this.logTxt.SelectionStart = this.logTxt.TextLength;

            this.logTxt.ScrollToCaret();
        }

        private void LogFrm_Shown(object sender, EventArgs e)
        {
            this.ScrollToEnd();
        }
    }
}
