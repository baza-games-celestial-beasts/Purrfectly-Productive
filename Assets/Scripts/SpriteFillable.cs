using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFillable : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    [Range(0f,1f)]
    public float fillAmount;

    private void Update() {
        float minX = spriteRend.sprite.textureRect.xMin / (float) spriteRend.sprite.texture.width;
        float maxX = spriteRend.sprite.textureRect.xMax / (float)spriteRend.sprite.texture.width;
        float uv = Mathf.Lerp(minX, maxX, fillAmount);

        spriteRend.material.SetFloat("_FillAmount", uv);
    }

}
