using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    [SerializeField] private Transform catCenter;
    [SerializeField] private IInteractable targetInteractable;

    private float _interactPressTime;

    private List<Collider2D> _interactablesInArea;
    private Collider2D[] _colliderCache = new Collider2D[64];

    Vector2 pos => catCenter.position;
    float interactRadius = 0.4f;

    private void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.E)) {
            interactPressTime = Time.time + 0.1f;
        }
        */

        targetInteractable = null;

        int colliderCount = Physics2D.OverlapCircleNonAlloc(pos, interactRadius, _colliderCache);

        List<IInteractable> interactables = _colliderCache
            .Take(colliderCount)
            .Where(x => x != null && x.GetComponent<IInteractable>() != null)
            .OrderBy(x => (pos -  (Vector2)x.transform.position).magnitude)
            .Select(x => x.GetComponent<IInteractable>())
            .ToList();

        if(interactables.Count > 0) {
            //Debug.Log("C " + interactables.Count);

            targetInteractable = interactables[0];
            Game.inst.actionPopup.Draw(targetInteractable.popupPos, targetInteractable.InteractText());

            if (Input.GetKeyDown(KeyCode.E)) {
                targetInteractable.Interact();
            }
        } else {
            //Debug.Log("B");
            //Game.inst.actionPopup.Draw(Vector2.zero, null);
        }        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, interactRadius);
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
    string InteractText();
}
