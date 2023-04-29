public partial class MainMenuScreen : Screen
{
    private void onLoadGameButtonPressed()
    {
        UIController.SetScreen("LoadGame");
    }

    private void onNewGameButtonPressed()
    {
        UIController.SetScreen("NewGame");
    }
}
