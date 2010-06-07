using Nova.Client;
using Nova.Common;

namespace Nova.WinForms.Gui
{
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// The main window.
    /// </summary>
    /// ----------------------------------------------------------------------------
    public static class MainWindow
    {
        public static NovaGUI nova = new NovaGUI();


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load controls with any data we may have for them.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void InitialiseControls()
        {
            nova.messages.Year = ClientState.Data.TurnYear;
            nova.messages.MessageList = ClientState.Data.Messages;

            nova.currentTurn = ClientState.Data.TurnYear;
            nova.currentRace = ClientState.Data.RaceName;

            nova.MapControl.Initialise();

            // Select a star owned by the player (if any) as the default display.

            foreach (Star star in ClientState.Data.InputTurn.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    nova.MapControl.SetCursor(star.Position);
                    nova.SelectionDetail.Value = star;
                    nova.SelectionSummary.Value = star;
                    break;
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Refresh the display for a new turn.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void NextTurn()
        {
            nova.messages.Year = ClientState.Data.TurnYear;
            nova.messages.MessageList = ClientState.Data.Messages;

            nova.Invalidate(true);

            nova.MapControl.Initialise();
            nova.MapControl.Invalidate();

            // Select a star owned by the player (if any) as the default display.

            foreach (Star star in ClientState.Data.InputTurn.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    nova.MapControl.SetCursor(star.Position);
                    nova.SelectionDetail.Value = star;
                    nova.SelectionSummary.Value = star;
                    break;
                }
            }
        }
    }
}