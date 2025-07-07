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
            var skinCreator = new SkinBlobXmlCreator("dsvskin", "DVS.Skins", typeof(Program).Assembly, null);
            DevExpress.XtraEditors.WindowsFormsSettings.RegisterUserSkin(skinCreator);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("dsvskin");

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}