using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.WinForms.Gui.Controls
{
    public partial class CargoMeter : Control
    {
        private int level = 0;
        private int maximum = 100;
        private Cargo cargoLevels = new Cargo();

        public enum CargoType
        {
            Fuel,
            Ironium,
            Boranium,
            Germanium,
            Colonists,
            Multi
        }

        public delegate void ValueChangedHandler(int newValue);

        public event ValueChangedHandler ValueChanged;

        public CargoMeter()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();    
        }

        private bool IsMulti
        {
            get { return Cargo == CargoType.Multi; }
        }

        [DefaultValue(false)]
        public bool UserCanChangeValue { get; set; }


        [DefaultValue(100)]
        public int Maximum
        {
            get { return maximum; }
            set 
            {  
                maximum = value;
                Value = Value;
            }
        }

        // for ... of Max KT/mg
        
        [DefaultValue(0)]
        public int Value
        {
            get
            {
                if (IsMulti)
                    return Math.Min( Maximum, CargoLevels.Mass);
                return level;
            }
            set
            {
                level = Math.Min(Maximum, value);
                Invalidate();
            }
        } // Used for total value and text 
        
        public CargoType Cargo { get; set; }
        
        public Cargo CargoLevels
        {
            get { return cargoLevels; }
            set { cargoLevels = value; Invalidate();}
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Rectangle area = ClientRectangle;
            Graphics g = pe.Graphics;

            ControlPaint.DrawBorder3D(g, area);

            // Draw border
            //g.DrawRectangle(Pens.Black, area.X, area.Y, area.Width - 1, area.Height - 1);

            // Prepare for internal area fill
            area.X += 1;
            area.Y += 1;
            area.Height -= 3;
            area.Width -= 3;

            
            if (Cargo == CargoType.Multi)
            {
                PaintMultiBar(g, area); 
            }
            else
            {
                PaintSingleBar(g, area);
            }

            string text = String.Format("{0} of {1}{2}", Value, Maximum, Cargo == CargoType.Fuel ? "mg" : "KT");
            Size s = TextRenderer.MeasureText(g, text, Font);
            int textX = area.Width/2 - s.Width/2;
            int textY = area.Height/2 - s.Height/2;
            TextRenderer.DrawText(g, text, Font, new Point(textX,textY), ForeColor );
        }

        private void PaintMultiBar(Graphics g, Rectangle area)
        {            
            if( CargoLevels == null )
                CargoLevels = new Cargo();
            int x = area.X;
            x += FillBar(area, g, x, CargoLevels.Ironium, GetCargoBrush(CargoType.Ironium));
            x += FillBar(area, g, x, CargoLevels.Boranium, GetCargoBrush(CargoType.Boranium));
            x += FillBar(area, g, x, CargoLevels.Germanium, GetCargoBrush(CargoType.Germanium));
            FillBar(area, g, x, CargoLevels.ColonistsInKilotons, GetCargoBrush(CargoType.Colonists));
        }

        private int FillBar(Rectangle area, Graphics g, int x, int val, Brush brush)
        {
            int fillWidth = CalcBarWidth(area, val);
            if( fillWidth > 0 )
                g.FillRectangle(brush, x, area.Y, fillWidth, area.Height);
            return fillWidth;
        }


        private void PaintSingleBar(Graphics g, Rectangle area)
        {
            int fillWidth = CalcBarWidth(area, Value);

            Color c = GetCargoColor(Cargo);
            if (fillWidth > 0)
            {
                using (
                    LinearGradientBrush brush = new LinearGradientBrush(area.Location, new Point(area.X + fillWidth, area.Y),
                                                                        Color.FromArgb(64,c.R,c.G,c.B), c))
                {                    
                    g.FillRectangle(brush, area.X, area.Y, fillWidth, area.Height);
                }
            }
        }

        private int CalcBarWidth(Rectangle area, int val)
        {
            double maxDouble = Maximum;
            return Maximum == 0 ? 0 : (int)Math.Round((area.Width / maxDouble) * val);
        }

        private Color GetCargoColor(CargoType cargoType)
        {
            switch (cargoType)
            {
                case CargoType.Fuel:
                    return Color.Red;
                case CargoType.Ironium:
                    return Color.Blue;
                case CargoType.Boranium:
                    return Color.DarkGreen;
                case CargoType.Germanium:
                    return Color.Yellow;
                case CargoType.Colonists:
                    return Color.White;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Brush GetCargoBrush(CargoType cargoType)
        {
            switch (cargoType)
            {
                case CargoType.Fuel:
                    return Brushes.Red;
                case CargoType.Ironium:
                    return Brushes.Blue;
                case CargoType.Boranium:
                    return Brushes.DarkGreen;
                case CargoType.Germanium:
                    return Brushes.Yellow;
                case CargoType.Colonists:
                    return Brushes.White;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FireValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(Value);
        }

        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (UserCanChangeValue)
            {
                PositionSlide(e.X);
                Capture = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Capture && UserCanChangeValue)
            {
                PositionSlide(e.X);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Capture = false;
        }

        private void PositionSlide( int x )
        {
            double maxDouble = Maximum;
            double pos = x - 1;
            pos = Math.Max(0, Math.Min(Size.Width - 2, pos));
            Value = (int)Math.Round(pos / (Size.Width - 2) * maxDouble);
            FireValueChanged();
            Invalidate();
        }
    }
}
