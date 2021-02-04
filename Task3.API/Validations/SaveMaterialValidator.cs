using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
            RuleFor(m => m.CategoryNameId)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(2);
        }
    }
}