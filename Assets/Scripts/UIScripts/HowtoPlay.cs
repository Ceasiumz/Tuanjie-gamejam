using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HowtoPlay : MonoBehaviour
{
    public GameObject showPannel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseEnter()
    {
        showPannel.SetActive(true);
    }
    public void OnMouseExit()
    {
        showPannel.SetActive(false);
    }
}
