using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomPositionGenerationStrategy", menuName = "SO/PositionGenerationStrategy/SimpleRandomPositionGenerationStrategy")]
public class SimpleRandomPositionGenerationStrategy : PositionGenerationStrategy
{
    public override Vector3[] GeneratePositions(Vector3 minPlacementPosition, Vector3 maxPlacementPosition, int attempts, int seed)
    {
        Vector3[] positions = new Vector3[attempts];
        System.Random rng = new System.Random(seed);

        for (int i = 0; i < attempts; i++)
        {
            float x = Mathf.Lerp(minPlacementPosition.x, maxPlacementPosition.x, (float)rng.NextDouble());
            float y = Mathf.Lerp(minPlacementPosition.y, maxPlacementPosition.y, (float)rng.NextDouble());
            float z = Mathf.Lerp(minPlacementPosition.z, maxPlacementPosition.z, (float)rng.NextDouble());
            positions[i] = new Vector3(x, y, z);
        }

        return positions;
    }
}
