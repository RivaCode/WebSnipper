using System;
using Infrastructure.Core;
using Infrastructure.Model;
using Optional;
using DomainModel = Domain.Models;

namespace Infrastructure.Converters
{
    internal class WebsiteConverter : IModelConverter<DomainModel.Website, WebSite>
    {
        public DomainModel.Website ToModel(WebSite persisted)
        {
            return new DomainModel.Website(
                new DomainModel.UrlHolder(persisted.Url), 
                new DomainModel.PageProperties(
                    persisted.Name,
                    persisted.ScannedAt,
                    persisted.Description.NoneWhen(value => value == null)));
        }

        public WebSite ToPersisted(DomainModel.Website model)
        {
            return new WebSite
            {
                Url = model.UrlHolder.Url.ToString(),
                Description = model.Properties.Description.ValueOr((string) null),
                Name = model.Properties.Name,
                ScannedAt = DateTime.Parse(model.Properties.ScanDate)
            };
        }

        public void SyncChanges(DomainModel.Website from, WebSite to)
        {
            to.Url = from.UrlHolder.Url.ToString();
            to.Description = from.Properties.Description.ValueOr((string) null);
            to.Name = from.Properties.Name;
            to.ScannedAt = DateTime.Parse(from.Properties.ScanDate);
        }
    }
}