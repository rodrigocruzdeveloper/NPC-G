using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int maxInGame;
    [SerializeField] private float interval;
    [SerializeField] private float xMin, xMax, yMin, yMax;

    private int currentInGame;

    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(interval);

        if (currentInGame < maxInGame)
        {
            GameObject e = Instantiate(enemy);

            Vector2 p = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

            e.transform.position = p;

            AddEnemy();
        }

        StartCoroutine(Spawn());
    }

    public void AddEnemy()
    {
        if(currentInGame < maxInGame)
        {
            currentInGame++;
        }
    }

    public void RemoveEnemy()
    {
        if (currentInGame > 0)
        {
            currentInGame--;
        }
    }
}
