using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public Vector2 Position;
}

public class CharacterInputConstants
{
    public static float MouseThreshold = 1.0f;
}

public class CharacterInput : MonoBehaviour
{
    [SerializeField] Vector2 MousePos;

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
        #region MOUSE CURSOR HANDLING.
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        Cursor.lockState = CursorLockMode.Confined;
#endif
        #endregion
    }

    private void Update()
    {
        MousePos = MouseMove();
/*        #region MOUSE CURSOR HANDLING.
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if(MiddleMouseButton && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState= CursorLockMode.Confined;
        }
#endif
        #endregion*/
    }

    public Vector2 Move() => inputSystem.Player.Move.ReadValue<Vector2>();
    
    #region PC OR EDITOR
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    public bool LeftMouseButton => Input.GetMouseButtonDown(0);
    // Scroll Wheel
    public bool MiddleMouseButton => Input.GetMouseButtonDown(2);
    public bool RightMouseButton => Input.GetMouseButtonDown(1);
    public Vector2 MouseMove() => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
#endif
    #endregion

}
