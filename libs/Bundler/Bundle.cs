using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Bundler
{
    public abstract class Bundle
    {
        public static bool TryBundle<TBundle>(DirectoryInfo path, BundleVisitor visitor, out TBundle bundle)
            where TBundle : Bundle
        {
            var data = BundleData.Empty;
            var dataFile = Path.Combine(path.FullName, BUNDLE_FILE);
            if (File.Exists(dataFile)
                && BundleData.TryReadData(visitor, dataFile, out BundleData d))
            {
                data = d;
            }

            return visitor.TryVisitBundle(path, data, out bundle);
        }

        const string BUNDLE_FILE = "bundle.yaml";

        public Guid Id { get; } = Guid.NewGuid();

        public DirectoryInfo Location { get; }

        public BundleData Data { get; }

        public Bundle(DirectoryInfo path, BundleData data)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            if (data is null)
                throw new ArgumentNullException(nameof(data));

            Location = path;
            Data = data;
        }

        public Bundle this[Guid id] => Children.First(b => b.Id == id);

        public ICollection<Bundle> Children { get; } = new List<Bundle>();

        public void QueryBundles(DirectoryInfo path, BundleVisitor visitor)
        {
            foreach (DirectoryInfo subdir in path.GetDirectories("*.*", SearchOption.AllDirectories))
            {
                // find bundle metadata
                var data = BundleData.Empty;
                var dataFile = Path.Combine(subdir.FullName, BUNDLE_FILE);
                if (File.Exists(dataFile)
                    && BundleData.TryReadData(visitor, dataFile, out BundleData d))
                {
                    data = d;
                }

                // create bundle
                if (visitor.TryVisitBundle(subdir, data, out Bundle b))
                {
                    b.QueryBundles(subdir, visitor);
                    Children.Add(b);
                }
            }
        }
    }
}
