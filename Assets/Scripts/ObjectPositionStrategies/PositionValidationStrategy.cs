using UnityEngine;

public abstract class PositionValidationStrategy : ScriptableObject
{
    public abstract bool IsValidPosition(RaycastHit hit);
}
