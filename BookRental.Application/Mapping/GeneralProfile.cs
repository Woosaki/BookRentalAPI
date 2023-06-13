using AutoMapper;
using BookRental.Application.Dtos;
using BookRental.Domain.Entities;

namespace BookRental.Application.Mapping;

public class GeneralProfile : Profile
{
	public GeneralProfile()
	{
		CreateMap<Author, AuthorDto>();
		CreateMap<Book, AuthorBooksDto>()
			.ForMember(m => m.Genre, c => c.MapFrom(b => b.Genre.Name));
		CreateMap<Genre, GenreDto>();
		CreateMap<CreateAuthorDto, Author>();
	}
}
