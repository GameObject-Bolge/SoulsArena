using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Souls Arena/Boss Data")]
public class BossData : ScriptableObject
{
    // Boss data
    [Header("General Info")]
    public string bossName;
    public string bossLore;

    [Header("Boss Stats")]
    public float bossHealth;
    public float bossSpeed;
    public float bossRange;
    public float bossAttackSpeed;

    [HideInInspector] public PlayerMovement target;
    
    public enum State
    {
        Idle,
        Tracking,
        Attacking,
        Dead,
    }

    public State state;

    public enum RangeType
    {
        Melee,
        Range,
        Both,
    }

    public RangeType range;
}
