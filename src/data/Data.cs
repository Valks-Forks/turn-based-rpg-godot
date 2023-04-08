using Godot;

public partial class Data : Resource
{
    public string FilePath
    {
        get { return ResourcePath; }
        set { ResourcePath = value; }
    }

    public string Name
    {
        get { return ResourceName; }
        set { ResourceName = value; }
    }
}
