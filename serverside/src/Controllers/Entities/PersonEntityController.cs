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
	/// The controller that provides rest endpoints for the PersonEntity model
	/// </summary>
	// % protected region % [Override controller attributes here] off begin
	[Route("/api/entity/PersonEntity")]
	[Authorize]
	[ApiController]
	// % protected region % [Override controller attributes here] end
	public class PersonEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public PersonEntityController(
			// % protected region % [Add any extra constructor arguments here] off begin
			// % protected region % [Add any extra constructor arguments here] end
			ICrudService crudService)
		{
			_crudService = crudService;
			// % protected region % [Add any extra constructor logic here] off begin
			// % protected region % [Add any extra constructor logic here] end
		}

		/// <summary>
		/// Get the PersonEntity for the given id
		/// </summary>
		/// <param name="id">The id of the PersonEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The PersonEntity object with the given id</returns>
		// % protected region % [Override get attributes here] off begin
		[HttpGet]
		[Route("{id}")]
		[AllowAnonymous]
		// % protected region % [Override get attributes here] end
		public async Task<PersonEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			// % protected region % [Override Get by id here] off begin
			var result = _crudService.GetById<PersonEntity>(id);
			return await result
				.Select(model => new PersonEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
			// % protected region % [Override Get by id here] end
		}

		/// <summary>
		/// Gets all PersonEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of PersonEntitys</returns>
		// % protected region % [Override get list attributes here] off begin
		[HttpGet]
		[Route("")]
		[AllowAnonymous]
		// % protected region % [Override get list attributes here] end
		public async Task<EntityControllerData<PersonEntityDto>> Get(
			[FromQuery]PersonEntityOptions options,
			CancellationToken cancellation)
		{
			// % protected region % [Override Get here] off begin
			return new EntityControllerData<PersonEntityDto>
			{
				Data = await _crudService.Get<PersonEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new PersonEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<PersonEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
			// % protected region % [Override Get here] end
		}

		/// <summary>
		/// Create PersonEntity
		/// </summary>
		/// <param name="model">The new PersonEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The PersonEntity object after creation</returns>
		// % protected region % [Override post attributes here] off begin
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		// % protected region % [Override post attributes here] end
		public async Task<PersonEntityDto> Post(
			[BindRequired, FromBody] PersonEntityDto model,
			CancellationToken cancellation)
		{
			// % protected region % [Override Post here] off begin
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new PersonEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
			// % protected region % [Override Post here] end
		}

		/// <summary>
		/// Create PersonEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The PersonEntity object after creation</returns>
		// % protected region % [Override post form attributes here] off begin
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		// % protected region % [Override post form attributes here] end
		public async Task<PersonEntityDto> PostForm(CancellationToken cancellation)
		{
			// % protected region % [Override Post form here] off begin
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<PersonEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new PersonEntityDto(result);
			// % protected region % [Override Post form here] end
		}

		/// <summary>
		/// Update an PersonEntity
		/// </summary>
		/// <param name="model">The PersonEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The PersonEntity object after it has been updated</returns>
		// % protected region % [Override put attributes here] off begin
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		// % protected region % [Override put attributes here] end
		public async Task<PersonEntityDto> Put(
			[BindRequired, FromBody] PersonEntityDto model,
			CancellationToken cancellation)
		{
			// % protected region % [Override Put here] off begin
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new PersonEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
			// % protected region % [Override Put here] end
		}

		/// <summary>
		/// Update an PersonEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The PersonEntity object after it has been updated</returns>
		// % protected region % [Override put form attributes here] off begin
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		// % protected region % [Override put form attributes here] end
		public async Task<PersonEntityDto> PutForm(CancellationToken cancellation)
		{
			// % protected region % [Override Put form here] off begin
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<PersonEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new PersonEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
			// % protected region % [Override Put form here] end
		}

		/// <summary>
		/// Deletes a PersonEntity
		/// </summary>
		/// <param name="id">The id of the PersonEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted PersonEntitys</returns>
		// % protected region % [Override delete attributes here] off begin
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		// % protected region % [Override delete attributes here] end
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			// % protected region % [Override Delete here] off begin
			return await _crudService.Delete<PersonEntity>(id, cancellation);
			// % protected region % [Override Delete here] end
		}

		/// <summary>
		/// Export the list of Persons with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Persons</returns>
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
			var queryable = _crudService.Get<PersonEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new PersonEntityDto(r)),
				"export_person.csv",
				cancellationToken);
			// % protected region % [Override Export here] end
		}

		/// <summary>
		/// Export a list of Persons with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Persons</returns>
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
			var queryable = _crudService.Get<PersonEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new PersonEntityDto(r)),
				"export_person.csv",
				cancellationToken);
			// % protected region % [Override ExportPost here] end
		}


		public class PersonEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}

