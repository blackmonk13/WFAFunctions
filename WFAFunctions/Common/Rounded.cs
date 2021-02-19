using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAFunctions.Common
{
    public static class Rounded
    {
        [DllImport("Gdi32.dll")]
        private static extern IntPtr CurveRegion(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

		// Rounding Form Edges
		public static void Apply(Form Form, int Curve)
		{
			try
			{
				Form.FormBorderStyle = FormBorderStyle.None;
				Form.Region = Region.FromHrgn(CurveRegion(0, 0, Form.Width, Form.Height, Curve, Curve));
			}
			catch (Exception)
			{
			}
		}

		// Rounding Control Edges
		public static void Apply(Control ctrl, int Curve)
		{
			try
			{
                ctrl.Region = Region.FromHrgn(CurveRegion(0, 0, ctrl.Width, ctrl.Height, Curve, Curve));
			}
			catch (Exception)
			{
			}
		}
	}
}
