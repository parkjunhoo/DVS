using DevExpress.XtraEditors;
using ClosedXML.Excel;
using DVS.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace DVS
{
    public partial class MainForm : XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void btn_openCheckPrice_Click(object sender, EventArgs e)
        {
            this.Hide();

            CheckPriceForm cpf = new CheckPriceForm();
            cpf.WindowState = FormWindowState.Maximized;
            cpf.FormClosed += (s, ev) => { this.Show(); };
            cpf.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
