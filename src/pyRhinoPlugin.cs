using System;
using System.IO;
using System.Reflection;
using SD = System.Drawing;

using Rhino.UI;
using Rhino.PlugIns;

using PyRhino.Panels;
using PyRhino.Bundles;

namespace PyRhino
{
    public class PyRhinoPlugin : Rhino.PlugIns.PlugIn
    {
        public static PyRhinoBundler Bundler { get; } = new PyRhinoBundler();

        public PyRhinoPlugin()
        {
            Instance = this;
            
            Rhino.UI.Panels.RegisterPanel(
                this, typeof(PyRhinoPanel), "pyRhino", GetIcon(typeof(PyRhinoPanel).Name), PanelType.System);
        }

        public override PlugInLoadTime LoadTime { get; } = PlugInLoadTime.AtStartup;

        public static PyRhinoPlugin Instance { get; private set; }

        #region Plugin Resources
        static readonly Assembly s_assembly = typeof(PyRhinoPlugin).Assembly;

        static SD.Icon GetIcon(Assembly assembly, string name)
        {
            string assemblyName = assembly.GetName().Name;
            try
            {
                using (Stream stream =
                    assembly.GetManifestResourceStream($"{assemblyName}.Icons.{name}.png"))
                {
                    var img = SD.Image.FromStream(stream);
                    return SD.Icon.FromHandle(((SD.Bitmap)img).GetHicon());
                }
            }
            catch
            {
                return null;
            }
        }

        public static SD.Icon GetIcon(string name) => GetIcon(s_assembly, name);
        #endregion
    }
}