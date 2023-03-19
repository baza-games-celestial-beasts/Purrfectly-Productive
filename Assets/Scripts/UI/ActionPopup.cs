using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionPopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;

    private float lastTimeDraw;

    public void Draw(Vector2 pos, string text) {
        if(text == null) {            
            textMesh.enabled = false;
            return;
        }

        lastTimeDraw = Time.time;

        textMesh.enabled = true;
        textMesh.text = text;

        transform.position = pos;
    }

    public void ClearIfNoRecentDraws() {
        if(Time.time > lastTimeDraw + 0.2f) {
            Draw(Vector2.zero, null);
        }
    }

}
