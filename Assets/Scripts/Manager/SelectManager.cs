using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [NonSerialized] public MemberNode SelectNode;

    public static SelectManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void OnClick(MemberNode node)
    {
        if (SelectNode == node)
        {
            SelectNode?.MemberUI.SelectHighlight(false);
            SelectNode = null;
        }
        else
        {
            SelectNode?.MemberUI.SelectHighlight(false);
            SelectNode = node;
            SelectNode?.MemberUI.SelectHighlight(true);
        }
    }
    public void Update()
    {
        if (SelectNode?.gameObject.activeSelf == false)
            SelectNode = null;
    }
}
