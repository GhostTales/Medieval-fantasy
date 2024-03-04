using Godot;
using playerstats;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class PlayerAttackArea : Area2D
{
	// Called when the node enters the scene tree for the first time.

	AnimatedSprite2D anim;
	List<Node2D> _overlapping = new List<Node2D>();
	Vector2 last_dir;
	public override void _Ready()
	{
		anim = GetParent().GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");

		if (Mathf.Abs(inputDirection.X) == 1 || Mathf.Abs(inputDirection.Y) == 1)
			last_dir = inputDirection;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton && !anim.Animation.ToString().Contains("attack") && player_stats.IsAlive)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left)
			{
				if (_overlapping.Count > 0)
					foreach (var item in _overlapping)
					{
						//GD.Print((item as enemy)._health);
						(item as enemy).Damage(player_stats.Damage);
					}

				_attack_animation(last_dir);

			}
		}
	}

	public void _attack_animation(Vector2 dir)
	{
		if (dir == Vector2.Left || dir == Vector2.Right)
		{
			anim.FlipH = dir.X < 0;
			anim.Play("side_attack");
		}
		else if (dir == Vector2.Up)
			anim.Play("back_attack");
		else if (dir == Vector2.Down)
			anim.Play("front_attack");


	}
	private void _on_animated_sprite_2d_animation_finished()
	{
		if (player_stats.IsAlive)
			anim.Play("front_idle");
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.HasMeta("enemy"))
			_overlapping.Add(body);
	}

	private void _on_body_exited(Node2D body)
	{
		if (body.HasMeta("enemy"))
			_overlapping.Remove(body);
	}
}
