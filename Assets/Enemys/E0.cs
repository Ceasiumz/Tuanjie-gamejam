using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E0 : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] int Ehealth;
    [SerializeField] int EmaxPointsInHand;
    [SerializeField] int Eattack;
    DialogueTreeController startDialogue;
    DialogueTreeController dieDialogue;
    private void Awake()
    {
        health = Ehealth;
        maxPointsInHand = EmaxPointsInHand;
        attack = Eattack;
    }

    void Start()
    {
       dieDialogue = GetComponent<DialogueTreeController>();
        startDialogue = GetComponent<DialogueTreeController>();
        eA = GetComponentInParent<EnemyAchive>();
        startDialogue.StartDialogue();
    }
  
    public override void OnTurnStart()
    {
        //Debug.Log("E0 Draw");
        TurnManager.Instance.EnemyTurn_draw();
    }

    public override void OnTurnDraw()
    {
        CowardDraw();
    }
    public override void OnPlayerDraw(){
        //Debug.Log("Nowa");
    }
}
