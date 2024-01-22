using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }

    public enum State
    {
        Die,
        Idle,
        Moving,
        Skill
    }

    public enum Layer
    {
        Ground = 6,
        Block = 7,
        Monster = 8
    }

    public enum Scene
    {
        Unknown,
        Login,
        Loading,
        Lobby,
        Game
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click
    }

    public enum CameraMode
    {
        QuerterView
    }
}
