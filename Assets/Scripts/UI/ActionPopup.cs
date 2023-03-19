using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionPopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;

    public void Draw(Vector2 pos, string text) {
        if(text == null) {            
            textMesh.enabled = false;
            return;
        }

        textMesh.enabled = true;
        textMesh.text = text;

        transform.position = pos;
    }

}
