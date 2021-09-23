using Lorem.Test.Framework.Optimizely.CMS.Builders;
using Lorem.Test.Framework.Optimizely.CMS.Utility;
using lorem_headless.Models.Pages;
using lorem_headless_tests.Services;
using System.Text;
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

        [Fact]
        public void CreateSiteWithCreateReactAppFinal() 
        {
            Fixture.CreateSite<StartPage>(p => p.Renderer = "create-react-app-final")
                .CreateMany<ArticlePage>(5,p=> {
                    p.VisibleInMenu = true;
                    p.Heading = IpsumGenerator.Generate(3, 6, false);
                    p.Preamble = IpsumGenerator.Generate(12, 16);

                    StringBuilder builder = new StringBuilder();

                    for(int index=0;index < 13; index++) 
                    {
                        if(index % 3 == 0) 
                        {
                            builder.Append($"<h2>{IpsumGenerator.Generate(5, 6)}");
                        }

                        builder.Append($"<p>{IpsumGenerator.Generate(12, 29)}</p>");
                    }

                    p.Text = new EPiServer.Core.XhtmlString(builder.ToString());
                });
        }
    }
}
