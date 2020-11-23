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
	public class LeagueEntityType : EfObjectGraphType<SportstatsDBContext, LeagueEntity>
	{
		public LeagueEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"League entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Fullname, type: typeof(StringGraphType)).Description(@"League name");
			Field(o => o.Shortname, type: typeof(StringGraphType)).Description(@"Short name / abbreviation for the league");
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			AddNavigationListField("FormVersions", context => context.Source.FormVersions);
			AddNavigationConnectionField("FormVersionConnection", context => context.Source.FormVersions);
			AddNavigationField("PublishedVersion", context => context.Source.PublishedVersion);

			Field(o => o.SportId, type: typeof(IdGraphType));

			// GraphQL reference to entity SeasonEntity via reference Seasons
			IEnumerable<SeasonEntity> SeasonssResolveFunction(ResolveFieldContext<LeagueEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<SeasonEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Seasonss.Where(filter.Compile());
			}
			AddNavigationListField("Seasonss", (Func<ResolveFieldContext<LeagueEntity>, IEnumerable<SeasonEntity>>) SeasonssResolveFunction);
			AddNavigationConnectionField("SeasonssConnection", SeasonssResolveFunction);

			// GraphQL reference to entity TeamEntity via reference Teams
			IEnumerable<TeamEntity> TeamssResolveFunction(ResolveFieldContext<LeagueEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TeamEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Teamss.Where(filter.Compile());
			}
			AddNavigationListField("Teamss", (Func<ResolveFieldContext<LeagueEntity>, IEnumerable<TeamEntity>>) TeamssResolveFunction);
			AddNavigationConnectionField("TeamssConnection", TeamssResolveFunction);

			// GraphQL reference to entity SportEntity via reference Sport
			AddNavigationField("Sport", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<SportEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Sport;

				if (value != null)
				{
					return new List<SportEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity LeagueEntityFormTileEntity via reference FormPage
			IEnumerable<LeagueEntityFormTileEntity> FormPagesResolveFunction(ResolveFieldContext<LeagueEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<LeagueEntityFormTileEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.FormPages.Where(filter.Compile());
			}
			AddNavigationListField("FormPages", (Func<ResolveFieldContext<LeagueEntity>, IEnumerable<LeagueEntityFormTileEntity>>) FormPagesResolveFunction);
			AddNavigationConnectionField("FormPagesConnection", FormPagesResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class LeagueEntityInputType : InputObjectGraphType<LeagueEntity>
	{
		public LeagueEntityInputType()
		{
			Name = "LeagueEntityInput";
			Description = "The input object for adding a new LeagueEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Fullname").Description = @"League name";
			Field<StringGraphType>("Shortname").Description = @"Short name / abbreviation for the league";
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<LeagueEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references
			Field<IdGraphType>("SportId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<SeasonEntityInputType>>("Seasonss");
			Field<ListGraphType<TeamEntityInputType>>("Teamss");
			Field<SportEntityInputType>("Sport");
			Field<ListGraphType<LeagueEntityFormTileEntityInputType>>("FormPages");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}