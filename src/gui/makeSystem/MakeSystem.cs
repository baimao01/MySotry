using Godot;
using Godot.Collections;

public partial class MakeSystem : Control
{
    [Export]
    public int gridColumns;

    public Array makeDatas
    {
        get
        {
            return Global.GetJson("res://assets/MakeData.json").AsGodotArray();
        }
    }

    private Panel makeDescribeNode;
    private GridContainer makeNodes;
    private PackedScene makeNodeScene = ResourceLoader.Load<PackedScene>("res://src/gui/makeSystem/MakeNode.tscn");

    public override void _Ready()
    {
        Global.MakeSystemNode = this;
        makeDescribeNode = GetParent().GetNode<Panel>("Describe");
        makeNodes = GetChild<GridContainer>(0);
        makeNodes.Columns = gridColumns;
        InitMakeNode();
    }

    private void InitMakeNode()
    {
        for (int i = 0; i < makeDatas.Count; i++)
        {
            MakeNode makeNode = makeNodeScene.Instantiate<MakeNode>();
            makeNode.makeData = makeDatas[i].AsGodotDictionary();
            makeNode.GetChild<TextureRect>(0).Texture = GD.Load<Texture2D>(makeDatas[i].AsGodotDictionary()["texture"].AsString());
            makeNodes.AddChild(makeNode);
        }
    }

    public int FindItem(string itemID)
    {
        Dictionary targetItemData = (Global.InventoryNode.itemDatas[itemID].AsGodotDictionary()).Duplicate();
        targetItemData.Add("id", itemID);
        int isFind = -1;
        for (int i = 0; i < Global.InventoryNode.Items.Count; ++i)
        {
            if (Global.InventoryNode.Items[i].ContainsKey("id"))
            {
                if (targetItemData["id"].AsString() == Global.InventoryNode.Items[i]["id"].AsString())
                {
                    isFind = i;
                }
            }
        }
        return isFind;
    }

    public void MakeItem(Array needItems, Array item, Array itemPos)
    {
        for (int i = 0; i < needItems.Count; i++)
        {
            Global.InventoryNode.IncreaseItem(itemPos[i].AsInt32(), -(needItems[i].AsGodotDictionary()["num"].AsInt32()));
        }

        for (int i = 0; i < item.Count; i++)
        {
            Global.InventoryNode.NewItem(Global.InventoryNode.itemDatas[needItems[i].AsGodotDictionary()["id"].AsString()].AsGodotDictionary());
        }
    }
}
