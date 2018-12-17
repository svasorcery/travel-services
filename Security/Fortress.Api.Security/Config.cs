// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Fortress.Api.Security
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("oasis_api", "Traveler API"),
                new ApiResource("rail_api", "Rail API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "console_app",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "oasis_api", "rail_api" }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "hermes_spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://localhost:5201/about",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:5201/index.html",
                        "http://localhost:5201/callback.html",
                        "http://localhost:5201/silent.html",
                        "http://localhost:5201/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5201/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5201" },

                    AllowedScopes = { "openid", "profile", "oasis_api", "rail_api" }
                }
            };
        }
    }
}