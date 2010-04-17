#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// A class with static methods for handling research.
// ===========================================================================
#endregion

using System;
using System.Collections;

using NovaCommon;

namespace NovaClient
{
 
   public class Research
   {
       private static ClientState StateData = ClientState.Data;

       /// ----------------------------------------------------------------------------
       /// <summary>
       /// Return the total energy cost for researching a level (taking into account
       /// the cost factor specified in the race designer). Note that we skip the first
       /// few turns of the Fibonacci series as they are too close together.
       /// </summary>
       /// <param name="level">The level to be researched.</param>
       /// <returns>The energy cost to reach that level.</returns>
       /// ----------------------------------------------------------------------------
       public static int Cost(int level)
       {
           int techAjustment = 0;

           foreach (int levelAttained in StateData.ResearchLevel)
           {
               techAjustment += levelAttained * 10;
           }

           // The research cost is based on a Fionacci series (starting) at 5
           // multimplied by 10 then 10 points per tech-level reached in all
           // fields is added. Finally, the cost factor specified in the Race
           // Designer is then added.
           // ??? (priority 3) is this the Stars! costs, or some approximation?

           int baseCost = (Fibonacci(level + 5) * 10) + techAjustment;
           int costFactor = (int)StateData.PlayerRace.ResearchCosts[StateData.ResearchTopic];

           return (baseCost * costFactor) / 100;
       }


       /// ----------------------------------------------------------------------------
       /// <summary>
       /// The resources required for each level are based on a Fibonacci series (the
       /// result of which is multiplied by a factor (TBD) to get the actual number
       /// required).
       /// </summary>
       /// <param name="n">The Nth term of the series.</param>
       /// <returns>The value of the Nth term.</returns>
       /// ----------------------------------------------------------------------------
       private static int Fibonacci(int n)
       {
           if (n < 2) return n;
           return Fibonacci(n - 1) + Fibonacci(n - 2);
       }

   }
}
