using System.Web;
using Foundation.PageRules.Rules.RuleContext;
using Sitecore;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Rules.Actions;

namespace Foundation.PageRules.Rules.Actions
{
    public class RedirectAction<T> : RuleAction<T> where T : PageRulesRuleContext
    {
        public ID TargetId { get; set; }

        public override void Apply(T ruleContext)
        {
            if (ID.IsNullOrEmpty(this.TargetId))
            {
                Log.Warn(string.Format("RedirectAction for page '{0}' - no item selected", (object)Context.Item.Paths.FullPath), (object)this);
            }
            else
            {
                var obj = Context.Database.GetItem(this.TargetId);
                if (obj != null)
                    HttpContext.Current.Response.Redirect(LinkManager.GetItemUrl(obj), true);
                else
                    Log.Warn(string.Format("RedirectAction for page '{0}' executed without target item", (object)Context.Item.Paths.FullPath), (object)this);
            }
        }
    }
}