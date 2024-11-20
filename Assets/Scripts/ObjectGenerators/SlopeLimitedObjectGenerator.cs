using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlopeLimitedObjectGenerator", menuName = "SO/ObjectGenerator/SlopeLimitedObjectGenerator")]
public class SlopeLimitedObjectGenerator : ObjectGenerator
{
    [SerializeField]
    private int placingAttempts;
    [SerializeField]
    private PositionGenerationStrategy positionGen;
    [SerializeField, Range(0f, 90f)]
    private float minSlope;
    [SerializeField, Range(0f, 90f)]
    private float maxSlope;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float maxScale;
    public override GameObject[] GenerateObjects(Vector3 minPlacementPosition, Vector3 maxPlacementPosition, int seed)
    {
        rayPositions = positionGen.GeneratePositions(minPlacementPosition, maxPlacementPosition, placingAttempts, seed);
        List<RaycastHit> hits = new List<RaycastHit>();

        for(int i = 0; i < rayPositions.Length; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(rayPositions[i], Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                float slopeAngle = Mathf.Abs(Vector3.Angle(hit.normal, Vector3.up));

                if (slopeAngle >= minSlope && slopeAngle <= maxSlope)
                    hits.Add(hit);
            }
        }

        RaycastHit[] hitsArray = hits.ToArray();
        hitPositions = new Vector3[hitsArray.Length];
        GameObject[] instances = new GameObject[hitPositions.Length];

        for (int i = 0; i < hitsArray.Length; i++)
        {
            GameObject instance = Instantiate(prefab, hitsArray[i].point, Quaternion.identity);
            instance.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
            instance.transform.up = hitsArray[i].normal;
            instance.transform.Rotate(Vector3.up, Random.Range(0f, 360f), Space.Self);
            
            instances[i] = instance;
            hitPositions[i] = hitsArray[i].point;
        }

        return instances;
    }
}
