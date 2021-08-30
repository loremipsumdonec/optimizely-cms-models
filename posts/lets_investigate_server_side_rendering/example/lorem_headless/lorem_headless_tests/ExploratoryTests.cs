﻿using Lorem.Test.Framework.Optimizely.CMS.Builders;
using lorem_headless.Models.Pages;
using lorem_headless_tests.Services;
using Xunit;

namespace lorem_headless_tests
{
    [Collection("Default")]
    [Trait("type", "exploratory")]
    public class ExploratoryTests
    {
        public ExploratoryTests(DefaultEngine engine)
        {
            Fixture = new ExploratoryFixture(engine);
        }

        public ExploratoryFixture Fixture { get; set; }

        [Fact]
        public void CreateSiteWithACreateReactApp()
        {
            Fixture.CreateSite<StartPage>(p => p.Renderer = "create-react-app");
        }


        [Fact]
        public void CreateSiteWithACreateReactAppWithHtml()
        {
            Fixture.CreateSite<StartPage>(p => p.Renderer = "create-react-app-with-html");
        }
    }
}