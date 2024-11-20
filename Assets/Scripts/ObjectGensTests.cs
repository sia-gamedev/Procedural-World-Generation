using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectGensTests : MonoBehaviour
{
    [SerializeField]
    bool runTests;
    [SerializeField]
    SimpleRandomPositionGenerationStrategy randomPositionGenerationStrategy;
    [SerializeField]
    NoiseDistributedPositionGenerationStrategy noiseDistributedPositionGenerationStrategy;
    [SerializeField]
    HeightLimitedValidationStrategy heightLimitedValidationStrategy;
    [SerializeField]
    SlopeLimitedValidationStrategy slopeLimitedValidationStrategyLow;
    [SerializeField]
    SlopeLimitedValidationStrategy slopeLimitedValidationStrategyHigh;
    [SerializeField]
    Collider raycastCollider;
    [SerializeField]
    LayerMask layerMask;

    void Start()
    {
        if (runTests)
            RunTests();
    }

    private void RunTests()
    {
        // SimpleRandomPositionGenerationStrategy
        PositionGenerationStrategy_GeneratePositions_SeedConsistency(randomPositionGenerationStrategy, "SimpleRandomPositionGenerationStrategy");
        PositionGenerationStrategy_GeneratePositions_MatchesExpectedRange(randomPositionGenerationStrategy, "SimpleRandomPositionGenerationStrategy");

        // NoiseDistributedPositionGenerationStrategy
        PositionGenerationStrategy_GeneratePositions_SeedConsistency(noiseDistributedPositionGenerationStrategy, "NoiseDistributedPositionGenerationStrategy");
        PositionGenerationStrategy_GeneratePositions_MatchesExpectedRange(noiseDistributedPositionGenerationStrategy, "NoiseDistributedPositionGenerationStrategy");

        // HeightLimitedValidationStrategy
        HeightLimitedValidation_IsValidPosition_MatchesExpectedRange();

        // SlopeLimitedValidationStrategy
        SlopeLimitedValidationStrategy_IsValidPosition_MatchesExpectedRange();
    }

    public void PositionGenerationStrategy_GeneratePositions_SeedConsistency(PositionGenerationStrategy positionGenerationStrategy, string strategyName)
    {
        // Arrange
        Vector3 minPos = new Vector3(-100, 0, -100);
        Vector3 maxPos = new Vector3(100, 0, 100);
        int attempts = 100;
        int seed = 0;
        // Act
        Vector3[] firstPositions = positionGenerationStrategy.GeneratePositions(minPos, maxPos, attempts, seed);
        Vector3[] secondPositions = positionGenerationStrategy.GeneratePositions(minPos, maxPos, attempts, seed);
        // Assert
        bool expected = true;
        for (int i = 0; i < firstPositions.Length; i++)
        {
            expected = expected && (firstPositions[i] == secondPositions[i]);
        }
        Test.Assert(expected, strategyName + "_GeneratePositions_SeedConsistency");
    }

    public void PositionGenerationStrategy_GeneratePositions_MatchesExpectedRange(PositionGenerationStrategy positionGenerationStrategy, string strategyName)
    {
        // Arrange
        Vector3 minPos = new Vector3(-100, 0, -100);
        Vector3 maxPos = new Vector3(100, 0, 100);
        Bounds bounds = new Bounds();
        bounds.SetMinMax(minPos, maxPos);
        int attempts = 100;
        int seed = 0;
        // Act
        Vector3[] positions = positionGenerationStrategy.GeneratePositions(minPos, maxPos, attempts, seed);
        // Assert
        bool expected = true;
        for (int i = 0; i < positions.Length; i++)
        {
            expected = expected && bounds.Contains(positions[i]);
        }
        Test.Assert(expected, strategyName + "_GeneratePositions_MatchesExpectedRange");
    }

    public void HeightLimitedValidation_IsValidPosition_MatchesExpectedRange()
    {
        // Arrange
        Bounds bounds = raycastCollider.bounds;
        Vector3 minPos = bounds.min;
        Vector3 maxPos = bounds.max;
        float minHeight = 0;
        float maxHeight = 10;

        int positionCount = 100;
        Vector2 spaceBetween = new Vector2((maxPos.x - minPos.x) / positionCount, (maxPos.z - minPos.z) / positionCount);

        Vector3[] positions = new Vector3[positionCount];
        int k = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                positions[k] = new Vector3(x * spaceBetween.x, maxPos.y, z * spaceBetween.y);
                k++;
            }
        }

        // Act
        List<RaycastHit> hits = new List<RaycastHit>();
        List<Vector3> expectedPositions = new List<Vector3>();
        for(int i = 0; i < positionCount; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(positions[i], Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                if (heightLimitedValidationStrategy.IsValidPosition(hit))
                    hits.Add(hit);
                if ((hit.point.y <= maxHeight) && (hit.point.y >= minHeight))
                    expectedPositions.Add(hit.point);
            }
        }
        // Assert
        RaycastHit[] hitsArray = hits.ToArray();
        bool expected = true;
        for (int j = 0; j < hitsArray.Length; j++)
        {
            expected = expected && (hitsArray[j].point == expectedPositions[j]);
        }
        Test.Assert(expected, "HeightLimitedValidation_IsValidPosition_MatchesExpectedRange");
    }

    public void SlopeLimitedValidationStrategy_IsValidPosition_MatchesExpectedRange()
    {
        // Arrange
        Bounds bounds = raycastCollider.bounds;
        Vector3 minPos = bounds.min;
        Vector3 maxPos = bounds.max;

        int positionCount = 100;
        Vector2 spaceBetween = new Vector2((maxPos.x - minPos.x) / positionCount, (maxPos.z - minPos.z) / positionCount);

        Vector3[] positions = new Vector3[positionCount];
        int k = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                positions[k] = new Vector3(x * spaceBetween.x, maxPos.y, z * spaceBetween.y);
                k++;
            }
        }

        // Act
        List<Vector3> positionsLowList = new List<Vector3>();
        for(int i = 0; i < positionCount; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(positions[i], Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                if (slopeLimitedValidationStrategyLow.IsValidPosition(hit))
                    positionsLowList.Add(hit.point);
            }
        }
        List<Vector3> positionsHighList = new List<Vector3>();
        Vector3[] expectedPositions = new Vector3[positionCount];
        for(int i = 0; i < positionCount; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(positions[i], Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                if (slopeLimitedValidationStrategyHigh.IsValidPosition(hit))
                    positionsHighList.Add(hit.point);
                expectedPositions[i] = hit.point;
            }
        }
        // Assert
        bool expected = true;
        Vector3[] positionsLow = positionsLowList.ToArray();
        for (int j = 0; j < positionsLow.Length; j++)
        {
            expected = false;
        }
        Vector3[] positionsHigh = positionsHighList.ToArray();
        for (int j = 0; j < positionsHigh.Length; j++)
        {
            expected = expected && (positionsHigh[j] == expectedPositions[j]);
        }
        Test.Assert(expected, "SlopeLimitedValidationStrategy_IsValidPosition_MatchesExpectedRange");
    }
}
