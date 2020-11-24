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
	public class RosterEntityType : EfObjectGraphType<SportstatsDBContext, RosterEntity>
	{
		public RosterEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Roster entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Fullname, type: typeof(StringGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.TeamId, type: typeof(IdGraphType));
			Field(o => o.SeasonId, type: typeof(IdGraphType));

			// GraphQL reference to entity RosterassignmentEntity via reference Rosterassignments
			IEnumerable<RosterassignmentEntity> RosterassignmentssResolveFunction(ResolveFieldContext<RosterEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterassignmentEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Rosterassignmentss.Where(filter.Compile());
			}
			AddNavigationListField("Rosterassignmentss", (Func<ResolveFieldContext<RosterEntity>, IEnumerable<RosterassignmentEntity>>) RosterassignmentssResolveFunction);
			AddNavigationConnectionField("RosterassignmentssConnection", RosterassignmentssResolveFunction);

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

			// GraphQL reference to entity SeasonEntity via reference Season
			AddNavigationField("Season", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<SeasonEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Season;

				if (value != null)
				{
					return new List<SeasonEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity RosterTimelineEventsEntity via reference LoggedEvent
			IEnumerable<RosterTimelineEventsEntity> LoggedEventsResolveFunction(ResolveFieldContext<RosterEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterTimelineEventsEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.LoggedEvents.Where(filter.Compile());
			}
			AddNavigationListField("LoggedEvents", (Func<ResolveFieldContext<RosterEntity>, IEnumerable<RosterTimelineEventsEntity>>) LoggedEventsResolveFunction);
			AddNavigationConnectionField("LoggedEventsConnection", LoggedEventsResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class RosterEntityInputType : InputObjectGraphType<RosterEntity>
	{
		public RosterEntityInputType()
		{
			Name = "RosterEntityInput";
			Description = "The input object for adding a new RosterEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Fullname");

			// Add entity references
			Field<IdGraphType>("TeamId");
			Field<IdGraphType>("SeasonId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<RosterassignmentEntityInputType>>("Rosterassignmentss");
			Field<TeamEntityInputType>("Team");
			Field<SeasonEntityInputType>("Season");
			Field<ListGraphType<RosterTimelineEventsEntityInputType>>("LoggedEvents");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}