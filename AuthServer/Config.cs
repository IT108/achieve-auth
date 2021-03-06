﻿using System.Collections.Generic;
using System.Security.Claims;
using AuthServer.Models;
using IdentityServer4.Models;

namespace AuthServer
{
	public class Config
	{
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Email(),
				new IdentityResources.Profile()
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("achieve-api", "Achieve API")
				{
					UserClaims = {"role"},
					Scopes = {new Scope("api.read"), new Scope("api.write")}
				}
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new[]
			{
				new Client {
					RequireConsent = false,
					ClientId = "angular_spa",
					ClientName = "Angular SPA",
					AllowedGrantTypes = GrantTypes.Implicit,
					AlwaysSendClientClaims = true,
					UpdateAccessTokenClaimsOnRefresh = true,
					AlwaysIncludeUserClaimsInIdToken = true,
					AllowedScopes = { "openid", "profile", "email", "api.read", "api.write" },
					RedirectUris = {"http://localhost:4200/#/auth-callback#", "http://localhost:4200/#/silent-callback#"},
					PostLogoutRedirectUris = {"http://localhost:4200/"},
					AllowedCorsOrigins = {"http://localhost:4200"},
					AllowAccessTokensViaBrowser = true,
					AccessTokenLifetime = 3600
				}
			};
		}
	}
}
