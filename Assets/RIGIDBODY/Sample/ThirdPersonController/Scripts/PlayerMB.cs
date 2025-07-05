using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMB : CharacterMB
{
    [SerializeField] float m_MoveSpeed;
    [SerializeField] float m_Torque = 400;

    [SerializeField] CharacterGroundCheck m_GroundCheck;

    /*[SerializeField] AnimationCurve m_ForwardBackSpeedCurve;
    [SerializeField] AnimationCurve m_LeftRightSpeedCurve;
    [SerializeField] AnimationCurve m_DiagonalSpeedCurve;*/

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
        // m_Speed = Mathf.Lerp(0, 12, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Player Rotation.
        var mouseX = CharacterInputData.MouseMove().x * Time.fixedDeltaTime * m_Torque;
        base.m_Rigidbody.AddTorque(transform.up * mouseX);

        // Player Movement.
        if(CharacterInputData.Move() == Vector2.zero && m_GroundCheck == false) 
            return;
        Vector3 direction = (transform.forward * CharacterInputData.Move().y + transform.right * CharacterInputData.Move().x);
        var finalPos = transform.position + (direction * Time.fixedDeltaTime * m_MoveSpeed);
        base.m_Rigidbody.Move(finalPos, Quaternion.identity);
    }

    void Move()
    {

    }
}
