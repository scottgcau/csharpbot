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
using ServersideGamerefereeEntity = Sportstats.Models.GamerefereeEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Game referee entity
	/// </summary>
	public class GamerefereeEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Boolean? Headreferee { get; set; }

		public Guid? GameId { get; set; }

		public GamerefereeEntityDto(GamerefereeEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Headreferee = model.Headreferee;
			GameId = model.GameId;
		}

		public GamerefereeEntityDto(ServersideGamerefereeEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Headreferee = model.Headreferee;
			GameId = model.GameId;
		}

		public GamerefereeEntity GetTesttargetGamerefereeEntity()
		{
			return new GamerefereeEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Headreferee = Headreferee,
				GameId = GameId,
			};
		}

		public ServersideGamerefereeEntity GetServersideGamerefereeEntity()
		{
			return new ServersideGamerefereeEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Headreferee = Headreferee,
				GameId = GameId,
			};
		}

		public static ServersideGamerefereeEntity Convert(GamerefereeEntity model)
		{
			var dto = new GamerefereeEntityDto(model);
			return dto.GetServersideGamerefereeEntity();
		}

		public static GamerefereeEntity Convert(ServersideGamerefereeEntity model)
		{
			var dto = new GamerefereeEntityDto(model);
			return dto.GetTesttargetGamerefereeEntity();
		}
	}
}