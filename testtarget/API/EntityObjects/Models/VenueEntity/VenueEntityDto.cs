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
using ServersideVenueEntity = Sportstats.Models.VenueEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Venue entity
	/// </summary>
	public class VenueEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public String Fullname { get; set; }
		public String Shortname { get; set; }
		public String Address { get; set; }
		public Double? Lat { get; set; }
		public Double? Lon { get; set; }

		public ICollection<GameEntity> Gamess { get; set; }

		public VenueEntityDto(VenueEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Address = model.Address;
			Lat = model.Lat;
			Lon = model.Lon;
			Gamess = model.Gamess;
		}

		public VenueEntityDto(ServersideVenueEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Address = model.Address;
			Lat = model.Lat;
			Lon = model.Lon;
			Gamess = model.Gamess.Select(GameEntityDto.Convert).ToList();
		}

		public VenueEntity GetTesttargetVenueEntity()
		{
			return new VenueEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Fullname = Fullname,
				Shortname = Shortname,
				Address = Address,
				Lat = Lat,
				Lon = Lon,
				Gamess = Gamess,
			};
		}

		public ServersideVenueEntity GetServersideVenueEntity()
		{
			return new ServersideVenueEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Fullname = Fullname,
				Shortname = Shortname,
				Address = Address,
				Lat = Lat,
				Lon = Lon,
				Gamess = Gamess?.Select(GameEntityDto.Convert).ToList(),
			};
		}

		public static ServersideVenueEntity Convert(VenueEntity model)
		{
			var dto = new VenueEntityDto(model);
			return dto.GetServersideVenueEntity();
		}

		public static VenueEntity Convert(ServersideVenueEntity model)
		{
			var dto = new VenueEntityDto(model);
			return dto.GetTesttargetVenueEntity();
		}
	}
}