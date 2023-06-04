using Godot;
using Godot.Collections;

public partial class ItemContainer : Control
{
    [Export]
    protected Array<Dictionary> items = new Array<Dictionary>();

    public Array<Dictionary> Items
    {
        get
        {
            return items;
        }
    }

    [Export]
    protected int gridColumns;

    [Export]
    public Array<string> initItem = new Array<string>();

    [Export]
    public Array<int> initItemNum = new Array<int>();

    public Dictionary itemDatas
    {
        get
        {
            return Global.GetJson("res://assets/ItemData.json").AsGodotDictionary();
        }
    }

    protected GridContainer itemSlots;
    protected PackedScene itemSlotScene = ResourceLoader.Load<PackedScene>("res://src/gui/inventory/base/ItemSlot.tscn");

    public override void _Ready() => BasicInit();

    private void BasicInit()
    {
        itemSlots = GetNode<GridContainer>("GridContainer");
        itemSlots.Columns = gridColumns;
        InitSlot();
        InitItem();
        UpdateItem();
        Init();
    }

    public virtual void Init() { }

    public virtual void InitSlot()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ItemSlot itemSlot = itemSlotScene.Instantiate<ItemSlot>();
            itemSlots.AddChild(itemSlot);
        }
    }

    public virtual void InitItem()
    {
        for (int i = 0; i < initItem.Count; ++i)
        {
            Dictionary itemData = (itemDatas[initItem[i]].AsGodotDictionary()).Duplicate();
            itemData.Add("num", initItemNum[i]);
            itemData.Add("id", initItem[i]);
            Dictionary item = new Dictionary();
            item.Add("item", itemData);
            item.Add("index", i);
            NewItem(item);
        }
    }

    public virtual void AddItem(int itemIndex, Dictionary itemData)
    {
        if (itemData.ContainsKey("item"))
        {
            Dictionary item = (itemData["item"].AsGodotDictionary()).Duplicate();
            Dictionary _item = (items[itemIndex]).Duplicate();
            item.Remove("num");
            _item.Remove("num");
            if (_item.ToString() != item.ToString())
            {
                // 添加物品
                if (items[itemIndex].Count == 0)
                {
                    item = itemData["item"].AsGodotDictionary();
                    if (!item.ContainsKey("num"))
                    {
                        item.Add("num", 0);
                    }
                    items[itemIndex] = item;
                }
                // 交换物品
                else
                {
                    item = itemData["item"].AsGodotDictionary();
                    SwapItem(itemIndex, itemData);
                }
            }
            // 增加物品
            else
            {
                item = itemData["item"].AsGodotDictionary();
                int itemNum = (int)item["num"];
                int targetItemNum = (int)items[itemIndex]["num"];
                // 物品不可以堆叠并且最大堆叠数量 < 当前物品数量 + 目标数量
                if ((item.ContainsKey("isStacking") && !(bool)item["isStacking"])
                 || (item.ContainsKey("maxNum") && (int)item["maxNum"] < itemNum + targetItemNum))
                {
                    SwapItem(itemIndex, itemData);
                }
                // 物品可以堆叠
                else if ((item.ContainsKey("isStacking") && (bool)item["isStacking"])
                     || (item.ContainsKey("maxNum") && (!item.ContainsKey("isStacking"))))
                {
                    // 物品最大堆叠数量 >= 当前物品数量 + 目标数量
                    if ((int)item["maxNum"] >= itemNum + targetItemNum)
                    {
                        item["num"] = itemNum + targetItemNum;
                        items[itemIndex] = item;
                    }
                }
            }

        }
        UpdateItem();
    }

    public virtual void SwapItem(int itemIndex, Dictionary itemData)
    {
        Dictionary item = items[itemIndex];
        // 将目标位置的物品交换到旧容器内
        (itemData["oldItemContainer"].As<ItemContainer>()).items[(int)itemData["index"]] = item;
        (itemData["oldItemContainer"].As<ItemContainer>()).UpdateItem();
        // 将目标位置的物品设为当前物品
        items[itemIndex] = itemData["item"].AsGodotDictionary();
    }

    public virtual void IncreaseItem(int itemIndex, int num)
    {
        items[itemIndex]["num"] = (int)items[itemIndex]["num"] + num;
        UpdateItem();
    }

    public virtual void NewItem(Dictionary itemData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Count == 0)
            {
                AddItem(i, itemData);
                return;
            }
        }
    }

    public virtual void UpdateItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ItemSlot slot = itemSlots.GetChild<ItemSlot>(i);
            if (items[i].ContainsKey("num") && (int)items[i]["num"] == 0)
            {
                RemoveItem(i);
            }
            Dictionary itemData = items[i];
            slot.UpdateSlot(itemData);
        }
    }

    public virtual Dictionary RemoveItem(int itemIndex)
    {
        Dictionary itemData = new Dictionary
        {
            { "item", (items[itemIndex]).Duplicate() },
            { "index", itemIndex },
            { "oldItemContainer", this }
        };
        items[itemIndex] = null;
        UpdateItem();
        return itemData;
    }

    ///<summary>
    ///查找物品 : old
    ///</summary>
    ///<param name="itemID"/>: "ID" : "MyStory:TestItem"
    ///仅对当前数据与目标数据比对，如果有附加数据则直接忽略交给高级FindItem
    public virtual int FindItem(string itemID)
    {
        Dictionary targetItemData = (itemDatas[itemID].AsGodotDictionary()).Duplicate();
        targetItemData.Add("id", itemID);
        int isFind = -1;
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i].ContainsKey("id"))
            {
                if (targetItemData["id"].AsString() == items[i]["id"].AsString())
                {
                    isFind = i;
                }
            }
        }
        return isFind;
    }

    ///<summary>
    ///查找具体物品
    ///<paramref name="itemData"/>: 物品的字典数据(字典中的所有数据为基本数据，如果缺少需要的数据则跳过)
    ///</summary>
    public virtual void FindItem(Dictionary itemData)
    {
    }

    public virtual void DiscardItem(Dictionary itemData)
    {
    }
}
