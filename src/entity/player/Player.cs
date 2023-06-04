using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public int moveSpeed;

    [Export]
    public int runSpeed;

    StateMachine stateMachine;
    Health health;

    public override void _Ready()
    {
        stateMachine = GetNode<StateMachine>("stateMachine");
        health = GetNode<Health>("Hurtbox");
    }
}
