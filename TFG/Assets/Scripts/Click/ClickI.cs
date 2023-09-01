using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickI: MonoBehaviour
{
    bool inside;
    Collider2D collisedObjectCollider;
    private TMP_Text text;
    [SerializeField] private AudioClip hitAudio; 
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
        if (Input.GetKeyDown(KeyCode.I) && inside == true){
          score = Int32.Parse(text.text);
          score = score + 5;
          text.text=(""+score);
          Destroy(collisedObjectCollider.gameObject);
          SoundFXScript.instance.CorrectHit(hitAudio,1f);
          collisedObjectCollider=null;
          inside=false;
        } else if(Input.GetKeyDown(KeyCode.I) && inside == false){
            score = Int32.Parse(text.text);
            score = score -1;
            text.text=(""+score);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="I")inside = true;
        collisedObjectCollider=other;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="I")inside = false;
        collisedObjectCollider=null;
    }

}
