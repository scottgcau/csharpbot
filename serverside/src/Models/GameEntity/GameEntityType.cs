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
	public class GameEntityType : EfObjectGraphType<SportstatsDBContext, GameEntity>
	{
		public GameEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Scheduled game";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Datestart, type: typeof(DateTimeGraphType));
			Field(o => o.Hometeamid, type: typeof(IntGraphType));
			Field(o => o.Awayteamid, type: typeof(IntGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			AddNavigationListField("FormVersions", context => context.Source.FormVersions);
			AddNavigationConnectionField("FormVersionConnection", context => context.Source.FormVersions);
			AddNavigationField("PublishedVersion", context => context.Source.PublishedVersion);

			Field(o => o.VenueId, type: typeof(IdGraphType));
			Field(o => o.ScheduleId, type: typeof(IdGraphType));

			// GraphQL reference to entity VenueEntity via reference Venue
			AddNavigationField("Venue", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<VenueEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Venue;

				if (value != null)
				{
					return new List<VenueEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity ScheduleEntity via reference Schedule
			AddNavigationField("Schedule", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<ScheduleEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Schedule;

				if (value != null)
				{
					return new List<ScheduleEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity PersonEntity via reference Referees
			IEnumerable<PersonEntity> RefereessResolveFunction(ResolveFieldContext<GameEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<PersonEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Refereess.Where(filter.Compile());
			}
			AddNavigationListField("Refereess", (Func<ResolveFieldContext<GameEntity>, IEnumerable<PersonEntity>>) RefereessResolveFunction);
			AddNavigationConnectionField("RefereessConnection", RefereessResolveFunction);

			// GraphQL reference to entity GameEntityFormTileEntity via reference FormPage
			IEnumerable<GameEntityFormTileEntity> FormPagesResolveFunction(ResolveFieldContext<GameEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<GameEntityFormTileEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.FormPages.Where(filter.Compile());
			}
			AddNavigationListField("FormPages", (Func<ResolveFieldContext<GameEntity>, IEnumerable<GameEntityFormTileEntity>>) FormPagesResolveFunction);
			AddNavigationConnectionField("FormPagesConnection", FormPagesResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class GameEntityInputType : InputObjectGraphType<GameEntity>
	{
		public GameEntityInputType()
		{
			Name = "GameEntityInput";
			Description = "The input object for adding a new GameEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<DateTimeGraphType>("Datestart");
			Field<IntGraphType>("Hometeamid");
			Field<IntGraphType>("Awayteamid");
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<GameEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references
			Field<IdGraphType>("VenueId");
			Field<IdGraphType>("ScheduleId");

			// Add references to foreign models to allow nested creation
			Field<VenueEntityInputType>("Venue");
			Field<ScheduleEntityInputType>("Schedule");
			Field<ListGraphType<PersonEntityInputType>>("Refereess");
			Field<ListGraphType<GameEntityFormTileEntityInputType>>("FormPages");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}