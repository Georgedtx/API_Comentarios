namespace ComentariosApi.Models;

public class ComentarioItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string Text { get; set; }
    public bool IsComplete { get; set; }
}