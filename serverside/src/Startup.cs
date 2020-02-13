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
using System.IO;
using System.Linq;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.EntityFramework;
using GraphQL.Utilities;
using Audit.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;

using Sportstats.Models;
using Sportstats.Services;
using Sportstats.Helpers;
using Sportstats.Utility;
using Sportstats.Graphql;
using Sportstats.Graphql.Types;
using Sportstats.Controllers;
using Sportstats.Services.Scheduling;
using Sportstats.Services.Scheduling.Tasks;
using Sportstats.Services.CertificateProvider;
using Sportstats.Services.Interfaces;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats
{
	public class Startup
	{
		public Startup(IWebHostEnvironment env, IConfiguration configuration)
		{
			Configuration = configuration;
			CurrentEnvironment = env;
		}

		private IWebHostEnvironment CurrentEnvironment { get; set; }

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
				{
					options.Filters.Add(new XsrfActionFilterAttribute());
					options.Filters.Add(new AntiforgeryFilterAttribute());
				})
				.AddControllersAsServices()
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

			var dbConnectionString = Configuration.GetConnectionString("DbConnectionString");

			// Set up the database connection
			services.AddDbContext<SportstatsDBContext>(options =>
			{
				options.UseNpgsql(dbConnectionString);
				options.UseOpenIddict<Guid>();
			});

			// Register Identity Services
			services.AddIdentity<User, Group>(options =>
				{
					// % protected region % [Configure password requirements here] off begin
					options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
					options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
					options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;

					options.User.AllowedUserNameCharacters += @"\*";

					if (CurrentEnvironment.IsDevelopment())
					{
						options.Password.RequiredLength = 6;
						options.Password.RequiredUniqueChars = 0;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireLowercase = false;
						options.Password.RequireUppercase = false;
						options.Password.RequireDigit = false;
					}
					else
					{
						options.Password.RequiredLength = 12;
						options.Password.RequiredUniqueChars = 0;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireLowercase = false;
						options.Password.RequireUppercase = false;
						options.Password.RequireDigit = false;
					}
					// % protected region % [Configure password requirements here] end
				})
				.AddEntityFrameworkStores<SportstatsDBContext>()
				.AddDefaultTokenProviders();

			var certSetting = Configuration.GetSection("CertificateSetting").Get<CertificateSetting>();

			services.AddOpenIddict()
				.AddCore(options =>
				{
					options.UseEntityFrameworkCore()
						.UseDbContext<SportstatsDBContext>()
						.ReplaceDefaultEntities<Guid>();
				})
				.AddServer(options =>
				{
					options.UseMvc();
					options.EnableTokenEndpoint("/api/authorization/connect/token");

					X509Certificate2 cert = null;
					if (CurrentEnvironment.IsDevelopment())
					{
						cert = new InRootFolderCertificateProvider(certSetting).ReadX509SigningCert();
					}
					else
					{
						// not for production, currently using the same as development testing.
						// todo: Create another Certificate Provider Inheriting BaseCertificateProvider, and override ReadX509SigningCert
						// to read cerficicate from another more secure place, e.g cerficate store, aws server...
						cert = new InRootFolderCertificateProvider(certSetting).ReadX509SigningCert();
					}

					if (cert == null) {
						// not for production, use x509 certificate and .AddSigningCertificate()
						options.AddEphemeralSigningKey();
					} else {
						options.AddSigningCertificate(cert);
					}

					// use jwt
					options.UseJsonWebTokens();
					options.AllowPasswordFlow();
					options.AllowRefreshTokenFlow();
					options.AcceptAnonymousClients();
					options.DisableHttpsRequirement();
				});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			services.AddAuthentication(options =>
				{
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.LoginPath = "/api/authorization/login";
					options.LogoutPath = "/api/authorization/logout";
					options.SlidingExpiration = true;
					options.ExpireTimeSpan = TimeSpan.FromDays(7);
					options.Events.OnRedirectToLogin = redirectOptions =>
					{
						redirectOptions.Response.StatusCode = StatusCodes.Status401Unauthorized;
						return Task.CompletedTask;
					};
				})
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
					options.Authority = certSetting.JwtBearerAuthority;
					options.Audience = certSetting.JwtBearerAudience;
					options.RequireHttpsMetadata = false;
					options.IncludeErrorDetails = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						NameClaimType = OpenIdConnectConstants.Claims.Subject,
						RoleClaimType = OpenIdConnectConstants.Claims.Role
					};
				});

			// % protected region % [Add additional authentication types here] off begin
			// % protected region % [Add additional authentication types here] end
			services.AddAuthorization(options =>
			{
				// % protected region % [Change authorization logic here] off begin
				options.DefaultPolicy = new AuthorizationPolicyBuilder(
						JwtBearerDefaults.AuthenticationScheme,
						CookieAuthenticationDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();
				// % protected region % [Change authorization logic here] end
				
				options.AddPolicy(
					"AllowVisitorPolicy",
					new AuthorizationPolicyBuilder(
							JwtBearerDefaults.AuthenticationScheme,
							CookieAuthenticationDefaults.AuthenticationScheme)
						.RequireAssertion(_ => true)
						.Build());
			});

			// Register service to seed test data
			services.AddScoped<DataSeedHelper>();

			// Register core scoped services
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IGraphQlService, GraphQlService>();
			services.AddScoped<ICrudService, CrudService>();
			services.AddScoped<ISecurityService, SecurityService>();
			services.AddScoped<IIdentityService, IdentityService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IAuditService, AuditService>();
			services.AddScoped<IXsrfService, XsrfService>();

			// Register context filters
			services.AddScoped<AntiforgeryFilter>();
			services.AddScoped<XsrfActionFilter>();

			// Add swagger service
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("json", new OpenApiInfo {Title = "Sportstats", Version = "v1"});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});

			// GraphQL types must be registered as singleton services. This is since building the underlying graph is
			// expensive and should only be done once.
			services.AddSingleton<SportType>();
			services.AddSingleton<SportInputType>();
			services.AddSingleton<LeagueType>();
			services.AddSingleton<LeagueInputType>();
			services.AddSingleton<UserType>();
			services.AddSingleton<UserInputType>();
			services.AddSingleton<UserCreateInputType>();


			// Connect the database type to the GraphQL type
			GraphTypeTypeRegistry.Register<Sport, SportType>();
			GraphTypeTypeRegistry.Register<League, LeagueType>();
			GraphTypeTypeRegistry.Register<User, UserType>();

			// Add GraphQL core services and executors
			services.AddGraphQL();
			services.AddSingleton<IDocumentExecuter, EfDocumentExecuter>();
			services.AddSingleton<IDependencyResolver>(
				provider => new FuncDependencyResolver(provider.GetRequiredService)
			);

			// Add the schema and query for graphql
			services.AddSingleton<ISchema, SportstatsSchema>();
			services.AddSingleton<SportstatsQuery>();
			services.AddSingleton<SportstatsMutation>();

			services.AddSingleton<IdObjectType>();
			services.AddSingleton<NumberObjectType>();
			services.AddSingleton<OrderGraph>();
			services.AddSingleton<BooleanObjectType>();
			// % protected region % [Add extra GraphQL types here] off begin
			// % protected region % [Add extra GraphQL types here] end

			// Send our db context to graphql to use
			EfGraphQLConventions.RegisterInContainer<SportstatsDBContext>(services);
			EfGraphQLConventions.RegisterConnectionTypesInContainer(services);

			AddConfigurations(services);

			// % protected region % [Add extra startup methods here] off begin
			// % protected region % [Add extra startup methods here] end

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ctx => new SportstatsActionResult();
			});

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "Client";
			});

			// Add scheduled tasks & scheduler
			LoadScheduledTasks(services);

			// Autofac Dependency Injection
			var container = RegisterAutofacTypes(services);

			//Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(container);
		}

		private void AddConfigurations(IServiceCollection services)
		{
			services.Configure<EmailAccount>(Configuration.GetSection("EmailAccount"));
			// % protected region % [Add more configuration sections here] off begin
			// % protected region % [Add more configuration sections here] end
		}

		private IContainer RegisterAutofacTypes(IServiceCollection services)
		{
			var builder = new ContainerBuilder();

			builder.Populate(services);
			// % protected region % [Register more Autofac Types here] off begin
			// % protected region % [Register more Autofac Types here] end
			return builder.Build();
		}

		private void LoadScheduledTasks(IServiceCollection services)
		{
			// The flowing line is the example of loading a scheduled task. please refer to Class "SomeTask" to create a new Task
			// services.AddSingleton<IScheduledTask, SomeTask>();

			// % protected region % [Add more scheduled task here] off begin
			// % protected region % [Add more scheduled task here] end

			services.AddScheduler((sender, args) =>
			{
				Console.Write(args.Exception.Message);
				args.SetObserved();
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			DataSeedHelper dataSeed)
		{
			Audit.Core.Configuration.Setup()
				.UseEntityFramework(ef => ef
					.AuditTypeMapper(t => typeof(AuditLog))
					.AuditEntityAction<AuditLog>((ev, entry, entity) =>
					{
						var context = entry.GetEntry().Context as SportstatsDBContext;

						entity.Id = Guid.NewGuid();
						entity.AuditData = JObject.FromObject(new
						{
							Table = entry.Table,
							Action = entry.Action,
							PrimaryKey = entry.PrimaryKey,
							ColumnValues = entry.ColumnValues,
							Values = entry
								.Changes
								?.Where(e => e.NewValue != null)
								.Select(e => new {ColumnName = e.ColumnName, Value = e.NewValue})
								.ToList()
						});
						entity.EntityType = entry.EntityType.Name;
						entity.AuditDate = DateTime.UtcNow;
						entity.Action = entry.Action;
						entity.TablePk = entry.PrimaryKey.First().Value.ToString();
						entity.UserId = context?.SessionUser;
						entity.HttpContextId = context?.SessionId;
					})
					.IgnoreMatchedProperties());

			dataSeed.Initialize();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				// % protected region % [Add dev environment settings here] off begin
				// % protected region % [Add dev environment settings here] end
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseExceptionHandler("/Error");
				app.UseHsts();
				// % protected region % [Add prod environment settings here] off begin
				// % protected region % [Add prod environment settings here] end

			}

			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMiddleware<AuditMiddleware>();

			// Add Swagger json and ui
			var swaggerUrl = "api/swagger/{documentName}/openapi.json";
			app.UseSwagger(options =>
			{
				options.RouteTemplate = swaggerUrl;
			});
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/api/swagger/json/openapi.json", "Sportstats");
				options.RoutePrefix = "api/swagger";
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
			});

			// % protected region % [add extra configuration settings here] off begin
			// % protected region % [add extra configuration settings here] end

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "Client";

				if (env.IsDevelopment())
				{
					var clientServerSettings = Configuration.GetSection("ClientServerSettings");
					spa.Options.SourcePath = clientServerSettings["ClientSourcePath"];
					bool.TryParse(clientServerSettings["UseProxyServer"], out var useProxyServer);

					if (useProxyServer)
					{
						spa.UseProxyToSpaDevelopmentServer(clientServerSettings["ProxyServerAddress"]);

					}
					else
					{
						spa.UseReactDevelopmentServer("start");
					}
				}
			});
		}
	}
}
