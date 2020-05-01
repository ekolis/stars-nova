#region Copyright Notice
// ============================================================================
// Copyright (C) 2009-2012 The Stars-Nova Project
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

namespace Nova.Common
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO; 

    /// <summary>
    /// A singleton containing all the useable race icons, which is used to load and
    /// store the game wide list of race icon images, as a collection of RaceIcons.
    /// </summary>
    public sealed class AllRaceIcons
    {
        private static readonly object Padlock = new object();
        private static AllRaceIcons instance;
        public List<RaceIcon> IconList = new List<RaceIcon>();

        /// <summary>
        /// Private constructor to prevent anyone else creating instances of this class.
        /// </summary>
        private AllRaceIcons()
        {
        }

        /// <summary>
        /// Provide a mechanism of accessing the single instance of this class that we
        /// will create locally. Creation of the data is thread-safe.
        /// </summary>
        public static AllRaceIcons Data
        {
            get
            {
                if (instance == null)
                {
                    lock (Padlock)
                    {
                        if (instance == null)
                        {
                            instance = new AllRaceIcons();
                        }
                    }
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Load the race images.
        /// </summary>
        /// <returns>Returns true if the race icons were successfuly loaded.</returns>
        public static bool Restore()
        {
            if (Data.IconList.Count < 1)
            {
                try
                {
                    using(Config conf = new Config())
                    {
                        var graphicFolder = FileSearcher.GetGraphicsPath();



                        // load the icons
                        DirectoryInfo info = new DirectoryInfo(Path.Combine(graphicFolder, "Race"));
                        foreach (FileInfo fi in info.GetFiles())
                        {
                            Bitmap i = new Bitmap(Path.Combine(fi.DirectoryName, fi.Name));
                            RaceIcon icon = new RaceIcon(fi.Name, i);
                            Data.IconList.Add(icon);
                        }
                    }
                }
                catch
                {
                    Report.Error("RaceIcon: Restore() - Failed to load race icons.");
                    return false;
                }
            }
            return true;
        }
    }
}
