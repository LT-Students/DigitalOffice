using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.FileService.Models;
using LT.DigitalOffice.FileService.Validators;
using NUnit.Framework;

namespace LT.DigitalOffice.FileServiceUnitTests.Validators
{
    public class AddNewFileRequestValidatorTests
    {
        private IValidator<FileCreateRequest> validator;

        private FileCreateRequest fileRequest;

        [SetUp]
        public void SetUp()
        {
            fileRequest = new FileCreateRequest
            {
                Content = "RGlnaXRhbCBPZmA5Y2U=",
                Extension = ".txt",
                Name = "DigitalOfficeTestFile"
            };

            validator = new NewFileValidator();
        }

        [Test]
        public void SuccessfulFileValidationTest()
        {
            validator.TestValidate(fileRequest).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void FailedFileEncodingValidationTest()
        {
            fileRequest.Content = "T 1 ! * & ? Z :C ; _____";
            var fileValidationResult = validator.TestValidate(fileRequest);
            fileValidationResult.ShouldHaveValidationErrorFor(f => f.Content);
        }

        [Test]
        public void FailedFileNameLengthValidationTest()
        {
            fileRequest.Name += fileRequest.Name.PadLeft(244);
            var fileValidationResult = validator.TestValidate(fileRequest);
            fileValidationResult.ShouldHaveValidationErrorFor(f => f.Name);
        }
    }
}
