#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;

    /// <Summary>
    /// The Nova GUI is the form used to play a turn of nova. In a multiplayer/network
    /// game it is the main client side program. The Nova GUI reads in a .intel
    /// file to determine what the player race knows about the universe and when a 
    /// turn is submitted, generates a .orders file for processing of the next game
    /// year. A history is maintained by the ConsoleState object as a .state file.
    ///
    /// This module holds the program entry Point and handles all things related to
    /// the main GUI window.
    /// </Summary>
    public partial class NovaGUI : Form
    {
        public int CurrentTurn;      // control turnvar used for to decide to load new turn... (Thread)
        public string CurrentRace;   // control var used for to decide to load new turn... (Thread)
        protected ClientData clientState;

        /// <Summary>
        /// Construct the main window.
        /// </Summary>
        public NovaGUI(string[] argArray)
        { 
            clientState = new ClientData();
            clientState.Initialize(argArray);
            
            InitializeComponent();
            
            InitialiseControls();
            
             // These used to be in the designer.cs file, but visual studio designer throws a whappy so they are here
            // for now so it works again
            SelectionDetail.FleetDetail.DetailSelectionChangedEvent += DetailChangeSelection;
            SelectionDetail.FleetDetail.RefreshStarMapEvent += MapControl.RefreshStarMap;
            SelectionDetail.FleetDetail.SummarySelectionChangedEvent += SummaryChangeSelection;
            SelectionDetail.PlanetDetail.CursorChangedEvent += MapControl.ChangeCursor;
            SelectionDetail.PlanetDetail.DetailSelectionChangedEvent += DetailChangeSelection;
            SelectionDetail.PlanetDetail.SummarySelectionChangedEvent += SummaryChangeSelection;
            

            MapControl.RequestSelectionEvent += SelectionDetail.ReportItem;
            MapControl.SummarySelectionChangedEvent += SelectionSummary.SummaryChangeSelection;
            MapControl.DetailSelectionChangedEvent += SelectionDetail.DetailChangeSelection;
            MapControl.WaypointChangedEvent += SelectionDetail.FleetDetail.WaypointListChanged;
        }

        public SelectionDetail SelectionDetail
        {
            get { return selectionDetail; }
        }

        public StarMap MapControl
        {
            get { return mapControl; }
        }

        public SelectionSummary SelectionSummary
        {
            get { return selectionSummary; }
        }

        public Messages Messages
        {
            get { return messages; }
        }

        /// <Summary>
        /// Exit menu Item selected.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuExit_Click(object sender, System.EventArgs e)
        {
            clientState.Save();
            Close();
        }

        /// <Summary>
        /// Pop up the ship design dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuShipDesign(object sender, System.EventArgs e)
        {
            ShipDesignDialog shipDesignDialog = new ShipDesignDialog(clientState);
            shipDesignDialog.ShowDialog();
            shipDesignDialog.Dispose();
        }

        /// <Summary>
        /// Deal with keys being pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '+':
                    e.Handled = true;
                    MapControl.ZoomInClick(null, null);
                    break;

                case '-':
                    e.Handled = true;
                    MapControl.ZoomOutClick(null, null);
                    break;
            }
        }

        /// <Summary>
        /// Display the "About" dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuAbout(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        /// <Summary>
        /// Display the research dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void MenuResearch(object sender, EventArgs e)
        {
            ResearchDialog newResearchDialog = new ResearchDialog(clientState);
            newResearchDialog.ResearchAllocationChangedEvent += new ResearchAllocationChanged(this.UpdateResearchBudgets);
            newResearchDialog.ShowDialog();
            newResearchDialog.Dispose();
        }

        /// <Summary>
        /// Main Window is closing (i.e. the "X" button has been pressed on the frame of
        /// the form. Save the local state data.
        /// </Summary><remarks>
        /// NB: Don't generate the orders file unless Save&Submit is selected.
        /// TODO (priority 7) - ask the user if they want to submit the current turn before closing.
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void NovaGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                clientState.Save();
            }
            catch (Exception ex)
            {
                Report.Error("Unable to save the client state." + Environment.NewLine + ex.Message);                
            }                
            // OrderWriter.WriteOrders(); // don't do this here, do it only on save & submit.
        }

        /// <Summary>
        /// Pop up the player relations dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlayerRelationsMenuItem_Click(object sender, EventArgs e)
        {
            PlayerRelations relationshipDialog = new PlayerRelations(clientState.EmpireState.EmpireReports, clientState.EmpireState.Id);
            relationshipDialog.ShowDialog();
            relationshipDialog.Dispose();
        }

        /// <Summary>
        /// Pop up the battle plans dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void BattlePlansMenuItem(object sender, EventArgs e)
        {
            BattlePlans battlePlans = new BattlePlans(clientState.EmpireState.BattlePlans);
            battlePlans.ShowDialog();
            battlePlans.Dispose();
        }

        /// <Summary>
        /// Pop up the Design Manager Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignManagerMenuItem_Click(object sender, EventArgs e)
        {
            DesignManager designManager = new DesignManager(clientState);
            designManager.RefreshStarMapEvent += new RefreshStarMap(this.MapControl.RefreshStarMap);
            designManager.ShowDialog();
            designManager.Dispose();
        }

        /// <Summary>
        /// Pop up the Planet Report Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void PlanetReportMenu_Click(object sender, EventArgs e)
        {
            PlanetReport planetReport = new PlanetReport(clientState.EmpireState);
            planetReport.ShowDialog();
            planetReport.Dispose();
        }

        /// <Summary>
        /// Pop up the Fleet Report Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void FleetReportMenu_Click(object sender, EventArgs e)
        {
            FleetReport fleetReport = new FleetReport(clientState.EmpireState);
            fleetReport.ShowDialog();
            fleetReport.Dispose();
        }

        /// <Summary>
        /// Pop up the battle Report Dialog
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void BattlesReportMenu_Click(object sender, EventArgs e)
        {
            BattleReportDialog battleReport = new BattleReportDialog(clientState.EmpireState);
            battleReport.ShowDialog();
            battleReport.Dispose();
        }

        /// <Summary>
        /// Pop up the score report dialog.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ScoresMenuItem_Click(object sender, EventArgs e)
        {
            ScoreReport scoreReport = new ScoreReport(clientState.InputTurn.AllScores);
            scoreReport.ShowDialog();
            scoreReport.Dispose();
        }

        /// <Summary>
        /// Menu->Commands->Save & Submit
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void SaveAndSubmitTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientState.Save();
            OrderWriter orderWriter = new OrderWriter(clientState);
            orderWriter.WriteOrders();
            this.Close();
        }

        /// <Summary>
        /// Load Next Turn
        /// </Summary>
        /// <remarks>
        /// This menu Item has been disabled as it does not currently detect if there is
        /// a valid next turn.
        /// TODO (priority 6) - detect when a new turn is available.
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void LoadNextTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prepare the arguments that will tell how to re-initialise.
            CommandArguments commandArguments = new CommandArguments();
            commandArguments.Add(CommandArguments.Option.RaceName, clientState.EmpireState.Race.Name);
            commandArguments.Add(CommandArguments.Option.Turn, clientState.EmpireState.TurnYear + 1);

            clientState.Initialize(commandArguments.ToArray());
            this.NextTurn();
        }
        
        /// <Summary>
        /// Reacts to Fleet selection information. 
        /// </Summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>The source of the event.</param>
        /// <param name="e">A <see cref="FleetSelectionArgs"/> that contains the event data.</param>
        public void DetailChangeSelection(object sender, DetailSelectionArgs e)
        {
            this.SelectionDetail.Value = e.Detail;
        }
        
        /// <Summary>
        /// Reacts to Star selection information. 
        /// </Summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>The source of the event.</param>
        /// <param name="e">A <see cref="FleetSelectionArgs"/> that contains the event data.</param>
        public void SummaryChangeSelection(object sender, SummarySelectionArgs e)
        {
            this.SelectionSummary.Value = e.Summary;
        }
        
        /// <Summary>
        /// Load controls with any data we may have for them.
        /// </Summary>
        public void InitialiseControls()
        {
            this.Messages.Year = clientState.EmpireState.TurnYear;
            this.Messages.MessageList = clientState.Messages;

            this.CurrentTurn = clientState.EmpireState.TurnYear;
            this.CurrentRace = clientState.EmpireState.Race.Name;

            this.MapControl.Initialise(clientState);

            // Select a Star owned by the player (if any) as the default display.

            foreach (StarIntel report in clientState.EmpireState.StarReports.Values)
            {
                if (report.Owner == clientState.EmpireState.Id)
                {
                    MapControl.SetCursor(report.Position);
                    MapControl.CenterMapOnPoint(report.Position);
                    SelectionDetail.Value = clientState.EmpireState.OwnedStars[report.Name];
                    SelectionSummary.Value = report;
                    break;
                }
            }
        }

        /// <Summary>
        /// Refresh the display for a new turn.
        /// </Summary>
        public void NextTurn()
        {
            Messages.Year = clientState.EmpireState.TurnYear;
            Messages.MessageList = clientState.Messages;

            Invalidate(true);

            MapControl.Initialise(clientState);
            MapControl.Invalidate();

            // Select a Star owned by the player (if any) as the default display.

            foreach (StarIntel report in clientState.EmpireState.StarReports.Values)
            {
                if (report.Owner == clientState.EmpireState.Id)
                {
                    MapControl.SetCursor((System.Drawing.Point)report.Position);
                    SelectionDetail.Value = clientState.EmpireState.OwnedStars[report.Name];
                    SelectionSummary.Value = report;
                    break;
                }
            }
        }

        /// <Summary>
        /// Makes the Planet Detail reflect new research budgets. 
        /// </Summary>
        /// <returns>
        /// A <see cref="System.Boolean"/> indicating if the planet Detail
        /// was updated or not.
        /// </returns>
        private bool UpdateResearchBudgets()
        {
            if (this.SelectionDetail.Control == this.SelectionDetail.PlanetDetail)
            {
                // Ugly hack so panel updates right away.
                this.SelectionDetail.Value = this.SelectionDetail.Value;
                return true;
            }
            
            return false;
        }
    }
}
