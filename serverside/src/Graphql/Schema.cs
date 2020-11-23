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
using System.Threading.Tasks;
using Sportstats.Graphql.Fields;
using Sportstats.Graphql.Helpers;
using Sportstats.Graphql.Types;
using Sportstats.Models;
using Sportstats.Models.RegistrationModels;
using GraphQL;
using GraphQL.EntityFramework;
using GraphQL.Types;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Sportstats.Graphql
{
	/// <summary>
	/// The GraphQL schema class for fetching and mutating data
	/// </summary>
	public class SportstatsSchema : Schema
	{
		public SportstatsSchema(IDependencyResolver resolver) : base(resolver)
		{
			Query = resolver.Resolve<SportstatsQuery>();
			Mutation = resolver.Resolve<SportstatsMutation>();
			// % protected region % [Add any extra schema constructor options here] off begin
			// % protected region % [Add any extra schema constructor options here] end
		}

		// % protected region % [Add any schema methods here] off begin
		// % protected region % [Add any schema methods here] end
	}

	/// <summary>
	/// The query class for the GraphQL schema
	/// </summary>
	public class SportstatsQuery : QueryGraphType<SportstatsDBContext>
	{
		private const string WhereDesc = "A list of where conditions that are joined with an AND";
		private const string ConditionalWhereDesc = "A list of lists of where conditions. The conditions inside the " +
													"innermost lists are joined with and OR and the results of those " +
													"lists are joined with an AND";

		public SportstatsQuery(IEfGraphQLService<SportstatsDBContext> efGraphQlService) : base(efGraphQlService)
		{
			// Add query types for each entity
			AddModelQueryField<ScheduleEntityType, ScheduleEntity>("ScheduleEntity");
			AddModelQueryField<ScheduleEntityFormVersionType, ScheduleEntityFormVersion>("ScheduleEntityFormVersion");
			AddModelQueryField<SeasonEntityType, SeasonEntity>("SeasonEntity");
			AddModelQueryField<SeasonEntityFormVersionType, SeasonEntityFormVersion>("SeasonEntityFormVersion");
			AddModelQueryField<VenueEntityType, VenueEntity>("VenueEntity");
			AddModelQueryField<VenueEntityFormVersionType, VenueEntityFormVersion>("VenueEntityFormVersion");
			AddModelQueryField<GameEntityType, GameEntity>("GameEntity");
			AddModelQueryField<GameEntityFormVersionType, GameEntityFormVersion>("GameEntityFormVersion");
			AddModelQueryField<SportEntityType, SportEntity>("SportEntity");
			AddModelQueryField<SportEntityFormVersionType, SportEntityFormVersion>("SportEntityFormVersion");
			AddModelQueryField<LeagueEntityType, LeagueEntity>("LeagueEntity");
			AddModelQueryField<LeagueEntityFormVersionType, LeagueEntityFormVersion>("LeagueEntityFormVersion");
			AddModelQueryField<TeamEntityType, TeamEntity>("TeamEntity");
			AddModelQueryField<TeamEntityFormVersionType, TeamEntityFormVersion>("TeamEntityFormVersion");
			AddModelQueryField<PersonEntityType, PersonEntity>("PersonEntity");
			AddModelQueryField<PersonEntityFormVersionType, PersonEntityFormVersion>("PersonEntityFormVersion");
			AddModelQueryField<RosterEntityType, RosterEntity>("RosterEntity");
			AddModelQueryField<RosterEntityFormVersionType, RosterEntityFormVersion>("RosterEntityFormVersion");
			AddModelQueryField<RosterassignmentEntityType, RosterassignmentEntity>("RosterassignmentEntity");
			AddModelQueryField<RosterassignmentEntityFormVersionType, RosterassignmentEntityFormVersion>("RosterassignmentEntityFormVersion");
			AddModelQueryField<ScheduleSubmissionEntityType, ScheduleSubmissionEntity>("ScheduleSubmissionEntity");
			AddModelQueryField<SeasonSubmissionEntityType, SeasonSubmissionEntity>("SeasonSubmissionEntity");
			AddModelQueryField<VenueSubmissionEntityType, VenueSubmissionEntity>("VenueSubmissionEntity");
			AddModelQueryField<GameSubmissionEntityType, GameSubmissionEntity>("GameSubmissionEntity");
			AddModelQueryField<SportSubmissionEntityType, SportSubmissionEntity>("SportSubmissionEntity");
			AddModelQueryField<LeagueSubmissionEntityType, LeagueSubmissionEntity>("LeagueSubmissionEntity");
			AddModelQueryField<TeamSubmissionEntityType, TeamSubmissionEntity>("TeamSubmissionEntity");
			AddModelQueryField<PersonSubmissionEntityType, PersonSubmissionEntity>("PersonSubmissionEntity");
			AddModelQueryField<RosterSubmissionEntityType, RosterSubmissionEntity>("RosterSubmissionEntity");
			AddModelQueryField<RosterassignmentSubmissionEntityType, RosterassignmentSubmissionEntity>("RosterassignmentSubmissionEntity");
			AddModelQueryField<ScheduleEntityFormTileEntityType, ScheduleEntityFormTileEntity>("ScheduleEntityFormTileEntity");
			AddModelQueryField<SeasonEntityFormTileEntityType, SeasonEntityFormTileEntity>("SeasonEntityFormTileEntity");
			AddModelQueryField<VenueEntityFormTileEntityType, VenueEntityFormTileEntity>("VenueEntityFormTileEntity");
			AddModelQueryField<GameEntityFormTileEntityType, GameEntityFormTileEntity>("GameEntityFormTileEntity");
			AddModelQueryField<SportEntityFormTileEntityType, SportEntityFormTileEntity>("SportEntityFormTileEntity");
			AddModelQueryField<LeagueEntityFormTileEntityType, LeagueEntityFormTileEntity>("LeagueEntityFormTileEntity");
			AddModelQueryField<TeamEntityFormTileEntityType, TeamEntityFormTileEntity>("TeamEntityFormTileEntity");
			AddModelQueryField<PersonEntityFormTileEntityType, PersonEntityFormTileEntity>("PersonEntityFormTileEntity");
			AddModelQueryField<RosterEntityFormTileEntityType, RosterEntityFormTileEntity>("RosterEntityFormTileEntity");
			AddModelQueryField<RosterassignmentEntityFormTileEntityType, RosterassignmentEntityFormTileEntity>("RosterassignmentEntityFormTileEntity");
			AddModelQueryField<RosterTimelineEventsEntityType, RosterTimelineEventsEntity>("RosterTimelineEventsEntity");

			// Add query types for each many to many reference

			// % protected region % [Add any extra query config here] off begin
			// % protected region % [Add any extra query config here] end
		}

		/// <summary>
		/// Adds single, multiple and connection queries to query
		/// </summary>
		/// <typeparam name="TModelType">The GraphQL type for returning data</typeparam>
		/// <typeparam name="TModel">The EF model type for querying the DB</typeparam>
		/// <param name="name">The name of the entity</param>
		public void AddModelQueryField<TModelType, TModel>(string name)
			where TModelType : ObjectGraphType<TModel>
			where TModel : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Add any extra logic before adding entity query fields here] off begin
			// % protected region % [Add any extra logic before adding entity query fields here] end

			// % protected region % [Override single query here] off begin
			AddQueryField(
				$"{name}s",
				QueryHelpers.CreateResolveFunction<TModel>(),
				typeof(TModelType)).Description = $"Query for fetching multiple {name}s";
			// % protected region % [Override single query here] end

			// % protected region % [Override multiple query here] off begin
			AddSingleField(
				name: name,
				resolve: QueryHelpers.CreateResolveFunction<TModel>(),
				graphType: typeof(TModelType)).Description = $"Query for fetching a single {name}";
			// % protected region % [Override multiple query here] end

			// % protected region % [Override connection query here] off begin
			AddQueryConnectionField(
				$"{name}sConnection",
				QueryHelpers.CreateResolveFunction<TModel>(),
				typeof(TModelType));
			// % protected region % [Override connection query here] end

			// % protected region % [Override count query here] off begin
			FieldAsync<NumberObjectType>(
				$"count{name}s",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<WhereExpressionGraph>>
					{
						Name = "where",
						Description = WhereDesc
					}
				),
				resolve: CountQuery.CreateCountQuery<TModel>(),
				description: "Counts the number of models according to a given set of conditions"
			);
			// % protected region % [Override count query here] end

			// % protected region % [Override conditional query here] off begin
			AddQueryField(
				$"{name}sConditional",
				ConditionalQuery.CreateConditionalQuery<TModel>(),
				typeof(TModelType),
				new QueryArguments(
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					}
				)
			);
			// % protected region % [Override conditional query here] end

			// % protected region % [Override count conditional query here] off begin
			FieldAsync<NumberObjectType>(
				$"count{name}sConditional",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					}
				),
				resolve: CountQuery.CreateConditionalCountQuery<TModel>(),
				description: "Counts the number of models according to a given set of conditions. This query can " +
							"perform both AND and OR conditions"
			);
			// % protected region % [Override count conditional query here] end

			// % protected region % [Add any extra per entity fields here] off begin
			// % protected region % [Add any extra per entity fields here] end
		}

		// % protected region % [Add any extra query methods here] off begin
		// % protected region % [Add any extra query methods here] end
	}

	/// <summary>
	/// The mutation class for the GraphQL schema
	/// </summary>
	public class SportstatsMutation : ObjectGraphType<object>
	{
		private const string ConditionalWhereDesc = "A list of lists of where conditions. The conditions inside the " +
											"innermost lists are joined with and OR and the results of those " +
											"lists are joined with an AND";

		public SportstatsMutation()
		{
			Name = "Mutation";

			// Add input types for each entity
			AddMutationField<ScheduleEntityInputType, ScheduleEntityInputType, ScheduleEntityType, ScheduleEntity>("ScheduleEntity");
			AddMutationField<ScheduleEntityFormVersionInputType, ScheduleEntityFormVersionInputType, ScheduleEntityFormVersionType, ScheduleEntityFormVersion>(
				"ScheduleEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<SeasonEntityInputType, SeasonEntityInputType, SeasonEntityType, SeasonEntity>("SeasonEntity");
			AddMutationField<SeasonEntityFormVersionInputType, SeasonEntityFormVersionInputType, SeasonEntityFormVersionType, SeasonEntityFormVersion>(
				"SeasonEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<VenueEntityInputType, VenueEntityInputType, VenueEntityType, VenueEntity>("VenueEntity");
			AddMutationField<VenueEntityFormVersionInputType, VenueEntityFormVersionInputType, VenueEntityFormVersionType, VenueEntityFormVersion>(
				"VenueEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<GameEntityInputType, GameEntityInputType, GameEntityType, GameEntity>("GameEntity");
			AddMutationField<GameEntityFormVersionInputType, GameEntityFormVersionInputType, GameEntityFormVersionType, GameEntityFormVersion>(
				"GameEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<SportEntityInputType, SportEntityInputType, SportEntityType, SportEntity>("SportEntity");
			AddMutationField<SportEntityFormVersionInputType, SportEntityFormVersionInputType, SportEntityFormVersionType, SportEntityFormVersion>(
				"SportEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<LeagueEntityInputType, LeagueEntityInputType, LeagueEntityType, LeagueEntity>("LeagueEntity");
			AddMutationField<LeagueEntityFormVersionInputType, LeagueEntityFormVersionInputType, LeagueEntityFormVersionType, LeagueEntityFormVersion>(
				"LeagueEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<TeamEntityInputType, TeamEntityInputType, TeamEntityType, TeamEntity>("TeamEntity");
			AddMutationField<TeamEntityFormVersionInputType, TeamEntityFormVersionInputType, TeamEntityFormVersionType, TeamEntityFormVersion>(
				"TeamEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<PersonEntityInputType, PersonEntityInputType, PersonEntityType, PersonEntity>("PersonEntity");
			AddMutationField<PersonEntityFormVersionInputType, PersonEntityFormVersionInputType, PersonEntityFormVersionType, PersonEntityFormVersion>(
				"PersonEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<RosterEntityInputType, RosterEntityInputType, RosterEntityType, RosterEntity>("RosterEntity");
			AddMutationField<RosterEntityFormVersionInputType, RosterEntityFormVersionInputType, RosterEntityFormVersionType, RosterEntityFormVersion>(
				"RosterEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<RosterassignmentEntityInputType, RosterassignmentEntityInputType, RosterassignmentEntityType, RosterassignmentEntity>("RosterassignmentEntity");
			AddMutationField<RosterassignmentEntityFormVersionInputType, RosterassignmentEntityFormVersionInputType, RosterassignmentEntityFormVersionType, RosterassignmentEntityFormVersion>(
				"RosterassignmentEntityFormVersion",
				deleteMutation: context => Task.FromResult((object)new Guid[]{}));
			AddMutationField<ScheduleSubmissionEntityInputType, ScheduleSubmissionEntityInputType, ScheduleSubmissionEntityType, ScheduleSubmissionEntity>("ScheduleSubmissionEntity");
			AddMutationField<SeasonSubmissionEntityInputType, SeasonSubmissionEntityInputType, SeasonSubmissionEntityType, SeasonSubmissionEntity>("SeasonSubmissionEntity");
			AddMutationField<VenueSubmissionEntityInputType, VenueSubmissionEntityInputType, VenueSubmissionEntityType, VenueSubmissionEntity>("VenueSubmissionEntity");
			AddMutationField<GameSubmissionEntityInputType, GameSubmissionEntityInputType, GameSubmissionEntityType, GameSubmissionEntity>("GameSubmissionEntity");
			AddMutationField<SportSubmissionEntityInputType, SportSubmissionEntityInputType, SportSubmissionEntityType, SportSubmissionEntity>("SportSubmissionEntity");
			AddMutationField<LeagueSubmissionEntityInputType, LeagueSubmissionEntityInputType, LeagueSubmissionEntityType, LeagueSubmissionEntity>("LeagueSubmissionEntity");
			AddMutationField<TeamSubmissionEntityInputType, TeamSubmissionEntityInputType, TeamSubmissionEntityType, TeamSubmissionEntity>("TeamSubmissionEntity");
			AddMutationField<PersonSubmissionEntityInputType, PersonSubmissionEntityInputType, PersonSubmissionEntityType, PersonSubmissionEntity>("PersonSubmissionEntity");
			AddMutationField<RosterSubmissionEntityInputType, RosterSubmissionEntityInputType, RosterSubmissionEntityType, RosterSubmissionEntity>("RosterSubmissionEntity");
			AddMutationField<RosterassignmentSubmissionEntityInputType, RosterassignmentSubmissionEntityInputType, RosterassignmentSubmissionEntityType, RosterassignmentSubmissionEntity>("RosterassignmentSubmissionEntity");
			AddMutationField<ScheduleEntityFormTileEntityInputType, ScheduleEntityFormTileEntityInputType, ScheduleEntityFormTileEntityType, ScheduleEntityFormTileEntity>("ScheduleEntityFormTileEntity");
			AddMutationField<SeasonEntityFormTileEntityInputType, SeasonEntityFormTileEntityInputType, SeasonEntityFormTileEntityType, SeasonEntityFormTileEntity>("SeasonEntityFormTileEntity");
			AddMutationField<VenueEntityFormTileEntityInputType, VenueEntityFormTileEntityInputType, VenueEntityFormTileEntityType, VenueEntityFormTileEntity>("VenueEntityFormTileEntity");
			AddMutationField<GameEntityFormTileEntityInputType, GameEntityFormTileEntityInputType, GameEntityFormTileEntityType, GameEntityFormTileEntity>("GameEntityFormTileEntity");
			AddMutationField<SportEntityFormTileEntityInputType, SportEntityFormTileEntityInputType, SportEntityFormTileEntityType, SportEntityFormTileEntity>("SportEntityFormTileEntity");
			AddMutationField<LeagueEntityFormTileEntityInputType, LeagueEntityFormTileEntityInputType, LeagueEntityFormTileEntityType, LeagueEntityFormTileEntity>("LeagueEntityFormTileEntity");
			AddMutationField<TeamEntityFormTileEntityInputType, TeamEntityFormTileEntityInputType, TeamEntityFormTileEntityType, TeamEntityFormTileEntity>("TeamEntityFormTileEntity");
			AddMutationField<PersonEntityFormTileEntityInputType, PersonEntityFormTileEntityInputType, PersonEntityFormTileEntityType, PersonEntityFormTileEntity>("PersonEntityFormTileEntity");
			AddMutationField<RosterEntityFormTileEntityInputType, RosterEntityFormTileEntityInputType, RosterEntityFormTileEntityType, RosterEntityFormTileEntity>("RosterEntityFormTileEntity");
			AddMutationField<RosterassignmentEntityFormTileEntityInputType, RosterassignmentEntityFormTileEntityInputType, RosterassignmentEntityFormTileEntityType, RosterassignmentEntityFormTileEntity>("RosterassignmentEntityFormTileEntity");
			AddMutationField<RosterTimelineEventsEntityInputType, RosterTimelineEventsEntityInputType, RosterTimelineEventsEntityType, RosterTimelineEventsEntity>("RosterTimelineEventsEntity");

			// Add input types for each many to many reference

			// % protected region % [Add any extra mutation queries here] off begin
			// % protected region % [Add any extra mutation queries here] end
		}

		/// <summary>
		/// Adds the required mutation fields to the GraphQL schema for create, update and delete
		/// </summary>
		/// <typeparam name="TModelCreateInputType">The GraphQL input type used for the create functions</typeparam>
		/// <typeparam name="TModelUpdateInputType">The GraphQL Input Type used for the update functions</typeparam>
		/// <typeparam name="TModelType">The GraphQL model type used for returning data</typeparam>
		/// <typeparam name="TModel">The EF model type for saving to the DB</typeparam>
		/// <param name="name">The name of the entity</param>
		/// <param name="createMutation">An override for the create mutation</param>
		/// <param name="updateMutation">An override for the update mutation</param>
		/// <param name="deleteMutation">An override for the delete mutation</param>
		/// <param name="conditionalUpdateMutation">An override for the conditional update mutation</param>
		/// <param name="conditionalDeleteMutation">An override for the conditional delete mutation</param>
		public void AddMutationField<TModelCreateInputType, TModelUpdateInputType, TModelType, TModel>(
			string name,
			Func<ResolveFieldContext<object>, Task<object>> createMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> updateMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> deleteMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> conditionalUpdateMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> conditionalDeleteMutation = null)
			where TModelCreateInputType : InputObjectGraphType<TModel>
			where TModelUpdateInputType : InputObjectGraphType<TModel>
			where TModelType : ObjectGraphType<TModel>
			where TModel : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Add any extra logic before adding entity mutation fields here] off begin
			// % protected region % [Add any extra logic before adding entity mutation fields here] end

			// % protected region % [Override create mutation here] off begin
			FieldAsync<ListGraphType<TModelType>>(
				$"create{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<TModelCreateInputType>> { Name = name + "s" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "MergeReferences" }
				),
				resolve: createMutation ?? CreateMutation.CreateCreateMutation<TModel>(name)
			);
			// % protected region % [Override create mutation here] end

			// % protected region % [Override update mutation here] off begin
			FieldAsync<ListGraphType<TModelType>>(
				$"update{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<TModelUpdateInputType>> { Name = name + "s" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "MergeReferences" }
				),
				resolve: updateMutation ?? UpdateMutation.CreateUpdateMutation<TModel>(name)
			);
			// % protected region % [Override update mutation here] end

			// % protected region % [Override delete mutation here] off begin
			FieldAsync<ListGraphType<IdObjectType>>(
				$"delete{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<IdGraphType>> { Name = $"{name}Ids" }
				),
				resolve: deleteMutation ?? DeleteMutation.CreateDeleteMutation<TModel>(name)
			);
			// % protected region % [Override delete mutation here] end

			// % protected region % [Override update conditional mutation here] off begin
			FieldAsync<BooleanObjectType>(
				$"update{name}sConditional",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					},
					new QueryArgument<TModelUpdateInputType> { Name = "valuesToUpdate" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "fieldsToUpdate" }
				),
				resolve: conditionalUpdateMutation ?? UpdateMutation.CreateConditionalUpdateMutation<TModel>(name)
			);
			// % protected region % [Override update conditional mutation here] end

			// % protected region % [Override delete conditional mutation here] off begin
			FieldAsync<BooleanObjectType>(
				$"delete{name}sConditional",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					}
				),
				resolve: conditionalDeleteMutation ?? DeleteMutation.CreateConditionalDeleteMutation<TModel>(name)
			);
			// % protected region % [Override delete conditional mutation here] end

			// % protected region % [Add any extra per entity mutations here] off begin
			// % protected region % [Add any extra per entity mutations here] end
		}

		// % protected region % [Add any extra mutation methods here] off begin
		// % protected region % [Add any extra mutation methods here] end
	}
}