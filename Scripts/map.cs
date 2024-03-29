using System.Collections.Generic;
using Godot;
using System;
using playerstats;

public partial class map : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (check_tree(GetNode("/root/World")) == false)
		{
			this.Visible = true;
			//GD.Print("no enemies");
		}
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		//GD.Print(body.Name);
		if (body.Name == "player" && this.Visible)
		{
			player_stats.map_unlocked = true;
			QueueFree();
		}
	}

	public bool check_tree(Node tree)
	{
		foreach (var item in tree.GetChildren())
		{
			foreach (var item2 in item.GetChildren())
			{
				if (item2.Name == "enemy")
				{
					this.Position = (item2 as Node2D).Position * 1.05f;
					return true;
				}
			}
		}

		return false;
	}
}
