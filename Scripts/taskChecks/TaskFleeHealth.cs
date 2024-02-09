using BehaviorTree;
using Godot;
using System;

public partial class TaskFleeHealth : BtNode
{
	// Called when the node enters the scene tree for the first time.
	int health;
	int max_health;
	public TaskFleeHealth(int health, int max_health)
	{
		this.health = health;
		this.max_health = max_health;
	}

	public override NodeState Evaluate()
	{
		if (health < max_health / 2)
			return NodeState.SUCCESS;

		return NodeState.FAILURE;
	}
}
