using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "BillowedPerlinNoiseGenerator", menuName = "SO/NoiseGenerator/BillowedPerlinNoiseGenerator")]
public class BillowedPerlinNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    private int seed = 0;
    [SerializeField]
    private int minRandomOffset = -1000;
    [SerializeField]
    private int maxRandomOffset = 1000;
    [SerializeField, Range(0.0001f, 100)]
    private float scale = 10f;
    [SerializeField, Range(1, 10)]
    private int octaves = 1;
    [SerializeField, Range(2, 10)]
	private float lacunarity = 2;
    [SerializeField, Range(0f, 1f)]
	private float persistance = 0.5f;
    [SerializeField, Range(1f, 100f)]
    private float height;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] noiseValues = new float[points.Length];

        float amplitude = 1;
		float frequency = 1;
        System.Random rng = new System.Random(seed);

        for (int octaveNum = 0; octaveNum < octaves; octaveNum++)
        {
            // Calculate random offsets so different seed results in a different map
            float offsetX = rng.Next(minRandomOffset, maxRandomOffset);
            float offsetZ = rng.Next(minRandomOffset, maxRandomOffset);

            // Calculate all noise values for a given octave and add them to the result
            for (int i = 0; i < points.Length; i++)
            {
                float sampleX = (points[i].x / scale * frequency) + offsetX;
			    float sampleZ = (points[i].z / scale * frequency) + offsetZ;
			    float perlinValue = 2 * noise.cnoise(new Vector2(sampleX, sampleZ)) - 1;
                noiseValues[i] += Mathf.Abs(perlinValue) * amplitude;
            }

			amplitude *= persistance;
			frequency *= lacunarity;
        }

        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] *= height;
        }

        return noiseValues;
    }
}
