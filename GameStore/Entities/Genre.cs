namespace GameStore.Entities;

public class Genre
{
    public int Id { get; set; }
    // When creating Genre it should contain a Names 
    public required string Name { get; set; }
}