using UnityEngine;

[CreateAssetMenu(fileName = "CombinedValidationStrategy", menuName = "SO/PositionValidationStrategy/CombinedValidationStrategy")]
public class CombinedValidationStrategy : PositionValidationStrategy
{
    [SerializeField]
    private PositionValidationStrategy[] strategies;
    public override bool IsValidPosition(RaycastHit hit)
    {
        bool valid = true;
        foreach (PositionValidationStrategy strategy in strategies)
        {
            valid = valid && strategy.IsValidPosition(hit);
        }
        return valid;
    }
}
