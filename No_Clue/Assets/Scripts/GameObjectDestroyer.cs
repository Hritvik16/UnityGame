using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        camera.GetComponent<EnvironmentGenerator>().getObjectList().Remove(collision.gameObject);
        Destroy(collision.gameObject);
    }
}
