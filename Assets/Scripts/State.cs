using System.Collections;

public abstract class State {
    protected readonly PlayerCombat System;

    public State(PlayerCombat system) {
        System = system;
    }

    public virtual IEnumerator Start() {
        yield break;
    }

    public virtual bool Update() {
        return true;
    }

    public virtual IEnumerator End() {
        yield break;
    }
}