using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAttackArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	List<Node2D> _overlapping = new List<Node2D>();
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (_overlapping.Count > 0)
			foreach (var item in _overlapping)
				GD.Print(item);

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
