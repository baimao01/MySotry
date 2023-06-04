using Godot;
using Godot.Collections;

public partial class ItemSlot : TextureRect
{
    protected PackedScene basicItem = GD.Load<PackedScene>("res://src/gui/inventory/base/Item.tscn");
    protected ItemContainer inventoryUI;

    [Export]
    protected Texture2D focusTexture;
    protected Texture2D noFocusTexture;

    public override void _Ready() => Init();

    public virtual void Init()
    {
        inventoryUI = GetParent().GetParent<ItemContainer>();
        noFocusTexture = Texture;
    }

    public virtual void UpdateSlot(Dictionary itemData)
    {
        for (int i = 0; i < GetChildCount(); i++)
        {
            GetChild(i).QueueFree();
        }
        if (itemData != null && itemData.Count != 0)
        {
            Item itemNode = basicItem.Instantiate<Item>();
            itemNode.Texture = GD.Load<Texture2D>((string)itemData["texture"]);
            if ((int)itemData["num"] > 1)
            {
                itemNode.GetChild<Label>(0).Text = ((int)itemData["num"]).ToString();
            }
            else
            {
                itemNode.GetChild<Label>(0).Text = "";
            }
            AddChild(itemNode);
        }
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        //return true;
        return data.AsGodotDictionary() is Godot.Collections.Dictionary;
    }

    public override void _DropData(Vector2 position, Variant data)
    {
        if ((object)data != null)
        {
            int index = GetIndex();
            inventoryUI.AddItem(index, (Dictionary)data);
            Global.isPickItem = false;
        }
    }

    private void _on_mouse_entered()
    {
        Texture = focusTexture;
    }

    private void _on_mouse_exited()
    {
        Texture = noFocusTexture;
    }
}
