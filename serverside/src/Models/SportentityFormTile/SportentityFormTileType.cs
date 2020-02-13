/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
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
	public class SportentityFormTileType : EfObjectGraphType<SportstatsDBContext, SportentityFormTile>
	{
		public SportentityFormTileType(IEfGraphQLService<SportstatsDBContext> service) : base(service)
		{
			Description = @"Stores the form entity to form tile mappings";

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Tile, type: typeof(StringGraphType)).Description(@"The tile that the form is contained in");
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.FormId, type: typeof(IdGraphType));

			// GraphQL reference to entity Sportentity via reference Form
			AddNavigationField("Form", context => {
				var graphQlContext = (SportstatsGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<Sportentity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext);
				var value = context.Source.Form;
				return new List<Sportentity> {value}.All(filter.Compile()) ? value : null;
			});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class SportentityFormTileInputType : InputObjectGraphType
	{
		public SportentityFormTileInputType()
		{
			Name = "SportentityFormTileInput";
			Description = "The input object for adding a new SportentityFormTile";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Tile").Description = @"The tile that the form is contained in";

			// Add entity references
			Field<IdGraphType>("FormId");

			// Add references to foreign models to allow nested creation
			Field<SportentityInputType>("Form");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}