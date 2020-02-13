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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audit.Core;
using Audit.Core.Extensions;
using Npgsql;
using Audit.EntityFramework;
using Audit.EntityFramework.ConfigurationApi;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Models {
	public partial class SportstatsDBContext : IdentityDbContext<User, Group, Guid>, IAuditDbContext
	{
		private readonly ILogger<SportstatsDBContext> _logger;
		private readonly DbContextHelper _helper = new DbContextHelper();

		// Fields required for the audit db context
		public string AuditEventType { get; set; }
		public bool AuditDisabled { get; set; }
		public bool IncludeEntityObjects { get; set; }
		public bool ExcludeValidationResults { get; set; }
		public AuditOptionMode Mode { get; set; }
		public AuditDataProvider AuditDataProvider { get; set; }
		public Dictionary<string, object> ExtraFields { get; } = new Dictionary<string, object>();
		public DbContext DbContext => this;
		public Dictionary<Type, EfEntitySettings> EntitySettings { get; set; }
		public bool ExcludeTransactionId { get; set; }

		public string SessionUser { get; }
		public string SessionId { get; }

		public DbSet<Sport> Sport { get; set; }
		public DbSet<League> League { get; set; }
		public DbSet<User> User { get; set; }

		static SportstatsDBContext()
		{
			// % protected region % [Add extra methods to the static constructor here] off begin
			// % protected region % [Add extra methods to the static constructor here] end
		}

		public SportstatsDBContext(
			DbContextOptions<SportstatsDBContext> options,
			IHttpContextAccessor httpContextAccessor,
			ILogger<SportstatsDBContext> logger) : base(options)
		{
			_logger = logger;
			_helper.SetConfig(this);

			SessionUser = httpContextAccessor?.HttpContext?.User?.FindFirst("UserId")?.Value;
			SessionId = httpContextAccessor?.HttpContext?.TraceIdentifier;

			// % protected region % [Add any constructor config here] off begin
			// % protected region % [Add any constructor config here] end
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			// Configure models from the entity diagram
			modelBuilder.HasPostgresExtension("uuid-ossp");
			modelBuilder.ApplyConfiguration(new SportConfiguration());
			modelBuilder.ApplyConfiguration(new LeagueConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());

			// Configure the user and group models
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GroupConfiguration());

			// Configure the audit log model
			modelBuilder.ApplyConfiguration(new AuditLogConfiguration());

			// % protected region % [Add any further model config here] off begin
			// % protected region % [Add any further model config here] end
		}

		public override int SaveChanges()
		{
			if (AuditDisabled)
			{
				return base.SaveChanges();
			}
			var efEvent = _helper.CreateAuditEvent(this);
			if (efEvent == null)
			{
				return base.SaveChanges();
			}
			var scope = _helper.CreateAuditScope(this, efEvent);
			try
			{
				efEvent.Result = base.SaveChanges();
			}
			catch (Exception ex)
			{
				efEvent.Success = false;
				efEvent.ErrorMessage = ex.GetExceptionInfo();
				try
				{
					AuditDisabled = true;
					_helper.SaveScope(this, scope, efEvent);
				}
				catch (Exception e)
				{
					_logger?.LogInformation("Failed to log database event, most likely due to a cancelled transaction");
					_logger?.LogDebug(e.ToString());
				}

				throw;
			}
			efEvent.Success = true;
			_helper.SaveScope(this, scope, efEvent);
			return efEvent.Result;
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (AuditDisabled)
			{
				return await base.SaveChangesAsync(cancellationToken);
			}
			var efEvent = _helper.CreateAuditEvent(this);
			if (efEvent == null)
			{
				return await base.SaveChangesAsync(cancellationToken);
			}
			var scope = await _helper.CreateAuditScopeAsync(this, efEvent);
			try
			{
				efEvent.Result = await base.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				efEvent.Success = false;
				efEvent.ErrorMessage = ex.GetExceptionInfo();
				try
				{
					AuditDisabled = true;
					await _helper.SaveScopeAsync(this, scope, efEvent);
				}
				catch (Exception e)
				{
					_logger?.LogInformation("Failed to log database event, most likely due to a cancelled transaction");
					_logger?.LogDebug(e.ToString());
				}
				throw;
			}
			efEvent.Success = true;
			await _helper.SaveScopeAsync(this, scope, efEvent);
			return efEvent.Result;
		}

		public void OnScopeSaving(AuditScope auditScope)
		{
			// % protected region % [Add any extra logic on audit scope saving here] off begin
			// % protected region % [Add any extra logic on audit scope saving here] end
		}

		public void OnScopeCreated(AuditScope auditScope)
		{
			// % protected region % [Add any extra logic on audit scope creating here] off begin
			// % protected region % [Add any extra logic on audit scope creating here] end
		}

		/// <summary>
		/// Gets a DbSet of a certain type from the context
		/// </summary>
		/// <param name="name">The name of the DbSet to retrieve</param>
		/// <typeparam name="T">The type to cast the DbSet to</typeparam>
		/// <returns>A DbSet of the given type</returns>
		public DbSet<T> GetDbSet<T>(string name = null) where T : class, IAbstractModel
		{
			return GetType().GetProperty(name ?? typeof(T).Name).GetValue(this, null) as DbSet<T>;
		}

		/// <summary>
		/// Gets a DbSet as an IQueryable over the owner abstract model
		/// </summary>
		/// <param name="name">The name of the DbSet to retrieve</param>
		/// <returns>The DbSet as an IQueryable over the OwnerAbstractModel or null if it doesn't exist</returns>
		public IQueryable GetOwnerDbSet(string name)
		{
			return GetType().GetProperty(name).GetValue(this, null) as IQueryable;
		}

		// % protected region % [Add any extra db config here] off begin
		// % protected region % [Add any extra db config here] end
	}
}
