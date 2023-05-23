using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUtilities : MonoBehaviour
{
    [SerializeField]
    static List<CardLogic> cards;

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<CardLogic>(FindObjectsOfType<CardLogic>());
        initializeLayers();
    }

    static void initializeLayers(int start = 0)
    {
        for(int i = start; i<cards.Count; i++)
        {
            cards[i].ChangeLayer(i);
        }
    }

    // I have my doubts about how efficient this is, but it works for now. Could probably store the index in each card?
    public static void SetFocus(CardLogic card)
    {
        int index = cards.IndexOf(card);

        CardLogic toChange = card;
        while(toChange != null)
        {
            cards.Remove(toChange);
            cards.Add(toChange);
            toChange = toChange.GetChild();
        }        

        initializeLayers(index);
    }

    public static void StackCards(CardLogic card)
    {
        BoxCollider2D collider = card.gameObject.GetComponent<BoxCollider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.NameToLayer("Card"));
        List<Collider2D> results = new List<Collider2D>();
            
        if(collider.OverlapCollider(filter, results) > 0)
        {
            int i = 0;
            bool found = false;

            while(i<results.Count && !found)
            {
                CardLogic target = results[i].gameObject.GetComponent<CardLogic>();
                if (!target.CheckIfChildOf(card) && target.GetChild() == null)
                {
                    card.SetParent(target);
                    found = true;
                }
                else
                {
                    i++;
                } 
            }
        }
    }
}
