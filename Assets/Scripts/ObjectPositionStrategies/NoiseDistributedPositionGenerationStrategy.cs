using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseDistributedPositionGenerationStrategy", menuName = "SO/PositionGenerationStrategy/NoiseDistributedPositionGenerationStrategy")]
public class NoiseDistributedPositionGenerationStrategy : PositionGenerationStrategy
{
    [SerializeField]
    NoiseGenerator noiseGenerator;
    public override Vector3[] GeneratePositions(Vector3 minPlacementPosition, Vector3 maxPlacementPosition, int attempts, int seed)
    {
        Vector3[] possiblePositions = new Vector3[attempts];

        System.Random rng = new System.Random(seed);

        for (int i = 0; i < attempts; i++)
        {
            float x = Mathf.Lerp(minPlacementPosition.x, maxPlacementPosition.x, (float)rng.NextDouble());
            float y = Mathf.Lerp(minPlacementPosition.y, maxPlacementPosition.y, (float)rng.NextDouble());
            float z = Mathf.Lerp(minPlacementPosition.z, maxPlacementPosition.z, (float)rng.NextDouble());
            possiblePositions[i] = new Vector3(x, y, z);
        }
        
        float[] noiseValues = noiseGenerator.GetHeightNoiseValues(possiblePositions);
        NormalizeNoiseValues(noiseValues);

        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < noiseValues.Length; i++)
        {
            float thresholdValue = (float)rng.NextDouble();
            
            if (noiseValues[i] > thresholdValue)
            {
                positions.Add(possiblePositions[i]);
            }
        }

        return positions.ToArray();
    }

    public void NormalizeNoiseValues(float[] noiseValues)
    {
        float maxValue = noiseValues.Max();
        float minValue = noiseValues.Min();

        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = Mathf.InverseLerp(minValue, maxValue, noiseValues[i]);
        }
    }
}