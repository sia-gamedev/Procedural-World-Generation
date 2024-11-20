using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainAlignedObjectGenerator", menuName = "SO/ObjectGenerator/TerrainAlignedObjectGenerator")]
public class TerrainAlignedObjectGenerator : ObjectGenerator
{
    [SerializeField]
    private int placingAttempts;
    [SerializeField]
    private PositionGenerationStrategy positionGen;
    [SerializeField]
    private PositionValidationStrategy positionValid;
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
                if (positionValid.IsValidPosition(hit))
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
