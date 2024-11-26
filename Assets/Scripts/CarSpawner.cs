using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject gold;
    public GameObject carPrefab;
    public GameObject carPrefab2;
    public GameObject carPrefab3;
    public GameObject carPrefab4;
    public GameObject carPrefab5;
    
    private GameObject[] spawns = new GameObject[5];
    private Transform[] spawnPoints = new Transform[4];
    
    public float spawnInterval = 2f; 
    public float minSpawnInterval = 1f; 
    public float spawnDecreaseRate = 0.1f; 
    public float carSpeed = 25f; 
    
   
    
    private int carPosition;
    private int rangeForSecondCar;
    
    void Start()
    {
        rangeForSecondCar = 30;
        spawns[0] = carPrefab;
        spawns[1] = carPrefab2;
        spawns[2] = carPrefab3;
        spawns[3] = carPrefab4;
        spawns[4] = carPrefab5;
        spawnPoints[0] = gameObject.transform.GetChild(0).transform;
        spawnPoints[1] = gameObject.transform.GetChild(1).transform;
        spawnPoints[2] = gameObject.transform.GetChild(2).transform;
        spawnPoints[3] = gameObject.transform.GetChild(3).transform;

        StartCoroutine(SpawnCar(spawnInterval));
        StartCoroutine(secondCarRateChanger());
    }

    private void Update()
    {
        float x = player.transform.position.x - 310;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    IEnumerator SpawnCar(float time)
    {
        while (true)
        {
            GameObject chosenCar = getRandomCar();
            Transform spawnPoint = getRandomPosition();
            GameObject spawnedCar = Instantiate(chosenCar, spawnPoint.position, chosenCar.transform.rotation);

            StartCoroutine(MoveCar(spawnedCar));
            Destroy(spawnedCar, 10);

           
            StartCoroutine(secondCarSpawn());

            
            time = Mathf.Max(minSpawnInterval, time - spawnDecreaseRate);

            
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator MoveCar(GameObject car)
    {
        while (car != null)
        {
            car.transform.Translate(-car.transform.right * carSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private Transform getRandomPosition()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        carPosition = randomIndex; 
        return spawnPoints[randomIndex];
    }

    private GameObject getRandomCar()
    {
        int randomIndex = Random.Range(0, spawns.Length);
        return spawns[randomIndex];
    }

    private IEnumerator secondCarSpawn()
    {
        int secondCarChance = Random.Range(0, 100);
        if (secondCarChance < rangeForSecondCar)
        {
            
            
            float delay = Random.Range(0f, 3f);
            Debug.Log("Yeni araba %" + rangeForSecondCar + " şansla spawn olacak. " + delay + " saniye bekleniyor");
            yield return new WaitForSeconds(delay);
            Debug.Log("yeni araba spawn oldu");
            
            GameObject chosenCar2 = getRandomCar();
            int randomIndex2 = Random.Range(0, spawnPoints.Length);
            while (randomIndex2 == carPosition)
            {
                randomIndex2 = Random.Range(0, spawnPoints.Length);
            }

            GameObject spawnedCar2 = Instantiate(chosenCar2, spawnPoints[randomIndex2].position, chosenCar2.transform.rotation);
            StartCoroutine(MoveCar(spawnedCar2));
            Destroy(spawnedCar2, 10);
        }
    }

    private IEnumerator secondCarRateChanger()
    {
        while (true)
        {
            rangeForSecondCar = Mathf.Min(rangeForSecondCar + 5, 100); 
            Debug.Log("Yeni arabanın spawn olma ihtimali: %" + rangeForSecondCar);
        
            yield return new WaitForSeconds(10); 
        }
    }
}
