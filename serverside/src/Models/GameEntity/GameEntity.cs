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
	/// Scheduled game
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("Game")]
	// % protected region % [Configure entity attributes here] end
	public class GameEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[Required]
		// % protected region % [Customise Datestart here] off begin
		[EntityAttribute]
		public DateTime? Datestart { get; set; }
		// % protected region % [Customise Datestart here] end

		// % protected region % [Customise Homepoints here] off begin
		[EntityAttribute]
		public int? Homepoints { get; set; }
		// % protected region % [Customise Homepoints here] end

		// % protected region % [Customise Awaypoints here] off begin
		[EntityAttribute]
		public int? Awaypoints { get; set; }
		// % protected region % [Customise Awaypoints here] end

		// % protected region % [Customise Hometeamid here] off begin
		[EntityAttribute]
		public String Hometeamid { get; set; }
		// % protected region % [Customise Hometeamid here] end

		// % protected region % [Customise Awayteamid here] off begin
		[EntityAttribute]
		public String Awayteamid { get; set; }
		// % protected region % [Customise Awayteamid here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public GameEntity()
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
			new VisitorsGameEntity(),
			new SystemuserGameEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		// % protected region % [Customise Round here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.RoundEntity"/>
		public Guid? RoundId { get; set; }
		[EntityForeignKey("Round", "Gamess", false, typeof(RoundEntity))]
		public RoundEntity Round { get; set; }
		// % protected region % [Customise Round here] end

		// % protected region % [Customise Gamerefereess here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.GamerefereeEntity"/>
		[EntityForeignKey("Gamerefereess", "Game", false, typeof(GamerefereeEntity))]
		public ICollection<GamerefereeEntity> Gamerefereess { get; set; }
		// % protected region % [Customise Gamerefereess here] end

		// % protected region % [Customise Venue here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.VenueEntity"/>
		public Guid? VenueId { get; set; }
		[EntityForeignKey("Venue", "Gamess", false, typeof(VenueEntity))]
		public VenueEntity Venue { get; set; }
		// % protected region % [Customise Venue here] end

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
			var modelList = models.Cast<GameEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Gamerefereess":
					var gamerefereesIds = modelList.SelectMany(x => x.Gamerefereess.Select(m => m.Id)).ToList();
					var oldgamereferees = await dbContext.GamerefereeEntity
						.Where(m => m.GameId.HasValue && ids.Contains(m.GameId.Value))
						.Where(m => !gamerefereesIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var gamereferees in oldgamereferees)
					{
						gamereferees.GameId = null;
					}

					dbContext.GamerefereeEntity.UpdateRange(oldgamereferees);
					return oldgamereferees.Count;
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