using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record class CreateGameDto(
    // Name is required and it cannot exceed 50
    [Required][StringLength(50)] string Name,
    int GenreId,
    [Required][Range(1, 100)] decimal Price,
    DateOnly RealeaseDate
);