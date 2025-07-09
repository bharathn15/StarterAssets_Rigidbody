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
    }

    [Tooltip("How fast the character turns to face mouse direction")]
    [Range(0.0f, 5f)]
    public float m_RotationSmoothTime = 0.12f;
    [Range(0.0f, 5f)]
    public float m_RotationThreshold = 0.05f;
    private float rotationVelocity;
    private float lastTargetRotation;
    private float currentRotation;
    private void FixedUpdate()
    {
        // ROTATION
        if (CharacterInputData.MouseMove().sqrMagnitude > m_RotationThreshold)
        {
            // Last rotation.
            lastTargetRotation = Mathf.Atan2(CharacterInputData.MouseMove().x, CharacterInputData.MouseMove().y) * Mathf.Rad2Deg +
                                 Camera.main.transform.eulerAngles.y;

            currentRotation = Mathf.LerpAngle(transform.eulerAngles.y, lastTargetRotation, Time.fixedDeltaTime * m_RotateSpeed);

            base.m_Rigidbody.transform.rotation = Quaternion.Euler(0.0f, currentRotation, 0.0f);
        }

        // MOVEMENT
        if (CharacterInputData.Move() != Vector2.zero && m_GroundCheck)
        {
            Vector3 direction = (transform.forward * CharacterInputData.Move().y + transform.right * CharacterInputData.Move().x);
            var finalPos = transform.position + (direction.normalized * Time.fixedDeltaTime * m_MoveSpeed);
            base.m_Rigidbody.Move(finalPos, Quaternion.Euler(0.0f, currentRotation, 0.0f));
        }

    }
}
