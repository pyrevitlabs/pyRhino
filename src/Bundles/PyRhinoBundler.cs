using System;
using System.Threading.Tasks;

using Bundler;

namespace PyRhino.Bundles
{
    public sealed class PyRhinoBundler : Bundler.Bundler<PyRhinoBundle>
    {
        bool _init = false;

        public async void InitAsync()
        {
            if (_init)
                return;

            var bundler = new PyRhinoBundleVisitor();

            BundlerResult result = await Task.Run(() =>
                PyRhinoPlugin.Bundler.QueryBundles(
                    "C:\\Users\\ein\\gits\\pyrevit-dev\\extensions", bundler
                ));

            _init = true;
        }
    }
}
