using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IfManGui.Settings;

namespace IfManRoot
{
    // Make sure Project Properties for Release has the Entry: 'Register for COM interop'
    // You may check registration with: https://community.sparxsystems.com/community-resources/772-ea-installation-inspector
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("9DB59F66-C4CD-4188-9388-2053C29AA0D7")]
    // ProgID is the same as the string to register for EA in 'Sparx Systems:EAAddins:AddInSimple:Default'
    // In description: 'Namespace.ClassName'
    // EA uses always the ProId.
    [ProgId("IfManRoot.IfManRoot")]
    public class IfManRoot :EaAddinFramework.EaAddinBase
    {
        private EA.Repository _rep;
        private IfManGui.IfManGui _gui;


        // settings
        readonly Setting _settings;


        //---------------------------------------------------
        // Menu
        const string MenuName = "-&IfManager";
        const string MenuAbout = "About";



        public IfManRoot()
        {
            try
            {
                // New settings (Merge IfManGui.dll.config with user.cofig, read from user.config
                _settings = new Setting();

                // Initialize Menu system
                MenuHeader = MenuName;
                MenuOptions = new[] { 
                    MenuAbout};

            }
            catch (Exception e)
            {
                MessageBox.Show($@"Error setup 'IfManRoot' Addin. Error:{Environment.NewLine}{e}",
                    @"IfManager Installation error");
            }
        }
        // ReSharper disable once RedundantOverriddenMember
        /// <summary>
        /// EA_Connect events enable Add-Ins to identify their type and to respond to Enterprise Architect start up.
        /// This event occurs when Enterprise Architect first loads your Add-In. Enterprise Architect itself is loading at this time so that while a Repository object is supplied, there is limited information that you can extract from it.
        /// The chief uses for EA_Connect are in initializing global Add-In data and for identifying the Add-In as an MDG Add-In.
        /// Also look at EA_Disconnect.
        /// </summary>
        /// <param name="repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <returns>String identifying a specialized type of Add-In: 
        /// - "MDG" : MDG Add-Ins receive MDG Events and extra menu options.
        /// - "" : None-specialized Add-In.</returns>
        public override string EA_Connect(EA.Repository repository)
        {
            return base.EA_Connect(repository);
        }
        #region EA_OnPostInitialized
        public override void EA_OnPostInitialized(EA.Repository repository)
        {
            _rep = repository;
            RegisterAddInAsTab();
            //RegisterAddInAsWindow();

        }
        // ReSharper disable once InconsistentNaming
        public override void EA_FileOpen(EA.Repository Repository)
        {
            InitializeForRepository(Repository);
        }
        // ReSharper disable once InconsistentNaming
        public override void EA_FileClose(EA.Repository Repository)
        {
            InitializeForRepository(null);


        }
        #region initializeForRepository
        /// <summary>
        /// Initialize repositories
        /// </summary>
        /// <param name="rep"></param>
        void InitializeForRepository(EA.Repository rep)
        {
            _rep = rep;
            // propagate Repository to GUI
            if (_gui != null) _gui.Rep = rep;


           try
            {
                
               
            }
            catch (Exception e)
            {
                MessageBox.Show($@"{e.Message}", @"IfManager: Error initializing repository");
            }


        }
        #endregion
        private void RegisterAddInAsTab()
        {
            if (_gui == null)
            {
                try
                {
                    _gui = _rep.AddWindow(IfManGui.IfManGui.TabName, IfManGui.IfManGui.ProgId);
                    if (_gui == null)
                    {
                        MessageBox.Show($"TabName:'{IfManGui.IfManGui.TabName}'\r\nProgId:'{IfManGui.IfManGui.ProgId}'",
                            "Can't install Add-In 'IfManager' as Tab");
                        
                    }
                    _gui.Settings = _settings;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"TabName:'{IfManGui.IfManGui.TabName}'\r\nProgId:'{IfManGui.IfManGui.ProgId}'\r\n\r\n{e}", 
                        "Can't install Add-In 'IfManager'");
                    throw;
                }
            }
        }

        private void RegisterAddInAsWindow()
        {
            try
            {
               _gui =  _rep.AddTab(IfManGui.IfManGui.TabName, IfManGui.IfManGui.ProgId);
                if (_gui == null)
                {
                    MessageBox.Show($"TabName:'{IfManGui.IfManGui.TabName}'\r\nProgId:'{IfManGui.IfManGui.ProgId}'", "Can't install Add-In 'IfManager' as Window");
                }
                ;
            }
            catch (Exception e)
            {
                MessageBox.Show($"TabName:'{IfManGui.IfManGui.TabName}'\r\nProgId:'{IfManGui.IfManGui.ProgId}'\r\n\r\n{e}", "Can't show Add-In 'IfManager' in EA.");
                throw;
            }
        }

        #endregion

