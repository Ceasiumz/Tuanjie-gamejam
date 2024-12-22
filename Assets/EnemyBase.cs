using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyAchive enemyAchive;
    void Start()
    {
        enemyAchive = GetComponentInParent<EnemyAchive>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
