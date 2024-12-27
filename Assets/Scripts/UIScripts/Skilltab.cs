using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skilltab : MonoBehaviour
{
    public GameObject skillTab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (skillTab != null)
            {
                skillTab.SetActive(!skillTab.activeSelf);
            }
        }
    }
   public void ColickinBuuton()
    {
            if (skillTab != null)
            {
                skillTab.SetActive(!skillTab.activeSelf);
            }
    }
    public void ColickoutBuuton()
    {
        if (skillTab != null)
        {
            skillTab.SetActive(!skillTab.activeSelf);
        }
    }
}
