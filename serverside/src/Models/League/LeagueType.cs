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
	public class LeagueType : EfObjectGraphType<SportstatsDBContext, League>
	{
		public LeagueType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"League";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Id, type: typeof(IntGraphType)).Description(@"Id");
			Field(o => o.Name, type: typeof(StringGraphType)).Description(@"Name");
			Field(o => o.Sportid, type: typeof(IntGraphType)).Description(@"Sport Id");
			Field(o => o.Shortname, type: typeof(StringGraphType)).Description(@"Short name");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.SportId, type: typeof(IdGraphType));

			// GraphQL reference to entity Sport via reference Sport
			AddNavigationField("Sport", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<Sport>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext);
				var value = context.Source.Sport;
				return new List<Sport> {value}.All(filter.Compile()) ? value : null;
			});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class LeagueInputType : InputObjectGraphType
	{
		public LeagueInputType()
		{
			Name = "LeagueInput";
			Description = "The input object for adding a new League";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("Id").Description = @"Id";
			Field<StringGraphType>("Name").Description = @"Name";
			Field<IntGraphType>("Sportid").Description = @"Sport Id";
			Field<StringGraphType>("Shortname").Description = @"Short name";

			// Add entity references
			Field<IdGraphType>("SportId");

			// Add references to foreign models to allow nested creation
			Field<SportInputType>("Sport");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}