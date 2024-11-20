using UnityEngine;

[CreateAssetMenu(fileName = "SlopeLimitedValidationStrategy", menuName = "SO/PositionValidationStrategy/SlopeLimitedValidationStrategy")]
public class SlopeLimitedValidationStrategy : PositionValidationStrategy
{
    [SerializeField, Range(0f, 90f)]
    private float minSlope;
    [SerializeField, Range(0f, 90f)]
    private float maxSlope;
    public override bool IsValidPosition(RaycastHit hit)
    {
        float slopeAngle = Mathf.Abs(Vector3.Angle(hit.normal, Vector3.up));
        return slopeAngle >= minSlope && slopeAngle <= maxSlope;
    }
}
