using System.Web;
using Foundation.PageRules.Rules.RuleContext;
using Sitecore;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules.Actions;

namespace Foundation.PageRules.Rules.Actions
{
    public class EndResponseWithStatusCodeAction<T> : RuleAction<T> where T : PageRulesRuleContext
    {
        private const string StatusCodeFieldName = "HTTP Status Code";

        public ID StatusCodeId { get; set; }

        public override void Apply(T ruleContext)
        {
            if (ID.IsNullOrEmpty(this.StatusCodeId))
            {
                Log.Warn(string.Format("EndResponseWithStatusCodeAction for page '{0}' - no status selected", (object)Context.Item.Paths.FullPath), (object)this);
            }
            else
            {
                var obj = Context.Database.GetItem(this.StatusCodeId);
                if (obj == null)
                {
                    Log.Warn(string.Format("EndResponseWithStatusCodeAction for page '{0}' - status code item does not exist", (object)Context.Item.Paths.FullPath), (object)this);
                }
                else
                {
                    HttpContext.Current.Response.StatusCode = MainUtil.GetInt(obj[StatusCodeFieldName], 200);
                    HttpContext.Current.Response.End();
                }
            }
        }
    }
}