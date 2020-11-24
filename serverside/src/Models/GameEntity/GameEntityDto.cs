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
	/// Scheduled game
	/// </summary>
	public class GameEntityDto : ModelDto<GameEntity>
	{
		// % protected region % [Customise Datestart here] off begin
		public DateTime? Datestart { get; set; }
		// % protected region % [Customise Datestart here] end

		// % protected region % [Customise Homepoints here] off begin
		public int? Homepoints { get; set; }
		// % protected region % [Customise Homepoints here] end

		// % protected region % [Customise Awaypoints here] off begin
		public int? Awaypoints { get; set; }
		// % protected region % [Customise Awaypoints here] end

		// % protected region % [Customise Hometeamid here] off begin
		public String Hometeamid { get; set; }
		// % protected region % [Customise Hometeamid here] end

		// % protected region % [Customise Awayteamid here] off begin
		public String Awayteamid { get; set; }
		// % protected region % [Customise Awayteamid here] end


		// % protected region % [Customise RoundId here] off begin
		public Guid? RoundId { get; set; }
		// % protected region % [Customise RoundId here] end

		// % protected region % [Customise VenueId here] off begin
		public Guid? VenueId { get; set; }
		// % protected region % [Customise VenueId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public GameEntityDto(GameEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public GameEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override GameEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new GameEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Datestart = Datestart,
				Homepoints = Homepoints,
				Awaypoints = Awaypoints,
				Hometeamid = Hometeamid,
				Awayteamid = Awayteamid,
				RoundId  = RoundId,
				VenueId  = VenueId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<GameEntity> LoadModelData(GameEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Datestart = model.Datestart;
			Homepoints = model.Homepoints;
			Awaypoints = model.Awaypoints;
			Hometeamid = model.Hometeamid;
			Awayteamid = model.Awayteamid;
			RoundId  = model.RoundId;
			VenueId  = model.VenueId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}