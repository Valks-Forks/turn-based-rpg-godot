using Godot;
using Godot.Collections;

public partial class NewGameScreen : Screen
{
    private TextureRect _characterTexture;
    private int _characterTextureIndex = 0;
    private Array<Texture2D> _characterTextures = new Array<Texture2D>();
    private GameData _gameData = new GameData();

    public override void _Ready()
    {
        base._Ready();

        _characterTexture = GetNode<TextureRect>("Layout/CharacterTextureLayout/CharacterTexture");
        string characterTexturesPath = "res://assets/textures/characters";

        foreach (string characterTextureFile in DirAccess.GetFilesAt(characterTexturesPath)) {
            if (!characterTextureFile.EndsWith(".svg"))
            {
                continue;
            }

            Texture2D characterTexture = GD.Load<Texture2D>($"{characterTexturesPath}/{characterTextureFile}");

            if (_characterTextures.Count == _characterTextureIndex)
            {
                _characterTexture.Texture = characterTexture;
            }

            _characterTextures.Add(characterTexture);
        }
    }

    private void updateCharacterTexture()
    {
        _characterTexture.Texture = _characterTextures[_characterTextureIndex];
        _gameData.CharacterData.Texture = _characterTextures[_characterTextureIndex];
    }

    private void onBackButtonPressed()
    {
        UIController.Back();
    }

    private void onSaveButtonPressed()
    {
        _gameData.ResourceName = GetNode<LineEdit>("Layout/Name").Text;
        ResourceSaver.Save(_gameData, $"data/game_data/{_gameData.ResourceName}.tres");

        // todo: remove UI.Back(), start game here using newly created game data
        UIController.Back();

        GD.Print($"NewGameScreen: created new game ({_gameData.ResourceName})");
    }

    private void onCharacterTextureBackButtonPressed()
    {
        _characterTextureIndex--;

        if (_characterTextureIndex < 0)
        {
            _characterTextureIndex = _characterTextures.Count - 1;
        }

        updateCharacterTexture();
    }

    private void onCharacterTextureNextButtonPressed()
    {
        _characterTextureIndex++;

        if (_characterTextureIndex > _characterTextures.Count - 1)
        {
            _characterTextureIndex = 0;
        }

        updateCharacterTexture();
    }

    private void onColorPickerColorChanged(Color color)
    {
        _gameData.CharacterData.Color = color;
        _characterTexture.Modulate = color;
    }
}
