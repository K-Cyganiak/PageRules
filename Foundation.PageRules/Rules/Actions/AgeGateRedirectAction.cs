using System.Linq;
using System.Net;
using System.Web;
using Foundation.PageRules.Rules.RuleContext;
using Sitecore;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Rules.Actions;

namespace Foundation.PageRules.Rules.Actions
{
    public class AgeGateRedirectAction<T> : RuleAction<T> where T : PageRulesRuleContext
    {
        public ID TargetId { get; set; }

        public override void Apply(T ruleContext)
        {
            if (ID.IsNullOrEmpty(this.TargetId))
            {
                Log.Warn(string.Format("AgeGateRedirectAction for page '{0}' - no item selected", (object)Context.Item.Paths.FullPath), (object)this);
            }
            else
            {
                var obj = Context.Database.GetItem(this.TargetId);
                if (obj != null)
                {
                    var urlWithoutParameters = HttpContext.Current.Request.RawUrl.Split('?').First();
                    var redirectUrl = LinkManager.GetItemUrl(obj);
                    var queryString = HttpUtility.ParseQueryString(string.Empty);
                    queryString["dest"] = System.Net.WebUtility.UrlEncode(urlWithoutParameters);
                    queryString.Add(HttpContext.Current.Request.QueryString);

                    HttpContext.Current.Response.Redirect($"{redirectUrl}?{queryString}", true);
                }
                else
                    Log.Warn(string.Format("AgeGateRedirectAction for page '{0}' executed without target item", (object)Context.Item.Paths.FullPath), (object)this);
            }
        }
    }
}