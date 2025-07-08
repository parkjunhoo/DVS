using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Design;
using DevExpress.Skins;
using DevExpress.Skins.Info;
using DevExpress.XtraEditors;
using System.Reflection;
using System.IO;
using System.Linq;

namespace DVS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly asm = typeof(DevExpress.UserSkins.DsvSkin).Assembly;
            DevExpress.XtraEditors.WindowsFormsSettings.RegisterUserSkins(asm);

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("dsvSkin");
            Application.Run(new MainForm());
        }
    }
}