using ASPnet.Api.Entities;
using ASPnet.Dto;

namespace ASPnet.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}