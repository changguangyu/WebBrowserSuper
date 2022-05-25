using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
namespace WebBrowserSuper
{
    public static class SecurityManagerHelper
    {
        public static void SetWebBrowser(System.Windows.Forms.WebBrowser webBrowser)
        {
            // ServiceProvider 接口获取
            object obj = webBrowser.ActiveXInstance;
            SecurityManagerCOM.IServiceProvider sp = obj as SecurityManagerCOM.IServiceProvider;
            object ops;
            sp.QueryService(ref SecurityManagerCOM.SID_SProfferService, ref SecurityManagerCOM.IID_IProfferService, out ops);
            SecurityManagerCOM.IProfferService ps = ops as SecurityManagerCOM.IProfferService;
            int cookie = 0;
            ps.ProfferService(ref SecurityManagerCOM.IID_IInternetSecurityManager, new SecurityManager(), ref cookie);

        }


        class SecurityManager : SecurityManagerCOM.IServiceProviderForIInternetSecurityManager, SecurityManagerCOM.IInternetSecurityManager
        {
            #region IServiceProviderForIInternetSecurityManager 实现
            int SecurityManagerCOM.IServiceProviderForIInternetSecurityManager.QueryService(ref Guid guidService, ref Guid riid, out SecurityManagerCOM.IInternetSecurityManager ppvObject)
            {
                ppvObject = null;
                if (guidService == SecurityManagerCOM.IID_IInternetSecurityManager)
                {
                    ppvObject = this as SecurityManagerCOM.IInternetSecurityManager;
                    return SecurityManagerCOM.S_OK;
                }
                return SecurityManagerCOM.E_NOINTERFACE;
            }
            #endregion


            #region IInternetSecurityManager 实现
            int SecurityManagerCOM.IInternetSecurityManager.SetSecuritySite(SecurityManagerCOM.IInternetSecurityMgrSite pSite)
            {
                return SecurityManagerCOM.INET_E_DEFAULT_ACTION;
            }

            int SecurityManagerCOM.IInternetSecurityManager.GetSecuritySite(SecurityManagerCOM.IInternetSecurityMgrSite pSite)
            {
                return SecurityManagerCOM.INET_E_DEFAULT_ACTION;
            }

            int SecurityManagerCOM.IInternetSecurityManager.MapUrlToZone(String pwszUrl, out int pdwZone, int dwFlags)
            {
                pdwZone = (int)SecurityManagerCOM.URLZONE.URLZONE_TRUSTED;
                return SecurityManagerCOM.S_OK;
            }

            //private const string strSecurity = "file:\0\0\0\0";
            int SecurityManagerCOM.IInternetSecurityManager.GetSecurityId(string pwszUrl, IntPtr pbSecurityId, ref uint pcbSecurityId, uint dwReserved)
            {
                /* byte[] by =  System.Text.Encoding.ASCII.GetBytes(strSecurity) ;
                 Marshal.Copy(by, 0, pbSecurityId, by.Length);
                 pcbSecurityId = (uint)strSecurity.Length;

                return SecurityManagerCOM.S_OK;*/
                return SecurityManagerCOM.INET_E_DEFAULT_ACTION;
            }

            int SecurityManagerCOM.IInternetSecurityManager.ProcessUrlAction(String pwszUrl, int dwAction, out byte pPolicy, int cbPolicy, byte pContext, int cbContext, int dwFlags, int dwReserved)
            {
                pPolicy = (int)SecurityManagerCOM.URLPOLICY_ALLOW;
                return SecurityManagerCOM.S_OK;
            }

            int SecurityManagerCOM.IInternetSecurityManager.QueryCustomPolicy(String pwszUrl, ref Guid guidKey, byte ppPolicy, int pcbPolicy, byte pContext, int cbContext, int dwReserved)
            {
                return SecurityManagerCOM.INET_E_DEFAULT_ACTION;
            }

            int SecurityManagerCOM.IInternetSecurityManager.SetZoneMapping(int dwZone, String lpszPattern, int dwFlags)
            {
                return SecurityManagerCOM.INET_E_DEFAULT_ACTION;
            }

            int SecurityManagerCOM.IInternetSecurityManager.GetZoneMappings(int dwZone, out IEnumString ppenumString, int dwFlags)
            {
                ppenumString = null;
                return SecurityManagerCOM.INET_E_DEFAULT_ACTION;
            }
            #endregion
        }

    }
}
