using Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.CreateTeam;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.DeleteTeam;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apsis.EmployeeStepsLeaderboard.Api.Controllers
{
    /// <summary>
    /// Represents Teams Web API controller.
    /// </summary>
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamsController"/> class.
        /// </summary>
        /// <param name="sender">Message sender.</param>
        public TeamsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="teamName">Name of the team.</param>
        /// <returns></returns>
        /// <response code="201">Indicates that a request to create a team has been successfully processed and a new team has been created.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpPost(Name = "CreateTeam")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateTeamResponse>> CreateTeam([FromBody] string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
            {
                return BadRequest();
            }

            var createTeamCommand = new CreateTeamCommand
            {
                Name = teamName
            };

            var createTeamResponse = await _sender.Send(createTeamCommand);

            return Created("", createTeamResponse);
        }

        /// <summary>
        /// Deletes a team with the specified id.
        /// </summary>
        /// <param name="id">Id of the team.</param>
        /// <response code="204">Indicates that a request to delete a team has been successfully processed and the team has been deleted.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpDelete("{id}", Name = "DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTeam([FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var deleteTeamCommand = new DeleteTeamCommand
            {
                TeamId = id
            };

            await _sender.Send(deleteTeamCommand);

            return NoContent();
        }
    }
}
