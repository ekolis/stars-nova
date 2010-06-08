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
        public static NovaGUI Nova = new NovaGUI();


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load controls with any data we may have for them.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void InitialiseControls()
        {
            Nova.Messages.Year = ClientState.Data.TurnYear;
            Nova.Messages.MessageList = ClientState.Data.Messages;

            Nova.CurrentTurn = ClientState.Data.TurnYear;
            Nova.CurrentRace = ClientState.Data.RaceName;

            Nova.MapControl.Initialise();

            // Select a star owned by the player (if any) as the default display.

            foreach (Star star in ClientState.Data.InputTurn.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    Nova.MapControl.SetCursor(star.Position);
                    Nova.SelectionDetail.Value = star;
                    Nova.SelectionSummary.Value = star;
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
            Nova.Messages.Year = ClientState.Data.TurnYear;
            Nova.Messages.MessageList = ClientState.Data.Messages;

            Nova.Invalidate(true);

            Nova.MapControl.Initialise();
            Nova.MapControl.Invalidate();

            // Select a star owned by the player (if any) as the default display.

            foreach (Star star in ClientState.Data.InputTurn.AllStars.Values)
            {
                if (star.Owner == ClientState.Data.RaceName)
                {
                    Nova.MapControl.SetCursor(star.Position);
                    Nova.SelectionDetail.Value = star;
                    Nova.SelectionSummary.Value = star;
                    break;
                }
            }
        }
    }
}