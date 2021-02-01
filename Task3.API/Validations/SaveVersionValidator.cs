using System.Data;
using FluentValidation;
using Task3.Core.DTO;

namespace Task3.API.Validations
{
    public class SaveVersionValidator : AbstractValidator<UploadMaterialVersionDTO>
    {
        public SaveVersionValidator()
        {
            RuleFor(m => m.File)
                .NotEmpty();
            RuleFor(m => m.Name)
                .NotEmpty();
        }
    }
}