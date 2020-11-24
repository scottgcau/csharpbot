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
	public class TeamEntityType : EfObjectGraphType<SportstatsDBContext, TeamEntity>
	{
		public TeamEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Team entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Represents, type: typeof(StringGraphType)).Description(@"City or area represented");
			Field(o => o.Fullname, type: typeof(StringGraphType)).Description(@"Name of the team (sans city / area)");
			Field(o => o.Shortname, type: typeof(StringGraphType)).Description(@"Short name / abbreviation for the team");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.DivisionId, type: typeof(IdGraphType));

			// GraphQL reference to entity LadderwinlossEntity via reference Ladderwinlosses
			IEnumerable<LadderwinlossEntity> LadderwinlossessResolveFunction(ResolveFieldContext<TeamEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<LadderwinlossEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Ladderwinlossess.Where(filter.Compile());
			}
			AddNavigationListField("Ladderwinlossess", (Func<ResolveFieldContext<TeamEntity>, IEnumerable<LadderwinlossEntity>>) LadderwinlossessResolveFunction);
			AddNavigationConnectionField("LadderwinlossessConnection", LadderwinlossessResolveFunction);

			// GraphQL reference to entity DivisionEntity via reference Division
			AddNavigationField("Division", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<DivisionEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Division;

				if (value != null)
				{
					return new List<DivisionEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity LaddereliminationEntity via reference Laddereliminations
			IEnumerable<LaddereliminationEntity> LaddereliminationssResolveFunction(ResolveFieldContext<TeamEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<LaddereliminationEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Laddereliminationss.Where(filter.Compile());
			}
			AddNavigationListField("Laddereliminationss", (Func<ResolveFieldContext<TeamEntity>, IEnumerable<LaddereliminationEntity>>) LaddereliminationssResolveFunction);
			AddNavigationConnectionField("LaddereliminationssConnection", LaddereliminationssResolveFunction);

			// GraphQL reference to entity RosterEntity via reference Rosters
			IEnumerable<RosterEntity> RosterssResolveFunction(ResolveFieldContext<TeamEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Rosterss.Where(filter.Compile());
			}
			AddNavigationListField("Rosterss", (Func<ResolveFieldContext<TeamEntity>, IEnumerable<RosterEntity>>) RosterssResolveFunction);
			AddNavigationConnectionField("RosterssConnection", RosterssResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class TeamEntityInputType : InputObjectGraphType<TeamEntity>
	{
		public TeamEntityInputType()
		{
			Name = "TeamEntityInput";
			Description = "The input object for adding a new TeamEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Represents").Description = @"City or area represented";
			Field<StringGraphType>("Fullname").Description = @"Name of the team (sans city / area)";
			Field<StringGraphType>("Shortname").Description = @"Short name / abbreviation for the team";

			// Add entity references
			Field<IdGraphType>("DivisionId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<LadderwinlossEntityInputType>>("Ladderwinlossess");
			Field<DivisionEntityInputType>("Division");
			Field<ListGraphType<LaddereliminationEntityInputType>>("Laddereliminationss");
			Field<ListGraphType<RosterEntityInputType>>("Rosterss");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}