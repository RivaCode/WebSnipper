using System;

namespace Domain
{
    public interface IWebsitePage
    {
        IWebsitePage UpdateWhenScanned(DateTime newScanDate);
    }
}
