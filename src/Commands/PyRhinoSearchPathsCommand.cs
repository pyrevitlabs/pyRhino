using Rhino;
using Rhino.Commands;

namespace PyRhino.Commands
{
    public class PyRhinoSearchPathsCommand : Command
    {
        public static PyRhinoSearchPathsCommand Instance { get; private set; }

        public PyRhinoSearchPathsCommand() => Instance = this;

        public override string EnglishName => "PyRhinoSearchPaths";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            return Result.Success;
        }
    }
}
