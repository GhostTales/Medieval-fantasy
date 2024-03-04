using System;
using Godot;

namespace playerstats
{
	public static class player_stats
	{
		public static Node2D Player_Id;
		public static int Health; // current health
		public static int Max_Health; // max health
		public static int Health_Regen; // passive health regen
		public static int Damage; // damage per hit
		public static int Armour; // damage reduction
		public static bool Damage_immunity = false;
		public static float delta;
		public static bool map_unlocked = false;
		public static bool IsAlive = true;
	}
}