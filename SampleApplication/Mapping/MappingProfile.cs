using AutoMapper;
using Feedback360.DB.Entities;
using Feedback360.Models.Models.Feedbacks.Request;
using Feedback360.Models.Models.Feedbacks.Response;

namespace SampleApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FeedbackAddRequestDTO, Feedback>();
            CreateMap<Feedback, FeedbackListResponseDTO>();
        }
    }
}
