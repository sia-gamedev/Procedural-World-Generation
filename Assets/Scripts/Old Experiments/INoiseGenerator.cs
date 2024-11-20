using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseGenerator
{
    public Vector3[] PopulateWithNoiseValues(Grid2D grid2D);
}
