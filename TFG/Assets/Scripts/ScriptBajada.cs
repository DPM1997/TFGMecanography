using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptBajada : MonoBehaviour
{
    public GameObject a;
    private GameObject s;
    private GameObject d;
    private ArrayList letterlist;
    public GameObject topScreen;
    private GameObject movingObject;
    public float downspeed;
    
    private Vector2 velocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        LoadGameObjects();
        s = (GameObject)Resources.Load("GameObjects/Letras/S");
        d = (GameObject)Resources.Load("GameObjects/Letras/D");
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
        int random = Random.Range(0,letterlist.Count);
        GameObject letterFromList = (GameObject)letterlist[random];
        movingObject = Instantiate(letterFromList, new Vector3(letterFromList.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
        rb2D = movingObject.AddComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        //InvokeRepeating("MoveObject", 0.05f, 0.05f);
        velocity = new Vector2(0,-downspeed);
        StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
        yield return new WaitForSeconds(1f);
        }
    }

    void LoadGameObjects(){
        letterlist = new ArrayList();
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/A") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/B") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/C") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/D") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/E") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/F") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/G") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/H") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/I") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/J") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/K") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/L") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/M") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/N") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/O") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/P") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/Q") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/R") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/S") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/T") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/U") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/V") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/W") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/X") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/Y") as GameObject);
        letterlist.Add((GameObject)Resources.Load("GameObjects/Letras/X") as GameObject);
        foreach(GameObject letter in letterlist){
            Debug.Log(letter.name);
        }
    }
}
