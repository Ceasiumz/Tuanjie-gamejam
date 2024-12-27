using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public float py=3f;
    public float ps=3f;
    public GameObject Button;
    void Start()
    {
        Button = GetComponentInChildren<Button>().gameObject;
        Button.SetActive(false);
        DynamicEventBus.Subscribe("RoundEndEvent", OnRoundEnd);
    }

    public void OnRoundEnd(){
        Debug.Log("Round ended");
        //transform.DOMove(new Vector3(py, 0, 0), 0.1f).SetEase(Ease.OutBack);
        //transform.DOScale(new Vector3(ps, ps, ps), 0.1f).SetEase(Ease.OutBack);
        //Button.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
