using System;

namespace Rhino.Runtime.Code.Bundles
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
