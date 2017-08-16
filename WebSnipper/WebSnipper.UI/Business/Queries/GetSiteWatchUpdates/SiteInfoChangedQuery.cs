using System;
using System.Reactive.Linq;

namespace WebSnipper.UI.Business.Queries
{
    public class SiteInfoChangedQuery : ISiteInfoChangedQuery
    {
        public IObservable<SiteUpdateModel> Execute()
        {
            return Observable.Empty<SiteUpdateModel>();
        }
    }
}