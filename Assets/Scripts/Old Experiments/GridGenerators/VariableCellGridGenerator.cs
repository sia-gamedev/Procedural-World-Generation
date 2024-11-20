using UnityEngine;

// [CreateAssetMenu(fileName = "SOVariableCellGridGenerator", menuName = "SOGridGenerator/SOVariableCellGridGenerator")]
// public class VariableCellGridGenerator : GridGenerator
// {
//     [SerializeField]
//     private Vector2Int gridCellCount = Vector2Int.one;
//     [SerializeField]
//     private Vector2 gridCellSize = Vector2.one;
//     public override VertexGrid GenerateDiscreteGrid()
//     {
//         Vector3[] gridPositions = new Vector3[gridCellCount.x * gridCellCount.y * 4];

//         int vertexNumber = 0;
//         for (int z = 0; z <= gridCellCount.y; z++) 
//         {
// 			for (int x = 0; x <= gridCellCount.x; x++) 
//             {
// 				gridPositions[vertexNumber] = new Vector3(x * gridCellSize.x, 0, z * gridCellSize.y);
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
// 				gridPositions[vertexNumber] = new Vector3(x * gridCellSize.x, 0, z * gridCellSize.y);
//                 vertexNumber++;
//             }
//         }

//         return new VertexGrid(gridCellCount, gridPositions);
//     }
// }