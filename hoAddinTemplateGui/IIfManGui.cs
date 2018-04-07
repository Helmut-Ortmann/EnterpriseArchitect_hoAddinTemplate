using System.Runtime.InteropServices;

namespace IfManGui
{
    /// <summary>
    /// Interface for EA COM 
    /// </summary>
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("A3F57B13-30B0-4B01-A49C-DE9789E37A95")]
    interface IIfManGui
    {
        string GetName();
    }
}
