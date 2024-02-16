using System;
using Godot;

namespace playerstats
{
	public static class player_stats
	{
		public static Node2D Player_Id;
		public static int Health; // current health
		public static int Max_Health; // max health
		public static int Health_Regen = 2; // passive health regen
		public static int Damage = 15; // damage per hit
		public static int Armour = 0; // damage reduction
		public static bool Damage_immunity = false;
	}
}