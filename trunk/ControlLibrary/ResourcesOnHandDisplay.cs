using System;

using Nova.Common;

namespace Nova.ControlLibrary
{
    public class ResourcesOnHandDisplay : ResourceDisplay
    {
        // Used to keep track of the resources not on hand.
        private int researchAllocation;
        
        public ResourcesOnHandDisplay() : base()
        {
            this.researchAllocation = 0;
        }
        
        public int ResearchAllocation
        {
            set
            {
                this.researchAllocation = value;
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
                    
                    this.ironium.Text = resources.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.boranium.Text = resources.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.germanium.Text = resources.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.energy.Text = resources.Energy.ToString(System.Globalization.CultureInfo.InvariantCulture) + " of " 
                        + (resources.Energy + this.researchAllocation).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    Report.Error("Unable to convert resource values.");
                }
            }

            get
            {

                Resources resources = new Resources();

                resources.Ironium = Convert.ToInt32(this.ironium.Text);
                resources.Boranium = Convert.ToInt32(this.boranium.Text);
                resources.Germanium = Convert.ToInt32(this.germanium.Text);
                
                // We remove the total resources from the Display, and return only the available ones.
                // We split the string, for example "35 of 43" by spaces and return only the first
                // token; 35 in this case.
                string [] energy = this.energy.Text.Split(' ');

                resources.Energy = Convert.ToInt32(energy[0]);
                
                return resources;
            }
        }
    }
}

