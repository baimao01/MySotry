using Godot;
using Godot.Collections;

public partial class StateMachine : Node
{
    [Export]
    public Dictionary<string, State> states = new Dictionary<string, State>();

    State currentState;

    public override void _Ready()
    {
        GetStates();
        if (GetChild(0) is State)
        {
            ChangeState(GetChild<State>(0));
        }
    }
    
    public override void _Process(double delta)
    {
        currentState.OnProcess(delta);
    }

    public void GetStates()
    {
        for (int i = 0; i < GetChildCount(); ++i)
        {
            if (GetChild(i) is State)
                states[GetChild(i).Name] = GetChild<State>(i);
        }
    }

    public void ChangeState(State targetState)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = targetState;
        currentState.OnEnter();
    }
}
