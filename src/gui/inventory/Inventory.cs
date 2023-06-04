using Godot;

public partial class Inventory : ItemContainer
{
    public override void Init()
    {
        Global.InventoryNode = this;
    }
}
