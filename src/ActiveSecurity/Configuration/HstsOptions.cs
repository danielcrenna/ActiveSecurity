// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ActiveRoutes;

namespace ActiveSecurity.Configuration
{
	/// <summary>
	///     See: https://hstspreload.org
	/// </summary>
	public class HstsOptions : IFeatureToggle
	{
		public bool Enabled { get; set; } = true;
		public HstsPreloadStage Stage { get; set; } = HstsPreloadStage.One;
		public bool IncludeSubdomains { get; set; } = true;
		public bool Preload { get; set; } = false;

		public TimeSpan HstsMaxAge
		{
			get
			{
				var now = DateTimeOffset.UtcNow;

				switch (Stage)
				{
					case HstsPreloadStage.One:
						return TimeSpan.FromMinutes(5);
					case HstsPreloadStage.Two:
						return TimeSpan.FromDays(7);
					case HstsPreloadStage.Three:
						return now.AddMonths(1) - now;
					case HstsPreloadStage.ReadyForPreload:
						return now.AddYears(2) - now;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}