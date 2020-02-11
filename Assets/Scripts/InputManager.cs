using UnityEngine;

public class InputManager : MonoBehaviour{
    public static InputManager instance;
    
    public Vector2 LeftStick;
    public Vector2 RightStick;

    void Awake() {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;
    }

    private void Update() {
        LeftStick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        RightStick = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
    }
}