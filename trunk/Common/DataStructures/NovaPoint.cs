#region Copyright Notice
// ============================================================================
// Copyright (C) 2010-2012 The Stars-Nova Project
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

namespace Nova.Common.DataStructures
{
    using System;
    using System.Xml;

    /// <summary>
    /// A class to represent a point in space.
    /// Like System.Drawing.Point, with added methods for serialization.
    /// </summary>
    [Serializable]
    public class NovaPoint : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NovaPoint()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Initialising constructor.
        /// </summary>
        /// <param name="x">The new X coordinate.</param>
        /// <param name="y">The new Y coordinate.</param>
        public NovaPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initialising constructor from a System.Drawing.Point.
        /// </summary>
        /// <param name="p">The initial position as a <see cref="System.Drawing.Point"/>.</param>
        public NovaPoint(System.Drawing.Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        /// <summary>
        /// Initialising constructor from a NovaPoint.
        /// </summary>
        /// <param name="p">The initial position as a <see cref="NovaPoint"/>.</param>
        public NovaPoint(NovaPoint p)
        {
            X = p.X;
            Y = p.Y;
        }

        /// <summary>
        /// Create a copy of this NovaPoint.
        /// </summary>
        /// <returns>A copy of this NovaPoint.</returns>
        public object Clone()
        {
            return (object)new NovaPoint(X, Y);
        }

        /// <summary>
        /// Enable implicit casting from a <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <param name="p">A <see cref="System.Drawing.Point"/>.</param>
        /// <returns>A NovaPoint with the same x and y coordinates as the <see cref="System.Drawing.Point"/>.</returns>
        public static implicit operator NovaPoint(System.Drawing.Point p)
        {
            return new NovaPoint(p.X, p.Y);
        }

        /// <summary>
        /// Enable explicit casting of a NovaPoint to a System.Drawing.Point.
        /// </summary>
        /// <param name="p">A NovaPoint to cast.</param>
        /// <returns>A System.Drawing.Point with the same coordinates as p.</returns>
        public static explicit operator System.Drawing.Point(NovaPoint p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        /// <summary>
        /// Implement the Equals function.
        /// </summary>
        /// <param name="obj">An object to test for equality with.</param>
        /// <returns>Returns true if this.X == obj.X and this.Y == obj.Y and obj is a NovaPoint or Point.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is NovaPoint)
            {
                return this.X == ((NovaPoint)obj).X && this.Y == ((NovaPoint)obj).Y;
            }
            else if (obj is System.Drawing.Point)
            {
                return this.X == ((System.Drawing.Point)obj).X && this.Y == ((System.Drawing.Point)obj).Y;
            }
            else
            {
                throw new ArgumentException("Cannot compare NovaPoint objects with objects of type " + obj.GetType().ToString());
            }
        }

        /// <summary>
        /// Implement the == operator for NovaPoint.
        /// </summary>
        /// <param name="a">A NovaPoint to compare.</param>
        /// <param name="b">Another NovaPoint to compare.</param>
        /// <returns>Returns true if the points have the same location (X, Y).</returns>
        public static bool operator ==(NovaPoint a, NovaPoint b)
        {
            if (Object.ReferenceEquals(a, b)) { return true; }
            
            if (((object)a == null) || ((object)b == null)) { return false; }
            
            return a.Equals(b);
        }

        /// <summary>
        /// Implement the != operator for NovaPoint.
        /// </summary>
        /// <param name="a">A NovaPoint to compare.</param>
        /// <param name="b">Another NovaPoint to compare.</param>
        /// <returns>Returns false if the points have the same location (X, Y).</returns>
        public static bool operator !=(NovaPoint a, NovaPoint b)
        {            
            return !(a == b);
        }

        /// <summary>
        /// Return a hash code with a good chance of separating points.
        /// </summary>
        /// <returns>10000X + Y.</returns>
        public override int GetHashCode()
        {
            return (X * 10000) + Y;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }

        /// <summary>
        /// This method adjusts the X and Y values of this Point to the sum of the X and Y values of this Point and p.
        /// </summary>
        /// <param name="p">An offset to be applied to this point.</param>
        public void Offset(NovaPoint p)
        {
            this.X += p.X;
            this.Y += p.Y;
        }

        /// <summary>
        /// This method adjusts the X and Y values of this Point to the sum of the X and Y values of this Point and p.
        /// </summary>
        /// <param name="x">X offset.</param>
        /// <param name="y">Y offset.</param>
        public void Offset(int x, int y)
        {
            this.X += x;
            this.Y += y;
        }

        // returns a unique string for each distinct NovaPoint
        public string ToHashString()
        {
            return X.ToString() + "#" + Y.ToString();
        }

        /// <summary>
        /// Load from XML: Initialising constructor from an XML node.
        /// </summary>
        /// <param name="node">An <see cref="XmlNode"/> within a Nova xml document.</param>
        public NovaPoint(XmlNode node)
        {
            XmlNode mainNode = node.FirstChild;
            while (mainNode != null)
            {
                try
                {
                    switch (mainNode.Name.ToLower())
                    {
                        case "x":
                            {
                                X = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                        case "y":
                            {
                                Y = int.Parse(mainNode.FirstChild.Value, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                mainNode = mainNode.NextSibling;
            }
        }

        /// <summary>
        /// Save: Serialise this NovaPoint to an <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="xmldoc">The parent <see cref="XmlDocument"/>.</param>
        /// <returns>An <see cref="XmlElement"/> representation of the NovaPoint.</returns>
        public XmlElement ToXml(XmlDocument xmldoc, string nodeName = "Point")
        {
            XmlElement xmlelPoint = xmldoc.CreateElement(nodeName);

            Global.SaveData(xmldoc, xmlelPoint, "X", X.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Global.SaveData(xmldoc, xmlelPoint, "Y", Y.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return xmlelPoint;
        }
    }
}
