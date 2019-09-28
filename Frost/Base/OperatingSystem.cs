using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace FrostDB.Base
{
    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static OSPlatform GetOSPlatform()
        {
            var os = new OSPlatform();

            if (OperatingSystem.IsWindows())
            {
                os = OSPlatform.Windows;
            }

            if (OperatingSystem.IsLinux())
            {
                os = OSPlatform.Linux;
            }

            return os;
        }
    }
}
