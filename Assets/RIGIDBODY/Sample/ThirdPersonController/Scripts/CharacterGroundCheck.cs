using UnityEngine;

public class CharacterGroundCheck : MonoBehaviour
{
    [field: SerializeField] public bool OnGround { get; private set; }

    #region Monobehaviour callbacks
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        OnGround = false;
    }
    #endregion
}
