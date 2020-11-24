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
using System.Collections.Generic;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Models
{
	/// <summary>
	/// League entity
	/// </summary>
	public class LeagueEntityDto : ModelDto<LeagueEntity>
	{
		// % protected region % [Customise Fullname here] off begin
		/// <summary>
		/// League name
		/// </summary>
		public String Fullname { get; set; }
		// % protected region % [Customise Fullname here] end

		// % protected region % [Customise Shortname here] off begin
		/// <summary>
		/// Short name / abbreviation for the league
		/// </summary>
		public String Shortname { get; set; }
		// % protected region % [Customise Shortname here] end


		// % protected region % [Customise SportId here] off begin
		public Guid? SportId { get; set; }
		// % protected region % [Customise SportId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public LeagueEntityDto(LeagueEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public LeagueEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override LeagueEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new LeagueEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Fullname = Fullname,
				Shortname = Shortname,
				SportId  = SportId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<LeagueEntity> LoadModelData(LeagueEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			SportId  = model.SportId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}