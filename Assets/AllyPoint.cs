using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public int allyPoints;

    public static AllyPoint Instance;
    public HorizontalCardHolder holder;

    Text text;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        text = GetComponent<Text>();
        holder.DrawEvent.AddListener(AddPoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (allyPoints == 0)
        {
            foreach (var card in holder.cards)
            {
                allyPoints += card.points;
            }
        }
        text.text = "Ally Points: " + allyPoints;
    }

    public void AddPoints(Card card)
    {
        allyPoints += card.points;
    }
}
