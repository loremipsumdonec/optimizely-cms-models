using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lorem_headless.Features.Render.Services
{
    public class EnginePool
    {


        public IEngine GetEngine()
        {
            return null;
        }
    }

    public interface IEngine
    {
        string Render(params object[] args);
    }
}