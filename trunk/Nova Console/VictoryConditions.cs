// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This dialog will determine the objectives of the game.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NovaConsole
{
   public partial class VictoryConditions : Form
   {
      ConsoleState stateData = ConsoleState.Data;


// ============================================================================
// Construction and initial field population.
// ============================================================================

      public VictoryConditions()
      {
         InitializeComponent();

         PlanetsOwned.Value       = stateData.PlanetsOwned;
         TechLevels.Value         = stateData.TechLevels;
         NumberOfFields.Value     = stateData.NumberOfFields;
         TotalScore.Value         = stateData.TotalScore;
         ProductionCapacity.Value = stateData.ProductionCapacity;
         CapitalShips.Value       = stateData.CapitalShips;
         HighestScore.Value       = stateData.HighestScore;
         TargetsToMeet.Value      = stateData.TargetsToMeet;
         MinimumGameTime.Value    = stateData.MinimumGameTime;
      }




// ============================================================================
// OK button pressed. Read the form fields and save them.
// ============================================================================

      private void OKSelected_Click(object sender, EventArgs e)
      {
         stateData.PlanetsOwned       = PlanetsOwned.Value;
         stateData.TechLevels         = TechLevels.Value;
         stateData.NumberOfFields     = NumberOfFields.Value;
         stateData.TotalScore         = TotalScore.Value;
         stateData.ProductionCapacity = ProductionCapacity.Value;
         stateData.CapitalShips       = CapitalShips.Value;
         stateData.HighestScore       = HighestScore.Value;
         stateData.TargetsToMeet      = Int32.Parse(TargetsToMeet.Text);
         stateData.MinimumGameTime    = Int32.Parse(MinimumGameTime.Text);
      }

   }

}
