using EPiServer.Web;
using Lorem.Test.Framework.Optimizely.CMS;
using Lorem.Test.Framework.Optimizely.CMS.Utility;
using lorem_headless.Models.Pages;
using System;
using System.Globalization;

namespace lorem_headless_tests.Services
{
    public class ExploratoryFixture
        : Fixture
    {
        public ExploratoryFixture(IEngine engine)
            : base(engine)
        {
            Cultures.Add(CultureInfo.GetCultureInfo("en"));

            RegisterBuilder<SiteDefinition>(s => {
                s.Name = "Lorem";
                s.SiteUrl = new Uri("http://localhost:59590");
            });

            RegisterBuilder<StartPage>(p => {
                p.Heading = "Welcome to Lorem";
                p.Preamble = IpsumGenerator.Generate(10, 18);
            });

            RegisterBuilder<ArticlePage>(p => {
                p.Heading = IpsumGenerator.Generate(3, 8, false);
                p.Preamble = IpsumGenerator.Generate(10, 18);
            });

            Start();

            CreateUser(
                "Administrator",
                "Administrator123!",
                "admin@supersecretpassword.io",
                "WebAdmins", "Administrators"
            );
        }
    }
}
