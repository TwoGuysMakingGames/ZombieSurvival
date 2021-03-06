﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    #region Singleton
    private static WavesManager _instance;

    public static WavesManager Instance
    {
        get
        { return _instance; }

        private set
        { _instance = value; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("More than one instance of a singleton detected");
            return;
        }
        _instance = this;
    }
    #endregion

    public List<GameObject> zombieSpawnPoints;

    public List<GameObject> zombiesOnMap;
    //public UnityEngine.Object zombiePrefab;
    public GameObject zombiePrefab;

    public int timeToSpawnZombie;
    private bool spawnedZombie;

    public int roundNumber;
    public int remainingZombies;

    Text remainingZombiesText;
    Text roundNumberText;

    void Start()
    {
        zombieSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("ZombieSpawnPoint"));
        if (zombieSpawnPoints.Count == 0)
            Debug.Log("Error, no ZombieSpawnPoints found");

        zombiesOnMap.AddRange(GameObject.FindGameObjectsWithTag("Zombie"));
        remainingZombies = zombiesOnMap.Count;
        timeToSpawnZombie = 3;

        remainingZombiesText = GameObject.Find("UI/RemainingZombies").GetComponent<Text>();
        roundNumberText = GameObject.Find("UI/RoundNumber").GetComponent<Text>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.J)) // spawn zombie
        //{
        //    int random = Random.Range(0, zombieSpawnPoints.Count);
        //    GameObject activeSP = zombieSpawnPoints[random];

        //    Instantiate(zombiePrefab, new Vector2(activeSP.transform.position.x, activeSP.transform.position.y), Quaternion.identity);
        //}

        if (Input.GetKeyDown(KeyCode.K)) // kill zombie
        {
            KillZombie();
        }
        if (zombiesOnMap.Count == 0)
        {
            if (remainingZombies == 0)
            {
                StartNewRound();
            }
        }

        if (spawnedZombie == false)
        {
            spawnedZombie = true;
            Invoke("SpawnZombie", timeToSpawnZombie);
        }

        remainingZombiesText.text = "Remaining Zombies " + remainingZombies;
        roundNumberText.text = "Round: " + roundNumber;
    }

    void StartNewRound()
    {
        ++roundNumber;
        remainingZombies = roundNumber * 3;
    }

    void SpawnZombie()
    {
        if (remainingZombies > 0 && remainingZombies > zombiesOnMap.Count && zombiesOnMap.Count < roundNumber * 2)
        {
            int random = Random.Range(0, zombieSpawnPoints.Count);
            GameObject activeSP = zombieSpawnPoints[random];
            GameObject newZombie = Instantiate(zombiePrefab, new Vector2(activeSP.transform.position.x, activeSP.transform.position.y), Quaternion.identity);
            zombiesOnMap.Add(newZombie);
        }
        spawnedZombie = false;
    }
    void KillZombie()
    {
        if (zombiesOnMap.Count > 0)
        {
            Destroy(zombiesOnMap[zombiesOnMap.Count - 1]);
            zombiesOnMap.RemoveAt(zombiesOnMap.Count - 1);
            --remainingZombies;
        }
    }

    //IEnumerator ExecuteAfterTime(float time)
    //{
    //    yield return new WaitForSeconds(time);

    //    // Code to execute after the delay
    //}
}
