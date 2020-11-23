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
	public class RosterTimelineEventsEntityType : EfObjectGraphType<SportstatsDBContext, RosterTimelineEventsEntity>
	{
		public RosterTimelineEventsEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Timeline Events Of Roster";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Action, type: typeof(StringGraphType)).Description(@"The action taken");
			Field(o => o.ActionTitle, type: typeof(StringGraphType)).Description(@"The title of the action taken");
			Field(o => o.Description, type: typeof(StringGraphType)).Description(@"Decription of the event");
			Field(o => o.GroupId, type: typeof(StringGraphType)).Description(@"Id of the group the events belong to");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.EntityId, type: typeof(IdGraphType));

			// GraphQL reference to entity RosterEntity via reference Entity
			AddNavigationField("Entity", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Entity;

				if (value != null)
				{
					return new List<RosterEntity> {value}.All(filter.Compile()) ? value : null;
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
	public class RosterTimelineEventsEntityInputType : InputObjectGraphType<RosterTimelineEventsEntity>
	{
		public RosterTimelineEventsEntityInputType()
		{
			Name = "RosterTimelineEventsEntityInput";
			Description = "The input object for adding a new RosterTimelineEventsEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Action").Description = @"The action taken";
			Field<StringGraphType>("ActionTitle").Description = @"The title of the action taken";
			Field<StringGraphType>("Description").Description = @"Decription of the event";
			Field<StringGraphType>("GroupId").Description = @"Id of the group the events belong to";

			// Add entity references
			Field<IdGraphType>("EntityId");

			// Add references to foreign models to allow nested creation
			Field<RosterEntityInputType>("Entity");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}