using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyClear : MonoBehaviour
{
    public List<GameObject> enemyList;
    public int enemyCount;
    public GameObject gateToRemove;
    public TextMeshProUGUI objective;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemyCount = enemyList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        objective.text = "Enemies Left: " + enemyCount; 
        foreach(GameObject enemy in enemyList)
        {
            if (enemy == null)
            {
                enemyCount--;
                enemyList.Remove(enemy);
            }
        }
        if(enemyCount == 0)
        {
            gateToRemove.SetActive(false);
        }


    }
}
