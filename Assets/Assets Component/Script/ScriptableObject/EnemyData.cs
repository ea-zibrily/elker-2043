﻿using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "ScriptableObject/Entities/New Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    [field: SerializeField] public float EnemyMoveSpeed { get; private set; }
    public EnemyTypeEnum enemyTypeEnum;
}