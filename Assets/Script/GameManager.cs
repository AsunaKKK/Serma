using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] obstacle;
    public GameObject scoreSpawn;
    public GameObject environment;

    public Transform spawnPoint;
    public Transform environmentPoint;


    public GameObject player;
    public GameObject playGameButton;
    public GameObject scoretext;
    public GameObject upBotton;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        scoretext.SetActive(false);
        upBotton.SetActive(false);
        playGameButton.SetActive(true);
        SpawnEnvirnments();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Random obstacle and Score
            float waitTime = Random.Range(0.5f, 4f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(obstacle[Random.Range(0,10)], spawnPoint.position, Quaternion.identity);
            Instantiate(scoreSpawn, spawnPoint.position, Quaternion.identity);
        }
    }

    IEnumerator SpawnEnvironment()
    {
        while (true)
        {
            float waitTimeEnvi = 6;
            yield return new WaitForSeconds(waitTimeEnvi);
            Instantiate(environment, spawnPoint.position, Quaternion.identity);
        }
    }
    public void StartObstacles()
    {
        player.SetActive(true);
        scoretext.SetActive(true);
        upBotton.SetActive(true);
        playGameButton.SetActive(false);
        StartCoroutine("SpawnObstacles");
    }

    public void SpawnEnvirnments()
    {
        StartCoroutine("SpawnEnvironment");
    }
 
}
