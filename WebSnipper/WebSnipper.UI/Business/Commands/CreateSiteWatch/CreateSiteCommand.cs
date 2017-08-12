using System;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Business.Commands
{
    public class CreateSiteCommand : ICreateSiteCommand
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IUrlValidator _urlValidator;

        public CreateSiteCommand(
            ISiteRepository siteRepository,
            IUrlValidator urlValidator)
        {
            _siteRepository = siteRepository;
            _urlValidator = urlValidator;
        }

        public async Task Execute(CreateSiteModel entity)
        {
            await _urlValidator.Validate(entity.Url);
            await _siteRepository.AddAsync(
                Site.New(entity.Url)
                    .With(entity.Description)
                    .With(DateTime.Now));
        }
    }
}