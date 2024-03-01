using Godot;
using playerstats;
using System;

public partial class KortStorGsbg : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Visible = false;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Keycode == Key.M)
			{
				if (player_stats.map_unlocked)
				{
					if (this.Visible)
						this.Visible = false;
					else this.Visible = true;
				}
			}
		}
	}
}
