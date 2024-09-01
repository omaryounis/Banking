using BankingPortal.Application.Features.Commands.Clients;
using BankingPortal.Application.Models;
using FluentValidation;
using PhoneNumbers;
namespace BankingPortal.Application.Validations
{

    public class ClientValidator : AbstractValidator<CreateClientCommand>
    {
        public ClientValidator()
        {
            // FirstName validation
            RuleFor(client => client.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(60).WithMessage("First name must not exceed 60 characters.");

            // LastName validation
            RuleFor(client => client.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(60).WithMessage("Last name must not exceed 60 characters.");

            // Email validation
            RuleFor(client => client.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // PersonalId validation
            RuleFor(client => client.PersonalId)
                .NotEmpty().WithMessage("Personal ID is required.")
                .Length(11).WithMessage("Personal ID must be exactly 11 characters.");

            // MobileNumber validation
            RuleFor(client => client.MobileNumber)
           .NotEmpty().WithMessage("Mobile number is required.")
           .Must((client, mobileNumber) => ValidateMobileNumber(client.Address?.CountryCode, mobileNumber))
           .WithMessage("Invalid mobile number format for the selected country.");

            // Sex validation
            RuleFor(client => client.Sex)
             .IsInEnum().WithMessage("Sex must be either Male or Female.")
             .NotEmpty().WithMessage("Sex is required.");

            // ProfilePhoto validation
            RuleFor(client => client.ProfilePhoto)
                .NotEmpty().WithMessage("Profile photo is required.") 
                .When(client => client.ProfilePhoto != null)
                .Must(photo => photo.Length > 0).WithMessage("Profile photo cannot be empty."); // Validates it's not empty if provided

            // Address validation (if necessary)
            RuleFor(client => client.Address)
                .SetValidator(new AddressValidator()); // Assuming you have a validator for the Address entity
            RuleFor(x => x.Accounts)
          .NotEmpty().WithMessage("At least one account is required.");
        }
        private bool ValidateMobileNumber(string mobileKeyCode, string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileKeyCode) || string.IsNullOrEmpty(mobileNumber))
                return false;
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var numberProto = phoneNumberUtil.Parse(mobileNumber, mobileKeyCode);
            // Check if the number is valid for the region
            return phoneNumberUtil.IsValidNumberForRegion(numberProto, mobileKeyCode);
        }

    }

}
