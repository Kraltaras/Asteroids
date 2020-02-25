using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnTime;

    private GameObject _enemy;
    private bool _spawning;

    private readonly int[] SpawnChances = { 5, 8, 10, 12, 13 }; //mathematical progress of enemies' spawn chances

    // Start is called before the first frame update
    void Start()
    {
        _spawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawning)
        {
            float cameraWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height; //screen width in unity points
            _enemy = Instantiate(enemies[SpawnID()]) as GameObject;
            float horizontalSpawn = Random.Range((-cameraWidth / 2) + 0.5f, (cameraWidth / 2) - 0.5f);
            _enemy.transform.position = new Vector3(horizontalSpawn, Camera.main.orthographicSize, 0);
            _enemy.transform.Rotate(0, 0, 180);
            _spawning = false;
            StartCoroutine(ReloadSpawn());
            spawnTime -= 0.02f; //each spawn time between spawns is descreased to make game harder
            if (spawnTime < 0.5f) spawnTime = 0.5f; //until this threshold
        }
    }

    private int SpawnID() //spawn enemy type based on spawn chance
    {
        int chance = Random.Range(1, SpawnChances[SpawnChances.Length - 1] + 1);
        int id = 0;
        while(SpawnChances[id] < chance)
        {
            id++;
        }
        return id;
    }

    private IEnumerator ReloadSpawn()
    {
        yield return new WaitForSeconds(spawnTime);
        _spawning = true;
    }
}
