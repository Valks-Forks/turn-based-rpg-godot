using Godot;
using System.Reflection;

public partial class DataEditor : Node
{
	private Data _data;
	private PackedScene _dataListItem = ResourceLoader.Load<PackedScene>("debug/data_editor/DataListItem.tscn");
	private PackedScene _dataEditorInput = ResourceLoader.Load<PackedScene>("debug/data_editor/DataEditorInput.tscn");

	public override void _Ready()
	{
		loadLoadList();
		GetNode<Button>("UI/SaveButton").Pressed += onSaveButtonPressed;
	}

	private void loadLoadList()
	{
		Node container = GetNode("UI/DataList/GridContainer");

		foreach (string file in DirAccess.Open("data").GetFiles())
		{
			DataListItem dataListItem = _dataListItem.Instantiate<DataListItem>();
			dataListItem.Data = ResourceLoader.Load<Data>($"data/{file}");
			dataListItem.Selected += loadData;
			container.AddChild(dataListItem);
		}
	}

	private void loadData(Data data)
	{
		_data = data;
		Node dataContainer = GetNode("UI/Data/GridContainer");

		foreach (Node child in dataContainer.GetChildren())
		{
			dataContainer.RemoveChild(child);
		}

		foreach (PropertyInfo propertyInfo in _data.GetType().GetProperties())
		{
			DataEditorInput dataEditorInput = _dataEditorInput.Instantiate<DataEditorInput>();
			dataEditorInput.Data = _data;
			dataEditorInput.DataProperty = propertyInfo.Name;
			dataContainer.AddChild(dataEditorInput);
		}
	}

	private void onSaveButtonPressed()
	{
		ResourceSaver.Save(_data, _data.ResourcePath);
        GetTree().ReloadCurrentScene();
	}
}
