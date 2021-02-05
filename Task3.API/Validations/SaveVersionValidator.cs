using System.Data;
using FluentValidation;
using Task3.API.DtoRes;

namespace Task3.API.Validations
{
    public class SaveVersionValidator : AbstractValidator<UploadMaterialVersionDto>
    {
        public SaveVersionValidator()
        {
            /*RuleFor(m => m.File)
                .NotEmpty();*/
            RuleFor(m => m.Name)
                .NotEmpty();
        }
    }
}