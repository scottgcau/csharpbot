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
using System.Text;
using EntityObject.Enums;
using APITests.Classes;
using RestSharp;
using TestDataLib;
using Sportstats.Utility;

namespace APITests.EntityObjects.Models
{
	public class PersonEntity : BaseEntity
	{
		// Form Name
		public string Name { get; set; }
		// First name
		public String Firstname { get; set; }
		// Last name
		public String Lastname { get; set; }
		// Date of birth
		public DateTime? Dateofbirth { get; set; }
		// Height (cm)
		public int? Height { get; set; }
		// Weight (kg)
		public int? Weight { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Rosterassignments"/>
		public List<Guid> RosterassignmentsIds { get; set; }
		public ICollection<RosterassignmentEntity> Rosterassignmentss { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Game"/>
		public Guid? GameId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.FormPage"/>
		public List<Guid> FormPageIds { get; set; }
		public ICollection<PersonEntityFormTileEntity> FormPages { get; set; }


		public PersonEntity()
		{
			EntityName = "PersonEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public PersonEntity(ConfigureOptions option)
		{
			Configure(option);
			InitialiseAttributes();
			InitialiseReferences();
		}

		public override void Configure(ConfigureOptions option)
		{
			switch (option)
			{
				case ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES:
					SetValidEntityAttributes();
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_ATTRIBUTES_ONLY:
					SetValidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_REFERENCES_ONLY:
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES:
					SetInvalidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES_VALID_REFERENCES:
					SetInvalidEntityAttributes();
					SetValidEntityAssociations();
					break;
			}
		}

		private void InitialiseAttributes()
		{
			Attributes.Add(new Attribute
			{
				Name = "Name",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Firstname",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Lastname",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Dateofbirth",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Height",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Weight",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "GameEntity",
				OppositeName = "Game",
				Name = "Referees",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
		}

		public override (int min, int max) GetLengthValidatorMinMax(string attribute)
		{
			switch(attribute)
			{
				default:
					throw new Exception($"{attribute} does not exist or does not have a length validator");
			}
		}

		public override string GetInvalidAttribute(string attribute, string validator)
		{
			switch (attribute)
			{
				case "FirstName":
					return GetInvalidFirstname(validator);
				case "LastName":
					return GetInvalidLastname(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidFirstname(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Firstname");
			}
		}
		private static string GetInvalidLastname(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Lastname");
			}
		}

		private static string GetInvalidGameId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Referees");
			}
		}

		/// <summary>
		/// Returns a list of invalid/mutated jsons and expected errors. The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override ICollection<(List<string> expectedErrors, RestSharp.JsonObject jsonObject)> GetInvalidMutatedJsons()
		{
			return new List<(List<string> expectedError, RestSharp.JsonObject jsonObject)>
			{

			(
				new List<string>
				{
					"The Firstname field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining firstname,
						["lastname"] = Lastname,
						["dateofbirth"] = Dateofbirth?.ToString("s"),
						["height"] = Height.ToString(),
						["weight"] = Weight.ToString(),
						["gameId"] = GameId,
				}
			),
			(
				new List<string>
				{
					"The Lastname field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining lastname,
						["firstname"] = Firstname,
						["dateofbirth"] = Dateofbirth?.ToString("s"),
						["height"] = Height.ToString(),
						["weight"] = Weight.ToString(),
						["gameId"] = GameId,
				}
			),

			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"name" , Name},
				{"firstname" , Firstname},
				{"lastname" , Lastname},
				{"dateofbirth" ,((DateTime)Dateofbirth).ToIsoString()},
				{"height" , Height.ToString()},
				{"weight" , Weight.ToString()},
			};

			if (GameId != default)
			{
				entityVar["gameId"] = GameId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["name"] = Name,
				["firstname"] = Firstname.ToString(),
				["lastname"] = Lastname.ToString(),
				["dateofbirth"] = Dateofbirth?.ToString("s"),
				["height"] = Height,
				["weight"] = Weight,
			};


			return entityVar;
		}


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "GameId":
						ReferenceIdDictionary.Add("GameId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					default:
						throw new Exception($"{key} not valid reference key");
				}
			}
		}

		private void SetOneReference (string key, Guid guid)
		{
			switch (key)
			{
				case "GameId":
					GameId = guid;
					break;
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		private void SetManyReference (string key, ICollection<Guid> guids)
		{
			switch (key)
			{
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		public override List<Guid> GetManyToManyReferences (string reference)
		{
			switch (reference)
			{
				default:
					throw new Exception($"{reference} not valid many to many reference key");
			}
		}

		private List<RestSharp.JsonObject> FormatManyToManyJsonList(string key, List<Guid> values)
		{
			var manyToManyList = new List<RestSharp.JsonObject>();
			values?.ForEach(x => manyToManyList.Add(new RestSharp.JsonObject {[key] = x }));
			return manyToManyList;
		}

		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		private void SetInvalidEntityAttributes()
		{
			Name = Guid.NewGuid().ToString();
			// not defining Firstname
			// not defining Lastname
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static PersonEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static PersonEntity GetInvalidEntity()
		{
			var personEntity = new PersonEntity
			{
				// not defining Firstname
				// not defining Lastname
			};
			return personEntity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			Name = Guid.NewGuid().ToString();
			Firstname = DataUtils.RandString();
			Lastname = DataUtils.RandString();
			Dateofbirth = DataUtils.RandDatetime();
			Height = DataUtils.RandInt();
			Weight = DataUtils.RandInt();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static PersonEntity GetValidEntity(string fixedStrValue = null)
		{
			var personEntity = new PersonEntity
			{
				Name = Guid.NewGuid().ToString(),

				Firstname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Lastname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Dateofbirth = DataUtils.RandDatetime(),

				Height = DataUtils.RandInt(),

				Weight = DataUtils.RandInt(),
			};

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return personEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.PersonEntity>(PersonEntityDto.Convert(this));
		}
	}
}
