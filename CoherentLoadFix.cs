/**
    ReSharper disable once InvalidXmlDocComment
    
    CoherentLoadFix - A ChilloutVR mod to fix Coherent Gameface's Native Imports
    Copyright (C) 2022  [information redacted]

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

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