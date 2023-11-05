using System;
using System.IO;
using System.Collections.Generic;

using Rhino.Runtime.Code.Bundles;
using Rhino;

namespace PyRhino.Bundles
{
    sealed class PyRhinoBundleVisitor : BundleVisitor
    {
        sealed class RhinoLogger : ILogger
        {
            public void Error(string message) => Rhino.RhinoApp.WriteLine(message);
            public void Info(string message) => Rhino.RhinoApp.WriteLine(message);
            public void Warning(string message) => Rhino.RhinoApp.WriteLine(message);
        }

        public override ILogger Logger => new RhinoLogger();

        public override bool TryVisitBundle(DirectoryInfo path, BundleDefinition data, out Bundle bundle)
        {
            bundle = new PyRhinoBundle(path, data);

            //if (bundle.Data.TryGet("title", out string title))
            //    RhinoApp.WriteLine($"-> {title}");

            //else if (bundle.Data.TryGetDictionary("title", out IDictionary<string, string> titles))
            //    RhinoApp.WriteLine($"-> {titles["en_us"]}");

            return true;
        }
    }
}
