using Feedback360.Models;
using Feedback360.Models.Models.Feedbacks.Request;
using Feedback360.Models.Models.Feedbacks.Response;

namespace Feedback360.Services.Services.Feedbacks
{
    public interface IFeedbacksService
    {
        Task<GenericResponse<bool>> AddFeedbackAsync(FeedbackAddRequestDTO feedbackAddRequestDTO);
        Task<GenericResponse<List<FeedbackListResponseDTO>>> GetAllFeedbacksByEmployeeIDAsync(long employeeId);
        Task<GenericResponse<List<FeedbackListResponseDTO>>> GetAllFeedbacksAsync();
    }
}
