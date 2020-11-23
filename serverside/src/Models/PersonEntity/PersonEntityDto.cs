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
	/// Person entity
	/// </summary>
	public class PersonEntityDto : ModelDto<PersonEntity>
	{
		public String Name { get; set; }
		// % protected region % [Customise Firstname here] off begin
		/// <summary>
		/// First name
		/// </summary>
		public String Firstname { get; set; }
		// % protected region % [Customise Firstname here] end

		// % protected region % [Customise Lastname here] off begin
		/// <summary>
		/// Last name
		/// </summary>
		public String Lastname { get; set; }
		// % protected region % [Customise Lastname here] end

		// % protected region % [Customise Dateofbirth here] off begin
		/// <summary>
		/// Date of birth
		/// </summary>
		public DateTime? Dateofbirth { get; set; }
		// % protected region % [Customise Dateofbirth here] end

		// % protected region % [Customise Height here] off begin
		/// <summary>
		/// Height (cm)
		/// </summary>
		public int? Height { get; set; }
		// % protected region % [Customise Height here] end

		// % protected region % [Customise Weight here] off begin
		/// <summary>
		/// Weight (kg)
		/// </summary>
		public int? Weight { get; set; }
		// % protected region % [Customise Weight here] end


		// % protected region % [Customise GameId here] off begin
		public Guid? GameId { get; set; }
		// % protected region % [Customise GameId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public PersonEntityDto(PersonEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public PersonEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override PersonEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

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
				GameId  = GameId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<PersonEntity> LoadModelData(PersonEntity model)
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
			GameId  = model.GameId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}