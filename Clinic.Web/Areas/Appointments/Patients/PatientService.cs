using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Web.Areas.Appointments.Patients
{
    public class PatientService
    {
        private readonly IMediator _mediator;

        public PatientService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IActionResult> CreateAsync([FromBody] CreatePatient request)
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

        public async Task<IActionResult> UpdateAync([FromBody] UpdatePatient request)
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

        public async Task<IActionResult> DeleteAsync(Guid id)
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

        public async Task<IActionResult> GetAsync(Guid id)
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

        public async Task<IActionResult> ListAsync([FromQuery()] PaginationInfo pagination)
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
