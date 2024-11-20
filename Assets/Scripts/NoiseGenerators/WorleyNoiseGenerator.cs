using UnityEngine;
using Unity.Mathematics;
using System.Linq;

[CreateAssetMenu(fileName = "WorleyNoiseGenerator", menuName = "SO/NoiseGenerator/WorleyNoiseGenerator")]
public class WorleyNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    private int seed = 0;
    [SerializeField]
    private int minRandomOffset = -1000;
    [SerializeField]
    private int maxRandomOffset = 1000;
    [SerializeField, Range(0.0001f, 100)]
    private float scale = 10f;
    [SerializeField]
    private float height;
    
    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] noiseValues = new float[points.Length];
        System.Random rng = new System.Random(seed);

        float offsetX = rng.Next(minRandomOffset, maxRandomOffset);
        float offsetZ = rng.Next(minRandomOffset, maxRandomOffset);

        for (int i = 0; i < points.Length; i++)
        {
            float sampleX = (points[i].x / scale) + offsetX;
			float sampleZ = (points[i].z / scale) + offsetZ;
            noiseValues[i] = 1f - noise.cellular(new float2(sampleX, sampleZ))[0];
            noiseValues[i] *= height;
        }
        // Debug.Log($"Worley: {noiseValues.Min()}, {noiseValues.Max()}");
        return noiseValues;
    }
}
