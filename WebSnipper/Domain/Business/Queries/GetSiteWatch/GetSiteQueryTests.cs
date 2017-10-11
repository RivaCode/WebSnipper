using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using Autofac.Extras.FakeItEasy;
using Domain.Business.Interfaces;
using Domain.Models;
using FakeItEasy;
using FluentAssertions;
using Optional;
using Xunit;

namespace Domain.Business
{
    public class GetSiteQueryTests
    {
        [Fact]
        public void GetSites_Execute_ShouldRetrieveListOfSites()
        {
            using (AutoFake autoFake = new AutoFake())
            {
                var repository = autoFake.Resolve<IWebsiteRepository>();
                var sud = new GetSiteQuery(repository, 0, ImmediateScheduler.Instance);

                var websiteItem = new Website(
                    new UrlHolder("www.google.com"), 
                    new PageProperties("google", DateTime.Now, Option.None<string>()));
                A.CallTo(
                        () => repository.GetAllAsync())
                    .Returns(new List<Website> {websiteItem});

                SiteModel actual = null;
                sud.Execute().Subscribe(result => actual = result);

                SiteModel expected = new SiteModel
                {
                    Description = null,
                    Url = websiteItem.UrlHolder.Url.ToString(),
                    Name = websiteItem.Properties.Name
                };

                actual.Should().NotBeNull();

                actual.Url.Should().NotBeNullOrWhiteSpace()
                    .And.StartWith("http")
                    .And.BeEquivalentTo(expected.Url);

                actual.Name.Should().NotBeNullOrWhiteSpace()
                    .And.BeEquivalentTo(expected.Name);

                actual.Description.Should().Be(expected.Description);
            }
        }
    }
}