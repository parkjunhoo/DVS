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
            // 등록된 스킨 목록 출력 (디버깅용)
            ListAvailableSkins();
        }

        /// <summary>
        /// 사용 가능한 스킨 목록을 출력합니다.
        /// </summary>
        private void ListAvailableSkins()
        {
            var skinCollection = SkinManager.Default.Skins;
            System.Diagnostics.Debug.WriteLine("사용 가능한 스킨 목록:");
            foreach (SkinContainer skin in skinCollection)
            {
                System.Diagnostics.Debug.WriteLine($"- {skin.SkinName}");
            }
        }

        /// <summary>
        /// 지정된 스킨을 적용합니다.
        /// </summary>
        /// <param name="skinName">스킨 이름</param>
        public void ApplySkin(string skinName)
        {
            UserLookAndFeel.Default.SetSkinStyle(skinName);
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
