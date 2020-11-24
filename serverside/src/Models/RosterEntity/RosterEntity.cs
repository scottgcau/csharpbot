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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sportstats.Helpers;
using Sportstats.Models.Interfaces;
using Sportstats.Enums;
using Sportstats.Security;
using Sportstats.Security.Acl;
using Sportstats.Validators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Z.EntityFramework.Plus;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Sportstats.Models {
	/// <summary>
	/// Roster entity
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("Roster")]
	// % protected region % [Configure entity attributes here] end
	public class RosterEntity : IOwnerAbstractModel, ITimelineEntity 	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise Fullname here] off begin
		[EntityAttribute]
		public String Fullname { get; set; }
		// % protected region % [Customise Fullname here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public RosterEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		// % protected region % [Customise ACL attributes here] off begin
		[NotMapped]
		[JsonIgnore]
		// % protected region % [Customise ACL attributes here] end
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new SuperAdministratorsScheme(),
			new VisitorsRosterEntity(),
			new SystemuserRosterEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise Rosterassignmentss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.RosterassignmentEntity"/>
		[EntityForeignKey("Rosterassignmentss", "Roster", false, typeof(RosterassignmentEntity))]
		public ICollection<RosterassignmentEntity> Rosterassignmentss { get; set; }
		// % protected region % [Customise Rosterassignmentss here] end

		// % protected region % [Customise Team here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.TeamEntity"/>
		public Guid? TeamId { get; set; }
		[EntityForeignKey("Team", "Rosterss", false, typeof(TeamEntity))]
		public TeamEntity Team { get; set; }
		// % protected region % [Customise Team here] end

		// % protected region % [Customise Season here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.SeasonEntity"/>
		public Guid? SeasonId { get; set; }
		[EntityForeignKey("Season", "Rosterss", false, typeof(SeasonEntity))]
		public SeasonEntity Season { get; set; }
		// % protected region % [Customise Season here] end

		// % protected region % [Customise LoggedEvents here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.RosterTimelineEventsEntity"/>
		[EntityForeignKey("LoggedEvents", "Entity", false, typeof(RosterTimelineEventsEntity))]
		public ICollection<RosterTimelineEventsEntity> LoggedEvents { get; set; }
		// % protected region % [Customise LoggedEvents here] end

		public async Task BeforeSave(
			EntityState operation,
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial before save logic here] off begin
			// % protected region % [Add any initial before save logic here] end

			// Create timeline event when an entity is added
			if (operation == EntityState.Added)
			{
				await CreateTimelineCreateEventsAsync(dbContext, serviceProvider, cancellationToken);
			}

			// Create a timeline event when an entity is modified
			if (operation == EntityState.Modified)
			{
				var original = await dbContext.RosterEntity
					.AsNoTracking()
					.Where(x => x.Id == Id)
					.FirstOrDefaultAsync(cancellationToken);
				await CreateTimelineEventsAsync(original, dbContext, serviceProvider, cancellationToken);
			}

			// Create a timeline event when the entity is going to be deleted
			if (operation == EntityState.Deleted)
			{
				await CreateTimelineDeleteEventsAsync(dbContext, serviceProvider, cancellationToken);
			}
			// % protected region % [Add any before save logic here] off begin
			// % protected region % [Add any before save logic here] end
		}

		public async Task AfterSave(
			EntityState operation,
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			ICollection<ChangeState> changes,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial after save logic here] off begin
			// % protected region % [Add any initial after save logic here] end

			// % protected region % [Add any after save logic here] off begin
			// % protected region % [Add any after save logic here] end
		}

		public async Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			SportstatsDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel
		{
			var modelList = models.Cast<RosterEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Rosterassignmentss":
					var rosterassignmentsIds = modelList.SelectMany(x => x.Rosterassignmentss.Select(m => m.Id)).ToList();
					var oldrosterassignments = await dbContext.RosterassignmentEntity
						.Where(m => m.RosterId.HasValue && ids.Contains(m.RosterId.Value))
						.Where(m => !rosterassignmentsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var rosterassignments in oldrosterassignments)
					{
						rosterassignments.RosterId = null;
					}

					dbContext.RosterassignmentEntity.UpdateRange(oldrosterassignments);
					return oldrosterassignments.Count;
				case "LoggedEvents":
					var loggedEventIds = modelList.SelectMany(x => x.LoggedEvents.Select(m => m.Id)).ToList();
					var oldloggedEvent = await dbContext.RosterTimelineEventsEntity
						.Where(m => m.EntityId.HasValue && ids.Contains(m.EntityId.Value))
						.Where(m => !loggedEventIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var loggedEvent in oldloggedEvent)
					{
						loggedEvent.EntityId = null;
					}

					dbContext.RosterTimelineEventsEntity.UpdateRange(oldloggedEvent);
					return oldloggedEvent.Count;
				// % protected region % [Add any extra clean reference logic here] off begin
				// % protected region % [Add any extra clean reference logic here] end
				default:
					return 0;
			}
		}

		// % protected region % [Add any further references here] off begin
		// % protected region % [Add any further references here] end

		// % protected region % [Override CreateTimelineEventsAsync method here] off begin
		public async Task CreateTimelineEventsAsync<TEntity>(
			TEntity original,
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
			where TEntity : IOwnerAbstractModel
		// % protected region % [Override CreateTimelineEventsAsync method here] end
		{
			// % protected region % [Override CreateTimelineEventsAsync type check here] off begin
			if (!(original is RosterEntity originalEntity))
			{
				return;
			}
			// % protected region % [Override CreateTimelineEventsAsync type check here] end

			var timelineEvents = new List<ITimelineEventEntity>();

			// % protected region % [Override CreateTimelineEventsAsync 'Fullname' case here] off begin
			timelineEvents.ConditionalAddUpdateEvent<RosterTimelineEventsEntity>(
				"RosterEntity",
				"Fullname",
				 originalEntity.Fullname,
				 Fullname,
				 Id);
			// % protected region % [Override CreateTimelineEventsAsync 'Fullname' case here] end
			// % protected region % [Override CreateTimelineEventsAsync 'Team' case here] off begin
			timelineEvents.ConditionalAddUpdateEvent<RosterTimelineEventsEntity>(
				"RosterEntity",
				"TeamId",
				originalEntity.TeamId,
				TeamId,
				Id);
			// % protected region % [Override CreateTimelineEventsAsync 'Team' case here] end
			// % protected region % [Override CreateTimelineEventsAsync 'Season' case here] off begin
			timelineEvents.ConditionalAddUpdateEvent<RosterTimelineEventsEntity>(
				"RosterEntity",
				"SeasonId",
				originalEntity.SeasonId,
				SeasonId,
				Id);
			// % protected region % [Override CreateTimelineEventsAsync 'Season' case here] end

			// % protected region % [Add any further timeline update events here] off begin
			// % protected region % [Add any further timeline update events here] end

			// % protected region % [Override CreateTimelineEventsAsync database call here] off begin
			await dbContext.AddRangeAsync(timelineEvents, cancellationToken);
			// % protected region % [Override CreateTimelineEventsAsync database call here] end
		}

		// % protected region % [Override CreateTimelineCreateEventsAsync here] off begin
		public async Task CreateTimelineCreateEventsAsync(
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			var timelineEvents = new List<ITimelineEventEntity>();
			timelineEvents.AddCreateEvent<RosterTimelineEventsEntity>("RosterEntity", Id);
			await dbContext.AddRangeAsync(timelineEvents, cancellationToken);
		}
		// % protected region % [Override CreateTimelineCreateEventsAsync here] end

		// % protected region % [Override CreateTimelineDeleteEventsAsync here] off begin
		public async Task CreateTimelineDeleteEventsAsync(
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			var timelineEvents = new List<ITimelineEventEntity>();
			timelineEvents.AddDeleteEvent<RosterTimelineEventsEntity>("RosterEntity", Id);
			await dbContext.AddRangeAsync(timelineEvents, cancellationToken);
		}
		// % protected region % [Override CreateTimelineDeleteEventsAsync here] end
	}
}