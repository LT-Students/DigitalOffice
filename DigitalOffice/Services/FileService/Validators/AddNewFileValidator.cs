using FluentValidation;
using LT.DigitalOffice.FileService.Models;
using System;

namespace LT.DigitalOffice.FileService.Validators
{
    public class AddNewFileValidator : AbstractValidator<FileCreateRequest>
    {
        public AddNewFileValidator()
        {
            RuleFor(file => file.Name)
                .NotEmpty()
                .WithMessage("File must have a name")
                .MaximumLength(244)
                .WithMessage("File name is too long")
                .Matches("^[A-Z 0-9][A-Z a-z 0-9]+$");

            RuleFor(file => file.Content)
                .NotNull()
                .WithMessage("The file is empty")
                .Must(IsBase64Coded);
        }

        private bool IsBase64Coded(string base64String)
        {
            var byteString = new Span<byte>(new byte[base64String.Length]);
            return Convert.TryFromBase64String(base64String, byteString, out _);
        }
    }
}
