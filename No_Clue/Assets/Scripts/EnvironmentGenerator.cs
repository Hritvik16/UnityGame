using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public GameObject terrain;

    public GameObject[] objects;
    private List<GameObject> objectList = new List<GameObject>();
    public GameObject gameObjectDestroyer;
    private GameObjectDestroyer gameObjectDestroyerObject;

    // Start is called before the first frame update
    void Start()
    {

        gameObjectDestroyerObject = gameObjectDestroyer.GetComponent<GameObjectDestroyer>();
        //GenerateNTressinArea(15, -7, -10, -1.8f, -5);
        GenerateNObjects(70);
        for (int x = 0; x < objectList.Count; x++)
        {
            for (int i = x + 1; i < objectList.Count; i++) 
            {
                float x0 = objectList[i].transform.position.x;
                float y0 = objectList[i].transform.position.y;
                float z0 = objectList[i].transform.position.z;

                float x1 = objectList[x].transform.position.x;
                float y1 = objectList[x].transform.position.y;
                float z1 = objectList[x].transform.position.z;

                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    Destroy(objectList[x]);
                    objectList.Remove(objectList[x]);
                }
            }
        }
        for(int i = 0; i < objectList.Count; i++)
        {
            Physics.IgnoreCollision(objectList[i].GetComponent<Collider>(), terrain.GetComponent<MeshCollider>());
            objectList[i].GetComponent<Collider>().enabled = true;
        }
    }
    void GenerateNObjects(int n)
    {
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        for (int i = 0; i < n; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            objectList.Add(Instantiate(objects[Random.Range(0, objects.Length)], position, rotation));

        }
    }

    // Update is called once per frame
    void Update()
    {

        int y = gameObjectDestroyerObject.getCount();
        if (y >= 10 && objectList.Count < 200)
        {
            GenerateNObjects(20);
        }
    }
}
