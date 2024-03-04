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
	public int Speed;    // Hastighed, redigerbar fra Inspector (ved Export)
	[Export]
	public int Health;
	[Export]
	public int Max_Health;
	[Export]
	public int Health_Regen;
	[Export]
	public int Damage;
	[Export]
	public int Armour;


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

		if (player_stats.Health > 0)
			MoveAndSlide(); // Flytter sig i henhold fysikkens kræfter og glider af kolliderende Objekter

		if (player_stats.Health <= 0 && player_stats.IsAlive)
		{
			anim.Play("death");
			player_stats.IsAlive = false;
		}

		Move_Rigidbody(10);
	}

	public override void _Process(double delta)
	{
		player_stats.delta = (float)delta;
	}



	public void PlayAnimation(int movement)
	{
		AttackArea.Rotation = Mathf.Atan2(current_dir.X, -current_dir.Y);

		if (!anim.Animation.ToString().Contains("attack") && player_stats.IsAlive)
		{
			if (current_dir == Vector2.Left || current_dir == Vector2.Right)
			{
				anim.FlipH = current_dir.X < 0;
				anim.Play("side_" + (movement == 1 ? "walk" : "idle"));
			}
			else if (current_dir == Vector2.Up)
				anim.Play("back_" + (movement == 1 ? "walk" : "idle"));

			else if (current_dir == Vector2.Down)
				anim.Play("front_" + (movement == 1 ? "walk" : "idle"));
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
		if (player_stats.Health < player_stats.Max_Health && player_stats.Health > 0)
			player_stats.Health += player_stats.Health_Regen;

		if (player_stats.Health > player_stats.Max_Health)
			player_stats.Health = player_stats.Max_Health;

		if (player_stats.Health < 0)
			player_stats.Health = 0;
	}
}
