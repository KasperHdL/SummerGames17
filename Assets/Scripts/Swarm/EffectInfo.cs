using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo{
    public int id;
    public float timestamp_end;
    public SwarmEffect effector;

    public EffectInfo(){}
}

public enum EffectType{
    Direction,
    Push,
    Pull,
    Stop
};
