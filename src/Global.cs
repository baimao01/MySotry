using Godot;

public partial class Global : Node
{
    public static bool isPickItem = false;
    private Inventory inventoryNode;
    ///<summary>
    ///第一次可以set,set之后只能get
    ///</summary>
    public static Inventory InventoryNode
    {
        get
        {
            return myGlobal.inventoryNode;
        }
        set
        {
            if (myGlobal.inventoryNode == null)
            {
                myGlobal.inventoryNode = value;
            }
            else
            {
                GD.Print("Has InventoryNode");
            }
        }
    }
    
    private MakeSystem makeSystemNode;
    ///<summary>
    ///第一次可以set,set之后只能get
    ///</summary>
    public static MakeSystem MakeSystemNode
    {
        get
        {
            return myGlobal.makeSystemNode;
        }
        set
        {
            if (myGlobal.makeSystemNode == null)
            {
                myGlobal.makeSystemNode = value;
            }
            else
            {
                GD.Print("Has MakeSystemNode");
            }
        }
    }

    private static Global myGlobal;

    public override void _Ready()
    {
        myGlobal = this;
    }

    public static Variant GetJson(string filePath)
    {
        var jsonString = FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
        Variant jsonData = Json.ParseString(jsonString);
        return jsonData;
    }
}
