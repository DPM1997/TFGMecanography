using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickB : MonoBehaviour
{
    bool inside;
    Collider2D collisedObjectCollider;
    private TMP_Text text;
    public GameObject scoreObject;
    [SerializeField] private AudioClip hitAudio; 
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
        if (Input.GetKeyDown(KeyCode.B) && inside == true){
          score = Int32.Parse(text.text);
          score = score + 5;
          text.text=(""+score);
          Destroy(collisedObjectCollider.gameObject);
          SoundFXScript.instance.CorrectHit(hitAudio,1f);
          collisedObjectCollider=null;
          inside=false;
        } else if(Input.GetKeyDown(KeyCode.B) && inside == false){
            score = Int32.Parse(text.text);
            score = score -1;
            text.text=(""+score);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="B")inside = true;
        collisedObjectCollider=other;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="B")inside = false;
        collisedObjectCollider=null;
    }

}
