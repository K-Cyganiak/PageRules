using System;
using Foundation.PageRules.Rules.RuleContext;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;

namespace Foundation.PageRules.Pipelines.RenderLayout
{
    public class RunPageRules : RequestBeginProcessor
    {
        private const string PageRulesFieldName = "Page Rules";

        public override void Process(RequestBeginArgs args)
        {
            try
            {
                var field = Context.Item?.Fields[PageRulesFieldName];
                if (field == null || string.IsNullOrWhiteSpace(field.Value))
                    return;
                RuleList<PageRulesRuleContext> rules = RuleFactory.GetRules<PageRulesRuleContext>(field);
                if (rules == null || rules.Count == 0)
                    return;
                PageRulesRuleContext ruleContext = new PageRulesRuleContext();
                rules.Run(ruleContext);
            }
            catch (Exception ex)
            {
                Log.Error("Exception while running page rules", ex, (object)this);
            }
        }
    }
}