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
	/// Person entity
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("Person")]
	// % protected region % [Configure entity attributes here] end
	public class PersonEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[Required]
		[EntityAttribute]
		public string Name { get; set; }

		/// <summary>
		/// First name
		/// </summary>
		[Required]
		// % protected region % [Customise Firstname here] off begin
		[EntityAttribute]
		public String Firstname { get; set; }
		// % protected region % [Customise Firstname here] end

		/// <summary>
		/// Last name
		/// </summary>
		[Required]
		// % protected region % [Customise Lastname here] off begin
		[EntityAttribute]
		public String Lastname { get; set; }
		// % protected region % [Customise Lastname here] end

		/// <summary>
		/// Date of birth
		/// </summary>
		// % protected region % [Customise Dateofbirth here] off begin
		[EntityAttribute]
		public DateTime? Dateofbirth { get; set; }
		// % protected region % [Customise Dateofbirth here] end

		/// <summary>
		/// Height (cm)
		/// </summary>
		// % protected region % [Customise Height here] off begin
		[EntityAttribute]
		public int? Height { get; set; }
		// % protected region % [Customise Height here] end

		/// <summary>
		/// Weight (kg)
		/// </summary>
		// % protected region % [Customise Weight here] off begin
		[EntityAttribute]
		public int? Weight { get; set; }
		// % protected region % [Customise Weight here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public PersonEntity()
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
			new VisitorsPersonEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		/// <summary>
		/// Reference to the versions for this form
		/// </summary>
		/// <see cref="Sportstats.Models.PersonEntityFormVersion"/>
		[EntityForeignKey("FormVersions", "Form", false, typeof(PersonEntityFormVersion))]
		public ICollection<PersonEntityFormVersion> FormVersions { get; set; }

		/// <summary>
		/// The current published version for the form
		/// </summary>
		/// <see cref="Sportstats.Models.PersonEntityFormVersion"/>
		public Guid? PublishedVersionId { get; set; }
		[EntityForeignKey("PublishedVersion", "PublishedForm", false, typeof(PersonEntityFormVersion))]
		public PersonEntityFormVersion PublishedVersion { get; set; }

		// % protected region % [Customise Rosterassignmentss here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.RosterassignmentEntity"/>
		[EntityForeignKey("Rosterassignmentss", "Person", false, typeof(RosterassignmentEntity))]
		public ICollection<RosterassignmentEntity> Rosterassignmentss { get; set; }
		// % protected region % [Customise Rosterassignmentss here] end

		// % protected region % [Customise Game here] off begin
		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.GameEntity"/>
		public Guid? GameId { get; set; }
		[EntityForeignKey("Game", "Refereess", false, typeof(GameEntity))]
		public GameEntity Game { get; set; }
		// % protected region % [Customise Game here] end

		// % protected region % [Customise FormPages here] off begin
		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.PersonEntityFormTileEntity"/>
		[EntityForeignKey("FormPages", "Form", false, typeof(PersonEntityFormTileEntity))]
		public ICollection<PersonEntityFormTileEntity> FormPages { get; set; }
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
			var modelList = models.Cast<PersonEntity>().ToList();
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