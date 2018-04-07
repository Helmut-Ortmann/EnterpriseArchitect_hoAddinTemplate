using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IfManGui.Settings;
using IfManUtil.General;

namespace IfManGui
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("00826A14-89DE-4702-B998-E0F144DFA8C5")]
    [ProgId(ProgId)]
    [ComDefaultInterface(typeof(IIfManGui))]
    public partial class IfManGui : UserControl, IIfManGui
    {
        public const string ProgId = "IfManGui.IfManGui";
        public const string TabName = "IfManager";

        private EA.Repository _rep;
        private Setting _settings;

        public Setting Settings
        {
            get => _settings;
            set => _settings = value;
        }

        public IfManGui()
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
                "IfManGui.dll",
                "IfManRoot.dll",
                "IfManUtil.dll",
            };
            string userData = _settings.SettingsPath;
            string description =
                $"Interface Management\r\n- Get Interface of a Component\r\n\r\nRep:\t\t{_rep.ConnectionString}\r\n";
            Generals.AboutMessage(description, "About IfManager (Interface Manager)", dllNames, userData);
        }
    }
}
