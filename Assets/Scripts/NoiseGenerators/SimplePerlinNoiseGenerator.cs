using UnityEngine;
using Unity.Mathematics;
using System.Linq;

[CreateAssetMenu(fileName = "SimplePerlinNoiseGenerator", menuName = "SO/NoiseGenerator/SimplePerlinNoiseGenerator")]
public class SimplePerlinNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    private int seed = 0;
    [SerializeField]
    private int minRandomOffset = -1000;
    [SerializeField]
    private int maxRandomOffset = 1000;
    [SerializeField]
    private Vector2 maxPeriod = new Vector2(100000f, 100000f);
    [SerializeField, Range(0.0001f, 100)]
    private float scale = 10f;
    [SerializeField]
    private float amplitude = 10f;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] noiseValues = new float[points.Length];

        System.Random rng = new System.Random(seed);
        float offsetX = rng.Next(minRandomOffset, maxRandomOffset);
        float offsetZ = rng.Next(minRandomOffset, maxRandomOffset);

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 position = new Vector2((points[i].x / scale) + offsetX, (points[i].z / scale) + offsetZ) * amplitude;
            noiseValues[i] = noise.pnoise(position, maxPeriod);
        }

        // Debug.Log($"pnoise: {noiseValues.Min()}, {noiseValues.Max()}");

        return noiseValues;
    }
}