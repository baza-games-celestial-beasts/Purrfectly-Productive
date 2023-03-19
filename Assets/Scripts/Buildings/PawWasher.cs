using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawWasher : MonoBehaviour, IInteractable {

    public Vector2 popupPos => transform.position + Vector3.up * 1.2f;

    public void Interact() {
        Game.inst.player.pawsAreWashed = true;
    }

    public string InteractText() {
        if(Game.inst.player.pawsAreWashed) {
            return "Лапки уже помыты\n:D";
        } else {
            return "Помыть лапки [E]";
        }
    }
}
