using UnityEngine;

public class InputManager : MonoBehaviour{
    public static InputManager input;
    
    public Vector2 LeftStick;
    public Vector2 RightStick;

    void Awake() {
        if (input != null)
            GameObject.Destroy(input);
        else
            input = this;
    }

    private void Update() {
        
    }
}