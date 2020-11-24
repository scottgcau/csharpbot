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
	/// Win loss ladder entity
	/// </summary>
	public class LadderwinlossEntityDto : ModelDto<LadderwinlossEntity>
	{
		// % protected region % [Customise Played here] off begin
		public int? Played { get; set; }
		// % protected region % [Customise Played here] end

		// % protected region % [Customise Won here] off begin
		public int? Won { get; set; }
		// % protected region % [Customise Won here] end

		// % protected region % [Customise Lost here] off begin
		public int? Lost { get; set; }
		// % protected region % [Customise Lost here] end

		// % protected region % [Customise Pointsfor here] off begin
		public int? Pointsfor { get; set; }
		// % protected region % [Customise Pointsfor here] end

		// % protected region % [Customise Pointsagainst here] off begin
		public int? Pointsagainst { get; set; }
		// % protected region % [Customise Pointsagainst here] end

		// % protected region % [Customise Homewon here] off begin
		public int? Homewon { get; set; }
		// % protected region % [Customise Homewon here] end

		// % protected region % [Customise Homelost here] off begin
		public int? Homelost { get; set; }
		// % protected region % [Customise Homelost here] end

		// % protected region % [Customise Homefor here] off begin
		public int? Homefor { get; set; }
		// % protected region % [Customise Homefor here] end

		// % protected region % [Customise Homeagainst here] off begin
		public int? Homeagainst { get; set; }
		// % protected region % [Customise Homeagainst here] end

		// % protected region % [Customise Awaywon here] off begin
		public int? Awaywon { get; set; }
		// % protected region % [Customise Awaywon here] end

		// % protected region % [Customise Awaylost here] off begin
		public int? Awaylost { get; set; }
		// % protected region % [Customise Awaylost here] end

		// % protected region % [Customise Awayfor here] off begin
		public int? Awayfor { get; set; }
		// % protected region % [Customise Awayfor here] end

		// % protected region % [Customise Awayagainst here] off begin
		public int? Awayagainst { get; set; }
		// % protected region % [Customise Awayagainst here] end


		// % protected region % [Customise TeamId here] off begin
		public Guid? TeamId { get; set; }
		// % protected region % [Customise TeamId here] end

		// % protected region % [Customise LadderId here] off begin
		public Guid? LadderId { get; set; }
		// % protected region % [Customise LadderId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public LadderwinlossEntityDto(LadderwinlossEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public LadderwinlossEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override LadderwinlossEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new LadderwinlossEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Played = Played,
				Won = Won,
				Lost = Lost,
				Pointsfor = Pointsfor,
				Pointsagainst = Pointsagainst,
				Homewon = Homewon,
				Homelost = Homelost,
				Homefor = Homefor,
				Homeagainst = Homeagainst,
				Awaywon = Awaywon,
				Awaylost = Awaylost,
				Awayfor = Awayfor,
				Awayagainst = Awayagainst,
				TeamId  = TeamId,
				LadderId  = LadderId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<LadderwinlossEntity> LoadModelData(LadderwinlossEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Played = model.Played;
			Won = model.Won;
			Lost = model.Lost;
			Pointsfor = model.Pointsfor;
			Pointsagainst = model.Pointsagainst;
			Homewon = model.Homewon;
			Homelost = model.Homelost;
			Homefor = model.Homefor;
			Homeagainst = model.Homeagainst;
			Awaywon = model.Awaywon;
			Awaylost = model.Awaylost;
			Awayfor = model.Awayfor;
			Awayagainst = model.Awayagainst;
			TeamId  = model.TeamId;
			LadderId  = model.LadderId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}