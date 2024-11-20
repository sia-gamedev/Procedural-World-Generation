using UnityEngine;

[CreateAssetMenu(fileName = "TerracedNoiseGenerator", menuName = "SO/NoiseGenerator/TerracedNoiseGenerator")]
public class TerracedNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    private NoiseGenerator noiseGenerator;
    [SerializeField]
    private float[] terraceThresholds;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] noiseValues = noiseGenerator.GetHeightNoiseValues(points);

        if (terraceThresholds.Length > 1)
        {
            for (int i = 0; i < noiseValues.Length; i++)
            {
                for (int terraceNumber = 0; terraceNumber < terraceThresholds.Length - 1; terraceNumber++)
                {
                    if (noiseValues[i] >= terraceThresholds[terraceNumber] && noiseValues[i] < terraceThresholds[terraceNumber + 1])
                    {
                        noiseValues[i] = terraceThresholds[terraceNumber];
                    }
                }
            }
        }
        else if (terraceThresholds.Length == 1)
        {
            for (int i = 0; i < noiseValues.Length; i++)
            {
                if (noiseValues[i] >= terraceThresholds[0])
                    noiseValues[i] = terraceThresholds[0];
            }
        }

        return noiseValues;
    }
}
