using Unity.VisualScripting;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    InputSystem inputSystem;

    private void Awake()
    {
        inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        inputSystem?.Enable();
    }

    private void OnDisable()
    {
        inputSystem?.Disable();
    }

    void Start()
    {
        
    }

    public Vector2 Move() => inputSystem.Player.Move.ReadValue<Vector2>();

}
