using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Eto.Drawing;
using Eto.Forms;

namespace PyRhino.Panels
{
    [Guid("132bc0f9-09e3-4343-a536-9c082bb03eba")]
    public class PyRhinoPanel : BasePanel
    {
        public static Guid PanelId => typeof(PyRhinoPanel).GUID;

        sealed class Canvas : Drawable
        {
            public Canvas()
            {
                PyRhinoPlugin.Bundler.InitAsync();
                PyRhinoPlugin.Bundler.BundleChanged += (b) => Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                var g = e.Graphics;
                var rect = (Rectangle)e.ClipRectangle;

                g.DrawLine(Colors.DarkGray, rect.TopLeft, rect.BottomRight);
                g.DrawLine(Colors.DarkGray, rect.TopRight, rect.BottomLeft);
            }
        }

        public PyRhinoPanel(uint documentSerialNumber)
        {
            Content = new Canvas();
        }
    }
}