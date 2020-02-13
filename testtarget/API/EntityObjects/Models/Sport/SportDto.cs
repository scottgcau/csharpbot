/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
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
using ServersideSport = Sportstats.Models.Sport;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Sport
	/// </summary>
	public class SportDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public int? Id { get; set; }
		public String Name { get; set; }

		public SportDto(Sport model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Name = model.Name;
		}

		public SportDto(ServersideSport model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Name = model.Name;
		}

		public Sport GetTesttargetSport()
		{
			return new Sport
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Name = Name,
			};
		}

		public ServersideSport GetServersideSport()
		{
			return new ServersideSport
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Name = Name,
			};
		}

		public static ServersideSport Convert(Sport model)
		{
			var dto = new SportDto(model);
			return dto.GetServersideSport();
		}

		public static Sport Convert(ServersideSport model)
		{
			var dto = new SportDto(model);
			return dto.GetTesttargetSport();
		}
	}
}