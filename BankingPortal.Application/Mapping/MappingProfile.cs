using AutoMapper;
using BankingPortal.Application.Features.Commands.Clients;
using BankingPortal.Application.Models;
using BankingPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             
            // Mapping for Client to ClientDto
            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.ProfilePhoto)).ReverseMap();
            // Mapping for Address and Account objects
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<CreateClientCommand, Client>()
          .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => ConvertToByteArray(src.ProfilePhoto)));

        }
        private byte[] ConvertToByteArray(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        // Helper method to convert byte[] to base64 string
        private string ConvertToBase64(byte[] fileBytes)
        {
            return fileBytes != null ? Convert.ToBase64String(fileBytes) : null;
        }
    }

}
