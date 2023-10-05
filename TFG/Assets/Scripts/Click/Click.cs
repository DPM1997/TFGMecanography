using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    [SerializeField] private UnityEngine.KeyCode keyCode;
    bool inside;
    Collider2D collisedObjectCollider;
    private TMP_Text text;
    [SerializeField] private AudioClip hitAudio; 
    [SerializeField] private AudioClip missAudio; 
    public GameObject scoreObject;
    [SerializeField] private Slider slider;
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
        if (Input.GetKeyDown(keyCode) && inside == true){
          score = Int32.Parse(text.text);
          score = score + 5;
          text.text=(""+score);
          Destroy(collisedObjectCollider.gameObject);
          //ReproducirSonido
          SoundFXScript.instance.PlayAudio(hitAudio,1f);
          collisedObjectCollider=null;
          inside=false;
        } else if(Input.GetKeyDown(keyCode) && inside == false){
            score = Int32.Parse(text.text);
            SoundFXScript.instance.PlayAudio(missAudio,1f);
            score = score -1;
            text.text=(""+score);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag==keyCode.ToString())inside = true;
        collisedObjectCollider=other;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag==keyCode.ToString())inside = false;
        collisedObjectCollider=null;
        //Bajar vida

    }

}
