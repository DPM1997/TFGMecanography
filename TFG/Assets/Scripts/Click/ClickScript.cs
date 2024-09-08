using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that manages all the keyboard collider.
/// </summary>
public class ClickScript : MonoBehaviour
{
    /// <summary>
    /// List fo KeyCodes of all the keyboard.
    /// </summary>
    [SerializeField] private List<KeyCode> keyCodeList;
    /// <summary>
    /// List of Feedbacks prefabs in the scene. 1 for each letter of the keyboard, with the same index of keyCodeList.
    /// </summary>
    [SerializeField] private List<GameObject> feedbackList;
    /// <summary>
    /// The objects that are right now in this collider.
    /// </summary>
    private List<Collider2D> collisedObjectColliderList;

    // Audio Clips of an input.
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip missAudio;

    /// <summary>
    /// GameObject of Score in the scene, only called in Start.
    /// </summary>
    public GameObject scoreObject;
    /// <summary>
    /// GameObject of Combo in the scene, only called in Start.
    /// </summary>
    public GameObject comboObject;

    /// <summary>
    /// Actual score value.
    /// </summary>
    private int score;
    /// <summary>
    /// Actual combo value.
    /// </summary>
    private int combo;

    // Flags of if an object is scored.
    private bool scored;
    private bool scored2;

    //Debug varibles
    /// <summary>
    /// To activate debug mode.
    /// </summary>
    private bool createLevels;
    /// <summary>
    /// Time for the last key pressed.
    /// </summary>
    private float pressTime= 0f;

    //UI Elements
    /// <summary>
    /// Score text in the scene.
    /// </summary>
    private TMP_Text scoreText;
    /// <summary>
    /// Combo text in the scene.
    /// </summary>
    private TMP_Text comboText;

    /// <summary>
    /// Slider that represents the lifes. Has an default value of 20.
    /// </summary>
    [SerializeField] private Slider lifes;
    /// <summary>
    /// Game over submenu.
    /// </summary>
    [SerializeField] private GameObject EndMenu;
    /// <summary>
    /// Game over submenu texts of score and message.
    /// </summary>
    [SerializeField] private TMP_Text EndMenuScoreText, EndMenuText;
    /// <summary>
    /// Initialize variables of the scene.
    /// </summary>
    private void Start()
    {
        score = 0;
        scored = false;
        scored2 = false;
        createLevels = false;
        combo = 0;
        pressTime = Time.time;
        lifes.value = 20;
        collisedObjectColliderList =new List<Collider2D>();
        scoreText = scoreObject.GetComponent<TMP_Text>();
        comboText = comboObject.GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Each frame check if a key is pressed, then:
    /// - If there is a letter in the collisedObjectColliderList, if the letter KeyCode is correct, scores and destroy the collided object.
    /// - If not, reduces the score.
    /// 
    /// Each of the conditions are accompanied with a sound.
    /// </summary>
    private void Update()
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

    /// <summary>
    /// Add the GameObject to collisedObjectColliderList.
    /// </summary>
    /// <param name="other">Collided GameObject</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        collisedObjectColliderList.Add(other);
    }

    /// <summary>
    /// When an object is destroyed it is detected that it has gone out of the collider. So the flags are to check if it is consecuence of a score.
    /// If it is, score is increased. If it have scored 5 times in a row, adds a life.
    /// If not, score is decreased and loses a life.
    /// </summary>
    /// <param name="other">Collided GameObject</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        collisedObjectColliderList.Remove(other);
        if (scored == true)
        {
            score = (int)(score + 100 + 100 * combo * 0.01);
            scoreText.text = ("" + score);
            combo++;
            comboText.text = ("" + combo);
            if (combo % 5 == 0)
                lifes.value++;
            scored = false;
        }
        else
        {
            combo = 0;
            comboText.text = ("" + combo);
            score = score - 20;
            scoreText.text = ("" + score);
            Destroy(other.gameObject);
            lifes.value--;
        }
        if(lifes.value<=0){
            //Mostrar escena de gameOver
            EndLevel("You Lose More luck next time");
        }
    }

    /// <summary>
    /// Shows the EndGame submenu and modifies the message that shows,
    /// </summary>
    /// <param name="text">String with the message</param>
    public void EndLevel(string text){
            Time.timeScale = 0;
            EndMenuText.text = text;
            EndMenuScoreText.text = scoreText.text;
            EndMenu.SetActive(true);
            //Llamamos al leaderboard
            this.GetComponent<LeaderboardScript>().SubmitUser(score);
    }

}
