using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickW : MonoBehaviour
{
    bool inside;
    Collider2D collisedObjectCollider;
    private TMP_Text text;
    public GameObject scoreObject;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        score = 0;
        text = scoreObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && inside == true){
          score = Int32.Parse(text.text);
          score = score + 5;
          text.text=(""+score);
          Destroy(collisedObjectCollider.gameObject);
          collisedObjectCollider=null;
          inside=false;
        } else if(Input.GetKeyDown(KeyCode.W) && inside == false){
            score = Int32.Parse(text.text);
            score = score -1;
            text.text=(""+score);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="W")inside = true;
        collisedObjectCollider=other;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="W")inside = false;
        collisedObjectCollider=null;
    }

}
