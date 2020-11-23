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
using System.Linq;
using Npgsql;
using Audit.EntityFramework;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sportstats.Enums;
using Microsoft.Extensions.Logging;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Models {
	// % protected region % [Change the class signature here] off begin
	public class SportstatsDBContext : AuditIdentityDbContext<User, Group, Guid>, IDataProtectionKeyContext
	// % protected region % [Change the class signature here] end
	{
		private readonly ILogger<SportstatsDBContext> _logger;

		public string SessionUserId { get; }
		public string SessionUser { get; }
		public string SessionId { get; }

		// % protected region % [Add any custom class variables] off begin
		// % protected region % [Add any custom class variables] end

		public DbSet<UploadFile> Files { get; set; }
		public DbSet<ScheduleEntity> ScheduleEntity { get; set; }
		public DbSet<ScheduleEntityFormVersion> ScheduleEntityFormVersion { get; set; }
		public DbSet<SeasonEntity> SeasonEntity { get; set; }
		public DbSet<SeasonEntityFormVersion> SeasonEntityFormVersion { get; set; }
		public DbSet<VenueEntity> VenueEntity { get; set; }
		public DbSet<VenueEntityFormVersion> VenueEntityFormVersion { get; set; }
		public DbSet<GameEntity> GameEntity { get; set; }
		public DbSet<GameEntityFormVersion> GameEntityFormVersion { get; set; }
		public DbSet<SportEntity> SportEntity { get; set; }
		public DbSet<SportEntityFormVersion> SportEntityFormVersion { get; set; }
		public DbSet<LeagueEntity> LeagueEntity { get; set; }
		public DbSet<LeagueEntityFormVersion> LeagueEntityFormVersion { get; set; }
		public DbSet<TeamEntity> TeamEntity { get; set; }
		public DbSet<TeamEntityFormVersion> TeamEntityFormVersion { get; set; }
		public DbSet<PersonEntity> PersonEntity { get; set; }
		public DbSet<PersonEntityFormVersion> PersonEntityFormVersion { get; set; }
		public DbSet<RosterEntity> RosterEntity { get; set; }
		public DbSet<RosterEntityFormVersion> RosterEntityFormVersion { get; set; }
		public DbSet<RosterassignmentEntity> RosterassignmentEntity { get; set; }
		public DbSet<RosterassignmentEntityFormVersion> RosterassignmentEntityFormVersion { get; set; }
		public DbSet<ScheduleSubmissionEntity> ScheduleSubmissionEntity { get; set; }
		public DbSet<SeasonSubmissionEntity> SeasonSubmissionEntity { get; set; }
		public DbSet<VenueSubmissionEntity> VenueSubmissionEntity { get; set; }
		public DbSet<GameSubmissionEntity> GameSubmissionEntity { get; set; }
		public DbSet<SportSubmissionEntity> SportSubmissionEntity { get; set; }
		public DbSet<LeagueSubmissionEntity> LeagueSubmissionEntity { get; set; }
		public DbSet<TeamSubmissionEntity> TeamSubmissionEntity { get; set; }
		public DbSet<PersonSubmissionEntity> PersonSubmissionEntity { get; set; }
		public DbSet<RosterSubmissionEntity> RosterSubmissionEntity { get; set; }
		public DbSet<RosterassignmentSubmissionEntity> RosterassignmentSubmissionEntity { get; set; }
		public DbSet<ScheduleEntityFormTileEntity> ScheduleEntityFormTileEntity { get; set; }
		public DbSet<SeasonEntityFormTileEntity> SeasonEntityFormTileEntity { get; set; }
		public DbSet<VenueEntityFormTileEntity> VenueEntityFormTileEntity { get; set; }
		public DbSet<GameEntityFormTileEntity> GameEntityFormTileEntity { get; set; }
		public DbSet<SportEntityFormTileEntity> SportEntityFormTileEntity { get; set; }
		public DbSet<LeagueEntityFormTileEntity> LeagueEntityFormTileEntity { get; set; }
		public DbSet<TeamEntityFormTileEntity> TeamEntityFormTileEntity { get; set; }
		public DbSet<PersonEntityFormTileEntity> PersonEntityFormTileEntity { get; set; }
		public DbSet<RosterEntityFormTileEntity> RosterEntityFormTileEntity { get; set; }
		public DbSet<RosterassignmentEntityFormTileEntity> RosterassignmentEntityFormTileEntity { get; set; }
		public DbSet<RosterTimelineEventsEntity> RosterTimelineEventsEntity { get; set; }
		public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

		static SportstatsDBContext()
		{
			NpgsqlConnection.GlobalTypeMapper.MapEnum<Scheduletype>();
			NpgsqlConnection.GlobalTypeMapper.MapEnum<Roletype>();
			// % protected region % [Add extra methods to the static constructor here] off begin
			// % protected region % [Add extra methods to the static constructor here] end
		}

		public SportstatsDBContext(
			// % protected region % [Add any custom constructor paramaters] off begin
			// % protected region % [Add any custom constructor paramaters] end
			DbContextOptions<SportstatsDBContext> options,
			IHttpContextAccessor httpContextAccessor,
			ILogger<SportstatsDBContext> logger) : base(options)
		{
			_logger = logger;

			SessionUser = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
			SessionUserId = httpContextAccessor?.HttpContext?.User?.FindFirst("UserId")?.Value;
			SessionId = httpContextAccessor?.HttpContext?.TraceIdentifier;

			// % protected region % [Add any constructor config here] off begin
			// % protected region % [Add any constructor config here] end
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasPostgresEnum<Scheduletype>();
			modelBuilder.HasPostgresEnum<Roletype>();
			// Configure models from the entity diagram
			modelBuilder.HasPostgresExtension("uuid-ossp");
			modelBuilder.ApplyConfiguration(new ScheduleEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SeasonEntityConfiguration());
			modelBuilder.ApplyConfiguration(new VenueEntityConfiguration());
			modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SportEntityConfiguration());
			modelBuilder.ApplyConfiguration(new LeagueEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TeamEntityConfiguration());
			modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterassignmentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ScheduleSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SeasonSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new VenueSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new GameSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SportSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new LeagueSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TeamSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new PersonSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterassignmentSubmissionEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ScheduleEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SeasonEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new VenueEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new GameEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SportEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new LeagueEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TeamEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new PersonEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterassignmentEntityFormTileEntityConfiguration());
			modelBuilder.ApplyConfiguration(new RosterTimelineEventsEntityConfiguration());

			// Configure the user and group models
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GroupConfiguration());

			// Configure the file upload models
			modelBuilder.ApplyConfiguration(new UploadFileConfiguration());

			// % protected region % [Add any further model config here] off begin
			// % protected region % [Add any further model config here] end
		}

		/// <summary>
		/// Gets a DbSet of a certain type from the context
		/// </summary>
		/// <param name="name">The name of the DbSet to retrieve</param>
		/// <typeparam name="T">The type to cast the DbSet to</typeparam>
		/// <returns>A DbSet of the given type</returns>
		[Obsolete("Please obtain the db set from the db context with generic type param instead.")]
		public DbSet<T> GetDbSet<T>(string name = null) where T : class, IAbstractModel
		{
			// % protected region % [Add any extra logic on GetDbSet here] off begin
			// % protected region % [Add any extra logic on GetDbSet here] end

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
