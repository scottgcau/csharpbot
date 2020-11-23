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
	public class RosterassignmentEntityFormVersionType : EfObjectGraphType<SportstatsDBContext, RosterassignmentEntityFormVersion>
	{
		public RosterassignmentEntityFormVersionType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"The form versions for the RosterAssignment Entity form behaviour";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Version, type: typeof(IntGraphType)).Description(@"The version number of this form version");
			Field(o => o.FormData, type: typeof(StringGraphType)).Description(@"The form data for this version");
			Field(o => o.FormId, type: typeof(IdGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			AddNavigationField("Form", context => context.Source.Form);
			AddNavigationField("PublishedForm", context => context.Source.PublishedForm);
			IEnumerable<RosterassignmentSubmissionEntity> FormSubmissionsResolveFunction(ResolveFieldContext<RosterassignmentEntityFormVersion> context)
			{
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<RosterassignmentSubmissionEntity>(
					graphQlContext.IdentityService, 
					graphQlContext.UserManager, 
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				return context.Source.FormSubmissions.Where(filter.Compile());
			}
			AddNavigationListField("FormSubmissions", FormSubmissionsResolveFunction);
			AddNavigationConnectionField("FormSubmissionConnection", FormSubmissionsResolveFunction);


			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class RosterassignmentEntityFormVersionInputType : InputObjectGraphType<RosterassignmentEntityFormVersion>
	{
		public RosterassignmentEntityFormVersionInputType()
		{
			Name = "RosterassignmentEntityFormVersionInput";
			Description = "The input object for adding a new RosterassignmentEntityFormVersion";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IntGraphType>("Version").Description = @"The version number of this form version";
			Field<StringGraphType>("FormData").Description = @"The form data for this version";
			Field<BooleanGraphType>("PublishVersion").Description = @"Should this version be published";
			Field<IdGraphType>("FormId").Description = @"The form id for this version";
			Field<ListGraphType<RosterassignmentSubmissionEntityInputType>>("FormSubmissions").Description = @"The submissions for this form version";

			// Add entity references

			// Add references to foreign models to allow nested creation

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}