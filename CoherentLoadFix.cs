using System;
using System.IO;
using System.Runtime.InteropServices;
using MelonLoader;

namespace CoherentLoadFix;

public class CoherentLoadFix : MelonMod
{
    private static MelonPreferences_Category CoherentLoadFixPreferences;
    private static MelonPreferences_Entry<bool> CoherentLoadFixEnabled;
    private static MelonPreferences_Entry<bool> CoherentLoadFixHTTPServerEnabled;
    private static MelonPreferences_Entry<bool> CoherentLoadFixMediaDecodersEnabled;
    private static string DllDirectory = $"{Directory.GetCurrentDirectory()}\\ChilloutVR_Data\\Plugins\\x86_64\\";
    
    #region Win32 API Deps
    [DllImport("kernel32", SetLastError = true)]
    private static extern IntPtr LoadLibraryW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName);
    #endregion
    
    
    public override void OnApplicationStart()
    {
        CoherentLoadFixPreferences = MelonPreferences.CreateCategory("Coherent Load Fix");
        CoherentLoadFixEnabled = CoherentLoadFixPreferences.CreateEntry<bool>("Enabled", true, "Enable CoherentLoadFix");
        CoherentLoadFixHTTPServerEnabled = CoherentLoadFixPreferences.CreateEntry<bool>("HTTPServerEnabled", true, "Load the HttpServer plugin");
        CoherentLoadFixMediaDecodersEnabled = CoherentLoadFixPreferences.CreateEntry<bool>("MediaDecodersEnabled", true, "Load the MediaDecoders plugin");

        if (!CoherentLoadFixEnabled.Value) return;
        if (CoherentLoadFixHTTPServerEnabled.Value) LoadLibraryW($"{DllDirectory}HttpServer.WindowsDesktop.dll");
        if (CoherentLoadFixMediaDecodersEnabled.Value) LoadLibraryW($"{DllDirectory}MediaDecoders.WindowsDesktop.dll");
    }
}