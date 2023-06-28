namespace ComentariosApi.Models;

public class ComentarioItemDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string Text { get; set; }

}