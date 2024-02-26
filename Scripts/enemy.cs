using Godot;
using System;

using BehaviorTree;
using System.Collections.Generic;
using playerstats;

public partial class enemy : BehaviorTree.Tree
{
	[Export]
	Node2D[] _waypoints;
	[Export]
	Timer _attackTimer;
	[Export]
	Node2D _enemy;
	[Export]
	AnimatedSprite2D _Enemy_Sprites;
	[Export]
	SpriteFrames _Enemy_Frames;
	[Export]
	Node2D _player;
	[Export]
	int _max_health;
	[Export]
	public int _health { get; set; }
	[Export]
	int _Damage;
	[Export]
	float _speed;

	int _chase_range = 100;
	int _attack_range = 15;
	protected override BtNode SetupTree()
	{
		BtNode root = new Sequence(new List<BtNode>
		{
			new Selector(new List<BtNode>{
				new TaskZeroHealth(_enemy),
				new TaskDeath(_enemy)
				}),

			new TaskAnimateSprite(_Enemy_Sprites, _Enemy_Frames, _enemy, _player, _waypoints, _chase_range),

			new Selector(new List<BtNode>
				{
					new Sequence(new List<BtNode>
					{
						new TaskFleeHealth(_health, _max_health),
						new TaskFleeDist(_enemy, _player, _chase_range),
						new TaskFlee(_enemy, _player, _speed)
					}),

					new Sequence(new List<BtNode>
					{
						new TaskAttackDist(_enemy, _player, _attack_range),
						new TaskAttack(_enemy, _player, _attackTimer, _Damage)
					}),

					new Sequence(new List<BtNode>
					{
						new TaskChaseDist(_enemy, _player , _chase_range),
						new TaskChase(_enemy, _player, _speed)
					}),

					new TaskPatrol(_enemy, _waypoints, _speed)
				})
		});
		return root;
	}

	public void Damage(int damage) => _health -= damage;
}
