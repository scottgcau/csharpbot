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
using Sportstats.Enums;
using TestEnums = EntityObject.Enums;
using ServersideRosterassignmentEntity = Sportstats.Models.RosterassignmentEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// RosterAssingment entity
	/// </summary>
	public class RosterassignmentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public DateTime? Datefrom { get; set; }
		public DateTime? Dateto { get; set; }
		public Roletype Roletype { get; set; }

		public Guid? RosterId { get; set; }
		public Guid PersonId { get; set; }

		public RosterassignmentEntityDto(RosterassignmentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Datefrom = model.Datefrom;
			Dateto = model.Dateto;
			Roletype = (Roletype)model.Roletype;
			RosterId = model.RosterId;
			PersonId = model.PersonId;
		}

		public RosterassignmentEntityDto(ServersideRosterassignmentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Datefrom = model.Datefrom;
			Dateto = model.Dateto;
			Roletype = model.Roletype;
			RosterId = model.RosterId;
			PersonId = model.PersonId;
		}

		public RosterassignmentEntity GetTesttargetRosterassignmentEntity()
		{
			return new RosterassignmentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Datefrom = Datefrom,
				Dateto = Dateto,
				Roletype = (TestEnums.Roletype)Roletype,
				RosterId = RosterId,
				PersonId = PersonId,
			};
		}

		public ServersideRosterassignmentEntity GetServersideRosterassignmentEntity()
		{
			return new ServersideRosterassignmentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Datefrom = Datefrom,
				Dateto = Dateto,
				Roletype = Roletype,
				RosterId = RosterId,
				PersonId = PersonId,
			};
		}

		public static ServersideRosterassignmentEntity Convert(RosterassignmentEntity model)
		{
			var dto = new RosterassignmentEntityDto(model);
			return dto.GetServersideRosterassignmentEntity();
		}

		public static RosterassignmentEntity Convert(ServersideRosterassignmentEntity model)
		{
			var dto = new RosterassignmentEntityDto(model);
			return dto.GetTesttargetRosterassignmentEntity();
		}
	}
}