/// <summary>
        /// called when the selected item changes
        /// This operation will show the guid of the selected element in the eaControl
        /// </summary>
        /// <param name="repository">the EA.rep</param>
        /// <param name="guid">the guid of the selected item</param>
        /// <param name="objectType">the object type of the selected item</param>
        public override void EA_OnContextItemChanged(EA.Repository repository, string guid, EA.ObjectType objectType)
        {
            if (objectType == EA.ObjectType.otElement)
            {
                
            }

        }
       
        /// <summary>
        /// Called once Menu has been opened to see what menu items should active.
        /// </summary>
        /// <param name="repository">the repository</param>
        /// <param name="location">the location of the menu</param>
        /// <param name="menuName">the name of the menu</param>
        /// <param name="itemName">the name of the menu item</param>
        /// <param name="isEnabled">boolean indicating whether the menu item is enabled</param>
        /// <param name="isChecked">boolean indicating whether the menu is checked</param>
        public override void EA_GetMenuState(EA.Repository repository, string location, string menuName, string itemName, ref bool isEnabled, ref bool isChecked)
        {
            if (IsProjectOpen(repository))
            {
                switch (itemName)
                {
       

                    // About information
                    case MenuAbout:
                        isEnabled = true;
                        break;


                    // there shouldn't be any other, but just in case disable it.
                    default:
                        isEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                isEnabled = false;
            }
        }

        /// <summary>
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        /// </summary>
        /// <param name="repository">the repository</param>
        /// <param name="location">the location of the menu</param>
        /// <param name="menuName">the name of the menu</param>
        /// <param name="itemName">the name of the selected menu item</param>
        public override void EA_MenuClick(EA.Repository repository, string location, string menuName, string itemName)
        {
          

            switch (itemName)
            {
                

                // run LINQ XML query for own EA queries which are stored in *.xml
                case MenuAbout:

                    // get product version
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                    // Get file-version of dll
                    string pathRoot = Assembly.GetExecutingAssembly().Location;
                    pathRoot = Path.GetDirectoryName(pathRoot);

                    string productVersion = $"{fileVersionInfo.ProductVersion}{Environment.NewLine}";
                    string pathIfManRoot = Path.Combine(new[] { pathRoot, "IfManRoot.dll" });
                    string pathIfManGui = Path.Combine(new[] { pathRoot, "IfManGui.dll" });
                    string pathIfManUtils = Path.Combine(new[] { pathRoot, "IfUtils.dll" });

                    MessageBox.Show($@"Product version: {productVersion}
IfManRoot.dll  {FileVersionInfo.GetVersionInfo(pathIfManRoot).FileVersion}
IfManGui.dll   {FileVersionInfo.GetVersionInfo(pathIfManGui).FileVersion}
IfManUtils.dll   {FileVersionInfo.GetVersionInfo(pathIfManUtils).FileVersion}

IfManager
Helmut.Ortmann@t-online.de
+49 172 51 79 16 7
", "IfManager, Interface Manager");
                    break;






            }
        }
    }
}

