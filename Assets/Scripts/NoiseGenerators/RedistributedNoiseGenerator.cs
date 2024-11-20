using UnityEngine;

[CreateAssetMenu(fileName = "RedistributedNoiseGenerator", menuName = "SO/NoiseGenerator/RedistributedNoiseGenerator")]
public class RedistributedNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    NoiseGenerator noiseGenerator;
    [SerializeField, Range(0.001f, 10f)]
    float exponent;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] noiseValues = noiseGenerator.GetHeightNoiseValues(points);

        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = Mathf.Pow(noiseValues[i], exponent);
        }

        return noiseValues;
    }
}