using System;

using Nova.Common;

namespace Nova.ControlLibrary
{
    /// <summary>
    /// Control used to display extended resource information of a planet.
    /// </summary>
    public class ResourcesOnHandDisplay : ResourceDisplay
    {
        // Used to keep track of the resources not on hand.
        private int resourceRate;
        private int percentage;
        
        public ResourcesOnHandDisplay() : base()
        {
            this.resourceRate = 0;
            this.percentage = 0;
        }
        
        public int ResourceRate
        {
            set
            {
                this.resourceRate = value;
            }
        }
        
        public int ResearchPercentage
        {
            set
            {
                this.percentage = value;
            }
        }
        
        public override Resources Value
        {
            set
            {
                try
                {
                    if (value == null) return;

                    Resources resources = value;
                    
                    // Do not refactor this to "resourceRate*(1-percentage/100)" as they are ints...
                    int currentEnergy = this.resourceRate - (this.resourceRate * percentage / 100);
                    
                    this.ironium.Text = resources.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.boranium.Text = resources.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.germanium.Text = resources.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.energy.Text = currentEnergy.ToString(System.Globalization.CultureInfo.InvariantCulture) + " of " 
                        + (this.resourceRate).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    Report.Error("Unable to convert resource values.");
                }
            }
            
            get
            {
            // There is no get. You should always get a planet's resources on hand
            // directly from the star, not the GUI.
            
                Report.Error("Access to invalid resource data");
                
                return null;                
            }
            
        }
    }
}

