// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Extensions.AppInsights.Data;
using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json.Linq;

namespace Microsoft.Azure.WebJobs.Extensions.AppInsights
{
    [Extension(nameof(AppInsightsContext))]
    public class AppInsightsExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly TelemetryConfiguration telemetryConfig;

        public AppInsightsExtensionConfigProvider(TelemetryConfiguration telemetryConfiguration)
        {
            this.telemetryConfig = telemetryConfiguration;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.AddConverter<JObject, AppInsightsData>(input => input.ToObject<AppInsightsData>());
            context.AddConverter<AppInsightsData, JObject>(input => JObject.FromObject(input));

            var rule = context.AddBindingRule<AppInsightsAttribute>();

            rule.BindToInput(this.BuildItemFromAttribute);
            rule.BindToCollector(this.BuildCollector);
        }

        private AppInsightsData BuildItemFromAttribute(AppInsightsAttribute arg)
        {
            return new AppInsightsData { Id = Activity.Current.Id, Tags = new List<AppInsightsTag>() };
        }

        private IAsyncCollector<AppInsightsData> BuildCollector(AppInsightsAttribute attribute)
        {
            return new AppInsightsContext
            {
                InstrumentationKey = this.telemetryConfig.InstrumentationKey,
            };
        }
    }
}
