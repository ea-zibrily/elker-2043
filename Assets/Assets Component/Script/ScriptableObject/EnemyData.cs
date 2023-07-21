using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "ScriptableObject/Entities/New Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float enemyMoveSpeed;
    public EnemyTypeEnum enemyTypeEnum;
}