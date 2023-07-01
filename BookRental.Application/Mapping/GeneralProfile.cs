using AutoMapper;
using BookRental.Application.Dtos.AuthorDtos;
using BookRental.Application.Dtos.BookDtos;
using BookRental.Application.Dtos.GenreDtos;
using BookRental.Application.Dtos.UserDtos;
using BookRental.Domain.Entities;

namespace BookRental.Application.Mapping;

public class GeneralProfile : Profile
{
	public GeneralProfile()
	{
		CreateMap<Author, AuthorDto>()
			.ReverseMap();

		CreateMap<Author, AuthorSummaryDto>();

		CreateMap<Book, AuthorBooksDto>()
			.ForMember(m => m.Genre, c => c.MapFrom(b => b.Genre.Name));

		CreateMap<Book, BookDto>()
			.ForMember(m => m.Genre, c => c.MapFrom(b => b.Genre.Name))
			.ReverseMap();

		CreateMap<Genre, GenreDto>();

		CreateMap<CreateAuthorDto, Author>();

		CreateMap<User, UserDto>()
			.ForMember(m => m.Role, c => c.MapFrom(u => u.Role.Name));
	}
}
