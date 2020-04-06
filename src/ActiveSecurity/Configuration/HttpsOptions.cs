// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ActiveRoutes;
using Microsoft.AspNetCore.HttpsPolicy;

namespace ActiveSecurity.Configuration
{
	public class HttpsOptions : IFeatureToggle
	{
		public bool Enabled { get; set; } = true;
		public HstsOptions Hsts { get; set; } = new HstsOptions();
	}
}