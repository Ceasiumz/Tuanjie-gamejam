using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class AllyPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public int allyPoints;

    public static AllyPoint Instance;
    public int lim = 21;
    public HorizontalCardHolder holder;
    public UnityEvent DrawOutEvent;
    Text text;
    private bool isDead = false;

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
    void Update()// revise ally points
    {
        int tempAllyPoints = 0;
        foreach (Card card in holder.cards)
        {
            tempAllyPoints += card.points;
        }
        if (tempAllyPoints != allyPoints)
        {
            allyPoints = tempAllyPoints;
            DrawOutTest();
        }
        text.text = "Ally Points: " + allyPoints;
    }

    public void DrawOutTest()// check if the ally points are over lim
    {
        if (isDead == false)
        {
            if (allyPoints == lim)
            {
            }
            else if (allyPoints > lim)
            {
                DrawOutEvent.Invoke();
                holder.DrawButton.SetActive(false);
                Invoke("Restart", 1f);
            }
        }
    }
    public void Restart()// latecheck if the ally points are over lim
    {
        if (allyPoints > lim)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else{
            holder.DrawButton.SetActive(true);
        }
    }


    public void AddPoints(Card card)
    {

    }
}
