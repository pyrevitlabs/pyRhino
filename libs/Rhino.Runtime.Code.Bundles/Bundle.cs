using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Rhino.Runtime.Code.Bundles
{
    public abstract class Bundle
    {
        public static bool TryBundle<TBundle>(DirectoryInfo path, BundleVisitor visitor, out TBundle bundle)
            where TBundle : Bundle
        {
            var data = BundleDefinition.Empty;
            var dataFile = Path.Combine(path.FullName, BUNDLE_FILE);
            if (File.Exists(dataFile)
                && BundleDefinition.TryReadData(visitor, dataFile, out BundleDefinition d))
            {
                data = d;
            }

            return visitor.TryVisitBundle(path, data, out bundle);
        }

        const string BUNDLE_FILE = "bundle.yaml";

        public Guid Id { get; } = Guid.NewGuid();

        public DirectoryInfo Location { get; } = new DirectoryInfo(Path.GetTempPath());

        public BundleDefinition Definition { get; } = BundleDefinition.Empty;

        public string Extension => Location.Extension;

        public Bundle(DirectoryInfo path, BundleDefinition definition)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            Location = path;
            Definition = definition;
        }

        public Bundle this[Guid id] => Children.First(b => b.Id == id);

        public ICollection<Bundle> Children { get; } = new List<Bundle>();

        public void QueryBundles(DirectoryInfo path, BundleVisitor visitor)
        {
            foreach (DirectoryInfo subdir in path.GetDirectories("*.*", SearchOption.AllDirectories))
            {
                // find bundle metadata
                var data = BundleDefinition.Empty;
                var dataFile = Path.Combine(subdir.FullName, BUNDLE_FILE);
                if (File.Exists(dataFile)
                    && BundleDefinition.TryReadData(visitor, dataFile, out BundleDefinition d))
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
