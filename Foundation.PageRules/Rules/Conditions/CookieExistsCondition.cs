using Foundation.PageRules.Rules.RuleContext;
using Sitecore.Rules.Conditions;
using System.Web;

namespace Foundation.PageRules.Rules.Conditions
{
    public class CookieExistsCondition<T> : WhenCondition<T> where T : PageRulesRuleContext
    {
        public string CookieName { get; set; }

        protected override bool Execute(T ruleContext)
        {
            if (string.IsNullOrEmpty(CookieName))
                return false;

            return HttpContext.Current.Request.Cookies[CookieName] != null;
        }
    }
}