using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FalloffNoiseGenerator", menuName = "SO/NoiseGenerator/FalloffNoiseGenerator")]
public class FalloffNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    private NoiseGenerator heightNoiseGenerator;
    [SerializeField]
    private NoiseGenerator falloffNoiseGenerator;
    [SerializeField, Range(0f, 1f)]
    private float falloffFactor;
    [SerializeField]
    private AnimationCurve falloffCurve;
    [SerializeField]
    private float height;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] falloffNoiseValues = falloffNoiseGenerator.GetHeightNoiseValues(points);
        float[] heightNoiseValues = heightNoiseGenerator.GetHeightNoiseValues(points);

        // Debug.Log($"Falloff: {falloffNoiseValues.Min()}, {falloffNoiseValues.Max()}");

        float[] resultNoiseValues = new float[falloffNoiseValues.Length];
        for (int i = 0; i < resultNoiseValues.Length; i++)
        {
            resultNoiseValues[i] = falloffCurve.Evaluate(Mathf.Clamp01(falloffNoiseValues[i])) * heightNoiseValues[i];
        }

        return resultNoiseValues;
    }
}
