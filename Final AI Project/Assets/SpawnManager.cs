using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpawnType
{
    TypeOne,
    TypeTwo,
    TypeThree
}

[System.Serializable]
public class SpawnConfig
{
    public GameObject prefab;
    public float spawnRate = 2f;
    public Transform spawnPoint; // Punto de spawn espec?fico
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private SpawnConfig[] spawnConfigs = new SpawnConfig[3];

    private const int MAX_TANKS_PER_TYPE = 1;

    private Dictionary<SpawnType, List<GameObject>> activeObjects = new Dictionary<SpawnType, List<GameObject>>();
    private Dictionary<SpawnType, Coroutine> activeSpawners = new Dictionary<SpawnType, Coroutine>();

    void Start()
    {
        // Verificar que todos los puntos de spawn est?n asignados
        foreach (SpawnConfig config in spawnConfigs)
        {
            if (config.spawnPoint == null)
            {
                Debug.LogError("?Falta asignar un punto de spawn!");
                return;
            }
        }

        // Inicializar las listas de objetos activos
        foreach (SpawnType type in System.Enum.GetValues(typeof(SpawnType)))
        {
            activeObjects[type] = new List<GameObject>();
        }

        // Iniciar todos los spawners
        StartSpawner(SpawnType.TypeOne);
        StartSpawner(SpawnType.TypeTwo);
        StartSpawner(SpawnType.TypeThree);
    }

    public void StartSpawner(SpawnType type)
    {
        if (!activeSpawners.ContainsKey(type))
        {
            activeSpawners[type] = StartCoroutine(SpawnRoutine(type));
        }
    }

    public void StopSpawner(SpawnType type)
    {
        if (activeSpawners.ContainsKey(type))
        {
            StopCoroutine(activeSpawners[type]);
            activeSpawners.Remove(type);
        }
    }

    private void RemoveDestroyedObjects(SpawnType type)
    {
        activeObjects[type].RemoveAll(obj => obj == null);
    }

    private IEnumerator SpawnRoutine(SpawnType type)
    {
        SpawnConfig config = spawnConfigs[(int)type];

        while (true)
        {
            RemoveDestroyedObjects(type);

            if (activeObjects[type].Count < MAX_TANKS_PER_TYPE)
            {
                // Usar la posici?n y rotaci?n del punto de spawn
                GameObject newObject = Instantiate(
                    config.prefab,
                    config.spawnPoint.position,
                    config.spawnPoint.rotation
                );

                activeObjects[type].Add(newObject);

                TankIdentifier identifier = newObject.AddComponent<TankIdentifier>();
                identifier.type = type;
            }

            yield return new WaitForSeconds(config.spawnRate);
        }
    }
}

public class TankIdentifier : MonoBehaviour
{
    public SpawnType type;
}