using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupData
{
    public string LeaderName;
    public Material leaderMaterial;

    public int score = 1;
    public int KillCount = 0;

    public GameObject groupLeader;
    public int groupId = 0;

    public Indicator indicatorInstance;
    public bool isDead = false;
    public int PhysicsLayerID;

    public Color groupColor;
}

