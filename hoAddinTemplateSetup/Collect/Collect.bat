rem ---------------------------------------------------
rem Collect/ Harvest information to register COM dlls needed by EA
rem ---------------------------------------------------
rem Check path to WIX Toolset
rem 
rem Attention: 
rem - 1. First build application in Release mode
rem - 2. Run Collect.bat
rem - 3. Build once more to ensure the changed registration information is part of the dll.
rem 
rem Replace the content between <Component> and </Component> in file-wxs with generated information
rem Only for COM objects:
rem - Main DLL
rem - Every DLL which is registered to EA to run in AddIn Window or as own Window
rem - Replace: Source="SourceDir\release\hoAddinTemplateRoot.dll" /> by
rem            Name="hoAddinTemplateRoot.dll" Source="$(var.hoAddinTemplateRoot.TargetPath)" />
rem            Change name accordingly


SET WIX=C:\Program Files (x86)\WiX Toolset v3.11\bin\heat
del *.wxs

"%WIX%" file ..\..\hoAddinTemplateRoot\bin\release\hoAddinTemplateRoot.dll -ag -template fragment -out hoAddinTemplateRoot.wxs
"%WIX%" file ..\..\hoAddinTemplateGui\bin\release\hoAddinTemplateGui.dll  -ag -template fragment -out hoAddinTemplateGui.wxs

dir
