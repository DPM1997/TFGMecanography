using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptBajada : MonoBehaviour
{
    public GameObject a;
    public GameObject topScreen;
    private GameObject movingObject;
    public float downspeed;
    
    private Vector2 velocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateObject());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator MoveObject(GameObject thisMovingObject, float repeatRate, Rigidbody2D rb2D){
        while (thisMovingObject != null){
            rb2D.MovePosition(rb2D.position + velocity);
            yield return new WaitForSeconds(repeatRate);
        }
    }

    IEnumerator CreateObject(){
        while(true){
        Rigidbody2D rb2D;
        movingObject = Instantiate(a, new Vector3(a.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
        rb2D = movingObject.AddComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        //InvokeRepeating("MoveObject", 0.05f, 0.05f);
        velocity = new Vector2(0,-downspeed);
        StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
        yield return new WaitForSeconds(1f);
        }
    }
}
