# Interface Manager

## History

- 24. May 2018 Created as reusable EA Addin Platform

## Installation

- Run IfManager.msi
  - Advanced
  - Follow default values to install without Admin rights

### Requirements

-  Windows, .net framework 4.6.2


## Development

-  Visual Studio 2017 community

### Deployment

-  Remember to update AssemblyVersion in Properties.AssemblyInfo.cs
   - [assembly: AssemblyVersion("1.0.0.0")]
   - [assembly: AssemblyFileVersion("1.0.0.0")]
-  Remember to update AssemblyVersion 
   - IfManSetup/Wxs/Files.wxs 
     - COM Components
	   - IfManRoot
	   - IfManGui
-  Remember to update Productversion 
   -  IfManSetup/Wxs/Files.wxs
-  Install without admin rights is possible

     
	   


## Glossary


## Reference 






 