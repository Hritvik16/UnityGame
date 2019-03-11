using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public GameObject terrain;

    public GameObject[] objects;
    private List<GameObject> objectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void GenerateNObjects(int n)
    {
        Vector3 cameraPosition = this.transform.position;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        for (int i = 0; i < n; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

            if (!(cameraPosition.x <= (position.x + 0.5) && cameraPosition.x >= (position.x - 0.5)
             && cameraPosition.y <= (position.y + 0.5) && cameraPosition.y >= (position.y - 0.5)))
            {
                objectList.Add(Instantiate(objects[Random.Range(0, objects.Length)], position, rotation));
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (objectList.Count < 100)
        {
            GenerateNObjects(100);
        }
        StartCoroutine(waiter());
    }

    public List<GameObject> getObjectList()
    {
        return objectList;
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        for (int i = 0; i < objectList.Count; i++)
        {
            Physics.IgnoreCollision(objectList[i].GetComponent<Collider>(), terrain.GetComponent<MeshCollider>());
            objectList[i].GetComponent<Collider>().enabled = true;
            if (objectList[i].transform.position.y == 0.0f)
            {
                objectList[i].GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
