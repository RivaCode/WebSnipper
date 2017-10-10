using System;
using System.Threading.Tasks;
using Domain.Business.Interfaces;
using Domain.Models;
using Optional;

namespace Domain.Business
{
    public class CreateSiteCommand : ICreateSiteCommand
    {
        private readonly IWebsiteRepository _siteRepository;

        public CreateSiteCommand(IWebsiteRepository siteRepository) 
            => _siteRepository = siteRepository;

        public async Task Execute(CreateSiteModel entity)
        {
            Website site = new Website(
                new UrlHolder(), 
                new PageProperties(
                    entity.Name, DateTime.Now, 
                    Option.Some(entity.Description).NotNull()));

            await _siteRepository.AddAsync(site);
        }
    }
}