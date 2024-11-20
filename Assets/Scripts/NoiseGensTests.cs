using System.Linq;
using UnityEngine;

public class NoiseGensTests : MonoBehaviour
{
    [SerializeField]
    bool runTests;
    [SerializeField]
    SimplePerlinNoiseGenerator simplePerlinNoiseGenerator;
    [SerializeField]
    FractalPerlinNoiseGenerator fractalPerlinNoiseGenerator;
    [SerializeField]
    RidgedPerlinNoiseGenerator ridgedPerlinNoiseGenerator;
    [SerializeField]
    BillowedPerlinNoiseGenerator billowedPerlinNoiseGenerator;
    [SerializeField]
    WorleyNoiseGenerator worleyNoiseGenerator;
    [SerializeField]
    FalloffNoiseGenerator falloffNoiseGenerator;
    private void Start()
    {
        if (runTests)
            RunTests();
    }

    private void RunTests()
    {
        // SimplePerlinNoiseGenerator
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(simplePerlinNoiseGenerator, "SimplePerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(simplePerlinNoiseGenerator, "SimplePerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_SeedConsistency(simplePerlinNoiseGenerator, "SimplePerlinNoiseGenerator");

        // FractalPerlinNoiseGenerator
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(fractalPerlinNoiseGenerator, "FractalPerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(fractalPerlinNoiseGenerator, "FractalPerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_SeedConsistency(fractalPerlinNoiseGenerator, "FractalPerlinNoiseGenerator");

        // RidgedPerlinNoiseGenerator
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(ridgedPerlinNoiseGenerator, "RidgedPerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(ridgedPerlinNoiseGenerator, "RidgedPerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_SeedConsistency(ridgedPerlinNoiseGenerator, "RidgedPerlinNoiseGenerator");

        // BillowedPerlinNoiseGenerator
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(billowedPerlinNoiseGenerator, "BillowedPerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(billowedPerlinNoiseGenerator, "BillowedPerlinNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_SeedConsistency(billowedPerlinNoiseGenerator, "BillowedPerlinNoiseGenerator");

        // WorleyNoiseGenerator
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(worleyNoiseGenerator, "WorleyNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(worleyNoiseGenerator, "WorleyNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_SeedConsistency(worleyNoiseGenerator, "WorleyNoiseGenerator");

        // FalloffNoiseGenerator
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(falloffNoiseGenerator, "FalloffNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(falloffNoiseGenerator, "FalloffNoiseGenerator");
        NoiseGenerator_GenerateHeightNoise_SeedConsistency(falloffNoiseGenerator, "FalloffNoiseGenerator");
    }

    public void NoiseGenerator_GenerateHeightNoise_MatchesExpectedRange(NoiseGenerator noiseGenerator, string noiseGeneratorName)
    {
        // Arrange
        System.Random rng = new System.Random(0);
        Vector3[] testPositions = new Vector3[10000];
        for (int i = 0; i < testPositions.Length; i++)
        {
            testPositions[i] = new Vector3(rng.Next(-100, 100), rng.Next(-100, 100), rng.Next(-100, 100));
        }
        // Act
        float[] heightValues = noiseGenerator.GetHeightNoiseValues(testPositions);
        // Assert
        float minValue = heightValues.Min();
        float maxValue = heightValues.Max();
        bool expected = (minValue >= -1f) && (maxValue <= 1f);
        Test.Assert(expected, noiseGeneratorName + "_GenerateHeightNoise_MatchesExpectedRange");
    }

    public void NoiseGenerator_GenerateHeightNoise_MatchesExpectedLength(NoiseGenerator noiseGenerator, string noiseGeneratorName)
    {
        // Arrange
        Vector3[] testPositions = new Vector3[10000];
        for (int i = 0; i < testPositions.Length; i++)
        {
            testPositions[i] = Vector3.one;
        }
        // Act
        float[] heightValues = noiseGenerator.GetHeightNoiseValues(testPositions);
        // Assert
        bool expected = heightValues.Length == testPositions.Length;
        Test.Assert(expected, noiseGeneratorName + "_GenerateHeightNoise_MatchesExpectedLength");
    }

    public void NoiseGenerator_GenerateHeightNoise_SeedConsistency(NoiseGenerator noiseGenerator, string noiseGeneratorName)
    {
        // Arrange
        System.Random rng = new System.Random(0);
        Vector3[] testPositions = new Vector3[10000];
        for (int i = 0; i < testPositions.Length; i++)
        {
            testPositions[i] = new Vector3(rng.Next(-100, 100), rng.Next(-100, 100), rng.Next(-100, 100));
        }
        // Act
        float[] firstHeightValues = noiseGenerator.GetHeightNoiseValues(testPositions);
        float[] secondHeightValues = noiseGenerator.GetHeightNoiseValues(testPositions);
        // Assert
        bool expected = true;
        for (int i = 0; i < firstHeightValues.Length; i++)
        {
            expected = expected && (firstHeightValues[i] == secondHeightValues[i]);
        }

        Test.Assert(expected, noiseGeneratorName + "_GenerateHeightNoise_SeedConsistency");
    }
}