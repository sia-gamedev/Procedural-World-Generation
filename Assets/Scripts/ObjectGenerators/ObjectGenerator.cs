using UnityEngine;

public abstract class ObjectGenerator : ScriptableObject
{
    public Vector3[] rayPositions;
    public Vector3[] hitPositions;
    public abstract GameObject[] GenerateObjects(Vector3 minPlacementPosition, Vector3 maxPlacementPosition, int seed);
    public Vector3[] GetLinePointsGizmo(Vector3[] rayPositions)
    {
        Vector3[] linePointsGizmo = new Vector3[rayPositions.Length * 2];
        int pointsCounter = 0;
        Vector3 down = Vector3.down * 100;
        for (int i = 0; i < rayPositions.Length; i++)
        {
            linePointsGizmo[pointsCounter] = rayPositions[i];
            pointsCounter++;
            linePointsGizmo[pointsCounter] = rayPositions[i] + down;
            pointsCounter++;
        }
        return linePointsGizmo;
    }
}
