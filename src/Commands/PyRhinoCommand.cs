using Rhino;
using Rhino.Commands;

using PyRhino.Bundles;

namespace PyRhino.Commands
{
    public class PyRhinoCommand : Command
    {
        public static PyRhinoCommand Instance { get; private set; }

        public PyRhinoCommand() => Instance = this;

        public override string EnglishName => "pyRhino";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            return Result.Success;
        }
    }
}
