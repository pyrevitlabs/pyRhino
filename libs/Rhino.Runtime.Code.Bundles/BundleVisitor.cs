using System;
using System.IO;

namespace Rhino.Runtime.Code.Bundles
{
    public abstract class BundleVisitor
    {
        public interface ILogger
        {
            void Error(string message);
            void Warning(string message);
            void Info(string message);
        }

        public abstract ILogger Logger { get; }

        public bool TryVisitBundle<TBundle>(DirectoryInfo path, BundleDefinition data, out TBundle bundle)
            where TBundle : Bundle
        {
            bundle = default;

            if (TryVisitBundle(path, data, out Bundle b)
                    && typeof(TBundle).IsAssignableFrom(b.GetType()))
            {
                bundle = (TBundle)b;
                return true;
            }

            return false;
        }

        public abstract bool TryVisitBundle(DirectoryInfo path, BundleDefinition data, out Bundle bundle);
    }
}
