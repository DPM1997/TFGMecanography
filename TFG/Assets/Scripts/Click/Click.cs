using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    [SerializeField] private List<KeyCode> keyCodeList;
    [SerializeField] private List<GameObject> feedbackList;
    private List<Collider2D> collisedObjectColliderList;
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
    bool scored2;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scored = false;
        scored2 = false;
        combo = 0;
        slider.value = 20;
        collisedObjectColliderList =new List<Collider2D>();
        scoreText = scoreObject.GetComponent<TMP_Text>();
        comboText = comboObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keyCodeList.Count; i++)
        {
            if (Input.GetKeyDown(keyCodeList[i]))
            {
                feedbackList[i].SetActive(true);
                for (int y = 0; y < collisedObjectColliderList.Count; y++)
                {
                    if (collisedObjectColliderList[y].tag == keyCodeList[i].ToString())
                    {
                        //Se mete en la función pero no modifica la variable del score
                        scored = true;
                        Collider2D destroyCollider = collisedObjectColliderList[y];
                        collisedObjectColliderList.Remove(collisedObjectColliderList[y]);
                        Destroy(destroyCollider.gameObject);
                        //ReproducirSonido
                        SoundFXScript.instance.PlayAudio(hitAudio, 1f, MusicTypes.sfx);
                        scored2=true;
                    }
                }
                if(scored==false&&scored2==false){
                SoundFXScript.instance.PlayAudio(missAudio, 1f, MusicTypes.sfx);
                score = score - 5;
                scoreText.text = ("" + score);
                }
                if(scored2==true)
                    scored2=false;
            }
            if (Input.GetKeyUp(keyCodeList[i]))
                feedbackList[i].SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collisedObjectColliderList.Add(other);
    }

    //Cuidado con esta función, cuando se destruye un objeto se detecta que se ha salido del trigger por lo que cuidadin
    void OnTriggerExit2D(Collider2D other)
    {
        collisedObjectColliderList.Remove(other);
        if (scored == true)
        {
            score = (int)(score + 100 + 100 * combo * 0.01);
            scoreText.text = ("" + score);
            combo++;
            comboText.text = ("" + combo);
            if (combo % 5 == 0)
                slider.value++;
            scored = false;
        }
        else
        {
            combo = 0;
            comboText.text = ("" + combo);
            score = score - 20;
            scoreText.text = ("" + score);
            Destroy(other.gameObject);
            slider.value--;
        }
    }

}
