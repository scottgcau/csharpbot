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
using Sportstats.Services;
using GraphQL.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Identity;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Sportstats.Models
{
	/// <summary>
	/// The GraphQL type for returning data in GraphQL queries
	/// </summary>
	public class PersonEntityType : EfObjectGraphType<SportstatsDBContext, PersonEntity>
	{
		public PersonEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Person entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Firstname, type: typeof(StringGraphType)).Description(@"First name");
			Field(o => o.Lastname, type: typeof(StringGraphType)).Description(@"Last name");
			Field(o => o.Dateofbirth, type: typeof(DateTimeGraphType)).Description(@"Date of birth");
			Field(o => o.Height, type: typeof(IntGraphType)).Description(@"Height (cm)");
			Field(o => o.Weight, type: typeof(IntGraphType)).Description(@"Weight (kg)");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.SystemuserId, type: typeof(IdGraphType));
			Field(o => o.GamerefereeId, type: typeof(IdGraphType));

			// GraphQL reference to entity RosterassignmentEntity via reference Rosterassignments
			IEnumerable<RosterassignmentEntity> RosterassignmentssResolveFunction(ResolveFieldContext<PersonEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterassignmentEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Rosterassignmentss.Where(filter.Compile());
			}
			AddNavigationListField("Rosterassignmentss", (Func<ResolveFieldContext<PersonEntity>, IEnumerable<RosterassignmentEntity>>) RosterassignmentssResolveFunction);
			AddNavigationConnectionField("RosterassignmentssConnection", RosterassignmentssResolveFunction);

			// GraphQL reference to entity SystemuserEntity via reference Systemuser
			AddNavigationField("Systemuser", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<SystemuserEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Systemuser;

				if (value != null)
				{
					return new List<SystemuserEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity GamerefereeEntity via reference Gamereferee
			AddNavigationField("Gamereferee", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<GamerefereeEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Gamereferee;

				if (value != null)
				{
					return new List<GamerefereeEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class PersonEntityInputType : InputObjectGraphType<PersonEntity>
	{
		public PersonEntityInputType()
		{
			Name = "PersonEntityInput";
			Description = "The input object for adding a new PersonEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Firstname").Description = @"First name";
			Field<StringGraphType>("Lastname").Description = @"Last name";
			Field<DateTimeGraphType>("Dateofbirth").Description = @"Date of birth";
			Field<IntGraphType>("Height").Description = @"Height (cm)";
			Field<IntGraphType>("Weight").Description = @"Weight (kg)";

			// Add entity references
			Field<IdGraphType>("SystemuserId");
			Field<IdGraphType>("GamerefereeId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<RosterassignmentEntityInputType>>("Rosterassignmentss");
			Field<SystemuserEntityInputType>("Systemuser");
			Field<GamerefereeEntityInputType>("Gamereferee");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}