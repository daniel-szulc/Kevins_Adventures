using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class w01l14_enemy_spawner : MonoBehaviour
{
    public GameObject enemy01prefab;
    public GameObject enemy03prefab;
    public GameObject enemy04prefab;
    public bool stopped = false;
    private IEnumerator newEnemies;
    public Enemy_Boss_01_Controller _Boss;
    void Start()
    {
        newEnemies = NewEnemies();
        StartCoroutine(newEnemies);
    }


    public void StopSpawn()
    {
        StopCoroutine(newEnemies);
        stopped = true;
    }
    
    IEnumerator NewEnemies()
    {
        Vector3 addTurtle = new Vector3(0,2.5f,0);
        Vector3 addHedgehog = new Vector3(0,7,0);
        while (!stopped)
        {
            GameObject _enemy3 = (GameObject) Instantiate(enemy03prefab, transform.position+addTurtle, transform.rotation);
            Change03(_enemy3);
            yield return new WaitForSeconds(3);
           GameObject _enemy = (GameObject) Instantiate(enemy01prefab, transform.position, transform.rotation);
          Change01(_enemy);
           yield return new WaitForSeconds(3);
           GameObject _enemy1 = (GameObject) Instantiate(enemy01prefab, transform.position, transform.rotation);
           Change01(_enemy1);
           yield return new WaitForSeconds(10);
           GameObject _enemy2 = (GameObject) Instantiate(enemy01prefab, transform.position, transform.rotation);
           Change01(_enemy2);
           yield return new WaitForSeconds(15);
           GameObject _enemy5 = (GameObject) Instantiate(enemy01prefab, transform.position, transform.rotation);
           Change01(_enemy5);
           yield return new WaitForSeconds(40);
           if (_Boss.Live < 5)
           {
               GameObject _enemy6 = (GameObject) Instantiate(enemy04prefab, transform.position+addHedgehog, transform.rotation);
               _enemy6.GetComponent<EnemyWalking>().ignoreGroundDetection = true;
               yield return new WaitForSeconds(10);
           }
        }
    }

    void Change01(GameObject enemy)
    {
        enemy.GetComponent<EnemyWalking>().ignoreGroundDetection = true;
        enemy.GetComponent<SpriteResolver>().SetCategoryAndLabel("Enemy01castle", "normal");
    }
    
    void Change03(GameObject enemy)
    {
        enemy.GetComponent<EnemyWalking>().ignoreGroundDetection = true;
        enemy.GetComponentInChildren<SpriteResolver>().SetCategoryAndLabel("Enemy03castle", "normal");
    }
    
}
