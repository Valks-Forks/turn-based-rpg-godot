using Godot;

public partial class NewGameScreen : Screen
{
    private void onBackButtonPressed()
    {
        UIController.Back();
    }

    private void onSaveButtonPressed()
    {
        GameData gameData = new GameData();
        gameData.ResourceName = GetNode<LineEdit>("Layout/Name").Text;
        ResourceSaver.Save(gameData, $"data/game_data/{gameData.ResourceName}.tres");

        // todo: remove UI.Back(), start game here using newly created game data
        UIController.Back();

        GD.Print($"NewGameScreen: created new game ({gameData.ResourceName})");
    }
}
