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
	/// Schedule entity
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("Schedule")]
	// % protected region % [Configure entity attributes here] end
	public class ScheduleEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		/// <summary>
		/// Schedule name
		/// </summary>
		[Required]
		// % protected region % [Customise Fullname here] off begin
		[EntityAttribute]
		public String Fullname { get; set; }
		// % protected region % [Customise Fullname here] end

		// % protected region % [Customise Scheduletype here] off begin
		[EntityAttribute]
		public Scheduletype Scheduletype { get; set; }
		// % protected region % [Customise Scheduletype here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public ScheduleEntity()
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
			new VisitorsScheduleEntity(),
			new SystemuserScheduleEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise Roundss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.RoundEntity"/>
		[EntityForeignKey("Roundss", "Schedule", false, typeof(RoundEntity))]
		public ICollection<RoundEntity> Roundss { get; set; }
		// % protected region % [Customise Roundss here] end

		// % protected region % [Customise Season here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.SeasonEntity"/>
		public Guid? SeasonId { get; set; }
		[EntityForeignKey("Season", "Scheduless", false, typeof(SeasonEntity))]
		public SeasonEntity Season { get; set; }
		// % protected region % [Customise Season here] end

		// % protected region % [Customise Ladder here] off begin
		/// <summary>
		/// Outgoing one to one reference
		/// </summary>
		/// <see cref="Sportstats.Models.LadderEntity"/>
		public Guid? LadderId { get; set; }
		[EntityForeignKey("Ladder", "Schedule", false, typeof(LadderEntity))]
		public LadderEntity Ladder { get; set; }
		// % protected region % [Customise Ladder here] end

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
			var modelList = models.Cast<ScheduleEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Roundss":
					var roundsIds = modelList.SelectMany(x => x.Roundss.Select(m => m.Id)).ToList();
					var oldrounds = await dbContext.RoundEntity
						.Where(m => m.ScheduleId.HasValue && ids.Contains(m.ScheduleId.Value))
						.Where(m => !roundsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var rounds in oldrounds)
					{
						rounds.ScheduleId = null;
					}

					dbContext.RoundEntity.UpdateRange(oldrounds);
					return oldrounds.Count;
				case "Ladder":
					var ladderIds = modelList
						.Select(m => m.LadderId)
						.Where(m => m.HasValue);
					var oldladder = await dbContext.ScheduleEntity
						.Where(m => ladderIds.Contains(m.LadderId))
						.ToListAsync(cancellation);

					foreach (var ladder in oldladder) {
						ladder.LadderId = null;
					}

					dbContext.ScheduleEntity.UpdateRange(oldladder);
					return oldladder.Count;
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