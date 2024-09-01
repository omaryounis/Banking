using BankingPortal.Application.Models;
using BankingPortal.Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;

namespace BankingPortal.Application.Validations
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Street)
                .NotEmpty().WithMessage("Street is required.");

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(address => address.ZipCode)
                .NotEmpty().WithMessage("Zip code is required.");
        }
    }
}