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
    /// Simple control that can display 2d graph
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
        /// <summary>
        /// Gets or sets data line color
        /// </summary>
        public Color LineColor { get; set; }
        /// <summary>
        /// Gets or sets axis color
        /// </summary>
        public Color AxisColor { get; set; }

        /// <summary>
        /// How much horizontal space should be left blank on left side of the graph. Value in percent 0.03 = 3%
        /// </summary>
        public double HoriozontalSpace { get; set; }
        /// <summary>
        /// How much veritcal space should be left blank on left side of the graph. Value in percent 0.03 = 3%
        /// </summary>
        public double VerticalSpace { get; set; }
        /// <summary>
        /// Size of the tile box in pixels
        /// </summary>
        public double TitleSize { get; set; }
        /// <summary>
        /// Gets or sets graph image
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Gets or sets Title 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets data
        /// </summary>
        public int[] Data
        {
            set
            {
                data = value;
                RefreshDiagram();
            }
        }
        #endregion

        #region Methods
        private void CalcMinMaxVal()
        {
            for (int i = 0; i < data.Length; i++)
            {
                minVal = Math.Min(minVal, data[i]);
                maxVal = Math.Max(maxVal, data[i]);
            }
        }
        /// <summary>
        /// Draws data on the graphics object
        /// </summary>
        /// <param name="g"></param>
        private void DrawData(ref Graphics g)
        {
            double tickY = ((graphBounds.Bottom - graphBounds.Top) / (maxVal - (double)minVal)) * 0.9;
            double tickX = (graphBounds.Right - graphBounds.Left) / data.Length;
            SolidBrush brush = new SolidBrush(LineColor);
            Pen p = new Pen(brush);
            Pen markPen = new Pen(new SolidBrush(AxisColor));
            Pen fuel100PercentPen = new Pen(new SolidBrush(System.Drawing.Color.Gray));
            for (int i = 1; i < data.Length; i++)
            {
                g.DrawLine(
                    p,
                    graphBounds.Left + 3 + (int)(tickX * (i - 1)),
                    graphBounds.Bottom - 2 - (int)(tickY * data[i - 1]),
                    graphBounds.Left + 3 + (int)(tickX * i),
                    graphBounds.Bottom - 2 - (int)(tickY * data[i]));

                // Draw a dashed line at 100% fuel
                g.DrawLine(
                    fuel100PercentPen,
                    graphBounds.Left + 3 + (int)(tickX * (i - 1)),
                    graphBounds.Bottom - 2 - (int)(tickY * 100),
                    graphBounds.Left + 3 + (int)(tickX * (i - 1) + tickX / 2),
                    graphBounds.Bottom - 2 - (int)(tickY * 100));

                // Tick marks on bottom (warp speed)
                g.DrawLine(markPen, (int)(tickX * i), graphBounds.Bottom, (int)(tickX * i), graphBounds.Bottom - 4);
            }




            markPen.Dispose();
            p.Dispose();
            brush.Dispose();
        }
        /// <summary>
        /// Draw axis on graphic object
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
        /// Draw title
        /// </summary>
        /// <param name="g"></param>
        private void DrawTitle(ref Graphics g)
        {
            int width = Image.Width;
            int height = Image.Height;

            Rectangle titleBounds = new Rectangle(
                    (int)(width * HoriozontalSpace),
                    (int)(height * VerticalSpace),
                    (int)(width * (1 - (2 * HoriozontalSpace))),
                    (int)(TitleSize * (1 + VerticalSpace)));
            
            g.DrawString(Title, Control.DefaultFont, Brushes.Black, titleBounds);
        }
        /// <summary>
        /// Calculate graph bounds for given image size
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        private void CalcBounds(int imageWidth, int imageHeight)
        {
            int posX = (int)(imageWidth * HoriozontalSpace);
            int posY = (int)(imageHeight * VerticalSpace);
            double leftOverHorizontal = 1.0 - (2.0 * HoriozontalSpace);
            double leftOverVertical = 1.0 - (2.0 * VerticalSpace);
            if (string.IsNullOrEmpty(Title) == true)
            {
                graphBounds = new Rectangle(
                    posX,
                    posY,
                    (int)(imageWidth * leftOverHorizontal),
                    (int)(imageHeight * leftOverVertical));
            }
            else
            {
                // reserve space at the top for title
                graphBounds = new Rectangle(
                    posX,
                    (int)(posY + (TitleSize * (1.0 + VerticalSpace))),
                    (int)(imageWidth * leftOverHorizontal),
                    (int)(imageHeight - (TitleSize * (1.0 + VerticalSpace) * leftOverVertical)));
            }
        }
        /// <summary>
        /// Refreshes the diagram
        /// </summary>
        public void RefreshDiagram()
        {
            if (data != null && data.Length > 0)
            {
                CalcMinMaxVal();
                Image = new Bitmap((int)(pictureBox.Width * 0.95), pictureBox.Height);
                Graphics g = Graphics.FromImage(Image);
                CalcBounds(Image.Width, Image.Height);
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
        public Graph()
        {
            InitializeComponent();
            LineColor = Color.Red;
            AxisColor = Color.Black;
            HoriozontalSpace = 0.05;
            VerticalSpace = 0.02;
            TitleSize = 13;
        }
        public Graph(int[] data)
            : this()
        {
            Data = data;
        }
        #endregion
    }
}
