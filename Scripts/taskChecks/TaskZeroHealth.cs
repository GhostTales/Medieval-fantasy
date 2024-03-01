using BehaviorTree;
using Godot;
using System;

public partial class TaskZeroHealth : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D enemy;
	public TaskZeroHealth(Node2D enemy)
	{
		this.enemy = enemy;
	}

	public override NodeState Evaluate()
	{
		var health = (enemy as enemy)._health;

		if (health > 0)
		{
			//GD.Print($"die {health}");
			return NodeState.SUCCESS;
		}
		return NodeState.FAILURE;

	}
}
