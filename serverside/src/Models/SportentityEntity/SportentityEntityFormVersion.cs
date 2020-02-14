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
// SportEntity Entity Form Version
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sportstats.Enums;
using Sportstats.Security;
using Sportstats.Security.Acl;
using Sportstats.Validators;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Sportstats.Models {
	/// <summary>
	/// The form versions for the SportEntity Entity form behaviour
	/// </summary>
	// % protected region % [Configure entity attributes here] off begin
	[Table("SportentityEntityFormVersion")]
	// % protected region % [Configure entity attributes here] end
	public class SportentityEntityFormVersion : IOwnerAbstractModel
	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		/// <summary>
		/// The version number of this form version
		/// </summary>
		[EntityAttribute]
		public int? Version { get; set; }

		/// <summary>
		/// The form data for this version
		/// </summary>
		[Column(TypeName = "text")]
		[EntityAttribute]
		public String FormData { get; set; }

		[NotMapped]
		public bool PublishVersion { get; set; } = false;

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public SportentityEntityFormVersion() 
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new VisitorsSportentityEntity(),
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
		};

		/// <summary>
		/// Reference to the form entity for this version entity
		/// </summary>
		/// <see cref="Sportstats.Models.SportentityEntity"/>
		public Guid FormId { get; set; }
		[EntityForeignKey("Form", "FormVersions", false, typeof(SportentityEntity))]
		public SportentityEntity Form { get; set; }

		/// <summary>
		/// The current form that this version is published against if it is published
		/// </summary>
		/// <see cref="Sportstats.Models.SportentityEntity"/>
		[EntityForeignKey("PublishedForm", "PublishedVersion", false, typeof(SportentityEntity))]
		public SportentityEntity? PublishedForm { get; set; }

		/// <summary>
		/// Reference to the submissions for this form version
		/// </summary>
		/// <see cref="Sportstats.Models.SportentitySubmissionEntity"/>
		[EntityForeignKey("FormSubmissions", "FormVersion", false, typeof(SportentitySubmissionEntity))]
		public ICollection<SportentitySubmissionEntity> FormSubmissions { get; set; }

		public void BeforeSave(EntityState operation, SportstatsDBContext dbContext, IServiceProvider serviceProvider)
		{
			if (operation == EntityState.Added)
			{
				var lastVersion = dbContext
					.SportentityEntityFormVersion
					.AsNoTracking()
					.OrderByDescending(m => m.Version)
					.FirstOrDefault(m => m.FormId == FormId);
				Version = lastVersion != null ? lastVersion.Version + 1 : 1;
			}
			// % protected region % [Add any before save logic here] off begin
			// % protected region % [Add any before save logic here] end
		}

		public void AfterSave(EntityState operation, SportstatsDBContext dbContext, IServiceProvider serviceProvider)
		{
			if (PublishVersion)
			{
				var formModel = dbContext.SportentityEntity.FirstOrDefault(m => m.Id == FormId);
				if (formModel != null)
				{
					formModel.PublishedVersionId = Id;
					dbContext.SaveChanges();
				}
			}
			// % protected region % [Add any after save logic here] off begin
			// % protected region % [Add any after save logic here] end
		}

		public int CleanReference<T>(string reference, IEnumerable<T> models, SportstatsDBContext dbContext)
			where T : IOwnerAbstractModel
		{
			var modelList = models.ToList();
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