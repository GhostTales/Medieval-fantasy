using Godot;
using System;

public partial class KortStorGsbg : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Keycode == Key.M)
			{
				if (this.Visible)
					this.Visible = false;
				else this.Visible = true;
			}
		}
	}
}
