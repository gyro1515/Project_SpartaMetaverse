using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSort : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private void Update()
    {
        // Y값이 낮을수록 위에 그리기 (Y값이 작을수록 order가 높아짐)
        spriteRenderers.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            // 정렬 순서대로 Sorting Order 지정
            spriteRenderers[i].sortingOrder = 15 + i; // 기본 값 15
        }
    }
}
