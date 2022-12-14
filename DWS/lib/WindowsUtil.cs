using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DWS.lib
{
    internal class WindowsUtil
    {
        public static object ReadSubKeyValue(string subKey, string keyName)
        {
            var rkSubKey = Registry.LocalMachine.OpenSubKey(subKey);
            if (rkSubKey == null)
            {
                MessageBox.Show($@"Error while reading registry key: {subKey}\{keyName} does not exist!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            try
            {
                var result = rkSubKey.GetValue(keyName);
                rkSubKey.Close();
                return result;

            }
            catch (Exception ex)   //This exception is thrown
            {
                MessageBox.Show(
                    $@"Error while reading registry key: {subKey} param: {keyName}. ErrorMessage: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rkSubKey.Close();
                return null;
            }
        }
        public static int GetWindowsBuildNumber()
        {
            return Convert.ToInt32(ReadSubKeyValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\", "CurrentBuildNumber"));
        }
        public static bool UAC_Status()
        {
            return !Convert.ToBoolean(ReadSubKeyValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\", "EnableLUA"));
        }
        public static int SystemRestore_Status()
        {
            return Convert.ToInt32(ReadSubKeyValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore\", "RPSessionInterval"));
        }
        public static string GetProductName()
        {
            return Convert.ToString(ReadSubKeyValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\", "ProductName"));
        }
        public static string GetSystemBuild()
        {
            return Convert.ToString(ReadSubKeyValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\", "BuildLabEx"));
        }
    }
}