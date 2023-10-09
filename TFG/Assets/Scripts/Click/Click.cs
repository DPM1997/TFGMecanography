using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    [SerializeField] private UnityEngine.KeyCode keyCode1;
    [SerializeField] private UnityEngine.KeyCode keyCode2;
    [SerializeField] private UnityEngine.KeyCode keyCode3;
    [SerializeField] private UnityEngine.KeyCode keyCode4;
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
        if ((Input.GetKeyDown(keyCode1) || Input.GetKeyDown(keyCode2) || Input.GetKeyDown(keyCode3) || Input.GetKeyDown(keyCode4)) && inside == true){
          score = Int32.Parse(text.text);
          Debug.Log("---inside:"+inside);
          //Se mete en la función pero no modifica la variable del score
          Debug.Log((Input.GetKeyDown(keyCode1) || Input.GetKeyDown(keyCode2) || Input.GetKeyDown(keyCode3) || Input.GetKeyDown(keyCode4) && inside == true));
          score = score + 10;
          text.text=(""+score);
          Destroy(collisedObjectCollider.gameObject);
          //ReproducirSonido
          SoundFXScript.instance.PlayAudio(hitAudio,1f);
          collisedObjectCollider=null;
          inside=false;
        } else if((Input.GetKeyDown(keyCode1) || Input.GetKeyDown(keyCode2) || Input.GetKeyDown(keyCode3) || Input.GetKeyDown(keyCode4)) && inside == false){
            score = Int32.Parse(text.text);
            SoundFXScript.instance.PlayAudio(missAudio,1f);
            score = score -1;
            text.text=(""+score);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag==keyCode1.ToString())inside = true;
        else if(other.tag==keyCode2.ToString())inside = true;
        else if(other.tag==keyCode3.ToString())inside = true;
        else if(other.tag==keyCode4.ToString())inside = true;
        collisedObjectCollider=other;
    }

    //Cuidado con esta función, cuando se destruye un objeto se detecta que se ha salido del trigger por lo que cuidadin
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag==keyCode1.ToString())inside = false;
        else if(other.tag==keyCode1.ToString())inside = false;
        else if(other.tag==keyCode1.ToString())inside = false;
        else if(other.tag==keyCode1.ToString())inside = false;
        collisedObjectCollider=null;
        //Bajar vida
        Destroy(other.gameObject);
        score = Int32.Parse(text.text);
        score = score - 5;
        text.text=(""+score);
    }

}
