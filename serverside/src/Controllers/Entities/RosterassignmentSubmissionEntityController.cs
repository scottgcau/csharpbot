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
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Sportstats.Helpers;
using Sportstats.Models;
using Sportstats.Services;
using Sportstats.Services.Interfaces;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Controllers.Entities
{
	/// <summary>
	/// The controller that provides rest endpoints for the RosterassignmentSubmissionEntity model
	/// </summary>
	// % protected region % [Override controller attributes here] off begin
	[Route("/api/entity/RosterassignmentSubmissionEntity")]
	[Authorize]
	[ApiController]
	// % protected region % [Override controller attributes here] end
	public class RosterassignmentSubmissionEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public RosterassignmentSubmissionEntityController(
			// % protected region % [Add any extra constructor arguments here] off begin
			// % protected region % [Add any extra constructor arguments here] end
			ICrudService crudService)
		{
			_crudService = crudService;
			// % protected region % [Add any extra constructor logic here] off begin
			// % protected region % [Add any extra constructor logic here] end
		}

		/// <summary>
		/// Get the RosterassignmentSubmissionEntity for the given id
		/// </summary>
		/// <param name="id">The id of the RosterassignmentSubmissionEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The RosterassignmentSubmissionEntity object with the given id</returns>
		// % protected region % [Override get attributes here] off begin
		[HttpGet]
		[Route("{id}")]
		[AllowAnonymous]
		// % protected region % [Override get attributes here] end
		public async Task<RosterassignmentSubmissionEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			// % protected region % [Override Get by id here] off begin
			var result = _crudService.GetById<RosterassignmentSubmissionEntity>(id);
			return await result
				.Select(model => new RosterassignmentSubmissionEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
			// % protected region % [Override Get by id here] end
		}

		/// <summary>
		/// Gets all RosterassignmentSubmissionEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of RosterassignmentSubmissionEntitys</returns>
		// % protected region % [Override get list attributes here] off begin
		[HttpGet]
		[Route("")]
		[AllowAnonymous]
		// % protected region % [Override get list attributes here] end
		public async Task<EntityControllerData<RosterassignmentSubmissionEntityDto>> Get(
			[FromQuery]RosterassignmentSubmissionEntityOptions options,
			CancellationToken cancellation)
		{
			// % protected region % [Override Get here] off begin
			return new EntityControllerData<RosterassignmentSubmissionEntityDto>
			{
				Data = await _crudService.Get<RosterassignmentSubmissionEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new RosterassignmentSubmissionEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<RosterassignmentSubmissionEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
			// % protected region % [Override Get here] end
		}

		/// <summary>
		/// Create RosterassignmentSubmissionEntity
		/// </summary>
		/// <param name="model">The new RosterassignmentSubmissionEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The RosterassignmentSubmissionEntity object after creation</returns>
		// % protected region % [Override post attributes here] off begin
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[AllowAnonymous]
		// % protected region % [Override post attributes here] end
		public async Task<RosterassignmentSubmissionEntityDto> Post(
			[BindRequired, FromBody] RosterassignmentSubmissionEntityDto model,
			CancellationToken cancellation)
		{
			// % protected region % [Override Post here] off begin
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new RosterassignmentSubmissionEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
			// % protected region % [Override Post here] end
		}

		/// <summary>
		/// Create RosterassignmentSubmissionEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The RosterassignmentSubmissionEntity object after creation</returns>
		// % protected region % [Override post form attributes here] off begin
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[AllowAnonymous]
		// % protected region % [Override post form attributes here] end
		public async Task<RosterassignmentSubmissionEntityDto> PostForm(CancellationToken cancellation)
		{
			// % protected region % [Override Post form here] off begin
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<RosterassignmentSubmissionEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new RosterassignmentSubmissionEntityDto(result);
			// % protected region % [Override Post form here] end
		}

		/// <summary>
		/// Update an RosterassignmentSubmissionEntity
		/// </summary>
		/// <param name="model">The RosterassignmentSubmissionEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The RosterassignmentSubmissionEntity object after it has been updated</returns>
		// % protected region % [Override put attributes here] off begin
		[HttpPut]
		[Consumes("application/json")]
		[AllowAnonymous]
		// % protected region % [Override put attributes here] end
		public async Task<RosterassignmentSubmissionEntityDto> Put(
			[BindRequired, FromBody] RosterassignmentSubmissionEntityDto model,
			CancellationToken cancellation)
		{
			// % protected region % [Override Put here] off begin
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new RosterassignmentSubmissionEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
			// % protected region % [Override Put here] end
		}

		/// <summary>
		/// Update an RosterassignmentSubmissionEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The RosterassignmentSubmissionEntity object after it has been updated</returns>
		// % protected region % [Override put form attributes here] off begin
		[HttpPut]
		[Consumes("multipart/form-data")]
		[AllowAnonymous]
		// % protected region % [Override put form attributes here] end
		public async Task<RosterassignmentSubmissionEntityDto> PutForm(CancellationToken cancellation)
		{
			// % protected region % [Override Put form here] off begin
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<RosterassignmentSubmissionEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new RosterassignmentSubmissionEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
			// % protected region % [Override Put form here] end
		}

		/// <summary>
		/// Deletes a RosterassignmentSubmissionEntity
		/// </summary>
		/// <param name="id">The id of the RosterassignmentSubmissionEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted RosterassignmentSubmissionEntitys</returns>
		// % protected region % [Override delete attributes here] off begin
		[HttpDelete]
		[Route("{id}")]
		[AllowAnonymous]
		// % protected region % [Override delete attributes here] end
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			// % protected region % [Override Delete here] off begin
			return await _crudService.Delete<RosterassignmentSubmissionEntity>(id, cancellation);
			// % protected region % [Override Delete here] end
		}

		/// <summary>
		/// Export the list of RosterAssignment Submissions with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of RosterAssignment Submissions</returns>
		// % protected region % [Override export attributes here] off begin
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[AllowAnonymous]
		// % protected region % [Override export attributes here] end
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			// % protected region % [Override Export here] off begin
			var queryable = _crudService.Get<RosterassignmentSubmissionEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new RosterassignmentSubmissionEntityDto(r)),
				"export_rosterassignment_submission.csv",
				cancellationToken);
			// % protected region % [Override Export here] end
		}

		/// <summary>
		/// Export a list of RosterAssignment Submissions with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of RosterAssignment Submissions</returns>
		// % protected region % [Override export post attributes here] off begin
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[AllowAnonymous]
		// % protected region % [Override export post attributes here] end
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			// % protected region % [Override ExportPost here] off begin
			var queryable = _crudService.Get<RosterassignmentSubmissionEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new RosterassignmentSubmissionEntityDto(r)),
				"export_rosterassignment_submission.csv",
				cancellationToken);
			// % protected region % [Override ExportPost here] end
		}


		public class RosterassignmentSubmissionEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}

