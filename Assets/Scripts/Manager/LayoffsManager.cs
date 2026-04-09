using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class LayoffsManager : MonoBehaviour
{
    [SerializeField] Button layoffsButton;
    [SerializeField] Button PromotionButton;
    [SerializeField] Button MergerButton;
    [SerializeField] Button CancelButton;
    [NonSerialized] public bool IsInLayoffs = false;
    SelectManager selectManager => SelectManager.Instance;

    MemberNode selectedNode => selectManager.SelectNode;

    public void Start()
    {
        layoffsButton.onClick.AddListener(Layoffs);
        PromotionButton.onClick.AddListener(Promotion);
        MergerButton.onClick.AddListener(Merger);
    }

    TreeManager treeManager => TreeManager.Instance;

    void Update()
    {
        if (!IsInLayoffs)
        {
            if (selectedNode != null)
            {
                if (treeManager.InDieProcess)
                {
                    layoffsButton.gameObject.SetActive(false);
                    PromotionButton.gameObject.SetActive(false);
                    MergerButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(true);
                }
                else
                {
                    layoffsButton.gameObject.SetActive(true);
                    PromotionButton.gameObject.SetActive(false);
                    MergerButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
            }
            else
            {
                layoffsButton.gameObject.SetActive(false);
                PromotionButton.gameObject.SetActive(false);
                MergerButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
            }
        }
        else
        {
            layoffsButton.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsInLayoffs)
            {
                IsInLayoffs = false;
                LayoffsTargetNode = null;
            }
        }
    }

    MemberNode LayoffsTargetNode;

    void Layoffs()
    {
        if (selectedNode == null) return;
        if (selectedNode.Children.Count == 0)
        {
            selectedNode.Remove3();
        }
        else
        {
            IsInLayoffs = true;
            LayoffsTargetNode = selectedNode;
            StartCoroutine(InLayoffs());
        }
    }

    void Promotion()
    {
        LayoffsTargetNode.Remove1(selectedNode);
        IsInLayoffs = false;
        LayoffsTargetNode = null;
    }

    void Merger()
    {
        LayoffsTargetNode.Remove2(selectedNode);
        IsInLayoffs = false;
        LayoffsTargetNode = null;
    }

    MemberNode CanBeSelectedNode = null;

    IEnumerator InLayoffs()
    {
        CanBeSelectedNode = null;
        while (IsInLayoffs)
        {
            if (LayoffsTargetNode.CanRemove1(selectedNode))
            {
                CanBeSelectedNode = selectedNode;
                PromotionButton.gameObject.SetActive(true);
                MergerButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
            }
            else if (LayoffsTargetNode.CanRemove2(selectedNode))
            {
                CanBeSelectedNode = selectedNode;
                MergerButton.gameObject.SetActive(true);
                PromotionButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
            }
            else
            {
                CanBeSelectedNode = null;
                PromotionButton.gameObject.SetActive(false);
                MergerButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(true);
            }
            yield return null;
        }
    }
}
