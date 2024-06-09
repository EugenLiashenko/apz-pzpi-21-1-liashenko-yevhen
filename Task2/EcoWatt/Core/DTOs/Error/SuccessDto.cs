namespace Core.DTOs.Error;

public class SuccessDto: IModel
{
    public string Message { get; set; }  = string.Empty;
}