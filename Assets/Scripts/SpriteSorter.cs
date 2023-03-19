using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    public float pivotY;
    public SpriteRenderer[] sprites;

    private void Update() {
        int layer = (int)(-transform.position.y * 10f);

        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].sortingOrder = layer;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * pivotY - Vector3.right, transform.position + Vector3.up * pivotY + Vector3.right);
    }
}
