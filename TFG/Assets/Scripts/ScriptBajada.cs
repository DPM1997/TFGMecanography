using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBajada : MonoBehaviour
{
    public GameObject a;
    public GameObject topScreen;
    private GameObject movingObject;
    public float downspeed;
    // Start is called before the first frame update
    void Start()
    {
        movingObject = Instantiate(a, new Vector3(a.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        movingObject.transform.position = new Vector3(movingObject.transform.position.x, movingObject.transform.position.y-(float)downspeed, 0);
    }
}
