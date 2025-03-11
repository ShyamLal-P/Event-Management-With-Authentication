using EventManagementWithAuthentication.Models;
using EventManagementWithAuthentication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWithAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbacksController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbacksAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(Feedback feedback)
        {
            await _feedbackRepository.AddFeedbackAsync(feedback);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.Id }, feedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            await _feedbackRepository.DeleteFeedbackAsync(id);
            return NoContent();
        }
    }
}
