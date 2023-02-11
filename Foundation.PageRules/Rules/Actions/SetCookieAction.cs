using System;
using System.Web;
using Foundation.PageRules.Rules.RuleContext;
using Sitecore.Diagnostics;
using Sitecore.Rules.Actions;

namespace Foundation.PageRules.Rules.Actions
{
    public class SetCookieAction<T> : RuleAction<T> where T : PageRulesRuleContext
    {
        public string CookieName { get; set; }
        public string CookieValue { get; set; }
        public string CookieExpiry { get; set; }

        public override void Apply(T ruleContext)
        {
            Assert.IsNotNull(HttpContext.Current, "HttpContext.Current");
            Assert.IsNotNull(CookieName, "CookieName");
            Assert.IsNotNull(CookieValue, "CookieValue");
            Assert.IsNotNull(CookieExpiry, "CookieExpiry");

            double.TryParse(CookieExpiry, out double expiry);

            var response = HttpContext.Current.Response;
            response.Cookies[CookieName].Value = CookieValue;
            response.Cookies[CookieName].Expires = DateTime.Now.AddDays(expiry);
        }
    }
}