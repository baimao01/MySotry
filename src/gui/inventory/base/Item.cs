using Godot;
using Godot.Collections;

public partial class Item : TextureRect
{
    public override Variant _GetDragData(Vector2 position)
    {
        int itemIndex = GetParent().GetIndex();
        Dictionary itemData = GetParent().GetParent().GetParent<ItemContainer>().RemoveItem(itemIndex);
        Item dragPreview = GD.Load<PackedScene>("res://src/gui/inventory/base/Item.tscn").Instantiate<Item>();
        dragPreview.Texture = Texture;
        dragPreview.GetChild<Label>(0).Text = GetChild<Label>(0).Text;
        SetDragPreview(dragPreview);
        Global.isPickItem = true;
        return itemData;
    }
}
