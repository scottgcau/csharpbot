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
	/// Season entity
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("Season")]
	// % protected region % [Configure entity attributes here] end
	public class SeasonEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[Required]
		[EntityAttribute]
		public string Name { get; set; }

		[Required]
		// % protected region % [Customise Startdate here] off begin
		[EntityAttribute]
		public DateTime? Startdate { get; set; }
		// % protected region % [Customise Startdate here] end

		[Required]
		// % protected region % [Customise Enddate here] off begin
		[EntityAttribute]
		public DateTime? Enddate { get; set; }
		// % protected region % [Customise Enddate here] end

		/// <summary>
		/// Name for the season
		/// </summary>
		[Required]
		// % protected region % [Customise Fullname here] off begin
		[EntityAttribute]
		public String Fullname { get; set; }
		// % protected region % [Customise Fullname here] end

		/// <summary>
		/// Short name / abbreviation
		/// </summary>
		[Required]
		// % protected region % [Customise Shortname here] off begin
		[EntityAttribute]
		public String Shortname { get; set; }
		// % protected region % [Customise Shortname here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public SeasonEntity()
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
			new VisitorsSeasonEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		/// <summary>
		/// Reference to the versions for this form
		/// </summary>
		/// <see cref="Sportstats.Models.SeasonEntityFormVersion"/>
		[EntityForeignKey("FormVersions", "Form", false, typeof(SeasonEntityFormVersion))]
		public ICollection<SeasonEntityFormVersion> FormVersions { get; set; }

		/// <summary>
		/// The current published version for the form
		/// </summary>
		/// <see cref="Sportstats.Models.SeasonEntityFormVersion"/>
		public Guid? PublishedVersionId { get; set; }
		[EntityForeignKey("PublishedVersion", "PublishedForm", false, typeof(SeasonEntityFormVersion))]
		public SeasonEntityFormVersion PublishedVersion { get; set; }

		// % protected region % [Customise Rosterss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.RosterEntity"/>
		[EntityForeignKey("Rosterss", "Season", false, typeof(RosterEntity))]
		public ICollection<RosterEntity> Rosterss { get; set; }
		// % protected region % [Customise Rosterss here] end

		// % protected region % [Customise Scheduless here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.ScheduleEntity"/>
		[EntityForeignKey("Scheduless", "Season", false, typeof(ScheduleEntity))]
		public ICollection<ScheduleEntity> Scheduless { get; set; }
		// % protected region % [Customise Scheduless here] end

		// % protected region % [Customise League here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.LeagueEntity"/>
		public Guid LeagueId { get; set; }
		[EntityForeignKey("League", "Seasonss", true, typeof(LeagueEntity))]
		public LeagueEntity League { get; set; }
		// % protected region % [Customise League here] end

		// % protected region % [Customise FormPages here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.SeasonEntityFormTileEntity"/>
		[EntityForeignKey("FormPages", "Form", false, typeof(SeasonEntityFormTileEntity))]
		public ICollection<SeasonEntityFormTileEntity> FormPages { get; set; }
		// % protected region % [Customise FormPages here] end

		public async Task BeforeSave(
			EntityState operation,
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
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
			var modelList = models.Cast<SeasonEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
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