using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour{

    [SerializeField] bool Controller;
    [SerializeField] float AimPortion;
    [SerializeField] UnityEvent grab;
    [SerializeField] UnityEvent release;

    [SerializeField] GameObject controlUI;

    [HideInInspector] public static InputManager instance;

    [HideInInspector] public Vector2 walkVector;
    [HideInInspector] public Vector2 AimVector;

    [HideInInspector] public bool Sprint;
    [HideInInspector] public float hold;
    [HideInInspector] public float lower;
    [HideInInspector] public bool jump;

    void Awake() {
        if (instance != null)
            GameObject.Destroy(instance);
        instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) switchInput();

        if (Input.GetKeyDown(KeyCode.E)) grab.Invoke();
        if (Input.GetKeyDown(KeyCode.Q)) release.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (controlUI.active) controlUI.SetActive(false);
            else controlUI.SetActive(true);
        }

        if (Controller) {
            walkVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            AimVector = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
            Sprint = Input.GetButton("sprint");
            hold = Input.GetAxis("hold");
            lower = Input.GetButton("lower") ? 1 : 0;
            jump = Input.GetButton("jump");
            if (Input.GetButtonDown("escape") || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(0);
        }
        else {
            walkVector = new Vector2((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0), (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0));
            Vector2 aim = new Vector2((Input.mousePosition.x / Screen.width * 2 - 1) * (Screen.width/ Screen.height), Input.mousePosition.y / Screen.height * 2 - 1) * AimPortion;
            AimVector = (aim.magnitude < 1) ? aim : aim.normalized;
            Sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            hold = (Input.GetMouseButton(0)) ? 1 : 0;
            lower = (Input.GetMouseButton(1)) ? 1 : 0;
            jump = Input.GetKey(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(0);
        }
    }

    void switchInput() {
        if (Controller) Controller = false;
        else Controller = true;
    }
}