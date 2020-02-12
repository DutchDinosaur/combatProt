using UnityEngine;

public class InputManager : MonoBehaviour{
    public static InputManager instance;

    public bool Controller;

    public Vector2 LeftStick;
    public Vector2 RightStick;
    public bool Sprint;
    public float hold;
    public float lower;

    void Awake() {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;
    }

    private void Update() {
        if (Controller) {
            LeftStick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            RightStick = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
            Sprint = Input.GetButton("sprint");
            hold = Input.GetAxis("hold");
            lower = Input.GetButton("lower") ? 1 : 0;
        }
        else {
            LeftStick = new Vector2(boolToFloat(Input.GetKey(KeyCode.D)) - boolToFloat(Input.GetKey(KeyCode.A)), boolToFloat(Input.GetKey(KeyCode.W)) - boolToFloat(Input.GetKey(KeyCode.S)));
            RightStick = new Vector2(Input.mousePosition.x / Screen.width * 2 - 1, Input.mousePosition.y / Screen.height * 2 - 1);
            Sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            hold = Input.GetKey(KeyCode.E) ? 1 : 0;
            lower = Input.GetKey(KeyCode.Q) ? 1 : 0;
        }
    }

    float boolToFloat(bool b) {
        return b ? 1 : 0;
    }
}