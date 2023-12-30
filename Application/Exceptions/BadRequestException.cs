using FluentValidation.Results;

namespace Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; set; } = new List<string>();

    public BadRequestException(string message) : base(message)
    {  
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
		foreach (ValidationFailure error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
		} 
    }
}
