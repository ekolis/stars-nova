#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// Simple control that can display 2d graph
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Nova.ControlLibrary
{
    /// <summary>
    /// Simple control that can display 2d graph.
    /// </summary>
    public partial class Graph : UserControl
    {
        #region Fields
        private int[] data;
        private int minVal = int.MaxValue;
        private int maxVal = int.MinValue;
        private Rectangle graphBounds;
        #endregion

        #region Properties
        public Color LineColor { get; set; }
        public Color AxisColor { get; set; }

        public double HoriozontalSpace { get; set; }
        public double VerticalSpace { get; set; }
        public double TitleSize { get; set; }
        public Image Image { get; set; }
        public string Title { get; set; }
        public int[] Data
        {
            get 
            { 
                return data; 
            }
            set
            {
                data = value;
                RefreshDiagram();
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void CalcMinMaxVal()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                minVal = Math.Min(minVal, data[i]);
                maxVal = Math.Max(maxVal, data[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void DrawData(ref Graphics g)
        {
            double yTick = (graphBounds.Bottom - (graphBounds.Top * 1.0)) / (maxVal - (minVal * 1.0)) * 0.9;
            double xTick = (graphBounds.Right - graphBounds.Left) / Data.Length;
            SolidBrush brush = new SolidBrush(LineColor);
            Pen linePen = new Pen(brush);
            for (int i = 1; i < Data.Length; i++)
            {
                g.DrawLine(
                    linePen, 
                    graphBounds.Left + 3 + (int)(xTick * (i - 1)), 
                    graphBounds.Bottom - 2 - (int)(yTick * Data[i - 1]), 
                    graphBounds.Left + 3 + (int)(xTick * i), 
                    graphBounds.Bottom - 2 - (int)(yTick * Data[i]));
            }
            linePen.Dispose();
            brush.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void DrawAxis(ref Graphics g)
        {
            SolidBrush brush = new SolidBrush(AxisColor);
            Pen p = new Pen(brush);
            p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, graphBounds.Left + 3, graphBounds.Bottom - 2, graphBounds.Left + 3, graphBounds.Top + 2);
            g.DrawLine(p, graphBounds.Left + 3, graphBounds.Bottom - 2, graphBounds.Right - 3, graphBounds.Bottom - 2);
            p.Dispose();
            brush.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void DrawTitle(ref Graphics g)
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;

            Rectangle titleBounds = new Rectangle(
                (int)(width * HoriozontalSpace),
                (int)(height * VerticalSpace),
                (int)(width * (1 - (2 * HoriozontalSpace))),
                (int)(TitleSize * (1 + VerticalSpace)));
            g.DrawRectangle(Pens.Black, titleBounds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void CalcBounds(int width, int height)
        {
            if (string.IsNullOrEmpty(Title) == false)
                graphBounds = new Rectangle(
                    (int)(width * HoriozontalSpace),
                    (int)(height * VerticalSpace),
                    (int)(width * (1 - (2 * HoriozontalSpace))),
                    (int)(height * (1 - (2 * VerticalSpace))));
            else // reserve space for title
                graphBounds = new Rectangle(
                    (int)(width * HoriozontalSpace),
                    (int)((height * VerticalSpace) + (TitleSize * (1 + VerticalSpace))),
                    (int)(width * (1 - (2 * HoriozontalSpace))),
                    (int)((height - (TitleSize * (1 + VerticalSpace))) * (1 - (2 * VerticalSpace))));
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshDiagram()
        {
            if (Data != null && Data.Length > 0)
            {
                CalcMinMaxVal();
                Image = new Bitmap((int)(pictureBox.Width * 0.95), pictureBox.Height);
                Graphics g = Graphics.FromImage(Image);
                CalcBounds(pictureBox.Width, pictureBox.Height);
                if (string.IsNullOrEmpty(Title) == false)
                    DrawTitle(ref g);

                DrawAxis(ref g);
                DrawData(ref g);
            }
            else
            {
                Image = null;
            }
            pictureBox.Image = Image;
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Graph()
        {
            InitializeComponent();
            LineColor = Color.Red;
            AxisColor = Color.Black;
            HoriozontalSpace = 0.05;
            VerticalSpace = 0.02;
            TitleSize = 13;
        }

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="data"></param>
        public Graph(int[] data)
            : this()
        {
            Data = data;
        }

        #endregion
    }
}
