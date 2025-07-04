using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMB : CharacterMB
{
    [SerializeField] float m_Speed;

    private void Start()
    {
        if(base.m_Rigidbody == null)
        {
            base.m_Rigidbody = GetComponent<Rigidbody>();
        }
    }

    [SerializeField] Vector2 input;

    private void Update()
    {
        if (input == null) return;
        input = base.characterInput.Move();
    }

    private void FixedUpdate()
    {   
        if(input == Vector2.zero) return; 
        Vector3 direction = (transform.forward * input.y + transform.right * input.x) * Time.fixedDeltaTime * m_Speed;
        // base.m_Rigidbody.linearVelocity = new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(direction.x, 0, direction.z);
        // base.m_Rigidbody.Move(new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(direction.x, 0, direction.z);
    }
}
