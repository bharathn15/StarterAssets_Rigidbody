using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public Vector2 Position;
}

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
#if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }

    public Vector2 Move() => inputSystem.Player.Move.ReadValue<Vector2>();

#if UNITY_EDITOR
    public Vector2 MouseMove() => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
#endif

}
