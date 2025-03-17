using Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.CreateMember;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.DeleteMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apsis.EmployeeStepsLeaderboard.Api.Controllers
{
    /// <summary>
    /// Represents Team Members Web API controller.
    /// </summary>
    [Route("api/teams/{teamId}/members")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMembersController"/> class.
        /// </summary>
        /// <param name="sender">Message sender.</param>
        public TeamMembersController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Creates a new team member.
        /// </summary>
        /// <param name="teamId">Id of the team.</param>
        /// <param name="memberName">Name of the team member.</param>
        /// <returns></returns>
        /// <response code="201">Indicates that a request to create a team member has been successfully processed and a new team member has been created.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpPost(Name = "CreateTeamMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateMemberResponse>> CreateTeamMember([FromRoute] int teamId, [FromBody] string memberName)
        {
            if (teamId < 1)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(memberName))
            {
                return BadRequest();
            }

            var createMemberCommand = new CreateMemberCommand
            {
                TeamId = teamId,
                Name = memberName
            };

            var createMemberResponse = await _sender.Send(createMemberCommand);

            return Created("", createMemberResponse);
        }

        /// <summary>
        /// Deletes a team member with the specified id.
        /// </summary>
        /// <param name="teamId">Id of the team.</param>
        /// <param name="id">Id of the team member.</param>
        /// <returns></returns>
        /// <response code="204">Indicates that a request to delete a team member has been successfully processed and the team member has been deleted.</response>
        /// <response code="400">Indicates that there are issues with the request.</response>
        /// <response code="500">Indicates that an unexpected error has occurred while processing the request.</response>
        [HttpDelete("{id}", Name = "DeleteTeamMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTeamMember([FromRoute] int teamId, [FromRoute] int id)
        {
            if (teamId < 1)
            {
                return BadRequest();
            }

            if (id < 1)
            {
                return BadRequest();
            }

            var deleteMemberCommand = new DeleteMemberCommand
            {
                TeamId = teamId,
                MemberId = id
            };

            await _sender.Send(deleteMemberCommand);

            return NoContent();
        }
    }
}
