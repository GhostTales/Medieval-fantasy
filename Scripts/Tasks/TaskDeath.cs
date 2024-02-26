using BehaviorTree;
using Godot;
using System;

public partial class TaskDeath : BtNode
{
    // Called when the node enters the scene tree for the first time.
    Node2D Enemy;
    public TaskDeath(Node2D Enemy)
    {
        this.Enemy = Enemy;
    }

    public override NodeState Evaluate()
    {
        //GD.Print($"{Enemy} has been freed");
        Enemy.GetParent().Free();

        return NodeState.RUNNING;

    }
}
