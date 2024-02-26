using BehaviorTree;
using Godot;
using playerstats;
using System;

public partial class TaskAttack : BtNode
{
	// Called when the node enters the scene tree for the first time.
	Node2D player;
	Node2D transform;
	Timer Timer;
	Timer damage_timer;
	int Damage;
	public TaskAttack(Node2D transform, Node2D player, Timer Timer, int Damage)
	{
		this.player = player;
		this.transform = transform;
		this.Timer = Timer;
		this.Damage = Damage;
		this.Timer.OneShot = false;
		this.Timer.Timeout += timeout;
		this.Timer.WaitTime = 1;
		this.Timer.Start();
		this.damage_timer = player.GetChild<Timer>(0);
	}

	public void timeout()
	{
		if (transform.Position.DistanceTo(player.Position) <= 40)
		{


			if (player_stats.Damage_immunity == false)
			{
				if (Damage - player_stats.Armour <= 0)
					player_stats.Health -= 1;
				else player_stats.Health -= Damage - player_stats.Armour;

				damage_timer.Start();
			}
			//GD.Print($"{transform} attacked {player} | player health: {player_stats.Health}/{player_stats.Max_Health}");
			//GD.Print($"{player_stats.Damage_immunity} | {damage_timer.TimeLeft}");

		}
	}
	public override NodeState Evaluate()
	{

		if (damage_timer.TimeLeft != 0)
			player_stats.Damage_immunity = true;
		else player_stats.Damage_immunity = false;

		return NodeState.RUNNING;
	}
}
