using System.Runtime.InteropServices;

namespace hoAddinTemplate
{
    /// <summary>
    /// Interface for EA COM 
    /// </summary>
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("A3F57B13-30B0-4B01-A49C-DE9789E37A95")]
    interface IhoAddinTemplate
    {
        string GetName();
    }
}
