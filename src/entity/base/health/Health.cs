using Godot;

public partial class Health : Node
{
    [Signal]
    public delegate void ChangedHealthEventHandler(int value);

    private int _health;

    [Export]
    public int HealthValue
    {
        get { return _health; }
        set
        {
            if (value != _health)
                EmitSignal("ChangedHealthEventHandler", value);
            _health = value;
        }
    }

    [Export]
    public int maxHealth;

    public int hurt(int hurt)
    {
        _health -= hurt;
        return _health;
    }

    public override void _Ready()
    {
        _health = maxHealth;
    }
}
