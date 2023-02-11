using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Rules.Conditions;
using System;
using System.Linq;

namespace Foundation.PageRules.Rules.Conditions
{
    public class HasVisitedCertainPageGivenNumberOfTimesCondition<T>
                    : OperatorCondition<T> where T : Sitecore.Rules.RuleContext
    {
        public string PageId { get; set; }
        public int Index { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
            Assert.IsNotNull(Tracker.Current.Session, "Tracker.Current.Session is not initialized");
            Assert.IsNotNull(Tracker.Current.Session.Interaction,
                "Tracker.Current.Session.Interaction is not initialized");

            Guid pageGuid;

            try
            {
                pageGuid = new Guid(PageId);
            }
            catch
            {
                Log.Warn(string.Format("Could not convert value to guid: {0}", PageId), GetType());
                return false;
            }

            var pageVisits = Tracker.Current.Session.Interaction.GetPages()
                .Count(row => row.Item.Id == pageGuid);

            switch (GetOperator())
            {
                case ConditionOperator.Equal:
                    return pageVisits == Index;
                case ConditionOperator.GreaterThanOrEqual:
                    return pageVisits >= Index;
                case ConditionOperator.GreaterThan:
                    return pageVisits > Index;
                case ConditionOperator.LessThanOrEqual:
                    return pageVisits <= Index;
                case ConditionOperator.LessThan:
                    return pageVisits < Index;
                case ConditionOperator.NotEqual:
                    return pageVisits != Index;
                default:
                    return false;
            }
        }
    }
}