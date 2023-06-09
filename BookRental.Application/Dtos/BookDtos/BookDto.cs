﻿using BookRental.Application.Dtos.AuthorDtos;

namespace BookRental.Application.Dtos.BookDtos;

#nullable disable

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public AuthorSummaryDto Author { get; set; }
    public string Genre { get; set; }
}
