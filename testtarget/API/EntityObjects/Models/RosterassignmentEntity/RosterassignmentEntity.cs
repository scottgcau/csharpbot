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
	public class RosterassignmentEntity : BaseEntity
	{
		// Form Name
		public string Name { get; set; }
		// Date assigned to the roster
		public DateTime? Datefrom { get; set; }
		// Date left the roster
		public DateTime? Dateto { get; set; }
		// 
		public Roletype Roletype { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Roster"/>
		public Guid? RosterId { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Person"/>
		public Guid PersonId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.FormPage"/>
		public List<Guid> FormPageIds { get; set; }
		public ICollection<RosterassignmentEntityFormTileEntity> FormPages { get; set; }


		public RosterassignmentEntity()
		{
			EntityName = "RosterassignmentEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public RosterassignmentEntity(ConfigureOptions option)
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
				Name = "Datefrom",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Dateto",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Roletype",
				IsRequired = true
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "RosterEntity",
				OppositeName = "Roster",
				Name = "Rosterassignments",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "PersonEntity",
				OppositeName = "Person",
				Name = "Rosterassignments",
				Optional = false,
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
				case "DateFrom":
					return GetInvalidDatefrom(validator);
				case "RoleType":
					return GetInvalidRoletype(validator);
				case "PersonId":
					return GetInvalidPersonId(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidDatefrom(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Datefrom");
			}
		}
		private static string GetInvalidRoletype(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Roletype");
			}
		}

		private static string GetInvalidRosterId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute RosterAssignments");
			}
		}
		private static string GetInvalidPersonId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute RosterAssignments");
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
					"The Datefrom field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining datefrom,
						["dateto"] = Dateto?.ToString("s"),
						["roletype"] = RoletypeEnum.GetRandomRoletype().ToString(),
						["rosterId"] = RosterId,
						["personId"] = PersonId,
				}
			),
			(
				new List<string>
				{
					"violates foreign key constraint",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining PersonId,
						["datefrom"] = Datefrom?.ToString("s"),
						["dateto"] = Dateto?.ToString("s"),
						["roletype"] = RoletypeEnum.GetRandomRoletype().ToString(),
						["rosterId"] = RosterId,
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
				{"datefrom" ,((DateTime)Datefrom).ToIsoString()},
				{"dateto" ,((DateTime)Dateto).ToIsoString()},
				{"roletype" , Roletype.ToString()},
			};

			if (RosterId != default)
			{
				entityVar["rosterId"] = RosterId.ToString();
			}
			if (PersonId != default)
			{
				entityVar["personId"] = PersonId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["name"] = Name,
				["datefrom"] = Datefrom?.ToString("s"),
				["dateto"] = Dateto?.ToString("s"),
				["roletype"] = Roletype.ToString(),
			};

			if (PersonId != default)
			{
				entityVar["personId"] = PersonId.ToString();
			}

			return entityVar;
		}


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "RosterId":
						ReferenceIdDictionary.Add("RosterId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "PersonId":
						ReferenceIdDictionary.Add("PersonId", guidCollection.FirstOrDefault());
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
				case "RosterId":
					RosterId = guid;
					break;
				case "PersonId":
					PersonId = guid;
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
			// not defining Datefrom
			// not defining Roletype
			Roletype = RoletypeEnum.GetRandomRoletype();
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static RosterassignmentEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static RosterassignmentEntity GetInvalidEntity()
		{
			var rosterassignmentEntity = new RosterassignmentEntity
			{
				// not defining Datefrom
				// not defining Roletype
				Roletype = RoletypeEnum.GetRandomRoletype(),
			};
			return rosterassignmentEntity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{

			PersonId = new PersonEntity(ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES).Save();

		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			Name = Guid.NewGuid().ToString();
			Datefrom = DataUtils.RandDatetime();
			Dateto = DataUtils.RandDatetime();
			Roletype = RoletypeEnum.GetRandomRoletype();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static RosterassignmentEntity GetValidEntity(string fixedStrValue = null)
		{
			var rosterassignmentEntity = new RosterassignmentEntity
			{
				Name = Guid.NewGuid().ToString(),

				Datefrom = DataUtils.RandDatetime(),

				Dateto = DataUtils.RandDatetime(),

				Roletype = RoletypeEnum.GetRandomRoletype(),
			};

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return rosterassignmentEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.RosterassignmentEntity>(RosterassignmentEntityDto.Convert(this));
		}
	}
}
