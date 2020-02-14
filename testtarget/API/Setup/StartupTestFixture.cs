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
using System.IO;
using System.Linq;
using Sportstats.Models;
using APITests.Utils;
using Microsoft.EntityFrameworkCore;
using APITests.Settings;
using Microsoft.Extensions.Configuration;

namespace APITests.Setup
{
	public class StartupTestFixture
	{
		public string BaseUrl { get; }
		public string TestUsername { get; }
		public string TestPassword { get;}
		public string SuperUsername { get; }
		public string SuperPassword { get; }
		public DbContextOptions<SportstatsDBContext> DbContextOptions {get;}
		public Guid SuperOwnerId { get; private set; }

		public StartupTestFixture()
		{
			var appSettingBuilder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddXmlFile("appsettings.Test.xml", optional: true, reloadOnChange: false);
			var appSettings = appSettingBuilder.Build();

			//load in site configuration
			var siteConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("SiteConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var siteSettings = new SiteSettings();
			siteConfiguration.GetSection("site").Bind(siteSettings);

			//load in the user configurations
			var userConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("UserConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var superUserSettings = new UserSettings();
			var testUserSettings = new UserSettings();
			userConfiguration.GetSection("super").Bind(superUserSettings);
			userConfiguration.GetSection("test").Bind(testUserSettings);

			var baseUrlFromEnvironment = Environment.GetEnvironmentVariable("BASE_URL");
			BaseUrl = baseUrlFromEnvironment ?? siteSettings.BaseUrl;

			TestUsername = testUserSettings.Username;
			TestPassword = testUserSettings.Password;
			SuperUsername = superUserSettings.Username;
			SuperPassword = superUserSettings.Password;

			var dbConnectionString = appSettings["ConnectionStrings:DbConnectionString"];
			DbContextOptions = new DbContextOptionsBuilder<SportstatsDBContext>()
				.UseNpgsql(dbConnectionString).Options;

			PingServer.TestConnection(BaseUrl);

			using (var context = new SportstatsDBContext(DbContextOptions, null, null))
			{
				SuperOwnerId = context.Users.First(x => x.UserName == SuperUsername).Id;
			}
		}
	}
}
