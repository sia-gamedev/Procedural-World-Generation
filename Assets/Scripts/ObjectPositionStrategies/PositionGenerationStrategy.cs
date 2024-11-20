using UnityEngine;

public abstract class PositionGenerationStrategy : ScriptableObject
{
    public abstract Vector3[] GeneratePositions(Vector3 minPlacementPosition, Vector3 maxPlacementPosition, int attempts, int seed);
}
