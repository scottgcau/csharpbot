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
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.DataProtection;

using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.EntityFramework;
using GraphQL.Utilities;
using Audit.Core;
using Audit.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;

using Sportstats.Configuration;
using Sportstats.Models;
using Sportstats.Services;
using Sportstats.Helpers;
using Sportstats.Utility;
using Sportstats.Graphql;
using Sportstats.Graphql.Types;
using Sportstats.Controllers;
using Sportstats.Enums;
using Sportstats.Services.Scheduling;
using Sportstats.Services.CertificateProvider;
using Sportstats.Services.Interfaces;
using Sportstats.Services.Files;
using Sportstats.Services.Files.Providers;
using Serilog;
using Serilog.Events;
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
			// % protected region % [Configure logging here] off begin
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", "Sportstats")
				.WriteTo.Console()
				.CreateLogger();
			// % protected region % [Configure logging here] end

			// % protected region % [Configure MVC here] off begin
			AddMvc(services);
			// % protected region % [Configure MVC here] end

			// % protected region % [Configure database connection here] off begin
			ConfigureDatabaseConnection(services);
			// % protected region % [Configure database connection here] end

			// % protected region % [Configure Auth services here] off begin
			ConfigureAuthServices(services);
			// % protected region % [Configure Auth services here] end

			// % protected region % [Configure scoped services here] off begin
			ConfigureScopedServices(services);
			// % protected region % [Configure scoped services here] end

			// % protected region % [Configure graphql services here] off begin
			ConfigureGraphql(services);
			// % protected region % [Configure graphql services here] end

			// % protected region % [Configure swagger services here] off begin
			AddSwaggerService(services);
			// % protected region % [Configure swagger services here] end

			// % protected region % [Configure configuration services here] off begin
			AddApplicationConfigurations(services);
			// % protected region % [Configure configuration services here] end

			// % protected region % [Add extra startup methods here] off begin
			// % protected region % [Add extra startup methods here] end

			// % protected region % [Configure ApiBehaviorOptions service here] off begin
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ctx => new SportstatsActionResult();
			});
			// % protected region % [Configure ApiBehaviorOptions service here] end

			// % protected region % [Configure SPA files here] off begin
			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "Client";
			});
			// % protected region % [Configure SPA files here] end

			// Add scheduled tasks & scheduler
			LoadScheduledTasks(services);

			// Autofac Dependency Injection
			var container = RegisterAutofacTypes(services);

			//Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(container);
		}

		private void AddMvc(IServiceCollection services)
		{
			services.AddMvc(options =>
				{
					// % protected region % [Configure MVC options here] off begin
					options.Filters.Add(new XsrfActionFilterAttribute());
					options.Filters.Add(new AntiforgeryFilterAttribute());
					// % protected region % [Configure MVC options here] end
				})
				.AddControllersAsServices()
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
				.AddNewtonsoftJson(options =>
				{
					// % protected region % [Configure JSON options here] off begin
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					// % protected region % [Configure JSON options here] end
				})
				.AddMvcOptions(options =>
				{
					// Add extra output formatters after JSON to ensure JSON is the default
					// % protected region % [Configure output formatters here] off begin
					options.OutputFormatters.Add(new CsvOutputFormatter());
					// % protected region % [Configure output formatters here] end
				});
		}

		// % protected region % [Customise ConfigureDatabaseConnection method here] off begin
		/// <summary>
		/// Set up the database connection
		/// </summary>
		/// <param name="services"></param>
		private void ConfigureDatabaseConnection(IServiceCollection services)
		{
			var dbConnectionString = Configuration.GetConnectionString("DbConnectionString");
			services.AddDbContext<SportstatsDBContext>(options =>
			{
				options.UseNpgsql(dbConnectionString);
				options.UseOpenIddict<Guid>();
			});
		}
		// % protected region % [Customise ConfigureDatabaseConnection method here] end

		private void AddSwaggerService(IServiceCollection services)
		{
			// % protected region % [Customise Swagger configuration here] off begin
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("json", new OpenApiInfo {Title = "Sportstats", Version = "v1"});
				options.ResolveConflictingActions(a => a.First());

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
			// % protected region % [Customise Swagger configuration here] end
		}

		private void ConfigureAuthServices(IServiceCollection services)
		{
			// % protected region % [Configure XSRF here] off begin
			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
			// % protected region % [Configure XSRF here] end

			// % protected region % [Configure data protection here] off begin
			services.AddDataProtection()
				.PersistKeysToDbContext<SportstatsDBContext>();
			// % protected region % [Configure data protection here] end

			// % protected region % [Configure password requirements here] off begin
			// Register Identity Services
			services.AddIdentity<User, Group>(options =>
				{
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

				})
				.AddEntityFrameworkStores<SportstatsDBContext>()
				.AddDefaultTokenProviders();
			// % protected region % [Configure password requirements here] end

			// % protected region % [Customize your OIDC/oAuth2 library] off begin
			ConfigureAuthorizationLibrary(services);
			// % protected region % [Customize your OIDC/oAuth2 library] end

			var certSetting = Configuration.GetSection("CertificateSetting").Get<CertificateSetting>();
			// % protected region % [add any configuration after the cretificate] off begin
			// % protected region % [add any configuration after the cretificate] end

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			services.AddAuthentication("Identity.Application")
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					// % protected region % [Change AddCookie logic here] off begin
					options.LoginPath = "/api/authorization/login";
					options.LogoutPath = "/api/authorization/logout";
					options.SlidingExpiration = true;
					options.ExpireTimeSpan = TimeSpan.FromDays(7);
					options.Events.OnRedirectToLogin = redirectOptions =>
					{
						redirectOptions.Response.StatusCode = StatusCodes.Status401Unauthorized;
						return Task.CompletedTask;
					};
					// % protected region % [Change AddCookie logic here] end
				})
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
					// % protected region % [Change AddJwtBearer logic here] off begin
					options.Authority = certSetting.JwtBearerAuthority;
					options.Audience = certSetting.JwtBearerAudience;
					options.RequireHttpsMetadata = false;
					options.IncludeErrorDetails = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						NameClaimType = OpenIdConnectConstants.Claims.Name,
						RoleClaimType = OpenIdConnectConstants.Claims.Role
					};
					// % protected region % [Change AddJwtBearer logic here] end
				})
				// % protected region % [Add additional authentication chain methods here] off begin
				// % protected region % [Add additional authentication chain methods here] end
				;

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
		}

		private void ConfigureAuthorizationLibrary(IServiceCollection services)
		{
			// % protected region % [Configure authorization library here] off begin
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

					if (cert == null)
					{
						// not for production, use x509 certificate and .AddSigningCertificate()
						options.AddEphemeralSigningKey();
					}
					else
					{
						options.AddSigningCertificate(cert);
					}

					// use jwt
					options.UseJsonWebTokens();
					options.AllowPasswordFlow();
					options.AllowRefreshTokenFlow();
					options.AcceptAnonymousClients();
					options.DisableHttpsRequirement();
				});
			// % protected region % [Configure authorization library here] end
		}

		private void ConfigureScopedServices(IServiceCollection services) {
			// Register service to seed test data
			services.TryAddScoped<DataSeedHelper>();

			// Register core scoped services
			services.TryAddScoped<IUserService, UserService>();
			services.TryAddScoped<IGraphQlService, GraphQlService>();
			services.TryAddScoped<ICrudService, CrudService>();
			services.TryAddScoped<ISecurityService, SecurityService>();
			services.TryAddScoped<IIdentityService, IdentityService>();
			services.TryAddScoped<IEmailService, EmailService>();
			services.TryAddScoped<IAuditService, AuditService>();
			services.TryAddScoped<IXsrfService, XsrfService>();
			services.TryAddScoped<ITimelineGroupingService, TimelineGroupingService>();

			// Register context filters
			services.TryAddScoped<AntiforgeryFilter>();
			services.TryAddScoped<XsrfActionFilter>();

			// % protected region % [Configure storage provider services here] off begin
			// Configure the file system provider to use
			var storageOptions = new StorageProviderConfiguration();
			Configuration.GetSection("StorageProvider").Bind(storageOptions);
			switch (storageOptions.Provider)
			{
				case StorageProviders.S3:
					services.TryAddScoped<IUploadStorageProvider, S3StorageProvider>();
					break;
				case StorageProviders.FILE_SYSTEM:
				default:
					services.TryAddScoped<IUploadStorageProvider, FileSystemStorageProvider>();
					break;
			}
			// % protected region % [Configure storage provider services here] end

			// % protected region % [Add extra core scoped services here] off begin
			// % protected region % [Add extra core scoped services here] end
		}

		private void ConfigureGraphql(IServiceCollection services)
		{
			// GraphQL types must be registered as singleton services. This is since building the underlying graph is
			// expensive and should only be done once.
			services.TryAddSingleton<ScheduleEntityType>();
			services.TryAddSingleton<ScheduleEntityInputType>();
			services.TryAddSingleton<ScheduleEntityFormVersionType>();
			services.TryAddSingleton<ScheduleEntityFormVersionInputType>();
			services.TryAddSingleton<SeasonEntityType>();
			services.TryAddSingleton<SeasonEntityInputType>();
			services.TryAddSingleton<SeasonEntityFormVersionType>();
			services.TryAddSingleton<SeasonEntityFormVersionInputType>();
			services.TryAddSingleton<VenueEntityType>();
			services.TryAddSingleton<VenueEntityInputType>();
			services.TryAddSingleton<VenueEntityFormVersionType>();
			services.TryAddSingleton<VenueEntityFormVersionInputType>();
			services.TryAddSingleton<GameEntityType>();
			services.TryAddSingleton<GameEntityInputType>();
			services.TryAddSingleton<GameEntityFormVersionType>();
			services.TryAddSingleton<GameEntityFormVersionInputType>();
			services.TryAddSingleton<SportEntityType>();
			services.TryAddSingleton<SportEntityInputType>();
			services.TryAddSingleton<SportEntityFormVersionType>();
			services.TryAddSingleton<SportEntityFormVersionInputType>();
			services.TryAddSingleton<LeagueEntityType>();
			services.TryAddSingleton<LeagueEntityInputType>();
			services.TryAddSingleton<LeagueEntityFormVersionType>();
			services.TryAddSingleton<LeagueEntityFormVersionInputType>();
			services.TryAddSingleton<TeamEntityType>();
			services.TryAddSingleton<TeamEntityInputType>();
			services.TryAddSingleton<TeamEntityFormVersionType>();
			services.TryAddSingleton<TeamEntityFormVersionInputType>();
			services.TryAddSingleton<PersonEntityType>();
			services.TryAddSingleton<PersonEntityInputType>();
			services.TryAddSingleton<PersonEntityFormVersionType>();
			services.TryAddSingleton<PersonEntityFormVersionInputType>();
			services.TryAddSingleton<RosterEntityType>();
			services.TryAddSingleton<RosterEntityInputType>();
			services.TryAddSingleton<RosterEntityFormVersionType>();
			services.TryAddSingleton<RosterEntityFormVersionInputType>();
			services.TryAddSingleton<RosterassignmentEntityType>();
			services.TryAddSingleton<RosterassignmentEntityInputType>();
			services.TryAddSingleton<RosterassignmentEntityFormVersionType>();
			services.TryAddSingleton<RosterassignmentEntityFormVersionInputType>();
			services.TryAddSingleton<ScheduleSubmissionEntityType>();
			services.TryAddSingleton<ScheduleSubmissionEntityInputType>();
			services.TryAddSingleton<SeasonSubmissionEntityType>();
			services.TryAddSingleton<SeasonSubmissionEntityInputType>();
			services.TryAddSingleton<VenueSubmissionEntityType>();
			services.TryAddSingleton<VenueSubmissionEntityInputType>();
			services.TryAddSingleton<GameSubmissionEntityType>();
			services.TryAddSingleton<GameSubmissionEntityInputType>();
			services.TryAddSingleton<SportSubmissionEntityType>();
			services.TryAddSingleton<SportSubmissionEntityInputType>();
			services.TryAddSingleton<LeagueSubmissionEntityType>();
			services.TryAddSingleton<LeagueSubmissionEntityInputType>();
			services.TryAddSingleton<TeamSubmissionEntityType>();
			services.TryAddSingleton<TeamSubmissionEntityInputType>();
			services.TryAddSingleton<PersonSubmissionEntityType>();
			services.TryAddSingleton<PersonSubmissionEntityInputType>();
			services.TryAddSingleton<RosterSubmissionEntityType>();
			services.TryAddSingleton<RosterSubmissionEntityInputType>();
			services.TryAddSingleton<RosterassignmentSubmissionEntityType>();
			services.TryAddSingleton<RosterassignmentSubmissionEntityInputType>();
			services.TryAddSingleton<ScheduleEntityFormTileEntityType>();
			services.TryAddSingleton<ScheduleEntityFormTileEntityInputType>();
			services.TryAddSingleton<SeasonEntityFormTileEntityType>();
			services.TryAddSingleton<SeasonEntityFormTileEntityInputType>();
			services.TryAddSingleton<VenueEntityFormTileEntityType>();
			services.TryAddSingleton<VenueEntityFormTileEntityInputType>();
			services.TryAddSingleton<GameEntityFormTileEntityType>();
			services.TryAddSingleton<GameEntityFormTileEntityInputType>();
			services.TryAddSingleton<SportEntityFormTileEntityType>();
			services.TryAddSingleton<SportEntityFormTileEntityInputType>();
			services.TryAddSingleton<LeagueEntityFormTileEntityType>();
			services.TryAddSingleton<LeagueEntityFormTileEntityInputType>();
			services.TryAddSingleton<TeamEntityFormTileEntityType>();
			services.TryAddSingleton<TeamEntityFormTileEntityInputType>();
			services.TryAddSingleton<PersonEntityFormTileEntityType>();
			services.TryAddSingleton<PersonEntityFormTileEntityInputType>();
			services.TryAddSingleton<RosterEntityFormTileEntityType>();
			services.TryAddSingleton<RosterEntityFormTileEntityInputType>();
			services.TryAddSingleton<RosterassignmentEntityFormTileEntityType>();
			services.TryAddSingleton<RosterassignmentEntityFormTileEntityInputType>();
			services.TryAddSingleton<RosterTimelineEventsEntityType>();
			services.TryAddSingleton<RosterTimelineEventsEntityInputType>();
			// % protected region % [Register additional graphql types here] off begin
			// % protected region % [Register additional graphql types here] end

			// Register enum GraphQl types
			services.TryAddSingleton<EnumerationGraphType<Scheduletype>>();
			services.TryAddSingleton<EnumerationGraphType<Roletype>>();

			// Connect the database type to the GraphQL type
			GraphTypeTypeRegistry.Register<ScheduleEntity, ScheduleEntityType>();
			GraphTypeTypeRegistry.Register<ScheduleEntityFormVersion, ScheduleEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<SeasonEntity, SeasonEntityType>();
			GraphTypeTypeRegistry.Register<SeasonEntityFormVersion, SeasonEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<VenueEntity, VenueEntityType>();
			GraphTypeTypeRegistry.Register<VenueEntityFormVersion, VenueEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<GameEntity, GameEntityType>();
			GraphTypeTypeRegistry.Register<GameEntityFormVersion, GameEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<SportEntity, SportEntityType>();
			GraphTypeTypeRegistry.Register<SportEntityFormVersion, SportEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<LeagueEntity, LeagueEntityType>();
			GraphTypeTypeRegistry.Register<LeagueEntityFormVersion, LeagueEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<TeamEntity, TeamEntityType>();
			GraphTypeTypeRegistry.Register<TeamEntityFormVersion, TeamEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<PersonEntity, PersonEntityType>();
			GraphTypeTypeRegistry.Register<PersonEntityFormVersion, PersonEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<RosterEntity, RosterEntityType>();
			GraphTypeTypeRegistry.Register<RosterEntityFormVersion, RosterEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<RosterassignmentEntity, RosterassignmentEntityType>();
			GraphTypeTypeRegistry.Register<RosterassignmentEntityFormVersion, RosterassignmentEntityFormVersionType>();
			GraphTypeTypeRegistry.Register<ScheduleSubmissionEntity, ScheduleSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<SeasonSubmissionEntity, SeasonSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<VenueSubmissionEntity, VenueSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<GameSubmissionEntity, GameSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<SportSubmissionEntity, SportSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<LeagueSubmissionEntity, LeagueSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<TeamSubmissionEntity, TeamSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<PersonSubmissionEntity, PersonSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<RosterSubmissionEntity, RosterSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<RosterassignmentSubmissionEntity, RosterassignmentSubmissionEntityType>();
			GraphTypeTypeRegistry.Register<ScheduleEntityFormTileEntity, ScheduleEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<SeasonEntityFormTileEntity, SeasonEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<VenueEntityFormTileEntity, VenueEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<GameEntityFormTileEntity, GameEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<SportEntityFormTileEntity, SportEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<LeagueEntityFormTileEntity, LeagueEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<TeamEntityFormTileEntity, TeamEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<PersonEntityFormTileEntity, PersonEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<RosterEntityFormTileEntity, RosterEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<RosterassignmentEntityFormTileEntity, RosterassignmentEntityFormTileEntityType>();
			GraphTypeTypeRegistry.Register<RosterTimelineEventsEntity, RosterTimelineEventsEntityType>();
			// % protected region % [Add custom GraphQL Types for custom models here] off begin
			// % protected region % [Add custom GraphQL Types for custom models here] end

			// Add GraphQL core services and executors
			services.TryAddSingleton<IDocumentExecuter, EfDocumentExecuter>();
			services.AddGraphQL();
			services.TryAddSingleton<IDependencyResolver>(
				provider => new FuncDependencyResolver(provider.GetRequiredService)
			);

			// Add the schema and query for graphql
			services.TryAddSingleton<ISchema, SportstatsSchema>();
			services.TryAddSingleton<SportstatsQuery>();
			services.TryAddSingleton<SportstatsMutation>();

			services.TryAddSingleton<IdObjectType>();
			services.TryAddSingleton<NumberObjectType>();
			services.TryAddSingleton<OrderGraph>();
			services.TryAddSingleton<BooleanObjectType>();
			// % protected region % [Add extra GraphQL types here] off begin
			// % protected region % [Add extra GraphQL types here] end

			// Send our db context to graphql to use
			EfGraphQLConventions.RegisterInContainer<SportstatsDBContext>(services);
			EfGraphQLConventions.RegisterConnectionTypesInContainer(services);
		}

		/// <summary>
		/// Read in configuration key value tuples from the appsettings.xxx files.
		/// </summary>
		/// <param name="services"></param>
		private void AddApplicationConfigurations(IServiceCollection services)
		{
			services.Configure<EmailAccount>(Configuration.GetSection("EmailAccount"));
			services.Configure<StorageProviderConfiguration>(Configuration.GetSection("StorageProvider"));
			services.Configure<FileSystemStorageProviderConfiguration>(Configuration.GetSection("FileSystemStorageProvider"));
			services.Configure<S3StorageProviderConfiguration>(Configuration.GetSection("S3StorageProvider"));
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
			// % protected region % [Add Configure arguments here] off begin
			// % protected region % [Add Configure arguments here] end
			IApplicationBuilder app,
			IWebHostEnvironment env,
			DataSeedHelper dataSeed,
			ILogger<AuditLog> logger)
		{
			// % protected region % [Add methods before audit config here] off begin
			// % protected region % [Add methods before audit config here] end

			Audit.Core.Configuration.Setup()
				.UseDynamicProvider(configurator =>
				{
					configurator.OnInsert(audit => AuditUtilities.LogAuditEvent(audit, logger));
					configurator.OnReplace((obj, audit) => AuditUtilities.LogAuditEvent(audit, logger));
				});

			// % protected region % [Add methods before data seeding here] off begin
			// % protected region % [Add methods before data seeding here] end

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

			// % protected region % [Add methods before logging config here] off begin
			// % protected region % [Add methods before logging config here] end

			app.UseSerilogRequestLogging(options =>
			{
				options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} by user: {User} responded {StatusCode} in {Elapsed:0.0000} ms";
				options.EnrichDiagnosticContext = (context, httpContext) =>
				{
					context.Set("User", httpContext.User?.Identity.Name);
					context.Set("UserId", httpContext.User?.FindFirst("UserId")?.Value);
					// % protected region % [Add extra log enrichment here] off begin
					// % protected region % [Add extra log enrichment here] end
				};
				// % protected region % [Add log configuration here] off begin
				// % protected region % [Add log configuration here] end
			});

			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMiddleware<AuditMiddleware>();

			// % protected region % [Alter swagger configuration here] off begin
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
			// % protected region % [Alter swagger configuration here] end

			app.UseRouting();
			// % protected region % [add configuration after routing] off begin
			// % protected region % [add configuration after routing] end

			app.UseAuthentication();
			app.UseAuthorization();
			// % protected region % [Add cors settings here] off begin
			// % protected region % [Add cors settings here] end

			// % protected region % [Configure endpoints here] off begin
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
			});
			// % protected region % [Configure endpoints here] end

			// % protected region % [add extra configuration settings here] off begin
			// % protected region % [add extra configuration settings here] end

			// % protected region % [Alter SPA configuration here] off begin
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
			// % protected region % [Alter SPA configuration here] end
		}
		// % protected region % [Add any custom startup methods here] off begin
		// % protected region % [Add any custom startup methods here] end
	}
}
