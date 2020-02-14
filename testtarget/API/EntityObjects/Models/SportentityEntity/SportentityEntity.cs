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
using APITests.DataFixtures;
using RestSharp;
using TestDataLib;
using Sportstats.Utility;

namespace APITests.EntityObjects.Models
{
	public class SportentityEntity : BaseEntity	{

		// Form Name
		public string Name { get; set; }
		// SportName
		public String Sportname { get; set; }
		// Order
		public int? Order { get; set; }

		public SportentityEntity()
		{
			EntityName = "SportentityEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public SportentityEntity(ConfigureOptions option)
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
				Name = "Sportname",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Order",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
		}

		public override string ToString() {
			var sb = new StringBuilder();
			sb.AppendLine($"Name: {Name}");
			sb.AppendLine($"Sportname: {Sportname}");
			sb.AppendLine($"Order: {Order}");
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
				case "SportName":
					return GetInvalidSportname(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidSportname(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Sportname");
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
					"The Sportname field is required.",
				},

				new JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining sportname,
						["order"] = Order.ToString(),
				}
			),

			};
		}

		public override Dictionary<string, string> toDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"name" , Name},
				{"sportname" , Sportname},
				{"order" , Order.ToString()},
			};


			return entityVar;
		}

		public override JsonObject toJson()
		{
			var entityVar = new JsonObject
			{
				["id"] = Id,
				["name"] = Name,
				["sportname"] = Sportname.ToString(),
				["order"] = Order,
			};


			return entityVar;
		}

		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					default:
						throw new Exception($"{key} not valid reference key");
				}
			}
		}

		private void SetOneReference (string key, Guid guid)
		{
			switch (key)
			{
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


		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		private void SetInvalidEntityAttributes()
		{
			Name = Guid.NewGuid().ToString();
			// not defining Sportname
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static SportentityEntity GetEntity(bool isValid, string fixedValue = null)
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

		public static SportentityEntity GetInvalidEntity()
		{
			var sportentityEntity = new SportentityEntity
			{
				// not defining Sportname
			};
			return sportentityEntity;
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
			Name = Guid.NewGuid().ToString();
			Sportname = DataUtils.RandString();
			Order = DataUtils.RandInt();
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static SportentityEntity GetValidEntity(string fixedStrValue = null)
		{
			return new SportentityEntity
			{
				Name = Guid.NewGuid().ToString(),
				Sportname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),
				Order = DataUtils.RandInt(),
			};
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.SportentityEntity>(SportentityEntityDto.Convert(this));
		}
	}
}
