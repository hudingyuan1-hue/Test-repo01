using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    bool isCharacterClicked = false;
    [SerializeField] Vector2 BeginPoint = new Vector2(960, 0);
    [SerializeField] Vector2 EndPoint = new Vector2(0, 0);
    [SerializeField] RectTransform characterCardRect;
    [SerializeField] float moveSpeed = 5f;

    Coroutine moveCoroutine;
    Coroutine moveBackCoroutine;
    void OnMouseDown()
    {
        if (isCharacterClicked) return;
        isCharacterClicked = true;
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        if (moveBackCoroutine != null) StopCoroutine(moveBackCoroutine);
        moveCoroutine = StartCoroutine(MoveCharacterCard());
    }

    IEnumerator MoveCharacterCard()
    {
        while (Vector2.Distance(characterCardRect.anchoredPosition, EndPoint) > 0.1f)
        {
            characterCardRect.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(characterCardRect.GetComponent<RectTransform>().anchoredPosition, EndPoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    void MoveOut()
    {
        if (!isCharacterClicked) return;
        isCharacterClicked = false;
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        if (moveBackCoroutine != null) StopCoroutine(moveBackCoroutine);
        moveBackCoroutine = StartCoroutine(MoveCharacterCardBack());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsClickingPopupObject())
            {
                MoveOut();
            }
        }
    }
    private bool IsClickingPopupObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
        if (hit)
        {
            // 如果点击的是触发弹窗的物体，不关闭
            return hit.transform == transform || hit.transform == characterCardRect;
        }
        return false;
    }
    IEnumerator MoveCharacterCardBack()
    {
        while (Vector2.Distance(characterCardRect.anchoredPosition, BeginPoint) > 0.1f)
        {
            characterCardRect.anchoredPosition = Vector2.Lerp(characterCardRect.anchoredPosition, BeginPoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
