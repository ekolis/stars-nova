namespace Nova.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public class ComboBoxItem<T> 
    {
        public T Tag { get; set; }
        public string DisplayName { get; set; }

        public ComboBoxItem(string display, T tag)
        {
            DisplayName = display;
            Tag = tag;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
