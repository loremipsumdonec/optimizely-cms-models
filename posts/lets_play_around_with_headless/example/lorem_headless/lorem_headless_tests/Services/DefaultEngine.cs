using Lorem.Test.Framework.Optimizely.CMS;
using Lorem.Test.Framework.Optimizely.CMS.Modules;

namespace lorem_headless_tests.Services
{
    public class DefaultEngine
        : Engine
    {
        public DefaultEngine()
        {
            Add(new CmsTestModule() 
            { 
                IamAwareThatTheDatabaseWillBeDeletedAndReCreated = true,
                IamAwareThatTheFilesAndFoldersAtAppDataPathWillBeDeleted = true,
            });
        }
    }
}
