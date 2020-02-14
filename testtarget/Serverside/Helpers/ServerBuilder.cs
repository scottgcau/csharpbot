/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;
using Sportstats;
using Sportstats.Helpers;
using Sportstats.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServersideTests.Helpers
{
	public static class ServerBuilder
	{
		public static IWebHost CreateServer(string databaseName = null)
		{
			var claim = CreateUserPrincipal(
				Guid.NewGuid(),
				"super@example.com",
				"super@example.com",
				new [] {"Visitors", "Super Administrators"});
			var httpContext = new DefaultHttpContext { User = claim };

			var host = WebHost.CreateDefaultBuilder()
				.ConfigureAppConfiguration((builderContext, config) =>
				{
					var env = builderContext.HostingEnvironment;
					config.AddXmlFile("appsettings.xml", optional: false);
					config.AddXmlFile($"appsettings.Test.xml", optional: true);
					config.AddEnvironmentVariables();
				})
				.UseStartup<Startup>()
				.UseEnvironment("Development")
				.ConfigureServices(sc =>
				{
					sc.AddDbContext<SportstatsDBContext>(options =>
					{
						options.UseInMemoryDatabase(databaseName ?? Path.GetRandomFileName());
						options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
						options.UseOpenIddict<Guid>();
					});
					sc.AddScoped<IHttpContextAccessor>(_ => new HttpContextAccessor
					{
						HttpContext = httpContext
					});
				})
				.Build();

			var dataSeed = host.Services.GetRequiredService<DataSeedHelper>();
			dataSeed.Initialize();

			return host;
		}

		private static ClaimsPrincipal CreateUserPrincipal(Guid userId, string userName, string email, IEnumerable<string> roles)
		{
			var identity = new ClaimsIdentity(
				CookieAuthenticationDefaults.AuthenticationScheme,
				ClaimTypes.Name,
				ClaimTypes.Role);
			identity.AddClaim(new Claim("UserId", userId.ToString()));
			identity.AddClaim(new Claim(OpenIdConnectConstants.Claims.Subject, userName));
			identity.AddClaim(new Claim(ClaimTypes.Name, email));
			identity.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));

			return new ClaimsPrincipal(identity);
		}
	}
}