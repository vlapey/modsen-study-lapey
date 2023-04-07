using AutoMapper;
using Entities.Models;
using WebApi.Dto;

namespace WebApi.AutoMapperProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
    }
}