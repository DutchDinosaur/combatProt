using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour{
    public static InputManager instance;

    public bool Controller;

    public Vector2 walkVector;
    public Vector2 AimVector;
    [SerializeField] float AimPortion;
    public bool Sprint;
    public float hold;
    public float lower;
    public bool jump;

    void Awake() {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) switchInput();
        if (Controller) {
            walkVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            AimVector = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
            Sprint = Input.GetButton("sprint");
            hold = Input.GetAxis("hold");
            lower = Input.GetButton("lower") ? 1 : 0;
            jump = Input.GetButton("jump");
            if (Input.GetButtonDown("escape")) SceneManager.LoadScene(0);
        }
        else {
            walkVector = new Vector2((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0), (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0));
            Vector2 aim = new Vector2((Input.mousePosition.x / Screen.width * 2 - 1), Input.mousePosition.y / Screen.height * 2 - 1) * AimPortion;
            AimVector = (aim.magnitude < 1) ? aim : aim.normalized;
            Sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            hold = (Input.GetKey(KeyCode.E) || Input.GetMouseButton(0)) ? 1 : 0;
            lower = (Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1)) ? 1 : 0;
            jump = Input.GetKey(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
        }
    }

    void switchInput() {
        if (Controller) Controller = false;
        else Controller = true;
    }
}