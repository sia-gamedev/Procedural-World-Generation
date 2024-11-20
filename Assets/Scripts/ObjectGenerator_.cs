using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator_ : MonoBehaviour
{
    [SerializeField]
    private int placingAttempts;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Vector3 minPlacementPosition;
    [SerializeField]
    private Vector3 maxPlacementPosition;

    private Vector3[] linePointsGizmo;
    private Vector3[] hitPointsGizmo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            print("Placing objects");

            GenerateObjects();
        }
    }

    private void GenerateObjects()
    {
        Vector3[] rayPositions = new Vector3[placingAttempts];
        List<Vector3> hitPositions = new List<Vector3>();

        for(int i = 0; i < placingAttempts; i++)
        {
            RaycastHit hit;
            rayPositions[i] = new Vector3(
                UnityEngine.Random.Range(minPlacementPosition.x, maxPlacementPosition.x), 
                UnityEngine.Random.Range(minPlacementPosition.y, maxPlacementPosition.y), 
                UnityEngine.Random.Range(minPlacementPosition.z, maxPlacementPosition.z));

            if (Physics.Raycast(rayPositions[i], transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            { 
                Debug.DrawRay(rayPositions[i], transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                hitPositions.Add(hit.point);
            }
            else
            { 
                Debug.DrawRay(rayPositions[i], transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }

        RegenerateGizmoData(rayPositions, hitPositions);
    }

    private void RegenerateGizmoData(Vector3[] rayPositions, List<Vector3> hitPositions)
    {
        hitPointsGizmo = hitPositions.ToArray();
        linePointsGizmo = new Vector3[rayPositions.Length * 2];
        int pointsCounter = 0;
        Vector3 down = Vector3.down * 100;
        for (int i = 0; i < rayPositions.Length; i++)
        {
            linePointsGizmo[pointsCounter] = rayPositions[i];
            pointsCounter++;
            linePointsGizmo[pointsCounter] = rayPositions[i] + down;
            pointsCounter++;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (linePointsGizmo == null || hitPointsGizmo == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLineList(linePointsGizmo);

        foreach (Vector3 center in hitPointsGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(center, 0.5f);
        }
    }
}
