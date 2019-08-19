// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.Azure.WebJobs.Extensions.AppInsights.Data
{
    public class AppInsightsData
    {
        public string Id { get; set; }

        public IEnumerable<AppInsightsTag> Tags { get; set; }
    }
}
