using System;
using FluentValidation;
using Task3.Core.DTO;

namespace Task3.API.Validations
{
    public class SaveMaterialValidator : AbstractValidator<UploadMaterialDTO>
    {
        public SaveMaterialValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(m => m.File)
                .NotNull();
            RuleFor(m => m.CategoryNameId == Convert.ToInt32('0')
                         || m.CategoryNameId == Convert.ToInt32('1')
                         || m.CategoryNameId == Convert.ToInt32('2'));
        }
    }
}