using System;

namespace Bundler
{
    public sealed class BundlerResult
    {
        public TimeSpan Elapsed { get; }

        public BundlerResult(TimeSpan elapsed)
        {
            Elapsed = elapsed;
        }
    }
}
