using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    [SerializeField]
    private bool runOnce;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisionMulplier = 5f;
        _spriteRenderer.sortingOrder = (int)(-(transform.position.y - transform.localPosition.y) * precisionMulplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
