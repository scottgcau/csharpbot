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
	/// Ladder entity
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("Ladder")]
	// % protected region % [Configure entity attributes here] end
	public class LadderEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		/// <summary>
		/// Type of ladder
		/// </summary>
		// % protected region % [Customise Laddertype here] off begin
		[EntityAttribute]
		public Laddertype Laddertype { get; set; }
		// % protected region % [Customise Laddertype here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public LadderEntity()
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
			new VisitorsLadderEntity(),
			new SystemuserLadderEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise Ladderwinlossess here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.LadderwinlossEntity"/>
		[EntityForeignKey("Ladderwinlossess", "Ladder", false, typeof(LadderwinlossEntity))]
		public ICollection<LadderwinlossEntity> Ladderwinlossess { get; set; }
		// % protected region % [Customise Ladderwinlossess here] end

		// % protected region % [Customise Laddereliminationss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.LaddereliminationEntity"/>
		[EntityForeignKey("Laddereliminationss", "Ladder", false, typeof(LaddereliminationEntity))]
		public ICollection<LaddereliminationEntity> Laddereliminationss { get; set; }
		// % protected region % [Customise Laddereliminationss here] end

		// % protected region % [Customise Schedule here] off begin
		/// <summary>
		/// Incoming one to one reference
		/// </summary>
		/// <see cref="Sportstats.Models.ScheduleEntity"/>
		[EntityForeignKey("Schedule", "Ladder", false, typeof(ScheduleEntity))]
		public ScheduleEntity Schedule { get; set; }
		// % protected region % [Customise Schedule here] end

		public async Task BeforeSave(
			EntityState operation,
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any initial before save logic here] off begin
			// % protected region % [Add any initial before save logic here] end

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
			var modelList = models.Cast<LadderEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Ladderwinlossess":
					var ladderwinlossesIds = modelList.SelectMany(x => x.Ladderwinlossess.Select(m => m.Id)).ToList();
					var oldladderwinlosses = await dbContext.LadderwinlossEntity
						.Where(m => m.LadderId.HasValue && ids.Contains(m.LadderId.Value))
						.Where(m => !ladderwinlossesIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var ladderwinlosses in oldladderwinlosses)
					{
						ladderwinlosses.LadderId = null;
					}

					dbContext.LadderwinlossEntity.UpdateRange(oldladderwinlosses);
					return oldladderwinlosses.Count;
				case "Laddereliminationss":
					var laddereliminationsIds = modelList.SelectMany(x => x.Laddereliminationss.Select(m => m.Id)).ToList();
					var oldladdereliminations = await dbContext.LaddereliminationEntity
						.Where(m => m.LadderId.HasValue && ids.Contains(m.LadderId.Value))
						.Where(m => !laddereliminationsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var laddereliminations in oldladdereliminations)
					{
						laddereliminations.LadderId = null;
					}

					dbContext.LaddereliminationEntity.UpdateRange(oldladdereliminations);
					return oldladdereliminations.Count;
				case "Schedule":
					var scheduleIds = modelList.Select(x => x.Schedule.Id).ToList();
					var oldschedule = await dbContext.ScheduleEntity
						.Where(m => m.LadderId.HasValue && ids.Contains(m.LadderId.Value))
						.Where(m => !scheduleIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var schedule in oldschedule)
					{
						schedule.LadderId = null;
					}

					dbContext.ScheduleEntity.UpdateRange(oldschedule);
					return oldschedule.Count;
				// % protected region % [Add any extra clean reference logic here] off begin
				// % protected region % [Add any extra clean reference logic here] end
				default:
					return 0;
			}
		}

		// % protected region % [Add any further references here] off begin
		// % protected region % [Add any further references here] end
	}
}