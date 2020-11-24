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
	public class ScheduleEntityType : EfObjectGraphType<SportstatsDBContext, ScheduleEntity>
	{
		public ScheduleEntityType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Schedule entity";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Fullname, type: typeof(StringGraphType)).Description(@"Schedule name");
			Field(o => o.Scheduletype, type: typeof(EnumerationGraphType<Scheduletype>));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.SeasonId, type: typeof(IdGraphType));
			Field(o => o.LadderId, type: typeof(IdGraphType));

			// GraphQL reference to entity RoundEntity via reference Rounds
			IEnumerable<RoundEntity> RoundssResolveFunction(ResolveFieldContext<ScheduleEntity> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RoundEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Roundss.Where(filter.Compile());
			}
			AddNavigationListField("Roundss", (Func<ResolveFieldContext<ScheduleEntity>, IEnumerable<RoundEntity>>) RoundssResolveFunction);
			AddNavigationConnectionField("RoundssConnection", RoundssResolveFunction);

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
	public class ScheduleEntityInputType : InputObjectGraphType<ScheduleEntity>
	{
		public ScheduleEntityInputType()
		{
			Name = "ScheduleEntityInput";
			Description = "The input object for adding a new ScheduleEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Fullname").Description = @"Schedule name";
			Field<EnumerationGraphType<Scheduletype>>("Scheduletype");

			// Add entity references
			Field<IdGraphType>("SeasonId");
			Field<IdGraphType>("LadderId");

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<RoundEntityInputType>>("Roundss");
			Field<SeasonEntityInputType>("Season");
			Field<LadderEntityInputType>("Ladder");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}