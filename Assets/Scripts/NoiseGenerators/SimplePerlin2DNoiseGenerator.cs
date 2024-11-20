using UnityEngine;

[CreateAssetMenu(fileName = "SimplePerlin2DNoiseGenerator", menuName = "SO/NoiseGenerator/SimplePerlin2DNoiseGenerator")]
public class SimplePerlin2DNoiseGenerator : NoiseGenerator
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
    private float amplitude = 10f;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] noiseValues = new float[points.Length];

        System.Random rng = new System.Random(seed);
        float offsetX = rng.Next(minRandomOffset, maxRandomOffset);
        float offsetZ = rng.Next(minRandomOffset, maxRandomOffset);

        for (int i = 0; i < points.Length; i++)
        {
            noiseValues[i] = Mathf.PerlinNoise((points[i].x / scale) + offsetX, (points[i].z / scale) + offsetZ) * amplitude;
        }

        return noiseValues;
    }
}
