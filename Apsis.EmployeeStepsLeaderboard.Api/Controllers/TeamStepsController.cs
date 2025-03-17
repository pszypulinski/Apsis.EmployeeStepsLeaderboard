using Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetAllTeamsSteps;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetTeamSteps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apsis.EmployeeStepsLeaderboard.Api.Controllers
{
    /// <summary>
    /// Represents Team Steps Web API controller.
    /// </summary>
    [Route("api/teams")]
    [ApiController]
    public class TeamStepsController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamStepsController"/> class.
        /// </summary>
        /// <param name="sender">Message sender.</param>
        public TeamStepsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retrieves steps counts for all teams.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns a collection of all teams' steps counts.</response>
        /// <response code="404">Indicates that no team steps counts have been found.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpGet("steps", Name = "GetAllTeamsSteps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetAllTeamsStepsResponse>>> GetAllTeamsSteps()
        {
            var getAllTeamsStepsQuery = new GetAllTeamsStepsQuery();

            var allTeamsSteps = await _sender.Send(getAllTeamsStepsQuery);

            if (!allTeamsSteps.Any())
            {
                return NotFound();
            }

            return Ok(allTeamsSteps);
        }

        /// <summary>
        /// Retrieves a steps count for a team with the specified id.
        /// </summary>
        /// <param name="teamId">Id of the team.</param>
        /// <returns></returns>
        /// <response code="200">Returns a team's steps count.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="404">Indicates that no team's steps count has been found.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpGet("{teamId}/steps", Name = "GetTeamSteps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetTeamStepsResponse>> GetTeamSteps([FromRoute] int teamId)
        {
            if (teamId < 1)
            {
                return BadRequest();
            }

            var getTeamStepsQuery = new GetTeamStepsQuery
            {
                TeamId = teamId
            };

            var teamSteps = await _sender.Send(getTeamStepsQuery);

            return Ok(teamSteps);
        }
    }
}
