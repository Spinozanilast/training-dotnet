namespace Movies.Contracts;

public class MoviesResponse
{
    public IEnumerable<MovieResponse> Items { get; init; } = Enumerable.Empty<MovieResponse>();
}
