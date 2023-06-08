using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "ScriptableObject/Entities/New Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    [field: SerializeField] public float PlayerSpeed { get; private set; }
    [field: SerializeField] public float PlayerJumpForce { get; private set; }
}