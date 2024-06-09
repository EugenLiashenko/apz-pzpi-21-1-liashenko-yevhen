namespace Core.DTOs.Error;

public class ErrorDto: IModel
{
    public string Message { get; set; }  = string.Empty;
}