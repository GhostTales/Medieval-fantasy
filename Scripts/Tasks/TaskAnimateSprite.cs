using BehaviorTree;
using Godot;
using System;

public partial class TaskAnimateSprite : BtNode
{
    // Called when the node enters the scene tree for the first time.
    private AnimatedSprite2D anim;
    public Vector2 current_dir = new Vector2(0, 0);
    Node2D Player;
    Node2D Enemy;
    SpriteFrames SpriteFrames;
    Node2D[] Waypoints;
    int Range;
    int currentWaypoint = 0;
    public TaskAnimateSprite(AnimatedSprite2D anim, SpriteFrames spriteframes, Node2D Enemy, Node2D Player, Node2D[] Waypoints, int Range)
    {
        this.Player = Player;
        this.Enemy = Enemy;
        this.SpriteFrames = spriteframes;
        this.anim = anim;
        this.Waypoints = Waypoints;
        this.Range = Range;
    }

    public override NodeState Evaluate()
    {
        var Target_Waypoint = Waypoints[currentWaypoint];
        anim.SpriteFrames = SpriteFrames;

        if (Enemy.Position.DistanceTo(Player.Position) <= Range)
            current_dir = (Enemy.Position.DirectionTo(Player.Position) * 2).Round() / 2;
        else
            current_dir = (Enemy.Position.DirectionTo(Target_Waypoint.Position) * 2).Round() / 2;

        PlayAnimation(current_dir);
        //GD.Print(current_dir);

        if (Enemy.Position.DistanceTo(Target_Waypoint.Position) < 2f)
        {
            currentWaypoint++;
            if (currentWaypoint >= Waypoints.Length)
                currentWaypoint = 0;
        }


        return NodeState.SUCCESS;
    }

    public void PlayAnimation(Vector2 dir)
    {
        if (dir == Vector2.Right || dir == Vector2.Left)
        {
            anim.FlipH = dir == Vector2.Right;
            anim.Play("side_walk");
        }
        else if (dir == Vector2.Down)
        {
            anim.Play("front_walk");
        }
        else if (dir == Vector2.Up)
        {
            anim.Play("back_walk");
        }
    }
}
