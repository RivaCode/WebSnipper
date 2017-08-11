using System;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Business.Commands
{
    public class CreateSiteWatchCommand : ICreateSiteWatchCommand
    {
        private readonly ISiteWatchRepository _siteWatchRepository;
        private readonly IUrlValidator _urlValidator;

        public CreateSiteWatchCommand(
            ISiteWatchRepository siteWatchRepository,
            IUrlValidator urlValidator)
        {
            _siteWatchRepository = siteWatchRepository;
            _urlValidator = urlValidator;
        }

        public async Task Execute(CreateSiteWatch entity)
        {
            await _urlValidator.Validate(entity.Url);
            await _siteWatchRepository.AddAsync(
                SiteWatch.New(entity.Url)
                    .With(entity.Description)
                    .With(DateTime.Now));
        }
    }
}