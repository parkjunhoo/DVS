using ClosedXML.Excel;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Skins.Info;
using DevExpress.XtraEditors;
using DVS.Forms;
using System.Reflection;
using System.Linq;

namespace DVS
{
    public partial class MainForm : XtraForm
    {
        public MainForm()
        {
            // 디자인 타임에도 스킨을 적용하기 위한 초기화
            InitializeSkin();
            InitializeComponent();
        }

        private void InitializeSkin()
        {
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
