using Foundation.PageRules.Rules.RuleContext;
using Sitecore;
using Sitecore.Rules.Conditions;

namespace Foundation.PageRules.Rules.Conditions
{
    public class IsAgeGateEnabled<T> : WhenCondition<T> where T : PageRulesRuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            return MainUtil.GetBool(Context.Item?.Fields["AgeGateEnabled"]?.Value, false);
        }
    }
}