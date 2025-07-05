using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMB : CharacterMB
{
    [SerializeField] float m_MoveSpeed      = 4;
    [SerializeField] float m_RotateSpeed    = 4;
    [Space(20)]
    [SerializeField] CharacterGroundCheck m_GroundCheck;

    public CharacterInput CharacterInputData;
    private void Start()
    {
        if(base.m_Rigidbody == null)
        {
            base.m_Rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        if (CharacterInputData.Move() == Vector2.zero && m_GroundCheck == false)
        {
            m_MoveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        // Movement.
        if (CharacterInputData.Move() == Vector2.zero && m_GroundCheck == false)
            return;
        Vector3 direction = (transform.forward * CharacterInputData.Move().y + transform.right * CharacterInputData.Move().x);
        var finalPos = transform.position + (direction.normalized * Time.fixedDeltaTime * m_MoveSpeed);
        base.m_Rigidbody.Move(finalPos, Quaternion.identity);
    }
}
