using BehaviorTree;
using Godot;
using System;

public partial class TaskAttack : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D player;
	Node2D transform;
	Timer Timer;
	public TaskAttack(Node2D transform, Node2D player, Timer Timer)
	{
		GD.Print(Timer);
		this.player = player;
		this.transform = transform;
		this.Timer = Timer;
		this.Timer.OneShot = false;
		this.Timer.Timeout += timeout;
		this.Timer.WaitTime = 1;
		this.Timer.Start();
	}

	public void timeout()
	{
		if (transform.Position.DistanceTo(player.Position) <= 60)
		{
			GD.Print($"{transform} attacked {player}");
		}
	}
	public override NodeState Evaluate()
	{


		return NodeState.RUNNING;
	}
}
