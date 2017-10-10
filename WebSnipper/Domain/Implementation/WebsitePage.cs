using System;
using Domain.Models;
using Domain.Util;

namespace Domain.Implementation
{
    internal class WebsitePage : IWebsitePage
    {
        public Website Website { get; }

        public WebsitePage(
            Website website)
        {
            Website = website.ValidateArgument(nameof(website));
        }

        public IWebsitePage UpdateWhenScanned(DateTime newScanDate)
            => new WebsitePage(Website.ChangeScannedDate(newScanDate));
    }
}
