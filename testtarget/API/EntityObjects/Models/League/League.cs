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
using System.Linq;
using System.Text;
using APITests.DataFixtures;
using RestSharp;
using Sportstats.Utility;

namespace APITests.EntityObjects.Models
{
	public class League : BaseEntity	{

		// Id
		public int? Id { get; set; }
		// Name
		public String Name { get; set; }
		// Sport Id
		public int? Sportid { get; set; }
		// Short name
		public String Shortname { get; set; }

		public League()
		{
			EntityName = "League";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public League(ConfigureOptions option)
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
				Name = "Id",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Name",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Sportid",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Shortname",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "Sport",
				OppositeName = "Sport",
				Name = "Leagues",
				Optional = false,
				Type = "One",
				OppositeType = "Many"
			});
		}

		public override string ToString() {
			var sb = new StringBuilder();
			sb.AppendLine($"Id: {Id}");
			sb.AppendLine($"Name: {Name}");
			sb.AppendLine($"Sportid: {Sportid}");
			sb.AppendLine($"Shortname: {Shortname}");
			return sb.ToString();
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
				case "Id":
					return GetInvalidId(validator);
				case "Name":
					return GetInvalidName(validator);
				case "SportId":
					return GetInvalidSportid(validator);
				case "SportId":
					return GetInvalidSportId(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidId(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Id");
			}
		}
		private static string GetInvalidName(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Name");
			}
		}
		private static string GetInvalidSportid(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Sportid");
			}
		}

		private static string GetInvalidSportId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return null;
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Sport");
			}
		}

		/// <summary>
		/// Returns a list of invalid/mutated jsons and expected errors. The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override ICollection<(List<string> expectedErrors, JsonObject jsonObject)> GetInvalidMutatedJsons()
		{
			return new List<(List<string> expectedError, JsonObject jsonObject)>
			{
				(
					new List<string>
					{
						"The Id field is required.",
					},

					new JsonObject
					{
						["id"] = Id,
						// not defining Id
						["name"] = Name.ToString(),
						["sportid"] = Sportid.ToString(),
						["shortname"] = Shortname.ToString(),
						["sportId"] = SportId.ToString(),
					}
				),
				(
					new List<string>
					{
						"The Name field is required.",
					},

					new JsonObject
					{
						["id"] = Id,
						["id"] = Id.ToString(),
						// not defining Name
						["sportid"] = Sportid.ToString(),
						["shortname"] = Shortname.ToString(),
						["sportId"] = SportId.ToString(),
					}
				),
				(
					new List<string>
					{
						"The Sportid field is required.",
					},

					new JsonObject
					{
						["id"] = Id,
						["id"] = Id.ToString(),
						["name"] = Name.ToString(),
						// not defining Sportid
						["shortname"] = Shortname.ToString(),
						["sportId"] = SportId.ToString(),
					}
				),

				(
					new List<string>
					{
						"violates foreign key constraint",
					},
					new JsonObject
					{
						["id"] = Id,
						["id"] = Id.ToString(),
						["name"] = Name.ToString(),
						["sportid"] = Sportid.ToString(),
						["shortname"] = Shortname.ToString(),
						["sportId"] = GetInvalidSportId("Required"), // invalid stationId
					}
				),
				(
					new List<string>
					{
						"The Id field is required.",
						"The Name field is required.",
						"The Sportid field is required.",
					},
					new JsonObject
					{
						["id"] = Id,
						// not defining Id
						// not defining Name
						// not defining Sportid
						["shortname"] = Shortname.ToString(),
						["sportId"] = SportId.ToString(),
					}
				)
			};
		}

		/// <summary>
		/// Returns a list of invalid/mutated jsons entities and expected errors for enum columns.
		/// Each enum column will have a dedicated entity and expected error pair
		/// The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override ICollection<(List<string> expectedErrors, JsonObject jsonObject)> GetInvalidMutatedJsonsForEnums()
		{
			return new List<(List<string> expectedError, JsonObject jsonObject)>
			{

			};
		}

		public override Dictionary<string, string> toDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"id" , Id.ToString()},
				{"name" , Name},
				{"sportid" , Sportid.ToString()},
				{"shortname" , Shortname},
			};

			if (SportId != null)
			{
				entityVar["sportId"] = SportId.ToString();
			}

			return entityVar;
		}

		public override JsonObject toJson()
		{
			var entityVar = new JsonObject
			{
				["id"] = Id,
				["id"] = Id.ToString(),
				["name"] = Name.ToString(),
				["sportid"] = Sportid.ToString(),
				["shortname"] = Shortname.ToString(),
			};

			if (SportId != null)
			{
				entityVar["sportId"] = SportId.ToString();
			}
			return entityVar;
		}

		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "SportId":
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
				case "SportId":
					SportId = guid;
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

		private List<JsonObject> FormatManyToManyJsonList(string key, List<Guid> values)
		{
			var manyToManyList = new List<JsonObject>();
			values?.ForEach(x => manyToManyList.Add(new JsonObject {[key] = x }));
			return manyToManyList;
		}


		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Sport"/>
		public Guid SportId { get; set; }

		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		private void SetInvalidEntityAttributes()
		{
			// not defining Id
			// not defining Name
			// not defining Sportid
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static League GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			else
			{
				return isValid ? GetValidEntity() : GetInvalidEntity();
			}
		}

		public static League GetInvalidEntity()
		{
			var league = new League
			{
				// not defining Id
				// not defining Name
				// not defining Sportid
			};
			return league;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{
			SportId = new Sport(ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES).Save();
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			Id = BaseChoice.GetValidint();
			Name = BaseChoice.GetValidString();
			Sportid = BaseChoice.GetValidint();
			Shortname = BaseChoice.GetValidString();
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static League GetValidEntity(string fixedStrValue = null)
		{
			return new League
			{
				Id = BaseChoice.GetValidint(),
				Name = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : BaseChoice.GetValidString(),
				Sportid = BaseChoice.GetValidint(),
				Shortname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : BaseChoice.GetValidString(),
			};
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.League>(LeagueDto.Convert(this));
		}
	}
}
