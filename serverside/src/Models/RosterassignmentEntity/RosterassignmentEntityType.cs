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
using Sportstats.Enums;
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
	public class RosterassignmentEntityType : EfObjectGraphType<SportstatsDBContext, RosterassignmentEntity>
	{
		public RosterassignmentEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"RosterAssingment entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Datefrom, type: typeof(DateTimeGraphType)).Description(@"Date assigned to the roster");
			Field(o => o.Dateto, type: typeof(DateTimeGraphType)).Description(@"Date left the roster");
			Field(o => o.Roletype, type: typeof(EnumerationGraphType<Roletype>));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.PublishedVersionId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			AddNavigationListField("FormVersions", context => context.Source.FormVersions);
			AddNavigationConnectionField("FormVersionConnection", context => context.Source.FormVersions);
			AddNavigationField("PublishedVersion", context => context.Source.PublishedVersion);

			Field(o => o.RosterId, type: typeof(IdGraphType));
			Field(o => o.PersonId, type: typeof(IdGraphType));

			// GraphQL reference to entity RosterEntity via reference Roster
			AddNavigationField("Roster", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Roster;

				if (value != null)
				{
					return new List<RosterEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity PersonEntity via reference Person
			AddNavigationField("Person", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<PersonEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Person;

				if (value != null)
				{
					return new List<PersonEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL reference to entity RosterassignmentEntityFormTileEntity via reference FormPage
			IEnumerable<RosterassignmentEntityFormTileEntity> FormPagesResolveFunction(ResolveFieldContext<RosterassignmentEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterassignmentEntityFormTileEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.FormPages.Where(filter.Compile());
			}
			AddNavigationListField("FormPages", (Func<ResolveFieldContext<RosterassignmentEntity>, IEnumerable<RosterassignmentEntityFormTileEntity>>) FormPagesResolveFunction);
			AddNavigationConnectionField("FormPagesConnection", FormPagesResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class RosterassignmentEntityInputType : InputObjectGraphType<RosterassignmentEntity>
	{
		public RosterassignmentEntityInputType()
		{
			Name = "RosterassignmentEntityInput";
			Description = "The input object for adding a new RosterassignmentEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<DateTimeGraphType>("Datefrom").Description = @"Date assigned to the roster";
			Field<DateTimeGraphType>("Dateto").Description = @"Date left the roster";
			Field<EnumerationGraphType<Roletype>>("Roletype");
			Field<StringGraphType>("Name");
			Field<IdGraphType>("PublishedVersionId").Description = "The current published version for the form";
			Field<ListGraphType<RosterassignmentEntityFormVersionInputType>>("FormVersions").Description = "The versions for this form";

			// Add entity references
			Field<IdGraphType>("RosterId");
			Field<IdGraphType>("PersonId");

			// Add references to foreign models to allow nested creation
			Field<RosterEntityInputType>("Roster");
			Field<PersonEntityInputType>("Person");
			Field<ListGraphType<RosterassignmentEntityFormTileEntityInputType>>("FormPages");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}