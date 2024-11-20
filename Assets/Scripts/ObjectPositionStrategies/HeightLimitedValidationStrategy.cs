using UnityEngine;

[CreateAssetMenu(fileName = "HeightLimitedValidationStrategy", menuName = "SO/PositionValidationStrategy/HeightLimitedValidationStrategy")]
public class HeightLimitedValidationStrategy : PositionValidationStrategy
{
    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float maxHeight;
    public override bool IsValidPosition(RaycastHit hit)
    {
        float height = hit.point.y;
        return height >= minHeight && height <= maxHeight;
    }
}