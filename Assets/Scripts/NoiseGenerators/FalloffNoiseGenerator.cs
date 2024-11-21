using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FalloffNoiseGenerator", menuName = "SO/NoiseGenerator/FalloffNoiseGenerator")]
public class FalloffNoiseGenerator : NoiseGenerator
{
    [SerializeField]
    private NoiseGenerator heightNoiseGenerator;
    [SerializeField]
    private NoiseGenerator falloffNoiseGenerator;
    [SerializeField]
    private AnimationCurve falloffCurve;

    public override float[] GetHeightNoiseValues(Vector3[] points)
    {
        float[] falloffNoiseValues = falloffNoiseGenerator.GetHeightNoiseValues(points);
        float[] heightNoiseValues = heightNoiseGenerator.GetHeightNoiseValues(points);

        // Debug.Log($"Falloff: {falloffNoiseValues.Min()}, {falloffNoiseValues.Max()}");

        float[] resultNoiseValues = new float[falloffNoiseValues.Length];
        for (int i = 0; i < resultNoiseValues.Length; i++)
        {
            Debug.Log(falloffNoiseValues[i]);
            resultNoiseValues[i] = falloffCurve.Evaluate(Mathf.Clamp01(falloffNoiseValues[i])) * Mathf.Abs(heightNoiseValues[i]);
        }

        Debug.Log($"Height: {heightNoiseValues.Min()}");

        return resultNoiseValues;
    }
}
