using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable {

    public Transform ladderStart;
    public Transform ladderEnd;
    private Rigidbody2D rb;
    private Collider2D col;

    public Vector2 popupPos => ladderStart.position;

    private bool isClimbedByPlayer;
    private float ladderLockTimer;

    public Vector2 GetPlayerPosition(float percent) {
        return Vector2.Lerp(ladderStart.position, ladderEnd.position, percent);
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void Interact() {
        if (Time.time < ladderLockTimer + 0.1f)
            return;

        ladderLockTimer = Time.time;

        if (isClimbedByPlayer) {            
            isClimbedByPlayer = false;
            Game.inst.player.movement.targetLadder = null;
            Game.inst.player.movement.SetMoveState(Player.PlayerMoveState.Walk);

            rb.isKinematic = false;
            col.enabled = true;
        } else {
            isClimbedByPlayer = true;
            Game.inst.player.movement.targetLadder = this;
            Game.inst.player.movement.SetMoveState(Player.PlayerMoveState.LadderClimb);

            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            col.enabled = false;
        }       
    }

    public string InteractText() {
        if (isClimbedByPlayer) {
            return "Слезть [E]";
        } else {
            return "Залезть [E]";
        }
    }
}
