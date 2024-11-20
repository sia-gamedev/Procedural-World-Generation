using UnityEngine;

public class Grid2D
{
    public Vector2 GridCellSize {get; private set;} // the size of one cell
    public Vector2Int GridCellCount {get; private set;} // the amount of cells
    public Vector2[] Grid {get; private set;}
    public Grid2D(Vector2 gridCellSize, Vector2Int gridCellCount)
    {
        GridCellSize = gridCellSize;
        GridCellCount = gridCellCount;

        Grid = new Vector2[gridCellCount.x * gridCellCount.y * 4];

        int i = 0;
        for (int z = 0; z <= gridCellCount.y; z++) 
        {
			for (int x = 0; x <= gridCellCount.x; x++) 
            {
                Grid[i] = new Vector2(x * gridCellSize.x, z * gridCellSize.y);
            }
        }
    }
}