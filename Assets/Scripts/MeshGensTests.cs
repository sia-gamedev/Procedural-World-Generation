using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGensTests : MonoBehaviour
{
    [SerializeField]
    bool runTests;
    [SerializeField]
    ContiguousGridMeshGenerator contiguousGridMeshGenerator;
    [SerializeField]
    DiscreteGridMeshGenerator discreteGridMeshGenerator;

    void Start()
    {
        if (runTests)
            RunTests();
    }

    private void RunTests()
    {
        ContiguousGrid_GeneratePositions_MatchesExpectedPositions();
        ContiguousGrid_GenerateMesh_MatchesExpectedTriangles();

        DiscreteGrid_GeneratePositions_MatchesExpectedPositions();
        DiscreteGrid_GenerateMesh_MatchesExpectedTriangles();
    }

    public void ContiguousGrid_GeneratePositions_MatchesExpectedPositions()
    {
        // Arrange
        Vector2Int meshSize = new Vector2Int(10, 10);
        Vector3[] expectedPositions = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 2), 
                                                    new Vector3(0, 0, 3), new Vector3(0, 0, 4), new Vector3(0, 0, 5), 
                                                    new Vector3(0, 0, 6), new Vector3(0, 0, 7), new Vector3(0, 0, 8), 
                                                    new Vector3(0, 0, 9), new Vector3(0, 0, 10), new Vector3(1, 0, 0), 
                                                    new Vector3(1, 0, 1), new Vector3(1, 0, 2), new Vector3(1, 0, 3), 
                                                    new Vector3(1, 0, 4), new Vector3(1, 0, 5), new Vector3(1, 0, 6), 
                                                    new Vector3(1, 0, 7), new Vector3(1, 0, 8), new Vector3(1, 0, 9), 
                                                    new Vector3(1, 0, 10), new Vector3(2, 0, 0), new Vector3(2, 0, 1), 
                                                    new Vector3(2, 0, 2), new Vector3(2, 0, 3), new Vector3(2, 0, 4), 
                                                    new Vector3(2, 0, 5), new Vector3(2, 0, 6), new Vector3(2, 0, 7), 
                                                    new Vector3(2, 0, 8), new Vector3(2, 0, 9), new Vector3(2, 0, 10), 
                                                    new Vector3(3, 0, 0), new Vector3(3, 0, 1), new Vector3(3, 0, 2), 
                                                    new Vector3(3, 0, 3), new Vector3(3, 0, 4), new Vector3(3, 0, 5), 
                                                    new Vector3(3, 0, 6), new Vector3(3, 0, 7), new Vector3(3, 0, 8), 
                                                    new Vector3(3, 0, 9), new Vector3(3, 0, 10), new Vector3(4, 0, 0), 
                                                    new Vector3(4, 0, 1), new Vector3(4, 0, 2), new Vector3(4, 0, 3), 
                                                    new Vector3(4, 0, 4), new Vector3(4, 0, 5), new Vector3(4, 0, 6), 
                                                    new Vector3(4, 0, 7), new Vector3(4, 0, 8), new Vector3(4, 0, 9), 
                                                    new Vector3(4, 0, 10), new Vector3(5, 0, 0), new Vector3(5, 0, 1), 
                                                    new Vector3(5, 0, 2), new Vector3(5, 0, 3), new Vector3(5, 0, 4), 
                                                    new Vector3(5, 0, 5), new Vector3(5, 0, 6), new Vector3(5, 0, 7), 
                                                    new Vector3(5, 0, 8), new Vector3(5, 0, 9), new Vector3(5, 0, 10), 
                                                    new Vector3(6, 0, 0), new Vector3(6, 0, 1), new Vector3(6, 0, 2), 
                                                    new Vector3(6, 0, 3), new Vector3(6, 0, 4), new Vector3(6, 0, 5), 
                                                    new Vector3(6, 0, 6), new Vector3(6, 0, 7), new Vector3(6, 0, 8), 
                                                    new Vector3(6, 0, 9), new Vector3(6, 0, 10), new Vector3(7, 0, 0), 
                                                    new Vector3(7, 0, 1), new Vector3(7, 0, 2), new Vector3(7, 0, 3), 
                                                    new Vector3(7, 0, 4), new Vector3(7, 0, 5), new Vector3(7, 0, 6), 
                                                    new Vector3(7, 0, 7), new Vector3(7, 0, 8), new Vector3(7, 0, 9), 
                                                    new Vector3(7, 0, 10), new Vector3(8, 0, 0), new Vector3(8, 0, 1), 
                                                    new Vector3(8, 0, 2), new Vector3(8, 0, 3), new Vector3(8, 0, 4), 
                                                    new Vector3(8, 0, 5), new Vector3(8, 0, 6), new Vector3(8, 0, 7), 
                                                    new Vector3(8, 0, 8), new Vector3(8, 0, 9), new Vector3(8, 0, 10), 
                                                    new Vector3(9, 0, 0), new Vector3(9, 0, 1), new Vector3(9, 0, 2), 
                                                    new Vector3(9, 0, 3), new Vector3(9, 0, 4), new Vector3(9, 0, 5), 
                                                    new Vector3(9, 0, 6), new Vector3(9, 0, 7), new Vector3(9, 0, 8), 
                                                    new Vector3(9, 0, 9), new Vector3(9, 0, 10), new Vector3(10, 0, 0), 
                                                    new Vector3(10, 0, 1), new Vector3(10, 0, 2), new Vector3(10, 0, 3), 
                                                    new Vector3(10, 0, 4), new Vector3(10, 0, 5), new Vector3(10, 0, 6), 
                                                    new Vector3(10, 0, 7), new Vector3(10, 0, 8), new Vector3(10, 0, 9), 
                                                    new Vector3(10, 0, 10)};
        // Act
        Vector3[] positions = contiguousGridMeshGenerator.GenerateVertexGridPositions(meshSize);
        // Assert
        bool expected = true;
        for (int i = 0; i < expectedPositions.Length; i++)
        {
            expected = expected && (positions[i] == expectedPositions[i]);
        }
        Test.Assert(expected, "ContiguousGrid_GeneratePositions_MatchesExpectedPositions");
    }

    public void ContiguousGrid_GenerateMesh_MatchesExpectedTriangles()
    {
        // Arrange
        Vector2Int meshSize = new Vector2Int(10, 10);
        int[] expectedTriangles = new int[] {0, 1, 11, 11, 1, 12, 1, 2, 12, 12, 2, 13, 2, 3, 13, 13, 3, 14, 3, 4, 14, 14, 4, 15, 4, 5, 15, 
                                            15, 5, 16, 5, 6, 16, 16, 6, 17, 6, 7, 17, 17, 7, 18, 7, 8, 18, 18, 8, 19, 8, 9, 19, 19, 9, 20, 
                                            9, 10, 20, 20, 10, 21, 11, 12, 22, 22, 12, 23, 12, 13, 23, 23, 13, 24, 13, 14, 24, 24, 14, 25, 
                                            14, 15, 25, 25, 15, 26, 15, 16, 26, 26, 16, 27, 16, 17, 27, 27, 17, 28, 17, 18, 28, 28, 18, 29, 
                                            18, 19, 29, 29, 19, 30, 19, 20, 30, 30, 20, 31, 20, 21, 31, 31, 21, 32, 22, 23, 33, 33, 23, 34, 
                                            23, 24, 34, 34, 24, 35, 24, 25, 35, 35, 25, 36, 25, 26, 36, 36, 26, 37, 26, 27, 37, 37, 27, 38, 
                                            27, 28, 38, 38, 28, 39, 28, 29, 39, 39, 29, 40, 29, 30, 40, 40, 30, 41, 30, 31, 41, 41, 31, 42, 
                                            31, 32, 42, 42, 32, 43, 33, 34, 44, 44, 34, 45, 34, 35, 45, 45, 35, 46, 35, 36, 46, 46, 36, 47, 
                                            36, 37, 47, 47, 37, 48, 37, 38, 48, 48, 38, 49, 38, 39, 49, 49, 39, 50, 39, 40, 50, 50, 40, 51, 
                                            40, 41, 51, 51, 41, 52, 41, 42, 52, 52, 42, 53, 42, 43, 53, 53, 43, 54, 44, 45, 55, 55, 45, 56, 
                                            45, 46, 56, 56, 46, 57, 46, 47, 57, 57, 47, 58, 47, 48, 58, 58, 48, 59, 48, 49, 59, 59, 49, 60, 
                                            49, 50, 60, 60, 50, 61, 50, 51, 61, 61, 51, 62, 51, 52, 62, 62, 52, 63, 52, 53, 63, 63, 53, 64, 
                                            53, 54, 64, 64, 54, 65, 55, 56, 66, 66, 56, 67, 56, 57, 67, 67, 57, 68, 57, 58, 68, 68, 58, 69, 
                                            58, 59, 69, 69, 59, 70, 59, 60, 70, 70, 60, 71, 60, 61, 71, 71, 61, 72, 61, 62, 72, 72, 62, 73, 
                                            62, 63, 73, 73, 63, 74, 63, 64, 74, 74, 64, 75, 64, 65, 75, 75, 65, 76, 66, 67, 77, 77, 67, 78, 
                                            67, 68, 78, 78, 68, 79, 68, 69, 79, 79, 69, 80, 69, 70, 80, 80, 70, 81, 70, 71, 81, 81, 71, 82, 
                                            71, 72, 82, 82, 72, 83, 72, 73, 83, 83, 73, 84, 73, 74, 84, 84, 74, 85, 74, 75, 85, 85, 75, 86, 
                                            75, 76, 86, 86, 76, 87, 77, 78, 88, 88, 78, 89, 78, 79, 89, 89, 79, 90, 79, 80, 90, 90, 80, 91, 
                                            80, 81, 91, 91, 81, 92, 81, 82, 92, 92, 82, 93, 82, 83, 93, 93, 83, 94, 83, 84, 94, 94, 84, 95, 
                                            84, 85, 95, 95, 85, 96, 85, 86, 96, 96, 86, 97, 86, 87, 97, 97, 87, 98, 88, 89, 99, 99, 89, 100, 
                                            89, 90, 100, 100, 90, 101, 90, 91, 101, 101, 91, 102, 91, 92, 102, 102, 92, 103, 92, 93, 103, 
                                            103, 93, 104, 93, 94, 104, 104, 94, 105, 94, 95, 105, 105, 95, 106, 95, 96, 106, 106, 96, 107, 
                                            96, 97, 107, 107, 97, 108, 97, 98, 108, 108, 98, 109, 99, 100, 110, 110, 100, 111, 100, 101, 111,
                                            111, 101, 112, 101, 102, 112, 112, 102, 113, 102, 103, 113, 113, 103, 114, 103, 104, 114, 
                                            114, 104, 115, 104, 105, 115, 115, 105, 116, 105, 106, 116, 116, 106, 117, 106, 107, 117, 
                                            117, 107, 118, 107, 108, 118, 118, 108, 119, 108, 109, 119, 119, 109, 120};
        // Act
        Vector3[] positions = contiguousGridMeshGenerator.GenerateVertexGridPositions(meshSize);
        Mesh mesh = contiguousGridMeshGenerator.GenerateMesh(positions);
        int[] triangles = mesh.triangles;
        // Assert
        bool expected = true;
        for (int i = 0; i < expectedTriangles.Length; i++)
        {
            expected = expected && (triangles[i] == expectedTriangles[i]);
        }
        Test.Assert(expected, "ContiguousGrid_GenerateMesh_MatchesExpectedTriangles");
    }


    public void DiscreteGrid_GeneratePositions_MatchesExpectedPositions()
    {
        // Arrange
        Vector2Int meshSize = new Vector2Int(10, 10);
        Vector3[] expectedPositions = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 2), 
                                                    new Vector3(0, 0, 3), new Vector3(0, 0, 4), new Vector3(0, 0, 5), 
                                                    new Vector3(0, 0, 6), new Vector3(0, 0, 7), new Vector3(0, 0, 8), 
                                                    new Vector3(0, 0, 9), new Vector3(0, 0, 10), new Vector3(1, 0, 0), 
                                                    new Vector3(1, 0, 1), new Vector3(1, 0, 2), new Vector3(1, 0, 3), 
                                                    new Vector3(1, 0, 4), new Vector3(1, 0, 5), new Vector3(1, 0, 6), 
                                                    new Vector3(1, 0, 7), new Vector3(1, 0, 8), new Vector3(1, 0, 9), 
                                                    new Vector3(1, 0, 10), new Vector3(2, 0, 0), new Vector3(2, 0, 1), 
                                                    new Vector3(2, 0, 2), new Vector3(2, 0, 3), new Vector3(2, 0, 4), 
                                                    new Vector3(2, 0, 5), new Vector3(2, 0, 6), new Vector3(2, 0, 7), 
                                                    new Vector3(2, 0, 8), new Vector3(2, 0, 9), new Vector3(2, 0, 10), 
                                                    new Vector3(3, 0, 0), new Vector3(3, 0, 1), new Vector3(3, 0, 2), 
                                                    new Vector3(3, 0, 3), new Vector3(3, 0, 4), new Vector3(3, 0, 5), 
                                                    new Vector3(3, 0, 6), new Vector3(3, 0, 7), new Vector3(3, 0, 8), 
                                                    new Vector3(3, 0, 9), new Vector3(3, 0, 10), new Vector3(4, 0, 0), 
                                                    new Vector3(4, 0, 1), new Vector3(4, 0, 2), new Vector3(4, 0, 3), 
                                                    new Vector3(4, 0, 4), new Vector3(4, 0, 5), new Vector3(4, 0, 6), 
                                                    new Vector3(4, 0, 7), new Vector3(4, 0, 8), new Vector3(4, 0, 9), 
                                                    new Vector3(4, 0, 10), new Vector3(5, 0, 0), new Vector3(5, 0, 1), 
                                                    new Vector3(5, 0, 2), new Vector3(5, 0, 3), new Vector3(5, 0, 4), 
                                                    new Vector3(5, 0, 5), new Vector3(5, 0, 6), new Vector3(5, 0, 7), 
                                                    new Vector3(5, 0, 8), new Vector3(5, 0, 9), new Vector3(5, 0, 10), 
                                                    new Vector3(6, 0, 0), new Vector3(6, 0, 1), new Vector3(6, 0, 2), 
                                                    new Vector3(6, 0, 3), new Vector3(6, 0, 4), new Vector3(6, 0, 5), 
                                                    new Vector3(6, 0, 6), new Vector3(6, 0, 7), new Vector3(6, 0, 8), 
                                                    new Vector3(6, 0, 9), new Vector3(6, 0, 10), new Vector3(7, 0, 0), 
                                                    new Vector3(7, 0, 1), new Vector3(7, 0, 2), new Vector3(7, 0, 3), 
                                                    new Vector3(7, 0, 4), new Vector3(7, 0, 5), new Vector3(7, 0, 6), 
                                                    new Vector3(7, 0, 7), new Vector3(7, 0, 8), new Vector3(7, 0, 9), 
                                                    new Vector3(7, 0, 10), new Vector3(8, 0, 0), new Vector3(8, 0, 1), 
                                                    new Vector3(8, 0, 2), new Vector3(8, 0, 3), new Vector3(8, 0, 4), 
                                                    new Vector3(8, 0, 5), new Vector3(8, 0, 6), new Vector3(8, 0, 7), 
                                                    new Vector3(8, 0, 8), new Vector3(8, 0, 9), new Vector3(8, 0, 10), 
                                                    new Vector3(9, 0, 0), new Vector3(9, 0, 1), new Vector3(9, 0, 2), 
                                                    new Vector3(9, 0, 3), new Vector3(9, 0, 4), new Vector3(9, 0, 5), 
                                                    new Vector3(9, 0, 6), new Vector3(9, 0, 7), new Vector3(9, 0, 8), 
                                                    new Vector3(9, 0, 9), new Vector3(9, 0, 10), new Vector3(10, 0, 0), 
                                                    new Vector3(10, 0, 1), new Vector3(10, 0, 2), new Vector3(10, 0, 3), 
                                                    new Vector3(10, 0, 4), new Vector3(10, 0, 5), new Vector3(10, 0, 6), 
                                                    new Vector3(10, 0, 7), new Vector3(10, 0, 8), new Vector3(10, 0, 9), 
                                                    new Vector3(10, 0, 10)};
         // Act
        Vector3[] positions = contiguousGridMeshGenerator.GenerateVertexGridPositions(meshSize);
        // Assert
        bool expected = true;
        for (int i = 0; i < expectedPositions.Length; i++)
        {
            expected = expected && (positions[i] == expectedPositions[i]);
        }
        Test.Assert(expected, "DiscreteGrid_GeneratePositions_MatchesExpectedPositions");
    }

    public void DiscreteGrid_GenerateMesh_MatchesExpectedTriangles()
    {
        // Arrange
        Vector2Int meshSize = new Vector2Int(10, 10);
        int[] expectedTriangles = new int[] {0, 1, 2, 2, 1, 3, 4, 5, 6, 6, 5, 7, 8, 9, 10, 10, 9, 11, 12, 13, 14, 
                                            14, 13, 15, 16, 17, 18, 18, 17, 19, 20, 21, 22, 22, 21, 23, 24, 25, 26, 
                                            26, 25, 27, 28, 29, 30, 30, 29, 31, 32, 33, 34, 34, 33, 35, 36, 37, 38, 
                                            38, 37, 39, 40, 41, 42, 42, 41, 43, 44, 45, 46, 46, 45, 47, 48, 49, 50, 
                                            50, 49, 51, 52, 53, 54, 54, 53, 55, 56, 57, 58, 58, 57, 59, 60, 61, 62, 
                                            62, 61, 63, 64, 65, 66, 66, 65, 67, 68, 69, 70, 70, 69, 71, 72, 73, 74, 
                                            74, 73, 75, 76, 77, 78, 78, 77, 79, 80, 81, 82, 82, 81, 83, 84, 85, 86, 
                                            86, 85, 87, 88, 89, 90, 90, 89, 91, 92, 93, 94, 94, 93, 95, 96, 97, 98, 
                                            98, 97, 99, 100, 101, 102, 102, 101, 103, 104, 105, 106, 106, 105, 107, 108, 109, 110, 
                                            110, 109, 111, 112, 113, 114, 114, 113, 115, 116, 117, 118, 118, 117, 119, 120, 121, 122, 
                                            122, 121, 123, 124, 125, 126, 126, 125, 127, 128, 129, 130, 130, 129, 131, 132, 133, 134, 
                                            134, 133, 135, 136, 137, 138, 138, 137, 139, 140, 141, 142, 142, 141, 143, 144, 145, 146, 
                                            146, 145, 147, 148, 149, 150, 150, 149, 151, 152, 153, 154, 154, 153, 155, 156, 157, 158, 
                                            158, 157, 159, 160, 161, 162, 162, 161, 163, 164, 165, 166, 166, 165, 167, 168, 169, 170, 
                                            170, 169, 171, 172, 173, 174, 174, 173, 175, 176, 177, 178, 178, 177, 179, 180, 181, 182, 
                                            182, 181, 183, 184, 185, 186, 186, 185, 187, 188, 189, 190, 190, 189, 191, 192, 193, 194, 
                                            194, 193, 195, 196, 197, 198, 198, 197, 199, 200, 201, 202, 202, 201, 203, 204, 205, 206, 
                                            206, 205, 207, 208, 209, 210, 210, 209, 211, 212, 213, 214, 214, 213, 215, 216, 217, 218, 
                                            218, 217, 219, 220, 221, 222, 222, 221, 223, 224, 225, 226, 226, 225, 227, 228, 229, 230, 
                                            230, 229, 231, 232, 233, 234, 234, 233, 235, 236, 237, 238, 238, 237, 239, 240, 241, 242, 
                                            242, 241, 243, 244, 245, 246, 246, 245, 247, 248, 249, 250, 250, 249, 251, 252, 253, 254, 
                                            254, 253, 255, 256, 257, 258, 258, 257, 259, 260, 261, 262, 262, 261, 263, 264, 265, 266, 
                                            266, 265, 267, 268, 269, 270, 270, 269, 271, 272, 273, 274, 274, 273, 275, 276, 277, 278, 
                                            278, 277, 279, 280, 281, 282, 282, 281, 283, 284, 285, 286, 286, 285, 287, 288, 289, 290, 
                                            290, 289, 291, 292, 293, 294, 294, 293, 295, 296, 297, 298, 298, 297, 299, 300, 301, 302, 
                                            302, 301, 303, 304, 305, 306, 306, 305, 307, 308, 309, 310, 310, 309, 311, 312, 313, 314, 
                                            314, 313, 315, 316, 317, 318, 318, 317, 319, 320, 321, 322, 322, 321, 323, 324, 325, 326, 
                                            326, 325, 327, 328, 329, 330, 330, 329, 331, 332, 333, 334, 334, 333, 335, 336, 337, 338,
                                            338, 337, 339, 340, 341, 342, 342, 341, 343, 344, 345, 346, 346, 345, 347, 348, 349, 350, 
                                            350, 349, 351, 352, 353, 354, 354, 353, 355, 356, 357, 358, 358, 357, 359, 360, 361, 362, 
                                            362, 361, 363, 364, 365, 366, 366, 365, 367, 368, 369, 370, 370, 369, 371, 372, 373, 374, 
                                            374, 373, 375, 376, 377, 378, 378, 377, 379, 380, 381, 382, 382, 381, 383, 384, 385, 386, 
                                            386, 385, 387, 388, 389, 390, 390, 389, 391, 392, 393, 394, 394, 393, 395, 396, 397, 398, 
                                            398, 397, 399};
        // Act
        Vector3[] positions = discreteGridMeshGenerator.GenerateVertexGridPositions(meshSize);
        Mesh mesh = discreteGridMeshGenerator.GenerateMesh(positions);
        int[] triangles = mesh.triangles;
        // Assert
        bool expected = true;
        for (int i = 0; i < expectedTriangles.Length; i++)
        {
            expected = expected && (triangles[i] == expectedTriangles[i]);
        }
        Test.Assert(expected, "DiscreteGrid_GenerateMesh_MatchesExpectedTriangles");
    }
}
