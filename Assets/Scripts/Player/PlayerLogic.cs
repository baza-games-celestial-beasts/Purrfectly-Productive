using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public IInteractable targetInteractable;

    private float interactPressTime;

    private List<Collider2D> interactablesInArea;
    private Collider2D[] colliderCache;

    Vector2 pos => transform.position;

    private void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.E)) {
            interactPressTime = Time.time + 0.1f;
        }
        */
       
        int colliderCount = Physics2D.OverlapCircleNonAlloc(pos, 2f, colliderCache);

        targetInteractable = null;
        List<IInteractable> interactables = colliderCache
            .Where(x => x.GetComponent<IInteractable>() != null)
            .OrderBy(x => x.ClosestPoint(pos).magnitude)
            .Select(x => x.GetComponent<IInteractable>())
            .ToList();

        if(interactables.Count > 0) {
            targetInteractable = interactables[0];
            Game.inst.actionPopup.Draw(targetInteractable.popupPos, "[E]");

            if (Input.GetKeyDown(KeyCode.E)) {
                targetInteractable.Interact();
            }
        } else {
            Game.inst.actionPopup.Draw(targetInteractable.popupPos, null);
        }        
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(pos, 2f);
    }

    /*
    private void OnTriggerStay2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponentInParent<IInteractable>();

        if(interactable != null) {
            if(Time.time < interactPressTime) {
                interactPressTime = -100f;

                interactable.Interact();
            }
        }
    }
    */

}

public interface IInteractable {
    Vector2 popupPos { get; }
    void Interact();
}
