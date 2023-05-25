
using System.Security.Claims;
using BibliotecaSys.Domain.Models;

namespace BibliotecaSys.Domain.Common;

/// <summary>
///     Class that defines the constant values for every object that needs to be static value in the entire app.
/// </summary>
public static class Constants
{
    /// <summary>
    ///     Application cookies.
    /// </summary>
    public static string AppCookies { get; } = AppDomain.CurrentDomain.FriendlyName + "_APP";

    /// <summary>
    ///     Application domain.
    /// </summary>
    public static string Domain { get; } = "unphu.edu.do";

    /// <summary>
    ///     Gets the application's execution directory path.
    ///     If unable to retrieve it, returns the system's temporary folder path.
    /// </summary>
    public static string AppPath
    {
        get
        {
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(exePath);
            return directory ?? Path.GetTempPath();
        }
    }
}