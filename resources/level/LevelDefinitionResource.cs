using Godot;

namespace Game.Resources.Level;

[GlobalClass]
public partial class LevelDefinitionResource : Resource
{
    [Export]
    public string Id { set; private get; }

    [Export]
    public int StartingResourceCount { get; private set; } = 4;

    [Export(PropertyHint.File, "*.tscn")]
    public string LevelScenePath { get; private set; }
}
