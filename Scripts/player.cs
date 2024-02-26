using System;
using Godot;
using playerstats;



public partial class player : CharacterBody2D
{
	AnimatedSprite2D anim;
	Vector2 current_dir = new Vector2(0, 0); // Vi gemmer retning her
	Area2D AttackArea;

	public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("front_idle");
		AttackArea = GetNode<Area2D>("Area2D");
		player_stats.Health = Health;
		player_stats.Max_Health = Max_Health;
		player_stats.Health_Regen = Health_Regen;
		player_stats.Damage = Damage;
		player_stats.Armour = Armour;

	}

	[Export]
	public int Speed = 100;    // Hastighed, redigerbar fra Inspector (ved Export)
	[Export]
	public int Health = 25;
	[Export]
	public int Max_Health = 25;
	[Export]
	public int Health_Regen = 2;
	[Export]
	public int Damage = 5;
	[Export]
	public int Armour = 0;


	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;

		if (inputDirection != new Vector2(0, 0))
		{
			PlayAnimation(1);
			current_dir = inputDirection;
		}
		else
			PlayAnimation(0);
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();     // Henter Player keyboard input
		MoveAndSlide(); // Flytter sig i henhold fysikkens kræfter og glider af kolliderende Objekter

		Move_Rigidbody(10);
	}

	public override void _Process(double delta)
	{
		player_stats.delta = (float)delta;
	}



	public void PlayAnimation(int movement)
	{

		Vector2 dir = current_dir;

		AttackArea.Rotation = Mathf.Atan2(dir.X, -dir.Y);

		if (dir == new Vector2(1, 0))
		{
			anim.FlipH = false;
			if (movement == 1)
				anim.Play("side_walk");
			else if (movement == 0)
				anim.Play("side_idle");
		}
		else if (dir == new Vector2(-1, 0))
		{
			anim.FlipH = true;
			if (movement == 1)
				anim.Play("side_walk");
			else if (movement == 0)
				anim.Play("side_idle");
		}
		else if (dir == new Vector2(0, -1))
		{
			if (movement == 1)
				anim.Play("back_walk");
			else if (movement == 0)
				anim.Play("back_idle");
		}
		else if (dir == new Vector2(0, 1))
		{
			if (movement == 1)
				anim.Play("front_walk");
			else if (movement == 0)
				anim.Play("front_idle");
		}
	}

	public void Move_Rigidbody(float push_force)
	{
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var c = GetSlideCollision(i);
			if (c.GetCollider() is RigidBody2D)
			{
				(c.GetCollider() as RigidBody2D).ApplyCentralImpulse(-c.GetNormal() * push_force);
			}
		}
	}

	public void _on_timer_timeout()
	{
		if (player_stats.Health < player_stats.Max_Health)
		{
			player_stats.Health += player_stats.Health_Regen;

			if (player_stats.Health > player_stats.Max_Health)
				player_stats.Health = player_stats.Max_Health;
			//GD.Print(player_stats.Health);
		}
	}
}
