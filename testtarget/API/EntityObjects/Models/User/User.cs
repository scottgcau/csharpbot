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
	public class User : UserBaseEntity 	{

		// Id
		public int? Id { get; set; }
		// Username
		public String Username { get; set; }

		public User()
		{
			EntityName = "User";
			EndpointName = "user";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public User(ConfigureOptions option)
		{
			EndpointName = "user";
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
				Name = "Username",
				IsRequired = true
			});
		}

		private void InitialiseReferences()
		{
		}

		public override string ToString() {
			var sb = new StringBuilder();
			sb.AppendLine($"Id: {Id}");
			sb.AppendLine($"Username: {Username}");
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
				case "Username":
					return GetInvalidUsername(validator);
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
		private static string GetInvalidUsername(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Username");
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
						["email"] = EmailAddress,
						["password"] = Password,
						// not defining Id
						["username"] = Username.ToString(),
					}
				),
				(
					new List<string>
					{
						"The Username field is required.",
					},

					new JsonObject
					{
						["id"] = Id,
						["email"] = EmailAddress,
						["password"] = Password,
						["id"] = Id.ToString(),
						// not defining Username
					}
				),

				(
					new List<string>
					{
					},
					new JsonObject
					{
						["id"] = Id,
						["email"] = EmailAddress,
						["password"] = Password,
						["id"] = Id.ToString(),
						["username"] = Username.ToString(),
					}
				),
				(
					new List<string>
					{
						"The Id field is required.",
						"The Username field is required.",
					},
					new JsonObject
					{
						["id"] = Id,
						["email"] = EmailAddress,
						["password"] = Password,
						// not defining Id
						// not defining Username
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
				{"email" , EmailAddress},
				{"password" , Password},
				{"id" , Id.ToString()},
				{"username" , Username},
			};


			return entityVar;
		}

		public override JsonObject toJson()
		{
			var entityVar = new JsonObject
			{
				["id"] = Id,
				["email"] = EmailAddress,
				["password"] = Password,
				["id"] = Id.ToString(),
				["username"] = Username.ToString(),
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
			// not defining Id
			// not defining Username
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static User GetEntity(bool isValid, string fixedValue = null)
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

		public static User GetInvalidEntity()
		{
			var user = new User
			{
				// not defining Id
				// not defining Username
			};
			return user;
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
			Id = BaseChoice.GetValidint();
			Username = BaseChoice.GetValidString();
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static User GetValidEntity(string fixedStrValue = null)
		{
			return new User
			{
				Id = BaseChoice.GetValidint(),
				Username = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : BaseChoice.GetValidString(),
			};
		}

		public override Guid Save()
		{
			return CreateUser();
		}
	}
}
