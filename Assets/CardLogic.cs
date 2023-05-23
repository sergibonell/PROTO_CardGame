using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEditor;

public class CardLogic : MonoBehaviour, IDraggable
{
    [SerializeField] CardUnityEvent grabEvent;
    [SerializeField] CardUnityEvent releaseEvent;
    [SerializeField] CardLogic parent;
    [SerializeField] CardLogic child;
    SortingGroup group;

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.name);
    }

    private void Awake()
    {
        group = GetComponent<SortingGroup>();
    }

    private void OnEnable()
    {
        grabEvent.AddListener(CardUtilities.SetFocus);
        releaseEvent.AddListener(CardUtilities.StackCards);
    }

    private void OnDisable()
    {
        grabEvent.RemoveAllListeners();
        releaseEvent.RemoveAllListeners();
    }

    public void ChangeLayer(int i)
    {
        group.sortingOrder = i;
    }

    public int GetCurrentLayer()
    {
        return group.sortingOrder;
    }

    public void SetParent(CardLogic card)
    {
        parent = card;
        transform.SetParent(card.transform);
        transform.localPosition = Vector3.down;

        card.SetChild(this);
    }

    public void SetChild(CardLogic card)
    {
        child = card;
    }

    public void RemoveParent()
    {
        parent.SetChild(null);

        parent = null;
        transform.SetParent(transform.root);
    }

    public bool CheckIfChildOf(CardLogic card)
    {
        if (parent == card)
            return true;
        else if (parent == null)
            return false;
        else
            return parent.CheckIfChildOf(card);
    }

    public CardLogic GetParent()
    {
        return parent;
    }

    public CardLogic GetChild()
    {
        return child;
    }

    public void OnGrab()
    {
        if(parent != null) RemoveParent();
        grabEvent.Invoke(this);
    }

    public void OnRelease()
    {
        releaseEvent.Invoke(this);
    }
}
