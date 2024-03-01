using Godot;
using playerstats;
using System;

public partial class Healthbar : TextureProgressBar
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Value = player_stats.Health;
		this.MaxValue = player_stats.Max_Health;
		GetChild<Label>(0).Text = $"{this.Value} / {this.MaxValue}";
	}
}
