using Godot;

public partial class PlayerMove : State
{
    Player player;

    public override void _Ready()
    {
        player = GetParent().GetParent<Player>();
    }

    public override void OnProcess(double delta)
    {
        Vector2 inputVelocity = Vector2.Zero;
        inputVelocity.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        inputVelocity.Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        player.GetNode<Node2D>("sprite").LookAt(player.GetGlobalMousePosition());
        player.Velocity = inputVelocity * (Input.IsActionPressed("move_run") ? player.runSpeed : player.moveSpeed);
        player.MoveAndSlide();
    }
}
