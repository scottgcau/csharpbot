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
using System.Linq;
using ServersideLadderwinlossEntity = Sportstats.Models.LadderwinlossEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Win loss ladder entity
	/// </summary>
	public class LadderwinlossEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public int? Played { get; set; }
		public int? Won { get; set; }
		public int? Lost { get; set; }
		public int? Pointsfor { get; set; }
		public int? Pointsagainst { get; set; }
		public int? Homewon { get; set; }
		public int? Homelost { get; set; }
		public int? Homefor { get; set; }
		public int? Homeagainst { get; set; }
		public int? Awaywon { get; set; }
		public int? Awaylost { get; set; }
		public int? Awayfor { get; set; }
		public int? Awayagainst { get; set; }

		public Guid? TeamId { get; set; }
		public Guid? LadderId { get; set; }

		public LadderwinlossEntityDto(LadderwinlossEntity model)
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
			TeamId = model.TeamId;
			LadderId = model.LadderId;
		}

		public LadderwinlossEntityDto(ServersideLadderwinlossEntity model)
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
			TeamId = model.TeamId;
			LadderId = model.LadderId;
		}

		public LadderwinlossEntity GetTesttargetLadderwinlossEntity()
		{
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
				TeamId = TeamId,
				LadderId = LadderId,
			};
		}

		public ServersideLadderwinlossEntity GetServersideLadderwinlossEntity()
		{
			return new ServersideLadderwinlossEntity
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
				TeamId = TeamId,
				LadderId = LadderId,
			};
		}

		public static ServersideLadderwinlossEntity Convert(LadderwinlossEntity model)
		{
			var dto = new LadderwinlossEntityDto(model);
			return dto.GetServersideLadderwinlossEntity();
		}

		public static LadderwinlossEntity Convert(ServersideLadderwinlossEntity model)
		{
			var dto = new LadderwinlossEntityDto(model);
			return dto.GetTesttargetLadderwinlossEntity();
		}
	}
}