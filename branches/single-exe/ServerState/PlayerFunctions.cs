// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Player-related functions, load turns, etc.

// TODO (priority 3) - Refactor AreEnemies to be a member of Race and remove this object.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

#region Using Statement
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using NovaCommon;
#endregion

namespace NovaServer
{

   public static class Players
   {
      private static ServerState    StateData = ServerState.Data;

      /// <summary>
      /// Determine if wolf wishes to treat lamb as an enemy.
      /// </summary>
      /// <param name="wolf">The name of the race who may attack.</param>
      /// <param name="lamb">The name of the race who may be attacked.</param>
      /// <returns>true if lamb is one of wolf's enemies, otherwise false.</returns>
      public static bool AreEnemies(string wolf, string lamb)
      {
          RaceData wolfData = ServerState.Data.AllRaceData[wolf] as RaceData;
          string lambRelation = wolfData.PlayerRelations[lamb] as string;

          if (lambRelation == "Enemy")
          {
              return true;
          }

          return false;
      }

   }
}
