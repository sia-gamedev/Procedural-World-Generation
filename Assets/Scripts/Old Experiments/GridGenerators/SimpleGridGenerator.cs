using UnityEngine;

// [CreateAssetMenu(fileName = "SOSimpleGridGenerator", menuName = "SOGridGenerator/SOSimpleGridGenerator")]
// public class SimpleGridGenerator : GridGenerator
// {
//     [SerializeField]
//     private Vector2Int gridCellCount = Vector2Int.one;
//     public override VertexGrid GenerateDiscreteGrid()
//     {
//         Vector3[] gridPositions = new Vector3[gridCellCount.x * gridCellCount.y * 4];

//         int vertexNumber = 0;
//         for (int z = 0; z <= gridCellCount.y; z++) 
//         {
// 			for (int x = 0; x <= gridCellCount.x; x++) 
//             {
// 				gridPositions[vertexNumber] = new Vector3(x, 0, z);
//                 vertexNumber++;
//             }
//         }

//         return new VertexGrid(gridCellCount, gridPositions);
//     }

//     public override VertexGrid GenerateContiguousGrid()
//     {
//         Vector3[] gridPositions = new Vector3[(gridCellCount.x + 1) * (gridCellCount.y + 1)];

//         int vertexNumber = 0;
//         for (int z = 0; z <= gridCellCount.y; z++) 
//         {
// 			for (int x = 0; x <= gridCellCount.x; x++) 
//             {
// 				gridPositions[vertexNumber] = new Vector3(x, 0, z);
//                 vertexNumber++;
//             }
//         }

//         return new VertexGrid(gridCellCount, gridPositions);
//     }
// }