using System;

using System.Runtime.InteropServices;
using System.Security;

namespace OrgManagement.Helpers
{
    public static class SecureStringHelper
    {
        public static string? GetString(this SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);

                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
