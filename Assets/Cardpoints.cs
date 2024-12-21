using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cardpoints : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    CardVisual cardVisual;
    void Start()
    {
        text = GetComponent<Text>();
        cardVisual = GetComponentInParent<CardVisual>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = cardVisual.parentCard.points.ToString();
    }
}
