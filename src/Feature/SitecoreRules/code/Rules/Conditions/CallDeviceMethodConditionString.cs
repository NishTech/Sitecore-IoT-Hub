using System.Web.Mvc;
using IoTHub.Foundation.Azure.Repositories;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace IoTHub.Feature.SitecoreRules.Rules.Conditions
{
    public class CallDeviceMethodConditionString<T> : StringOperatorCondition<T> where T : RuleContext
    {
        private readonly IIoTHubRepository _hubRepository =
            DependencyResolver.Current.GetService<IIoTHubRepository>();

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, nameof (ruleContext));

            // Get passed method and property
            var deviceItem = Sitecore.Context.Database.GetItem(DeviceId);
            var methodItem = Sitecore.Context.Database.GetItem(MethodId);
            var propertyItem = Sitecore.Context.Database.GetItem(PropertyId);
            Assert.ArgumentNotNull(deviceItem, $"deviceItem.ID='{DeviceId}'");
            Assert.ArgumentNotNull(methodItem, $"methodItem.ID='{MethodId}'");
            Assert.ArgumentNotNull(propertyItem, $"propertyItem.ID='{PropertyId}'");
            var device = _hubRepository.CastToDevice(deviceItem);
            var method = _hubRepository.CastToMethod(methodItem);
            var property = _hubRepository.CastToMessageProperty(propertyItem);
            
            // Call method and receive results
            var parsedResults = method.Invoke(device, Payload);
            if (parsedResults == null)
                return false;
            var dynamicValue = property.GetValue(parsedResults);
            var parsedValue = dynamicValue==null ? string.Empty : dynamicValue.ToString();

            // Compare values
            var comparisonResult = Compare(parsedValue, Value);
            var comparisonSign = comparisonResult ? "=" : "!=";
            Log.Debug($"CallDeviceMethodConditionString: '{parsedValue}'{comparisonSign}'{Value}'");
            return comparisonResult;
        }

        public override bool CanEvaluate(T ruleContext)
        {
            return true;
        }

        protected virtual string GetValue(T ruleContext)
        {
            return Value ?? string.Empty;
        }

        public ID DeviceId { get; set; }
        public ID MethodId { get; set; }
        public ID PropertyId { get; set; }
        public string Value { get; set; }
        public string Payload { get; set; }
    }
}