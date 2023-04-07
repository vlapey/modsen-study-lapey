using AutoMapper;
using Entities.Models;
using modsen_study_lapey.Dto;

namespace modsen_study_lapey.AutoMapperProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
    }
}