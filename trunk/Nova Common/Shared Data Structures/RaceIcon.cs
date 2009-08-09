// ===========================================================================
// Nova. (c) 2009 Daniel Vale
//
// This module defines the class RaceIcon which manages an icon as a paired
// Bitmap and a String holding the image file's path. The Bitmap is for 
// display purposes and the flie path is for loading/saving.
//
// This module also defines the class AllRaceIcons which is used to load and
// store the game wide list of race icon images, as a collection of RaceIcons.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.IO;


namespace NovaCommon
{
    [Serializable]
    public class RaceIcon 
    {
        public String Source = String.Empty;
        public Bitmap Image = null; 
        
        public RaceIcon(String source, Bitmap image)
        {
            Source = source;
            Image = image;
            
        }

        static public RaceIcon operator ++(RaceIcon icon)
        {
            if (AllRaceIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator++ - Race Icons failed to load.");
                return icon;
            }
            int next = AllRaceIcons.Data.IconList.IndexOf(icon) + 1;
            if (next > AllRaceIcons.Data.IconList.Count - 1) next = 0;
            return (RaceIcon)AllRaceIcons.Data.IconList[next];
        }

        static public RaceIcon operator --(RaceIcon icon)
        {
            if (AllRaceIcons.Data.IconList.Count == 0)
            {
                Report.Error("RaceIcon: operator-- - Race Icons failed to load.");
                return icon;
            } 
            int prev = AllRaceIcons.Data.IconList.IndexOf(icon) - 1;
            if (prev < 0) prev = AllRaceIcons.Data.IconList.Count - 1;
            return (RaceIcon)AllRaceIcons.Data.IconList[prev];
        }

        public RaceIcon Clone()
        {
            RaceIcon clone = new RaceIcon(Source, Image);
            return clone;
        }
    }

    public sealed class AllRaceIcons
    {
        private static AllRaceIcons Instance = null;
        private static Object Padlock = new Object();
        public ArrayList IconList = new ArrayList();

        // ============================================================================
        // Private constructor to prevent anyone else creating instances of this class.
        // ============================================================================

        private AllRaceIcons()
        {
            //Restore();
        }


        // ============================================================================
        // Provide a mechanism of accessing the single instance of this class that we
        // will create locally. Creation of the data is thread-safe.
        // ============================================================================

        public static AllRaceIcons Data
        {
            get
            {
                if (Instance == null)
                {
                    lock (Padlock)
                    {
                        if (Instance == null)
                        {
                            Instance = new AllRaceIcons();
                        }
                    }
                }
                return Instance;
            }

            // ----------------------------------------------------------------------------

            set
            {
                Instance = value;
            }
        }

        // Load the race images
        public static bool Restore()
        {
            

            if (Data.IconList.Count < 1)
            {
                try
                {
                    // load the icons
                    DirectoryInfo info = new DirectoryInfo(Path.Combine(AllComponents.Graphics, "Race"));
                    foreach (FileInfo fi in info.GetFiles())
                    {
                        //fileList.Add(fi);

                        Bitmap i = new Bitmap(Path.Combine(fi.DirectoryName, fi.Name));
                        RaceIcon icon = new RaceIcon(fi.Name, i);
                        Data.IconList.Add(icon);

                    }
                }
                catch
                {
                    Report.Error("RaceIcon: Restore() - Failed to load race icons.");
                }

            }
            return true;
        }

    }//AllRaceIcons
        
    
    
}
