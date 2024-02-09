using BehaviorTree;
using Godot;
using System;

public partial class TaskPatrol : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D[] Waypoints;
	Node2D Enemy;
	int currentWaypoint = 0;
	float Speed;
	public TaskPatrol(Node2D Enemy, Node2D[] Waypoints, float Speed)
	{
		this.Waypoints = Waypoints;
		this.Enemy = Enemy;
		this.Speed = Speed;
	}

	public override NodeState Evaluate()
	{
		//GD.Print("test");
		var target = Waypoints[currentWaypoint];
		//GD.Print("test1");
		//GD.Print(Enemy);
		var moveDirection = (target.Position - Enemy.Position).Normalized();
		//GD.Print("test2");
		Enemy.Position += moveDirection * Speed;

		//Enemy.Velocity = moveDirection * Speed;
		//GD.Print("test3");
		//Enemy.MoveAndSlide();
		//GD.Print("test4");

		if (Enemy.Position.DistanceTo(target.Position) < 2f)
		{
			currentWaypoint++;

			if (currentWaypoint >= Waypoints.Length)
				currentWaypoint = 0;
		}

		return NodeState.RUNNING;
	}
}
