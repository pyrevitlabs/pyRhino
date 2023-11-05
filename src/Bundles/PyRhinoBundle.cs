using System;
using System.IO;

using Bundler;

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

        public PyRhinoBundle(DirectoryInfo path, BundleData data)
            : base(path, data)
        {
        }
    }
}
