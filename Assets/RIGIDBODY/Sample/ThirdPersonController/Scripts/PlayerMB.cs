using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.LightAnchor;

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
        mouseX = CharacterInputData.MouseMove().x;
    }

    float mouseX;
    float targetRotation;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 1f)]
    public float RotationSmoothTime = 0.12f;
    [Range(0.0f, 1f)]
    public float rotationThreshold = 0.05f;
    private float rotationVelocity;
    private float lastTargetRotation;
    private void FixedUpdate()
    {
        if (CharacterInputData.MouseMove().sqrMagnitude > rotationThreshold)
        {
            lastTargetRotation = Mathf.Atan2(CharacterInputData.MouseMove().x, CharacterInputData.MouseMove().y) * Mathf.Rad2Deg +
                                 Camera.main.transform.eulerAngles.y;
        }
        // Smoothly rotate towards the last valid target rotation
        float rotation = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            lastTargetRotation,
            ref rotationVelocity,
            RotationSmoothTime
        );

        // Apply rotation
        base.m_Rigidbody.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        // Movement.
        if (CharacterInputData.Move() == Vector2.zero && m_GroundCheck == false)
            return;
        Vector3 direction = (transform.forward * CharacterInputData.Move().y + transform.right * CharacterInputData.Move().x);
        var finalPos = transform.position + (direction.normalized * Time.fixedDeltaTime * m_MoveSpeed);
        base.m_Rigidbody.Move(finalPos, Quaternion.identity);
    }
}
