using Godot;
using Godot.Collections;

public partial class MakeNode : Button
{
    public Dictionary makeData;

    private void makeButtonDown()
    {
        if (Global.InventoryNode != null)
        {
            Array makeDataItems = makeData["items"].AsGodotArray();
            Array<int> itemPos = new Array<int>();
            int isOK = 0;

            // 需要的
            int count = makeDataItems.Count;

            // 需要的物品总数量
            // for (int i = 0; i < makeDataItems.Count; i++)
            // {
            //     count += makeDataItems[i].AsGodotDictionary()["num"].AsInt32();
            // }

            for (int i = 0; i < makeDataItems.Count; i++)
            {
                // 需要的物品总数量
                count += makeDataItems[i].AsGodotDictionary()["num"].AsInt32();
                // 字典(获取 items 数组中的第 i 项)中的 id 的值
                GD.Print((makeData["items"].AsGodotArray()[i]).AsGodotDictionary()["id"].AsString());
                int item = Global.MakeSystemNode.FindItem((makeData["items"].AsGodotArray()[i]).AsGodotDictionary()["id"].AsString());
                itemPos.Add(item);
                // 如果背包中能找到指定物品
                if (item != -1)
                {
                    isOK = item + 1;
                }
            }

            if (isOK < makeData["items"].AsGodotArray().Count)
            {
                GD.Print("缺少材料");
            }
            else if (isOK == makeData["items"].AsGodotArray().Count)
            {
                GD.Print("可以合成");
                // make(makeDataItems, makeData["item"].AsGodotArray());
            }
        }
    }
}
