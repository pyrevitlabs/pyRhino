using System;
using System.IO;

namespace Bundler
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

        public bool TryVisitBundle<TBundle>(DirectoryInfo path, BundleData data, out TBundle bundle)
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

        public abstract bool TryVisitBundle(DirectoryInfo path, BundleData data, out Bundle bundle);
    }
}
