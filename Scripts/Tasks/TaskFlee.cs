using BehaviorTree;
using Godot;
using playerstats;
using System;

public partial class TaskFlee : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D Player;
	Node2D Enemy;
	float Speed;
	public TaskFlee(Node2D Enemy, Node2D Player, float Speed)
	{
		this.Player = Player;
		this.Enemy = Enemy;
		this.Speed = Speed;
	}

	public override NodeState Evaluate()
	{
		var moveDirection = -(Player.Position - Enemy.Position).Normalized();
		Enemy.Position += moveDirection * Speed * player_stats.delta;

		//Enemy.Velocity = moveDirection * Speed;
		//Enemy.MoveAndSlide();

		return NodeState.RUNNING;

	}
}
