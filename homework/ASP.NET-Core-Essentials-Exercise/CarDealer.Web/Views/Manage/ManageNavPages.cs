using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CarDealer.Web.Views.Manage
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext ViewContext) => PageNavClass(ViewContext, Index);

        public static string ChangePasswordNavClass(ViewContext ViewContext) => PageNavClass(ViewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext ViewContext) => PageNavClass(ViewContext, ExternalLogins);

        public static string TwoFactorAuthenticationNavClass(ViewContext ViewContext) => PageNavClass(ViewContext, TwoFactorAuthentication);
    
        public static string PageNavClass(ViewContext ViewContext, string page)
        {
            var activePage = ViewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary ViewData, string activePage) => ViewData[ActivePageKey] = activePage;
    }
}
