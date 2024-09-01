using BankingPortal.Application.Features.Queries.Clients;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Validations
{
    public class GetClientsQueryValidator : AbstractValidator<GetClientsQuery>
    {
        public GetClientsQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.SortBy).NotEmpty();
            // No validation required for SearchTerm if it's optional
            RuleFor(x => x.SortBy).NotEmpty().WithMessage("SortBy is required.");
        }
    }

}
