using BehaviorTree;
using Godot;
using System;

public partial class TaskFleeDist : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D player;
	Node2D transform;
	int range;
	public TaskFleeDist(Node2D transform, Node2D player, int range)
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
