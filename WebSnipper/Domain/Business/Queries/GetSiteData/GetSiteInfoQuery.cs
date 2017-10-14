using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using Domain.Business.Interfaces;
using Domain.Util;

namespace Domain.Business
{
    internal class GetSiteInfoQuery : IGetSiteInfoQuery
    {
        private readonly IWebsiteRepository _repository;

        public GetSiteInfoQuery(
            IWebsiteRepository repository) => _repository = repository;

        public IObservable<SiteInfoModel> Execute(string id)
            => _repository
                .TryFindById(id)
                .ToObservable()
                .ThrowIfEmpty(() => new KeyNotFoundException($"{id} not found"))
                .ReduceTo(website => new SiteInfoModel());
    }
}
