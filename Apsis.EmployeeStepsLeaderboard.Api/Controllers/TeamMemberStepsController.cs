using Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Commands.CreateStepsIncrement;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Commands.DeleteMemberSteps;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetTeamMembersSteps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apsis.EmployeeStepsLeaderboard.Api.Controllers
{
    /// <summary>
    /// Represents Team Member Steps Web API controller.
    /// </summary>
    [Route("api/teams/{teamId}/members")]
    [ApiController]
    public class TeamMemberStepsController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberStepsController"/> class.
        /// </summary>
        /// <param name="sender">Message sender.</param>
        public TeamMemberStepsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retrieves steps counts for all team members.
        /// </summary>
        /// <param name="teamId">Id of the team.</param>
        /// <returns></returns>
        /// <response code="200">Returns a collection of all team members' steps counts.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="404">Indicates that no team members' steps counts have been found.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpGet("steps", Name = "GetTeamMembersSteps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetTeamMembersStepsResponse>>> GetTeamMembersSteps([FromRoute] int teamId)
        {
            if (teamId < 1)
            {
                return BadRequest();
            }

            var getTeamMembersStepsQuery = new GetTeamMembersStepsQuery
            {
                TeamId = teamId
            };

            var teamMembersSteps = await _sender.Send(getTeamMembersStepsQuery);

            return Ok(teamMembersSteps);
        }

        /// <summary>
        /// Creates a new team member's steps increment.
        /// </summary>
        /// <param name="teamId">Id of the team.</param>
        /// <param name="memberId">Id of the team member.</param>
        /// <param name="stepsIncrement">Number of steps.</param>
        /// <returns></returns>
        /// <response code="201">Indicates that a request to create a team member's steps increment has been successfully processed and a new team member's steps increment has been created.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpPost("{memberId}/steps", Name = "CreateTeamMemberStepsIncrement")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateStepsIncrementResponse>> CreateTeamMemberStepsIncrement([FromRoute] int teamId, [FromRoute] int memberId, [FromBody] int stepsIncrement)
        {
            if (teamId < 1)
            {
                return BadRequest();
            }

            if (memberId < 1)
            {
                return BadRequest();
            }

            if (stepsIncrement < 1)
            {
                return BadRequest();
            }

            var createStepsIncrementCommand = new CreateStepsIncrementCommand
            {
                TeamId = teamId,
                MemberId = memberId,
                Steps = stepsIncrement
            };

            var createStepsIncrementResponse = await _sender.Send(createStepsIncrementCommand);

            return Created("", createStepsIncrementResponse);
        }

        /// <summary>
        /// Deletes team member's steps increments.
        /// </summary>
        /// <param name="teamId">Id of the team.</param>
        /// <param name="memberId">Id of the team member.</param>
        /// <returns></returns>
        /// <response code="204">Indicates that a request to delete a team member's steps increments has been successfully processed and the team member's steps increments have been deleted.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpDelete("{memberId}/steps", Name = "DeleteTeamMemberStepsIncrements")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTeamMemberStepsIncrements([FromRoute] int teamId, [FromRoute] int memberId)
        {
            if (teamId < 1)
            {
                return BadRequest();
            }

            if (memberId < 1)
            {
                return BadRequest();
            }

            var deleteMemberStepsCommand = new DeleteMemberStepsCommand
            {
                TeamId = teamId,
                MemberId = memberId
            };

            await _sender.Send(deleteMemberStepsCommand);

            return NoContent();
        }
    }
}
