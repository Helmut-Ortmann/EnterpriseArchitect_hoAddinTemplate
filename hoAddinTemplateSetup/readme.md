# ho Addin Template

## Installation

- Run hoAddinTemplate.msi
  - Advanced
  - Follow default values to install without Admin rights

### Requirements

-  Windows, 
-  .net framework 4.6.2


## Development

-  Visual Studio 2017 community
-  WIX 3.11 for creating deployment packages

### Deployment

-  Remember to update AssemblyVersion in Properties.AssemblyInfo.cs
   - [assembly: AssemblyVersion("1.0.0.0")]
   - [assembly: AssemblyFileVersion("1.0.0.0")]
-  Remember to update AssemblyVersion 
   - hoAddinTemplate_Setup/Wxs/Files.wxs 
     - COM Components
	   - hoAddinTemplate.Root  (initial starting the Addin)
	   - hoAddinTemplate.Gui   (Gui as COM object to fit in the EA Add-In Window/Tab)
-  Remember to update Assemebly version 
   -  hoAddinTemplate_Setup/Wxs/Files.wxs
-  Install with or without admin rights is possible
   -  per user
   -  per machine

     
	   


## Glossary


## Reference 






 