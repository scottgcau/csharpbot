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
using ServersidePersonEntity = Sportstats.Models.PersonEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Person entity
	/// </summary>
	public class PersonEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public String Firstname { get; set; }
		public String Lastname { get; set; }
		public DateTime? Dateofbirth { get; set; }
		public int? Height { get; set; }
		public int? Weight { get; set; }

		public ICollection<RosterassignmentEntity> Rosterassignmentss { get; set; }
		public Guid? GameId { get; set; }

		public PersonEntityDto(PersonEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Firstname = model.Firstname;
			Lastname = model.Lastname;
			Dateofbirth = model.Dateofbirth;
			Height = model.Height;
			Weight = model.Weight;
			Rosterassignmentss = model.Rosterassignmentss;
			GameId = model.GameId;
		}

		public PersonEntityDto(ServersidePersonEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Firstname = model.Firstname;
			Lastname = model.Lastname;
			Dateofbirth = model.Dateofbirth;
			Height = model.Height;
			Weight = model.Weight;
			Rosterassignmentss = model.Rosterassignmentss.Select(RosterassignmentEntityDto.Convert).ToList();
			GameId = model.GameId;
		}

		public PersonEntity GetTesttargetPersonEntity()
		{
			return new PersonEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Firstname = Firstname,
				Lastname = Lastname,
				Dateofbirth = Dateofbirth,
				Height = Height,
				Weight = Weight,
				Rosterassignmentss = Rosterassignmentss,
				GameId = GameId,
			};
		}

		public ServersidePersonEntity GetServersidePersonEntity()
		{
			return new ServersidePersonEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Firstname = Firstname,
				Lastname = Lastname,
				Dateofbirth = Dateofbirth,
				Height = Height,
				Weight = Weight,
				Rosterassignmentss = Rosterassignmentss?.Select(RosterassignmentEntityDto.Convert).ToList(),
				GameId = GameId,
			};
		}

		public static ServersidePersonEntity Convert(PersonEntity model)
		{
			var dto = new PersonEntityDto(model);
			return dto.GetServersidePersonEntity();
		}

		public static PersonEntity Convert(ServersidePersonEntity model)
		{
			var dto = new PersonEntityDto(model);
			return dto.GetTesttargetPersonEntity();
		}
	}
}