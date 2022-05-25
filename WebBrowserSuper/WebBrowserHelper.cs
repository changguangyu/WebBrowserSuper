using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowserSuper
{
    public static class WebBrowserHelper
    {
        private static readonly string currentProcName = Process.GetCurrentProcess().ProcessName + ".exe";

        public static bool EnableIE11()
        {
            String regPath = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            try
            {
                Registry.SetValue(regPath, currentProcName, 0x2EDF, RegistryValueKind.DWord);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool EnableDPIAware()
        {
            String regPath = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_96DPI_PIXEL";
            try
            {
                Registry.SetValue(regPath, currentProcName, 1, RegistryValueKind.DWord);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EnableLongScript()
        { 
            String regTimeOutPath = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Styles";
            try
            {
                Registry.SetValue(regTimeOutPath, "MaxScriptStatements", unchecked((int)UInt32.MaxValue), RegistryValueKind.DWord);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
