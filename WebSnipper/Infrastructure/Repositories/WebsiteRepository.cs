using Domain.Business.Interfaces;
using Infrastructure.Core;
using Infrastructure.Model;
using Newtonsoft.Json.Linq;
using DomainModel = Domain.Models;

namespace Infrastructure.Repositories
{
    internal class WebsiteRepository : MappingRepository<DomainModel.Website, WebSite>, IWebsiteRepository
    {
        public WebsiteRepository(
            IJsonStore store,
            IModelConverter<DomainModel.Website, WebSite> converter)
            : base(store, converter)
        {
        }

        protected override StoreKey MappingKey => StoreKey.Sites;

        protected override void AddOverride(JToken head, WebSite newNode)
            => ((JArray) head).Add(JObject.FromObject(newNode));

        protected override void RemoveOverride(JToken head, WebSite oldNode) 
            => ((JArray) head).Remove(JObject.FromObject(oldNode));

        protected override void UpdateOverride(JToken head, WebSite node) 
            => ((JArray)head).Replace(JObject.FromObject(node));
    }
}