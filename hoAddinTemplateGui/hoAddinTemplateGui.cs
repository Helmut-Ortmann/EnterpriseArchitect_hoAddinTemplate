using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using hoAddinTemplate.Settings;
using hoAddinTemplateUtil.General;

namespace hoAddinTemplate
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("00826A14-89DE-4702-B998-E0F144DFA8C5")]
    [ProgId(ProgId)]
    [ComDefaultInterface(typeof(IhoAddinTemplate))]
    public partial class hoAddinTemplate : UserControl, IhoAddinTemplate
    {
        public const string ProgId = "hoAddinTemplate.hoAddinTemplate";
        public const string TabName = "hoAddinTemplate";

        private EA.Repository _rep;
        private Setting _settings;

        public Setting Settings
        {
            get => _settings;
            set => _settings = value;
        }

        public hoAddinTemplate()
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
                "hoAddinTemplate.dll",
                "hoAddinTemplateRoot.dll",
                "hoAddinTemplateUtil.dll",
            };
            string userData = _settings.SettingsPath;
            string description =
                $"Interface Management\r\n- Get Interface of a Component\r\n\r\nRep:\t\t{_rep.ConnectionString}\r\n";
            Generals.AboutMessage(description, "About hoAddinTemplate (ho Addin Template)", dllNames, userData);
        }
    }
}
