using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemysSkill : MonoBehaviour
{
    public GameObject E0;
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E6;
    public GameObject E7;
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
        if(E0.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "�����ڵ����е��������û����֪������������������һ���Ȼ����ƿ��£�" +
                "����ȴֻ����ѭ���ܹ������Ǹ�ʮ�ֺ�սʤ�Ķ���";
        }
        else if (E1.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "����������Ů��"  + "\n" +
                "���ܣ�����֮��" + "\n" + "Ч���������ÿ�γ鵽����2-9ʱ�������ٳ�һ����";
        }
        else if(E2.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "������ߵ��������ӣ������������볤��"  + "\n" +
                "���ܣ���������" + "\n" + "Ч�������ÿ�γ���ʱ���ͻ��ȡһ�ŵ���Ϊ2�Ĳݻ���"; 
        }
        else if(E3.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "һ������,�Ĵ��ε�������,�ṥ��һ������" + "\n" +
                "���ܣ����ղ���" + "\n" + "Ч�������������һ���ƣ�������¸��غϵĳ���֮ǰ���������ű���ǵ���";
        }
        else if(E6.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "��·����..." + "\n" +
                "���ܣ�������ˡ" + "\n" + "Ч�����ܵ��˺��������һ���غ�ÿ�����Ƶĵ���+1�����Ч������������";
        }
        else if(E7.activeSelf)
        {
            showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "�˵�����ϵĴ��ڣ��ܶ��˲²��������ֻص����ף�������û����֪��" + "\n" +
                "���ܣ�������ɾ����" + "\n" + "Ч����ֻҪ��������д�������һ�Ų���ʹ�Լ����Ƶ��ƣ����������л����Щ���е���������";
        }
    }
    public void OnMouseExit()
    {
        showPannel.GetComponentInChildren<TextMeshProUGUI>().text = "";
        showPannel.SetActive(false);
    }
}
