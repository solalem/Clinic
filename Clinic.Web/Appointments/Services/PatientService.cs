using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Web.Appointments.Services
{
    public class PatientService
    {
        private readonly IMediator _mediator;
        private readonly IPatientQuery _patientQueries;
        private readonly IIdentityService _identityService;

        public PatientService(IMediator mediator, IPatientQuery patientQueries, IIdentityService identityService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _patientQueries = patientQueries ?? throw new ArgumentNullException(nameof(patientQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatient request)
        {
            try
            {
                var commandResult = await _mediator.Send(new CreatePatientCommand(request));

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
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatient request)
        {
            try
            {
                var commandResult = await _mediator.Send(new UpdatePatientCommand(request));

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
        public async Task<IActionResult> ArchivePatient(Guid id)
        {
            try
            {
                var commandResult = await _mediator.Send(new ArchivePatientCommand { Id = id });

                return commandResult > 0 ? Ok() : (IActionResult)NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PatientDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            try
            {
                var result = await _patientQueries.GetPatientAsync(id);
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
        [ProducesResponseType(typeof(ModelCollection<PatientSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPatients([FromQuery()] PaginationInfo pagination)
        {
            try
            {
                QueryParameters query = new QueryParameters { Skip = pagination.Index, Top = pagination.PageSize, SearchString = pagination.SearchString };
                var result = await _patientQueries.GetPatientsAsync(query);
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
