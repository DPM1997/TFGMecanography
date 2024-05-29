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
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip missAudio;
    public GameObject scoreObject;
    public GameObject comboObject;

    int score;
    int combo;
    bool scored;
    bool scored2;
    //Debug varibles
    bool createLevels;
    float pressTime= 0f;

    //UI Elements
    private TMP_Text scoreText;
    private TMP_Text comboText;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject EndMenu;
    [SerializeField] private TMP_Text EndMenuScoreText, EndMenuText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scored = false;
        scored2 = false;
        createLevels = false;
        combo = 0;
        pressTime = Time.time;
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
                if (createLevels==true){
                    float pressTime2=Time.time;
                    float auxTime = pressTime2- pressTime;
                    pressTime=pressTime2;
                    Debug.Log(keyCodeList[i].ToString()+';'+auxTime.ToString());
                }
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
                        SoundFXScript.instance.PlayAudio(hitAudio, 1f, MusicTypes.sfx,0);
                        scored2=true;
                        break;
                    }
                }
                if(scored==false&&scored2==false){
                SoundFXScript.instance.PlayAudio(missAudio, 1f, MusicTypes.sfx,0);
                score = score - 5;
                scoreText.text = ("" + score);
                }
                if(scored2==true)
                    scored2=false;
            }
            if (Input.GetKeyUp(keyCodeList[i]))
                feedbackList[i].SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
            createLevels = true;
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
        if(slider.value<=0){
            //Mostrar escena de gameOver
            EndLevel("Perdiste Mas suerte la proxima vez");
        }
    }
    public void EndLevel(string text){
            Time.timeScale = 0;
            EndMenuText.text = text;
            EndMenuScoreText.text = scoreText.text;
            EndMenu.SetActive(true);
            //Llamamos al leaderboard
            this.GetComponent<Leaderboard>().SubmitUser(score);
    }

}
