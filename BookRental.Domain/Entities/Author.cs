﻿using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

#nullable disable

public sealed class Author : BaseEntity
{
	public string Name { get; set; }
	public DateOnly Born { get; set; }
	public List<Book> Books { get; set; } = new();
}
