using BehaviorTree;
using Godot;
using playerstats;
using System;

public partial class TaskChase : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D player;
	Node2D enemy;
	float speed;
	public TaskChase(Node2D enemy, Node2D player, float speed)
	{
		this.player = player;
		this.enemy = enemy;
		this.speed = speed;
	}

	public override NodeState Evaluate()
	{
		GD.Print(speed);
		var moveDirection = (player.Position - enemy.Position).Normalized();
		enemy.Position += moveDirection * speed * player_stats.delta;

		//enemy.Velocity = moveDirection * speed;
		//enemy.MoveAndSlide();

		return NodeState.RUNNING;
	}
}
