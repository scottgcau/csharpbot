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
	public class SportentityEntityType : EfObjectGraphType<SportstatsDBContext, SportentityEntity>
	{
		public SportentityEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"SportDesc";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Sportname, type: typeof(StringGraphType)).Description(@"SportName");
			Field(o => o.Order, type: typeof(IntGraphType)).Description(@"Order");
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			AddNavigationListField("FormVersions", context => context.Source.FormVersions);
			AddNavigationConnectionField("FormVersionConnection", context => context.Source.FormVersions);
			AddNavigationField("PublishedVersion", context => context.Source.PublishedVersion);


			// GraphQL reference to entity SportentityEntityFormTileEntity via reference FormPage
			IEnumerable<SportentityEntityFormTileEntity> FormPagesResolveFunction(ResolveFieldContext<SportentityEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<SportentityEntityFormTileEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext);
				return context.Source.FormPages.Where(filter.Compile());
			}
			AddNavigationListField("FormPages", (Func<ResolveFieldContext<SportentityEntity>, IEnumerable<SportentityEntityFormTileEntity>>) FormPagesResolveFunction);
			AddNavigationConnectionField("FormPagesConnection", FormPagesResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class SportentityEntityInputType : InputObjectGraphType
	{
		public SportentityEntityInputType()
		{
			Name = "SportentityEntityInput";
			Description = "The input object for adding a new SportentityEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Sportname").Description = @"SportName";
			Field<IntGraphType>("Order").Description = @"Order";
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<SportentityEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<SportentityEntityFormTileEntityInputType>>("FormPages");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}