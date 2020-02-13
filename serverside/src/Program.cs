/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
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
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length > 0 && args[0] == "swagger")
			{
				Console.WriteLine(GenerateSwagger(args));
				return;
			}
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((builderContext, config) =>
				{
					// % protected region % [Configure environment settings here] off begin
					var env = builderContext.HostingEnvironment;

					config.SetBasePath(env.ContentRootPath);
					config.AddXmlFile("appsettings.xml", optional: false, reloadOnChange: true);
					config.AddXmlFile($"appsettings.{env.EnvironmentName}.xml", optional: true);
					config.AddEnvironmentVariables();
					config.AddEnvironmentVariables("Sportstats_");
					config.AddEnvironmentVariables($"Sportstats_{env.EnvironmentName}_");
					config.AddCommandLine(args);
					// % protected region % [Configure environment settings here] end
				})
				// % protected region % [Add any further web host configuration here] off begin
				// % protected region % [Add any further web host configuration here] end
				.UseStartup<Startup>();

		private static string GenerateSwagger(string[] args)
		{
			var host = CreateWebHostBuilder(args.Skip(1).ToArray()).Build();
			var sw = (ISwaggerProvider)host.Services.GetService(typeof(ISwaggerProvider));
			var doc = sw.GetSwagger("json", null, "/");
			return JsonConvert.SerializeObject(
				doc,
				Formatting.Indented,
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
					ContractResolver = new DefaultContractResolver()
				}
			);
		}
	}
}
