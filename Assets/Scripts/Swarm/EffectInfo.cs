using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo{
    public int id;
    public float timestamp_end;
    public float next_icon;
    public SwarmEffect effector;

    public float excitement_received;

    public EffectInfo(){}
}

public enum EffectType{
    Direction,
    Push,
    Pull,
    Stop
};
