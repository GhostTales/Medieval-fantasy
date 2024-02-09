using Godot;
using System;

using BehaviorTree;
using System.Collections.Generic;

public partial class enemy : BehaviorTree.Tree
{
    [Export]
    Node2D[] _waypoints;
    [Export]
    Timer _attackTimer;
    [Export]
    Node2D _enemy;
    [Export]
    AnimatedSprite2D _Enemy_Sprites;
    [Export]
    SpriteFrames _Enemy_Frames;
    [Export]
    Node2D _player;
    [Export]
    int _max_health;
    [Export]
    int _health;
    [Export]
    float _speed;


    protected override BtNode SetupTree()
    {

        BtNode root = new Sequence(new List<BtNode>
        {
            new TaskAnimateSprite(_Enemy_Sprites, _Enemy_Frames, _enemy, _player, _waypoints, 150),

            new Selector(new List<BtNode>
                {
                    new Sequence(new List<BtNode>
                    {
                        new TaskFleeHealth(_health, _max_health),
                        new TaskFleeDist(_enemy, _player, 150),
                        new TaskFlee(_enemy, _player, _speed)
                    }),

                    new Sequence(new List<BtNode>
                    {
                        new TaskAttackDist(_enemy, _player, 50),
                        new TaskAttack(_enemy, _player, _attackTimer)
                    }),

                    new Sequence(new List<BtNode>
                    {
                        new TaskChaseDist(_enemy, _player , 150),
                        new TaskChase(_enemy, _player, _speed)
                    }),

                    new TaskPatrol(_enemy, _waypoints, _speed)
                })
        });
        return root;
    }
}
