// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ActiveRoutes;

namespace ActiveSecurity.Configuration
{
	public class CorsOptions : IFeatureToggle
    {
        public bool Enabled { get; set; } = true;

        public CorsOptions() : this(false) { }

        public CorsOptions(bool forBinding)
        {
            // IConfiguration.Bind adds to existing arrays...
            if (forBinding)
                return;

            Origins = new[] {"*"};
            Methods = new[] {"*"};
            Headers = new[] {"*"};
            ExposedHeaders = new string[] { };
        }

        public string[] Origins { get; set; }
        public string[] Methods { get; set; }
        public string[] Headers { get; set; }
        public string[] ExposedHeaders { get; set; }
        public bool AllowCredentials { get; set; } = true;
        public bool AllowOriginWildcards { get; set; } = true;
        public int? PreflightMaxAgeSeconds { get; set; } = null;
    }
}