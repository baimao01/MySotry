using Godot;

public partial class WindowManager : Panel
{
    public override void _GuiInput(InputEvent @event)
    {
        if (!Global.isPickItem && Input.IsMouseButtonPressed(MouseButton.Left))
        {
            GetParent<Panel>().GlobalPosition = GetGlobalMousePosition();
        }
    }
}
