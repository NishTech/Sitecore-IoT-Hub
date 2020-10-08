using System.Web.Mvc;
using IoTHub.Foundation.Azure.Repositories;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace IoTHub.Feature.SitecoreRules.Rules.Conditions
{
    public class CallDeviceMethodConditionDouble<T> : OperatorCondition<T> where T : RuleContext
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
            var dynamicValue = property.GetValue(parsedResults);
            var parsedValue = dynamicValue==null ? 0 : (double)dynamicValue;

            // Compare values
            var comparisonResult = false;
            switch (this.GetOperator())
            {
                case ConditionOperator.Equal:
                    comparisonResult = parsedValue == Value;
                    break;
                case ConditionOperator.GreaterThanOrEqual:
                    comparisonResult = parsedValue >= Value;
                    break;
                case ConditionOperator.GreaterThan:
                    comparisonResult = parsedValue > Value;
                    break;
                case ConditionOperator.LessThanOrEqual:
                    comparisonResult = parsedValue <= Value;
                    break;
                case ConditionOperator.LessThan:
                    comparisonResult = parsedValue < Value;
                    break;
                case ConditionOperator.NotEqual:
                    comparisonResult = parsedValue != Value;
                    break;
                default:
                    comparisonResult = false;
                    break;
            }

            var comparisonSign = comparisonResult ? "=" : "!=";
            Log.Debug($"CallDeviceMethodConditionDouble: '{parsedValue}'{comparisonSign}'{Value}'");
            return comparisonResult;
        }

        public override bool CanEvaluate(T ruleContext)
        {
            return true;
        }

        public ID DeviceId { get; set; }
        public ID MethodId { get; set; }
        public string PropertyId { get; set; }
        public int Value { get; set; }
        public string Payload { get; set; }
    }
}