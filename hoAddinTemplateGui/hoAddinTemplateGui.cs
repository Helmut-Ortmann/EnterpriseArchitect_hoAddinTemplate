using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using hoAddinTemplateGui.Settings;
using hoAddinTemplateUtil.General;

namespace hoAddinTemplateGui
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("358015AA-734E-4E27-B518-D6C71903EC4E")]
    [ProgId(ProgId)]
    [ComDefaultInterface(typeof(IhoAddinTemplateGui))]
    public partial class Gui : UserControl, IhoAddinTemplateGui
    {
        public const string ProgId = "hoAddinTemplate.Gui";
        public const string TabName = "hoAddinTemplate";

        private EA.Repository _rep;
        private Setting _settings;

        public Setting Settings
        {
            get => _settings;
            set => _settings = value;
        }

        public Gui()
        {
            InitializeComponent();
        }
        public string GetName()
        {
            return TabName;
        }

        /// <summary>
        /// Update Repository when Model new loaded
        /// - Requirement
        /// - Component
        /// </summary>
        public EA.Repository Rep
        {
            get => _rep;
            set => _rep = value;
        }

        private void settingsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Basic.StartFile(Settings.SettingsPath);
        }

        private void settingsFacturyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Basic.StartFile(Settings.SettingsFacturyPath);
        }

        private void resetToFacturySettingsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
           Settings.JsonBackup();
          
        }

        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string[] dllNames = new string[]
            {
                
                "hoAddinTemplateRoot.dll",
                "hoAddinTemplateUtil.dll",
                "hoAddinTemplateUtil.dll"
            };
            string userData = _settings.SettingsPath;
            string description =
                $"hoAddinTemplate\r\n- Show a simple running EA Add-In with GUI\r\n\r\nRep:\t\t{_rep.ConnectionString}\r\n";
            Generals.AboutMessage(description, "About hoAddinTemplate ", dllNames, userData);
        }

        private void readmeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Debug.Assert(Settings.SettingsFacturyPath != null, "Settings.SettingsFacturyPath != null");
            string pathReadmeMd = Path.Combine(Path.GetDirectoryName(Settings.SettingsFacturyPath), "readme.md");
            Basic.StartFile(pathReadmeMd);
        }
    }
}
