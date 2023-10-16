using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Clinic.Core.Appointments.Application.Commands.VisitCommands;
using Clinic.Core.Appointments.Application.Queries.VisitQueries;
using Clinic.Core.Appointments.Domain.AggregatesModel;
using Clinic.Application.Abstractions.Services;
using Clinic.Application.Abstractions.Query.Models;
using Clinic.ViewModels.Appointments;
using Clinic.ViewModels;
using Clinic.Controllers;

namespace Clinic.Appointments.Controllers
{    
    public class VisitsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IVisitQuery _visitQueries;
        private readonly IIdentityService _identityService;

        public VisitsController(IMediator mediator, IVisitQuery visitQueries, IIdentityService identityService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _visitQueries = visitQueries ?? throw new ArgumentNullException(nameof(visitQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateVisit([FromBody] CreateVisit request)
        {
            try
            {
                var commandResult = await _mediator.Send(new CreateVisitCommand(request));

                return commandResult != null ? Ok(commandResult) : (IActionResult)NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVisit([FromBody] UpdateVisit request)
        {
            try
            {
                var commandResult = await _mediator.Send(new UpdateVisitCommand(request));

                return commandResult != null ? Ok(commandResult) : (IActionResult)NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ArchiveVisit(Guid id)
        {
            try
            {
                var commandResult = await _mediator.Send(new ArchiveVisitCommand { Id = id });

                return commandResult > 0 ? Ok() : (IActionResult)NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(VisitDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVisit(Guid id)
        {
            try
            {
                var result = await _visitQueries.GetVisitAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ModelCollection<VisitSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVisits([FromQuery()] PaginationInfo pagination)
        {
            try
            {
                QueryParameters query = new QueryParameters { Skip = pagination.Index, Top = pagination.PageSize, SearchString = pagination.SearchString };
                var result = await _visitQueries.GetVisitsAsync(query);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ModelCollection<VisitSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVisitsByPatientId(Guid patientId, [FromQuery()] PaginationInfo pagination)
        {
            try
            {
                QueryParameters query = new QueryParameters { Skip = pagination.Index, Top = pagination.PageSize, SearchString = pagination.SearchString };
                var result = await _visitQueries.GetVisitsByPatientIdAsync(patientId, query);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
