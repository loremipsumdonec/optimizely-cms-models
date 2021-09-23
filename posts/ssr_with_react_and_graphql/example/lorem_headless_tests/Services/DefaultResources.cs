using Lorem.Test.Framework.Optimizely.CMS.Utility;
using System;
using System.IO;
using Xunit;

namespace lorem_headless_tests.Services
{
    public class DefaultResources
        : Resources
    {
        public DefaultResources()
            : base(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"))
        {
        }

        public string GetFile(string name) 
        {
            string path = System.IO.Path.Combine(Path, name);
            Assert.True(File.Exists(path));

            return path;
        }

        public string GetContent(string name)
        {
            string file = GetFile(name);
            return File.ReadAllText(file);
        }
    }
}
