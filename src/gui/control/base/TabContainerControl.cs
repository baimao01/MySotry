// NOTE: This code is not complete (这个代码烂尾了)
using Godot;

public partial class TabContainerControl : Control
{
    public override void _Ready() => BasicInit();

    protected TabBar tabBar;
    public TabBar TabBar
    {
        get
        {
            return tabBar;
        }

        set
        {
            if (tabBar != value)
            {
                EmitSignal("ChangeCurrentTabEventHandler", value);
            }
            tabBar = value;
        }
    }

    [Signal]
    public delegate void ChangeCurrentTabEventHandler(int currentTab);

    [Export]
    protected Control container;

    protected void BasicInit()
    {
        tabBar = GetChild<TabBar>(0);
        Init();
    }

    protected virtual void Init() { }
    
    protected void OnChangeCurrentTab(long currentTab)
    {
    }
}
