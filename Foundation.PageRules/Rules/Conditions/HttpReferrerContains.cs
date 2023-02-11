using Foundation.PageRules.Rules.RuleContext;
using Sitecore.Rules.Conditions;
using System.Web;

namespace Foundation.PageRules.Rules.Conditions
{
    public class HttpReferrerContains<T> : StringOperatorCondition<T> where T : PageRulesRuleContext
    {
        public string HttpReferrer { get; set; }

        protected override bool Execute(T ruleContext)
        {
            if (!string.IsNullOrEmpty(HttpReferrer))
            {
                if (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.ToString().ToLower().Contains(HttpReferrer.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}