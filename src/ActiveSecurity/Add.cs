// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ActiveLogging;
using ActiveSecurity.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveSecurity
{
	public static class Add
	{
		public static void AddHttps(this IServiceCollection services, ISafeLogger logger, HttpsOptions options)
		{
			if (options.Enabled)
			{
				logger?.Trace(() => "HTTPS enabled.");

				services.AddHttpsRedirection(o =>
				{
					o.HttpsPort = null;
					o.RedirectStatusCode = options.Hsts.Enabled ? 307 : 301;
				});

				if (options.Hsts.Enabled)
				{
					logger?.Trace(() => "HSTS enabled.");

					services.AddHsts(o =>
					{
						o.MaxAge = options.Hsts.HstsMaxAge;
						o.IncludeSubDomains = options.Hsts.IncludeSubdomains;
						o.Preload = options.Hsts.Preload;
					});
				}
			}
		}

		public static void AddCors(this IServiceCollection services, ISafeLogger logger, CorsOptions cors)
		{
			if (!cors.Enabled)
				return;

			logger?.Trace(() => "CORS enabled.");

			services.AddSafeLogging();
			services.AddRouting(o => { });
			services.AddCors(o =>
			{
				o.AddPolicy(Constants.Security.Policies.CorsPolicy, builder =>
				{
					builder
						.WithOrigins(cors.Origins ?? new[] {"*"})
						.WithMethods(cors.Methods ?? new[] {"*"})
						.WithHeaders(cors.Headers ?? new[] {"*"})
						.WithExposedHeaders(cors.ExposedHeaders ?? new string[0]);

					if (cors.AllowCredentials && cors.Origins?.Length > 0 && cors.Origins[0] != "*")
						builder.AllowCredentials();
					else
						builder.DisallowCredentials();

					if (cors.AllowOriginWildcards)
						builder.SetIsOriginAllowedToAllowWildcardSubdomains();

					if (cors.PreflightMaxAgeSeconds.HasValue)
						builder.SetPreflightMaxAge(TimeSpan.FromSeconds(cors.PreflightMaxAgeSeconds.Value));
				});
			});
		}
	}
}