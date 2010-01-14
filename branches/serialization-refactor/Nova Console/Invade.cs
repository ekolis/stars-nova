// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module is invoked when a planet is to be invaded
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using NovaCommon;

namespace NovaConsole {


// ============================================================================
// Class invade a planet
// ============================================================================

   public static class Invade
   {

      public static void Planet(Fleet fleet)
      {
         // First check that we are actuallly in orbit around a planet.

         if (fleet.InOrbit == null) {
            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
               + "invade but the waypoint is not a planet";
            Intel.Data.Messages.Add(message);
            return;
         }

         // and that we have troops.

         double troops = fleet.Cargo.Colonists;
         Star   star   = fleet.InOrbit;

         if (troops == 0) {
            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
               + "invade " + star.Name + " but there are no troops on board";
            Intel.Data.Messages.Add(message);
            return;
         }

         // Take into account the Defenses

         Defenses.ComputeDefenseCoverage(star);
         troops          *= Defenses.InvasionCoverage;
         double colonists = star.Colonists;

         colonists -= troops;
         if (colonists > 0) {
            Message message = new Message();
            message.Audience = fleet.Owner;
            message.Text = "Fleet " + fleet.Owner + " has killed " 
               + troops.ToString(System.Globalization.CultureInfo.InvariantCulture)  + " colonisists on " + star.Name
               + " but did not manage to capture the planet";
            Intel.Data.Messages.Add(message);
            star.Colonists -= (int) troops;
            return;
         }
         
         Message captureMessage  = new Message();
         captureMessage.Audience = fleet.Owner;
         captureMessage.Text = "Fleet " + fleet.Owner + " has killed " 
            + "all the colonists on " + star.Name
            + " and has captured the planet";
         Intel.Data.Messages.Add(captureMessage);

         star.Owner     = fleet.Owner;
         star.Colonists = (int) (troops - star.Colonists);
      }
 
  }
}


