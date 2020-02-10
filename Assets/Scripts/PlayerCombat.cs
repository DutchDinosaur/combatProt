using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    private State currentState;

    private void Update() {
        currentState.Update();
    }

    public void SetState(State state) {
        StartCoroutine(currentState.End());
        currentState = state;
        StartCoroutine(currentState.Start());
    }
}