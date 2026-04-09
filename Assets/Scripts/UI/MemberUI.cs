using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MemberUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] BezierLineRenderer bezierLineRenderer;
    [SerializeField] Image image;
    [SerializeField] GameObject InfoBox;
    [NonSerialized] MemberNode memberNode;
    [SerializeField] TMP_Text MaxStressText;
    [SerializeField] Scrollbar scrollbar;

    SelectManager selectManager => SelectManager.Instance;

    public void Init(MemberNode _memberNode)
    {
        InfoBox.SetActive(false);
        InfoBox.transform.localScale = Vector2.one / TreeManager.Instance.MinTimes;
        memberNode = _memberNode;
        memberNode.MemberUI = this;
    }

    public void Update()
    {
        // NameText.text = "Name: " + memberNode.Member.Name;
        if (memberNode == TreeManager.Root || memberNode == null)
        {
            return;
        }
        image.sprite = memberNode.Sprite;
        MaxStressText.text = "压力值：" + memberNode.Member.MaxAbility.ToString();
        scrollbar.value = memberNode.Member.CurrentAbility / memberNode.Member.MaxAbility;
        DrawLine();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        selectManager.OnClick(memberNode);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoBox.SetActive(false);
    }

    Color defaultSpriteColor = Color.white;
    Color highlightSpriteColor = Color.yellow;

    public void SelectHighlight(bool highlight)
    {
        image.color = highlight ? highlightSpriteColor : defaultSpriteColor;
    }

    Vector3 oldPosition;
    Vector3 oldFatherPosition;

    void DrawLine()
    {
        if (memberNode.Father == TreeManager.Root)
        {
            bezierLineRenderer.enabled = false;
            return;
        }
        bezierLineRenderer.enabled = true;
        if (!memberNode.rect || !memberNode.Father.rect)
        {
            Debug.LogError("Transform is null");
            return;
        }
        if (oldPosition != memberNode.rect.position || oldFatherPosition != memberNode.Father.rect.position)
        {
            oldPosition = memberNode.rect.position;
            oldFatherPosition = memberNode.Father.rect.position;
            bezierLineRenderer.SetPosition(0, (Vector2)memberNode.rect.position);
            bezierLineRenderer.SetPosition(1, new Vector2(memberNode.rect.position.x, (memberNode.rect.position.y + memberNode.Father.rect.position.y) / 2));
            bezierLineRenderer.SetPosition(2, new Vector2(memberNode.Father.rect.position.x, (memberNode.rect.position.y + memberNode.Father.rect.position.y) / 2));
            bezierLineRenderer.SetPosition(3, (Vector2)memberNode.Father.rect.position);
        }
    }
}