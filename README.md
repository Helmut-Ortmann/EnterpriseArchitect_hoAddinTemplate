# EnterpriseArchitect_hoAddinTemplate

Template for EA Addins with GUI written in C#. The features are:

-  Add-In with GUI
   -  Add-In Tab 
   -  Add-In Window
-  Settings in *.json
-  WIX installer for admin and non admin install
   - Single Package authoring
     -  per user install
	 -  per machine install
	 -  GUI to install
   - Build *.msi with product number


My environemt:

- Windows
- VS 2017 community as IDE
- WIX 3.11 for installer package


   If you have ideas, requests or advice feel free to contact me or participate.

   Best regards,

   Helmut

 ## Make your Addin


   1.  Open in Visual Studio
   2.  Update References 
       - interop.EA.dll
       - Newtonsoft.Json (Package Manager)
       - WixUIExtension (hoAddinTemplateSetup to c:\Program Files (x86)\WiX Toolset v3.11\bin\WixUIExtension.dll, my installation)
       - Project properties: hoAddinTemplateRoot, Debug, Start external program=C:\Program Files (x86)\Sparx Systems\EA\EA.exe, my installation)
   3.  Build and test as release and debug
