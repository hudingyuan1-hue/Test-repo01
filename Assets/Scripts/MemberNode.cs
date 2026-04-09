using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MemberNode", menuName = "MemberNode/MemberNode")]
public class MemberNode : ScriptableObject
{
    public Sprite Sprite;
    [NonSerialized] public GameObject gameObject;
    public RectTransform rect => gameObject.GetComponent<RectTransform>();
    [NonSerialized] public MemberNode Father;
    [NonSerialized] public MemberUI MemberUI;

    [NonSerialized] public int Level;

    public Member Member;

    public List<MemberNode> Children;

    GameManager gameManager => GameManager.Instance;

    public bool CanRemove1(MemberNode node)
    {
        if (node == TreeManager.Root) return false;
        if (node.Father == this) return true;
        return false;
    }

    public void Remove1(MemberNode node)
    {
        Children.Remove(node);
        int? index = this != TreeManager.Root ? Father.Children.IndexOf(this) : null;
        if (index != null && index >= 0)
        {
            Father.Children[(int)index] = node;
        }
        node.Father = Father;
        node.Children.AddRange(Children);
        node.ChildrenFatherUpdate();
        gameObject.SetActive(false);
        GameManager.Instance.Score += Member.AddScore;
        gameManager.StartCoroutine(Father.ChildrenAbilityUpdate(Member.CurrentAbility));
        node.LevelUpdate();
        gameManager.Step++;
    }
    public bool CanRemove2(MemberNode node)
    {
        if (node?.Father == Father && node != this) return true;
        return false;
    }
    public void Remove2(MemberNode node)
    {
        Father.Children.Remove(this);
        node.Children.AddRange(Children);
        node.Father = Father;
        node.ChildrenFatherUpdate();
        gameObject.SetActive(false);
        GameManager.Instance.Score += Member.AddScore;
        gameManager.StartCoroutine(Father.ChildrenAbilityUpdate(Member.CurrentAbility));
        node.LevelUpdate();
        gameManager.Step++;
    }

    public void Remove3()
    {
        Father.Children.Remove(this);
        gameObject.SetActive(false);
        GameManager.Instance.Score += Member.AddScore;
        gameManager.StartCoroutine(Father.ChildrenAbilityUpdate(Member.CurrentAbility));
        LevelUpdate();
        gameManager.Step++;
    }

    public IEnumerator ChildrenAbilityUpdate(float currentAbility)
    {
        yield return gameManager.StartCoroutine(ChildrenAbilityUpdatePart(currentAbility));

        IEnumerator ChildrenAbilityUpdatePart(float currentAbility)
        {
            if (Children.Count == 0)
            {
                Member.CurrentAbility += currentAbility;
                yield break;
            }
            var sum = 0f;
            foreach (var child in Children)
            {
                sum += child.Member.MaxAbility;
            }
            foreach (var child in Children)
            {
                child.Member.CurrentAbility += currentAbility * child.Member.MaxAbility / sum;
            }
            var flag = false;
            var temp = 0f;
            List<Func<IEnumerator>> dieAction = new();
            foreach (var child in Children)
            {
                if (child.AbilityCheck())
                {
                    flag = true;
                    temp += child.Member.CurrentAbility;
                    dieAction.Add(child.Die);
                }
            }
            foreach (var action in dieAction)
            {
                yield return gameManager.StartCoroutine(action());
            }
            if (flag) yield return gameManager.StartCoroutine(ChildrenAbilityUpdatePart(temp));
        }

        if (AbilityCheck())
        {
            yield return gameManager.StartCoroutine(Die());
            if (this == TreeManager.Root) yield break;
            yield return gameManager.StartCoroutine(Father.ChildrenAbilityUpdate(Member.CurrentAbility));
        }
    }

    public bool AbilityCheck()
    {
        if (Member.CurrentAbility > Member.MaxAbility)
        {
            return true;
        }
        return false;
    }

    TreeManager treeManager => TreeManager.Instance;

    IEnumerator Die()
    {
        if (this == TreeManager.Root) yield break;
        if (treeManager.InDieProcess) yield break;
        treeManager.InDieProcess = true;
        yield return new WaitForSeconds(1f);
        Father.Children.Remove(this);
        Father.Children.AddRange(Children);
        Father.ChildrenFatherUpdate();
        gameObject.SetActive(false);
        GameManager.Instance.Score -= Member.DeadScore;
        treeManager.InDieProcess = false;
    }

    public void LevelUpdate()
    {
        if (this != TreeManager.Root)
            Level = Father.Level + 1;
        foreach (var child in Children)
            child.LevelUpdate();
    }

    public void ChildrenFatherUpdate()
    {
        foreach (var child in Children)
        {
            child.Father = this;
        }
    }

    [NonSerialized] public float Width;
}