using System.Collections;
using UnityEngine;

public class BeginState : State {
    public BeginState(PlayerCombat system) : base(system) {
    }

    public override IEnumerator Start()  {
        yield break;
    }

    public override bool Update() {
        return true;
    }

    public override IEnumerator End() {
        yield break;
    }
}