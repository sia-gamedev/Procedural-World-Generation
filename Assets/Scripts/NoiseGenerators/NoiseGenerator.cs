using UnityEngine;

public abstract class NoiseGenerator : ScriptableObject
{
    public abstract float[] GetHeightNoiseValues(Vector3[] points);
}
