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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Sportstats.Models {
	public class ScheduleEntityConfiguration : IEntityTypeConfiguration<ScheduleEntity>
	{
		public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			// % protected region % [Override form data configuration here] off begin
			builder
				.HasMany(e => e.FormVersions)
				.WithOne(e => e.Form)
				.OnDelete(DeleteBehavior.Cascade);
			builder
				.HasOne(e => e.PublishedVersion)
				.WithOne(e => e.PublishedForm)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override form data configuration here] end

			// % protected region % [Override Gamess Schedule configuration here] off begin
			builder
				.HasMany(e => e.Gamess)
				.WithOne(e => e.Schedule)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override Gamess Schedule configuration here] end
			// % protected region % [Override Season Scheduless configuration here] off begin
			builder
				.HasOne(e => e.Season)
				.WithMany(e => e.Scheduless)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override Season Scheduless configuration here] end
			// % protected region % [Override FormPages Form configuration here] off begin
			builder
				.HasMany(e => e.FormPages)
				.WithOne(e => e.Form)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override FormPages Form configuration here] end
			// % protected region % [Override Fullname index configuration here] off begin
			builder.HasIndex(e => e.Fullname);
			// % protected region % [Override Fullname index configuration here] end
			// % protected region % [Add any extra db model config options here] off begin
			// % protected region % [Add any extra db model config options here] end
		}
	}
}