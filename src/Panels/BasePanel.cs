using System;

using Eto.Drawing;
using Eto.Forms;

namespace PyRhino.Panels
{
    public abstract class BasePanel : Panel
    {
        public BasePanel()
        {
            Styles.Add<TableLayout>(
              "pyrhino.panels.tableContent", ct =>
              {
                  ct.Padding = new Padding(20, 20);
                  ct.Spacing = new Size(10, 10);
              });

            Styles.Add<StackLayout>(
              "pyrhino.panels.stackContent", cs =>
              {
                  cs.Padding = new Padding(20, 20);
                  cs.Spacing = 10;
              });

            Styles.Add<TableLayout>(
              "pyrhino.panels.tableLayout", t =>
              {
                  t.Spacing = new Size(10, 10);
              });

            Styles.Add<StackLayout>(
              "pyrhino.panels.stackLayout", s =>
              {
                  s.Padding = new Padding(10, 10);
                  s.Spacing = 10;
              });

            Styles.Add<StackLayout>(
              "pyrhino.panels.horizStackLayout", s =>
              {
                  s.Orientation = Orientation.Horizontal;
                  s.HorizontalContentAlignment = HorizontalAlignment.Left;
                  s.VerticalContentAlignment = VerticalAlignment.Center;
                  s.AlignLabels = true;
                  s.Spacing = 10;
              });

            Styles.Add<StackLayout>(
              "pyrhino.panels.horizStackLayoutRight", s =>
              {
                  s.Orientation = Orientation.Horizontal;
                  s.HorizontalContentAlignment = HorizontalAlignment.Right;
                  s.VerticalContentAlignment = VerticalAlignment.Center;
                  s.AlignLabels = true;
                  s.Spacing = 10;
              });

            Styles.Add<TextBox>(
              "pyrhino.panels.textBox", ct =>
              {
                  ct.Height = 24;
              });

            Styles.Add<DropDown>(
              "pyrhino.panels.dropDown", ct =>
              {
                  ct.Height = 24;
              });

            Styles.Add<Button>(
              "pyrhino.panels.button", ct =>
              {
                  ct.MinimumSize = new Size(100, 24);
              });
        }
    }
}
