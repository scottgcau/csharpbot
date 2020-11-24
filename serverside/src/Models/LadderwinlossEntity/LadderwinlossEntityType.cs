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
	public class LadderwinlossEntityType : EfObjectGraphType<SportstatsDBContext, LadderwinlossEntity>
	{
		public LadderwinlossEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Win loss ladder entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Played, type: typeof(IntGraphType));
			Field(o => o.Won, type: typeof(IntGraphType));
			Field(o => o.Lost, type: typeof(IntGraphType));
			Field(o => o.Pointsfor, type: typeof(IntGraphType));
			Field(o => o.Pointsagainst, type: typeof(IntGraphType));
			Field(o => o.Homewon, type: typeof(IntGraphType));
			Field(o => o.Homelost, type: typeof(IntGraphType));
			Field(o => o.Homefor, type: typeof(IntGraphType));
			Field(o => o.Homeagainst, type: typeof(IntGraphType));
			Field(o => o.Awaywon, type: typeof(IntGraphType));
			Field(o => o.Awaylost, type: typeof(IntGraphType));
			Field(o => o.Awayfor, type: typeof(IntGraphType));
			Field(o => o.Awayagainst, type: typeof(IntGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.TeamId, type: typeof(IdGraphType));
			Field(o => o.LadderId, type: typeof(IdGraphType));

			// GraphQL reference to entity TeamEntity via reference Team
			AddNavigationField("Team", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TeamEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Team;

				if (value != null)
				{
					return new List<TeamEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity LadderEntity via reference Ladder
			AddNavigationField("Ladder", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<LadderEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Ladder;

				if (value != null)
				{
					return new List<LadderEntity> {value}.All(filter.Compile()) ? value : null;
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
	public class LadderwinlossEntityInputType : InputObjectGraphType<LadderwinlossEntity>
	{
		public LadderwinlossEntityInputType()
		{
			Name = "LadderwinlossEntityInput";
			Description = "The input object for adding a new LadderwinlossEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("Played");
			Field<IntGraphType>("Won");
			Field<IntGraphType>("Lost");
			Field<IntGraphType>("Pointsfor");
			Field<IntGraphType>("Pointsagainst");
			Field<IntGraphType>("Homewon");
			Field<IntGraphType>("Homelost");
			Field<IntGraphType>("Homefor");
			Field<IntGraphType>("Homeagainst");
			Field<IntGraphType>("Awaywon");
			Field<IntGraphType>("Awaylost");
			Field<IntGraphType>("Awayfor");
			Field<IntGraphType>("Awayagainst");

			// Add entity references
			Field<IdGraphType>("TeamId");
			Field<IdGraphType>("LadderId");

			// Add references to foreign models to allow nested creation
			Field<TeamEntityInputType>("Team");
			Field<LadderEntityInputType>("Ladder");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}