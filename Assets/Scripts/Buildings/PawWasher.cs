using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawWasher : MonoBehaviour, IInteractable
{
    [SerializeField] private float swearTimer = 15f;
    public Vector2 popupPos => transform.position + Vector3.up * 1.2f;
    
    private void Start()
    {
        Game.inst.player.pawsAreWashed = false;
    }

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

    private IEnumerator SmearPaws()
    {
        yield return new WaitForSeconds(swearTimer);
        Game.inst.player.pawsAreWashed = false;
    }
}
