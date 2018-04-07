using System.Runtime.InteropServices;

namespace hoAddinTemplateGui
{
    /// <summary>
    /// Interface for EA COM Gui. 
    /// </summary>
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("BE999EE3-CDFF-4A6C-B8DB-C419F457EBF0")]
    interface IhoAddinTemplateGui
    {
        string GetName();
    }
}
