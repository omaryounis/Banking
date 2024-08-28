using BankingPortal.Application.Features.Commands.Clients;
using BankingPortal.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/client")]
    public class ClientController : BaseController
    {
        private readonly MediatR.IMediator _mediator;
        public ClientController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromForm] ClientDto clientDto, IFormFile profilePhoto)
        {
            //TO DO move it to service to prepare the received photo
            byte[] photoData = null;

            if (profilePhoto != null && profilePhoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await profilePhoto.CopyToAsync(memoryStream);
                    photoData = memoryStream.ToArray();
                }
            }
            var command = new CreateClientCommand
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                PersonalId = clientDto.PersonalId,
                MobileNumber = clientDto.MobileNumber,
                Sex = clientDto.Sex,
                Address = new AddressDto
                {
                    Country = clientDto.Address.Country,
                    City = clientDto.Address.City,
                    Street = clientDto.Address.Street,
                    ZipCode = clientDto.Address.ZipCode
                },
                Accounts = clientDto.Accounts,
                ProfilePhoto = photoData // Pass the photo as byte array
            };

            var clientId = await _mediator.Send(command);
            return Ok(clientId);
        }

    }
}
