using AutoMapper;
using Feedback360.DB;
using Feedback360.DB.Entities;
using Feedback360.Models;
using Feedback360.Models.Enums;
using Feedback360.Models.Models.Feedbacks.Request;
using Feedback360.Models.Models.Feedbacks.Response;
using Microsoft.EntityFrameworkCore;

namespace Feedback360.Services.Services.Feedbacks
{
    public class FeedbacksService : IFeedbacksService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FeedbacksService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse<bool>> AddFeedbackAsync(FeedbackAddRequestDTO feedbackAddRequestDTO)
        {
            var feedback = _mapper.Map<Feedback>(feedbackAddRequestDTO);
            feedback.Created_By = "Manager";
            feedback.Created_Date = DateTime.Now;
            feedback.Modified_By = "Manager";
            feedback.Modified_Date = DateTime.Now;
            feedback.Deleted = false;
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return GenericResponse<bool>.Failure("Added Successfully!", ApiStatusCode.RecordNotFound);
        }

        public async Task<GenericResponse<List<FeedbackListResponseDTO>>> GetAllFeedbacksAsync()
        {
            List<FeedbackListResponseDTO> list = new List<FeedbackListResponseDTO>();
            var listOfFeedbacks = await _context.Feedbacks.ToListAsync();
            var users = await _context.Users.ToListAsync();

            if (listOfFeedbacks.Count > 0)
            {
                list = (from feedback in listOfFeedbacks
                        select new FeedbackListResponseDTO
                        {
                            Id = feedback.Id,
                            User_Name = _context.Users.Where(x => x.Id == Convert.ToInt64(feedback.Employee_Id)).Select(x => x.First_Name + " " + x.Last_Name).FirstOrDefault(),
                            Category_id = feedback.Category_Id,
                            Severity_Id = feedback.Severity_Id,
                            Created_By = feedback.Created_By,
                            Modified_Date = feedback.Modified_Date,
                            Status = feedback.Status,
                            Comments = feedback.Comments
                        }).OrderByDescending(x => x.Id).ToList();
            }

            return GenericResponse<List<FeedbackListResponseDTO>>.Success(list, "Data Successfully Retreived!");
        }

        public async Task<GenericResponse<List<FeedbackListResponseDTO>>> GetAllFeedbacksByEmployeeIDAsync(long employeeId)
        {
            List<FeedbackListResponseDTO> list = new List<FeedbackListResponseDTO>();
            if (employeeId != 0)
            {
                var listOfFeedbacks = await _context.Feedbacks.Where(x => x.Employee_Id == employeeId && x.Deleted == false).ToListAsync();

                if (listOfFeedbacks.Count > 0)
                {
                    list = (from feedback in listOfFeedbacks
                            select new FeedbackListResponseDTO
                            {
                                Id = feedback.Id,
                                Category_id = feedback.Category_Id,
                                Severity_Id = feedback.Severity_Id,
                                Created_By = feedback.Created_By,
                                Modified_Date = feedback.Modified_Date,
                                Status = feedback.Status,
                                Comments = feedback.Comments
                            }).ToList();
                }

                return GenericResponse<List<FeedbackListResponseDTO>>.Success(list, "Data Successfully Retreived!");
            }
            return GenericResponse<List<FeedbackListResponseDTO>>.Failure("Something went wrong!", ApiStatusCode.SomethingWentWrong);
        }
    }
}
