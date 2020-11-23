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
	public class VenueEntityType : EfObjectGraphType<SportstatsDBContext, VenueEntity>
	{
		public VenueEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Venue entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Fullname, type: typeof(StringGraphType));
			Field(o => o.Shortname, type: typeof(StringGraphType));
			Field(o => o.Address, type: typeof(StringGraphType));
			Field(o => o.Lat, type: typeof(FloatGraphType));
			Field(o => o.Lon, type: typeof(FloatGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			AddNavigationListField("FormVersions", context => context.Source.FormVersions);
			AddNavigationConnectionField("FormVersionConnection", context => context.Source.FormVersions);
			AddNavigationField("PublishedVersion", context => context.Source.PublishedVersion);


			// GraphQL reference to entity GameEntity via reference Games
			IEnumerable<GameEntity> GamessResolveFunction(ResolveFieldContext<VenueEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<GameEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Gamess.Where(filter.Compile());
			}
			AddNavigationListField("Gamess", (Func<ResolveFieldContext<VenueEntity>, IEnumerable<GameEntity>>) GamessResolveFunction);
			AddNavigationConnectionField("GamessConnection", GamessResolveFunction);

			// GraphQL reference to entity VenueEntityFormTileEntity via reference FormPage
			IEnumerable<VenueEntityFormTileEntity> FormPagesResolveFunction(ResolveFieldContext<VenueEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<VenueEntityFormTileEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.FormPages.Where(filter.Compile());
			}
			AddNavigationListField("FormPages", (Func<ResolveFieldContext<VenueEntity>, IEnumerable<VenueEntityFormTileEntity>>) FormPagesResolveFunction);
			AddNavigationConnectionField("FormPagesConnection", FormPagesResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class VenueEntityInputType : InputObjectGraphType<VenueEntity>
	{
		public VenueEntityInputType()
		{
			Name = "VenueEntityInput";
			Description = "The input object for adding a new VenueEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Fullname");
			Field<StringGraphType>("Shortname");
			Field<StringGraphType>("Address");
			Field<FloatGraphType>("Lat");
			Field<FloatGraphType>("Lon");
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<VenueEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<GameEntityInputType>>("Gamess");
			Field<ListGraphType<VenueEntityFormTileEntityInputType>>("FormPages");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}