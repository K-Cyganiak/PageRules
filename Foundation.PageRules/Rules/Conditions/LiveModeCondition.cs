using Sitecore.Rules.Conditions;
using Foundation.PageRules.Rules.RuleContext;

namespace Foundation.PageRules.Rules.Conditions
{
    public class LiveModeCondition<T> : StringOperatorCondition<T> where T : PageRulesRuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            return Sitecore.Context.PageMode.IsNormal 
                && !Sitecore.Context.PageMode.IsDebugging 
                && !Sitecore.Context.PageMode.IsExploring
                && !Sitecore.Context.PageMode.IsExperienceEditor;
        }
    }
}