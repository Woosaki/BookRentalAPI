using AutoMapper;
using BookRental.Application.Dtos;
using BookRental.Domain.Entities;

namespace BookRental.Application.Mapping;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<Book, AuthorBooksDto>();
        CreateMap<Genre, GenreDto>();
    }
}
