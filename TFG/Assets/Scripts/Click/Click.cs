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
    private TMP_Text scoreText;
    private TMP_Text comboText;
    [SerializeField] private AudioClip hitAudio; 
    [SerializeField] private AudioClip missAudio; 
    public GameObject scoreObject;
    public GameObject comboObject;
    [SerializeField] private Slider slider;
    int score;
    int combo;
    bool scored;

    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        score = 0;
        scored=false;
        combo=0;
        slider.value=20;
        scoreText = scoreObject.GetComponent<TMP_Text>();
        comboText = comboObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score = Int32.Parse(scoreText.text);
        if ((Input.GetKeyDown(keyCode1) || Input.GetKeyDown(keyCode2) || Input.GetKeyDown(keyCode3) || Input.GetKeyDown(keyCode4)) && inside == true){
          Debug.Log("---inside:"+inside);
          //Se mete en la función pero no modifica la variable del score
          Debug.Log((Input.GetKeyDown(keyCode1) || Input.GetKeyDown(keyCode2) || Input.GetKeyDown(keyCode3) || Input.GetKeyDown(keyCode4) && inside == true));
          scored=true;
          Destroy(collisedObjectCollider.gameObject);
          //ReproducirSonido
          SoundFXScript.instance.PlayAudio(hitAudio,1f);
          collisedObjectCollider=null;
          inside=false;
        } else if((Input.GetKeyDown(keyCode1) || Input.GetKeyDown(keyCode2) || Input.GetKeyDown(keyCode3) || Input.GetKeyDown(keyCode4)) && inside == false){
            SoundFXScript.instance.PlayAudio(missAudio,1f);
            score = score - 5;
            scoreText.text=(""+score);
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
        combo = Int32.Parse(comboText.text);
        score = Int32.Parse(scoreText.text);
        if(other.tag==keyCode1.ToString())inside = false;
        else if(other.tag==keyCode1.ToString())inside = false;
        else if(other.tag==keyCode1.ToString())inside = false;
        else if(other.tag==keyCode1.ToString())inside = false;
        if(scored==true){
            score = (int)(score+100+100*combo*0.01);
            scoreText.text=(""+score);
            combo++;
            comboText.text=(""+combo);
            if(combo%5==0)
                slider.value++;
            scored=false;
        }else{
            combo=0;
            comboText.text=(""+combo);
            score = score - 20;
            scoreText.text=(""+score);
            Destroy(other.gameObject);
            slider.value--;
        }
        collisedObjectCollider=null;
    }

}
