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
using Sportstats.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Models
{
	/// <summary>
	/// RosterAssingment entity
	/// </summary>
	public class RosterassignmentEntityDto : ModelDto<RosterassignmentEntity>
	{
		public String Name { get; set; }
		// % protected region % [Customise Datefrom here] off begin
		/// <summary>
		/// Date assigned to the roster
		/// </summary>
		public DateTime? Datefrom { get; set; }
		// % protected region % [Customise Datefrom here] end

		// % protected region % [Customise Dateto here] off begin
		/// <summary>
		/// Date left the roster
		/// </summary>
		public DateTime? Dateto { get; set; }
		// % protected region % [Customise Dateto here] end

		// % protected region % [Customise Roletype here] off begin
		[JsonProperty("roletype")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Roletype Roletype { get; set; }
		// % protected region % [Customise Roletype here] end


		// % protected region % [Customise RosterId here] off begin
		public Guid? RosterId { get; set; }
		// % protected region % [Customise RosterId here] end

		// % protected region % [Customise PersonId here] off begin
		public Guid PersonId { get; set; }
		// % protected region % [Customise PersonId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public RosterassignmentEntityDto(RosterassignmentEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public RosterassignmentEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override RosterassignmentEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new RosterassignmentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Datefrom = Datefrom,
				Dateto = Dateto,
				Roletype = Roletype,
				RosterId  = RosterId,
				PersonId  = PersonId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<RosterassignmentEntity> LoadModelData(RosterassignmentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Datefrom = model.Datefrom;
			Dateto = model.Dateto;
			Roletype = model.Roletype;
			RosterId  = model.RosterId;
			PersonId  = model.PersonId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}