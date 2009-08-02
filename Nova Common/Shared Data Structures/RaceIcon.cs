using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.IO;


namespace NovaCommon.Shared_Data_Structures
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
            int next = AllRaceIcons.Data.IconList.IndexOf(icon) + 1;
            if (next > AllRaceIcons.Data.IconList.Count) next = 0;
            return (RaceIcon)AllRaceIcons.Data.IconList[next];
        }

        static public RaceIcon operator --(RaceIcon icon)
        {
            int prev = AllRaceIcons.Data.IconList.IndexOf(icon) - 1;
            if (prev < 0) prev = AllRaceIcons.Data.IconList.Count;
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
                // load the icons
                DirectoryInfo info = new DirectoryInfo(Path.Combine(AllComponents.Graphics, "Race"));
                foreach (FileInfo fi in info.GetFiles())
                {
                    //fileList.Add(fi);
                    Bitmap i = new Bitmap(fi.Name);
                    RaceIcon icon = new RaceIcon(fi.Name, i) ;
                    Data.IconList.Add(icon);

                }

            }
            return true;
        }

    }//AllRaceIcons
        
    
    
}
