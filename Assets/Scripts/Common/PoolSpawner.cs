using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class PoolSpawner : MonoBehaviour
    {
        private ObjectPooler objectPoolerInstance;
        private int queueSize;

        private void Start()
        {
            objectPoolerInstance = ObjectPooler.instance;
        }

        // Instancia um objeto da pool. Se a pool estiver vazia, instancia um novo objeto.
        // Object pooling é um padrão de projeto que ajuda a substituir o instantiate e
        // destroy de objetos em tempo de execução. Isso ajuda a reduzir o uso de memória
        // e o uso do garbage collector.
        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            queueSize = objectPoolerInstance.poolDictionary[tag].Count;
            if (queueSize == 0)
            {
                foreach (ObjectPooler.Pool pool in objectPoolerInstance.pools)
                {
                    if (pool.tag == tag)
                    {
                        objectPoolerInstance.newObj(pool, objectPoolerInstance.poolDictionary[tag]);
                    }
                }
            }
            GameObject objectToSpawn = objectPoolerInstance.poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            return objectToSpawn;
        }

        // Retorna um objeto para a pool ao invés de destruí-lo.
        public void ReturnToPool(string tag, GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            objectPoolerInstance.poolDictionary[tag].Enqueue(objectToReturn);
        }
    }
}