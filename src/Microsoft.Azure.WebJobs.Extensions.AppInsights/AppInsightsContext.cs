// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.AppInsights.Data;

namespace Microsoft.Azure.WebJobs.Extensions.AppInsights
{
    public class AppInsightsContext : IAsyncCollector<AppInsightsData>
    {
        public string InstrumentationKey { get; set; }

        public Task AddAsync(AppInsightsData item, CancellationToken cancellationToken = default)
        {
            foreach (AppInsightsTag tag in item.Tags)
            {
                Activity.Current.AddTag(tag.Key, tag.Value);
            }

            return Task.CompletedTask;
        }

        public Task FlushAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
