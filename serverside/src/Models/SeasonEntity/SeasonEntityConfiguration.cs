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
	public class SeasonEntityConfiguration : IEntityTypeConfiguration<SeasonEntity>
	{
		public void Configure(EntityTypeBuilder<SeasonEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			// % protected region % [Override Divisionss Season configuration here] off begin
			builder
				.HasMany(e => e.Divisionss)
				.WithOne(e => e.Season)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override Divisionss Season configuration here] end

			// % protected region % [Override League Seasonss configuration here] off begin
			builder
				.HasOne(e => e.League)
				.WithMany(e => e.Seasonss)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override League Seasonss configuration here] end

			// % protected region % [Override Rosterss Season configuration here] off begin
			builder
				.HasMany(e => e.Rosterss)
				.WithOne(e => e.Season)
				.OnDelete(DeleteBehavior.Restrict);
			// % protected region % [Override Rosterss Season configuration here] end

			// % protected region % [Override Scheduless Season configuration here] off begin
			builder
				.HasMany(e => e.Scheduless)
				.WithOne(e => e.Season)
				.OnDelete(DeleteBehavior.Cascade);
			// % protected region % [Override Scheduless Season configuration here] end

			// % protected region % [Override Startdate index configuration here] off begin
			builder.HasIndex(e => e.Startdate);
			// % protected region % [Override Startdate index configuration here] end

			// % protected region % [Override Enddate index configuration here] off begin
			builder.HasIndex(e => e.Enddate);
			// % protected region % [Override Enddate index configuration here] end

			// % protected region % [Override Fullname index configuration here] off begin
			builder.HasIndex(e => e.Fullname);
			// % protected region % [Override Fullname index configuration here] end

			// % protected region % [Override Shortname index configuration here] off begin
			builder.HasIndex(e => e.Shortname);
			// % protected region % [Override Shortname index configuration here] end

			// % protected region % [Add any extra db model config options here] off begin
			// % protected region % [Add any extra db model config options here] end
		}
	}
}