using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : Singleton<TreeManager>
{
    [SerializeField] float nodeWidth;
    public MemberNode RootNode;
    public static MemberNode Root;

    [NonSerialized] public bool InDieProcess = false;

    public float MinTimes;

    public void Start()
    {
        content.localScale = Vector2.one * MinTimes;
        DrawLineAction = null;
        Root = MemberNode.CreateInstance<MemberNode>();
        Root.Member = new();
        Root.Level = -1;
        Root.Member.Name = "Root";
        InitTree(ref RootNode);
        Root.Children = new List<MemberNode>() { RootNode };
        FatherInit(Root);
        Root.LevelUpdate();
        DrawTree(Root, 0);
        DrawLineAction?.Invoke();
    }

    public void Clean(MemberNode node)
    {
        if (node != Root)
        {
            if (node.gameObject)
                Destroy(node.gameObject);
            node.gameObject = null;
        }
        foreach (var child in node.Children)
        {
            Clean(child);
        }
    }

    void InitTree(ref MemberNode node)
    {
        node = Instantiate(node);
        if (node.Children.Count == 0)
            return;
        for (int i = 0; i < node.Children.Count; i++)
        {
            var child = node.Children[i];
            InitTree(ref child);
            node.Children[i] = child;
        }
    }

    public void FatherInit(MemberNode node)
    {
        if (node.Children.Count == 0) return;
        node.ChildrenFatherUpdate();
        foreach (var child in node.Children)
            FatherInit(child);
    }

    public float DrawTree(MemberNode node, float correctionX)
    {
        if (node.Children.Count == 0)
        {
            node.Width = nodeWidth;
            DrawTreeNode(node, correctionX);
            return nodeWidth;
        }
        float childrenWidth = 0;
        for (int i = 0; i < node.Children.Count; i++)
        {
            childrenWidth += DrawTree(node.Children[i], correctionX + childrenWidth);
        }
        node.Width = childrenWidth;
        DrawTreeNode(node, correctionX);
        return node.Width;
    }

    [SerializeField] ScrollRect scrollRect;

    Transform content => scrollRect.content;

    [SerializeField] GameObject NodePrefab;

    [SerializeField] GameObject ParentPrefab;

    List<Transform> ParentTransforms = new();

    public void DrawTreeNode(MemberNode node, float correctionX)
    {
        if (node == Root) return;
        while (node.Level >= ParentTransforms.Count)
        {
            var transform = Instantiate(ParentPrefab, content).transform;
            var parent = transform.transform.GetChild(0).transform;
            ParentTransforms.Add(parent);
        }
        if (!node.gameObject)
        {
            if (node == null)
                Debug.LogError("Node is null");
            node.gameObject = Instantiate(NodePrefab, ParentTransforms[node.Level]);
            node.gameObject.GetComponent<MemberUI>().Init(node);
        }
        node.rect.SetParent(ParentTransforms[node.Level]);
        node.rect.anchoredPosition = new Vector2(correctionX + node.Width / 2, 0);
    }
    Action DrawLineAction;
    public void Update()
    {
        if (Root.Children.Count == 0) return;
        FatherInit(Root);
        Root.LevelUpdate();
        DrawTree(Root, 0);
    }
}