using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserSuper
{
    public partial class WebBrowserEx: WebBrowser
    {
        static WebBrowserEx()
        {
            WebBrowserHelper.EnableIE11();
            WebBrowserHelper.EnableDPIAware();
            WebBrowserHelper.EnableLongScript();
        }
        public WebBrowserEx()
        {
            InitializeComponent();
        }

        protected override void OnNavigating(WebBrowserNavigatingEventArgs e)
        {
            SecurityManagerHelper.SetWebBrowser(this);            
            base.OnNavigating(e);
            
        }
    }
}
