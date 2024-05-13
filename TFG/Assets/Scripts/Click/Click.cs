using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    [SerializeField] private List<KeyCode> keyCodeList;
    [SerializeField] private List<GameObject> feedbackList;
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
        for (int i = 0; i < keyCodeList.Count; i++)
        {
            if (Input.GetKeyDown(keyCodeList[i])){
                feedbackList[i].SetActive(true);
                if(inside==true&&collisedObjectCollider.tag==keyCodeList[i].ToString()){
                //Se mete en la función pero no modifica la variable del score
                scored=true;
                Destroy(collisedObjectCollider.gameObject);
                //ReproducirSonido
                SoundFXScript.instance.PlayAudio(hitAudio,1f,MusicTypes.sfx);
                collisedObjectCollider=null;
                inside=false;
                }else{
                SoundFXScript.instance.PlayAudio(missAudio,1f,MusicTypes.sfx);
                score = score - 5;
                scoreText.text=(""+score);
                }
            }
            if (Input.GetKeyUp(keyCodeList[i]))
                feedbackList[i].SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        inside=true;
        //Comprobamos la lista y lo metemos
        collisedObjectCollider=other;
    }

    //Cuidado con esta función, cuando se destruye un objeto se detecta que se ha salido del trigger por lo que cuidadin
    void OnTriggerExit2D(Collider2D other)
    {
        inside = false;
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
