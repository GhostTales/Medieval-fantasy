using BehaviorTree;
using Godot;
using System;

public partial class TaskAttackDist : BtNode
{
	// Called when the node enters the scene tree for the first time.
	int range;
	Node2D transform;
	Node2D player;
	public TaskAttackDist(Node2D transform, Node2D player, int range)
	{
		this.player = player;
		this.transform = transform;
		this.range = range;

	}

	public override NodeState Evaluate()
	{
		if (transform.Position.DistanceTo(player.Position) < range)
			return NodeState.SUCCESS;

		return NodeState.FAILURE;
	}
}
