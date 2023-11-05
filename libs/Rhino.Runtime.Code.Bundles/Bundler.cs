using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace Rhino.Runtime.Code.Bundles
{
    public abstract class Bundler<TBundle> : IEnumerable<TBundle> where TBundle : Bundle
    {
        readonly HashSet<DirectoryInfo> _paths = new HashSet<DirectoryInfo>();
        readonly List<TBundle> _bundles = new List<TBundle>();

        public delegate void BundleChangedHandler(TBundle bundle);

        public event BundleChangedHandler BundleChanged;

        public BundlerResult QueryBundles(string path, BundleVisitor visitor)
            => VisitBundles(new DirectoryInfo(path), visitor);

        public BundlerResult QueryBundles(DirectoryInfo path, BundleVisitor visitor)
            => VisitBundles(path, visitor);

        BundlerResult VisitBundles(DirectoryInfo path, BundleVisitor visitor)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (DirectoryInfo subdir in path.GetDirectories("*.*", SearchOption.AllDirectories))
            {
                if (Bundle.TryBundle(subdir, visitor, out TBundle b))
                {
                    b.QueryBundles(subdir, visitor);
                    _bundles.Add(b);
                }
            }

            sw.Stop();

            _paths.Add(path);
            return new BundlerResult(sw.Elapsed);
        }

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<TBundle> GetEnumerator() => _bundles.GetEnumerator();
        #endregion
    }
}
