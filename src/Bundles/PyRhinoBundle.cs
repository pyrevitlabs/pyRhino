using System;
using System.IO;

using Rhino.Runtime.Code.Bundles;

namespace PyRhino.Bundles
{
    public enum BundleKind
    {
        Toolbar,
        PushButton,
    }

    public sealed class PyRhinoBundle : Bundle
    {
        public BundleKind Kind { get; } = BundleKind.PushButton;

        public PyRhinoBundle(DirectoryInfo path, BundleDefinition data)
            : base(path, data)
        {
        }
    }
}
