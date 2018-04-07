using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using hoAddinTemplateGui.Settings;

namespace hoAddinTemplateRoot
{
    // Make sure Project Properties for Release has the Entry: 'Register for COM interop'
    // You may check registration with: https://community.sparxsystems.com/community-resources/772-ea-installation-inspector
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("7FFFBAAB-EC10-4937-AA0D-E355DC97089B")]
    // ProgID is the same as the string to register for EA in 'Sparx Systems:EAAddins:AddInSimple:Default'
    // In description: 'Namespace.ClassName'
    // EA uses always the ProId.
    [ProgId("hoAddinTemplate.Root")]
    public class Root :EaAddinFramework.EaAddinBase
    {
        private EA.Repository _rep;
        private hoAddinTemplateGui.Gui _gui;


        // settings
        readonly Setting _settings;


        //---------------------------------------------------
        // Menu
        const string MenuName = "-&hoAddinTemplate";
        const string MenuAbout = "About";



        public Root()
        {
            try
            {
                // New settings (Merge hoAddinTemplate.dll.config with user.cofig, read from user.config
                _settings = new Setting();

                // Initialize Menu system
                MenuHeader = MenuName;
                MenuOptions = new[] { 
                    MenuAbout};

            }
            catch (Exception e)
            {
                MessageBox.Show($@"Error setup 'hoAddinTemplateRoot' Addin. Error:{Environment.NewLine}{e}",
                    @"hoAddinTemplate Installation error");
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
                MessageBox.Show($@"{e.Message}", @"hoAddinTemplate: Error initializing repository");
            }


        }
        #endregion
        private void RegisterAddInAsTab()
        {
            if (_gui == null)
            {
                try
                {
                    _gui = _rep.AddWindow(hoAddinTemplateGui.Gui.TabName, hoAddinTemplateGui.Gui.ProgId);
                    if (_gui == null)
                    {
                        MessageBox.Show($"TabName:'{hoAddinTemplateGui.Gui.TabName}'\r\nProgId:'{hoAddinTemplateGui.Gui.ProgId}'",
                            "Can't install Add-In 'hoAddinTemplate' as Tab");
                        
                    }
                    _gui.Settings = _settings;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"TabName:'{hoAddinTemplateGui.Gui.TabName}'\r\nProgId:'{hoAddinTemplateGui.Gui.ProgId}'\r\n\r\n{e}", 
                        "Can't install Add-In 'hoAddinTemplate'");
                    throw;
                }
            }
        }

        private void RegisterAddInAsWindow()
        {
            try
            {
               _gui =  _rep.AddTab(hoAddinTemplateGui.Gui.TabName, hoAddinTemplateGui.Gui.ProgId);
                if (_gui == null)
                {
                    MessageBox.Show($"TabName:'{hoAddinTemplateGui.Gui.TabName}'\r\nProgId:'{hoAddinTemplateGui.Gui.ProgId}'", "Can't install Add-In 'hoAddinTemplate' as Window");
                }
                ;
            }
            catch (Exception e)
            {
                MessageBox.Show($"TabName:'{hoAddinTemplateGui.Gui.TabName}'\r\nProgId:'{hoAddinTemplateGui.Gui.ProgId}'\r\n\r\n{e}", "Can't show Add-In 'hoAddinTemplate' in EA.");
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
                    string pathhoAddinTemplateRoot = Path.Combine(new[] { pathRoot, "hoAddinTemplateRoot.dll" });
                    string pathhoAddinTemplate = Path.Combine(new[] { pathRoot, "hoAddinTemplate.dll" });
                    string pathhoAddinTemplateUtils = Path.Combine(new[] { pathRoot, "IfUtils.dll" });

                    MessageBox.Show($@"Product version: {productVersion}
hoAddinTemplateRoot.dll  {FileVersionInfo.GetVersionInfo(pathhoAddinTemplateRoot).FileVersion}
hoAddinTemplate.dll   {FileVersionInfo.GetVersionInfo(pathhoAddinTemplate).FileVersion}
hoAddinTemplateUtils.dll   {FileVersionInfo.GetVersionInfo(pathhoAddinTemplateUtils).FileVersion}

hoAddinTemplate
Helmut.Ortmann@t-online.de
+49 172 51 79 16 7
", "hoAddinTemplate, Interface Manager");
                    break;






            }
        }
    }
}

