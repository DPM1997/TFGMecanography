using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class LevelKey
{
    string key; //Maybe in a future this element is a int or an enum for memory optimization
    int timeToAppear; //In miliseconds
    int tiemToWait; //Time to wait for the next key

    public LevelKey(string key, int timeToAppear, int tiemToWait)
    {
        this.key = key;
        this.timeToAppear = timeToAppear;
        this.tiemToWait = tiemToWait;
    }

    public override string ToString()
    {
        return "" + key + ";" + timeToAppear + ";" + tiemToWait + "";
    }

    public string getKey()
    {
        return key;
    }

    public int getTimeToWait()
    {
        return tiemToWait;
    }
}

public class ScriptBajada : MonoBehaviour
{
    private ArrayList letterList;
    private Dictionary<string, GameObject> dicctionaryList;
    private Dictionary<string, int> dicctionaryRow;
    private ArrayList levelList;
    private ArrayList letterListOnlyMiddleRow;
    public GameObject topScreen;
    private GameObject movingObject;
    public float downspeed;
    public bool randomMode;
    public bool randomModeMusic;
    private float spawningSpeed;
    public string levelPath;
    private Vector2 velocity;
    private AudioClip backgroundMusic;
    private string lastLetter;
    private bool firstTime;


    // Start is called before the first frame update
    void Start()
    {
        spawningSpeed = 1f;
        firstTime = true;
        LoadGameObjects();
        if (randomMode){
            if(randomModeMusic){
                backgroundMusic = (AudioClip)Resources.Load("Music/RandomLevel-38");
                Debug.Log(backgroundMusic);
                spawningSpeed=1.6f;
                SoundFXScript.instance.PlayAudio(backgroundMusic, 1f);
            }
            StartCoroutine(CreateObjectRandom());
            //StartCoroutine(CreateObjectRandomMiddleRow());
        }
        else
            TestLevel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoadGameObjects()
    {
        //Load the gameObjects to random mode
        letterList = new ArrayList();
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/A") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/B") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/C") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/D") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/E") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/F") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/G") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/H") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/I") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/J") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/K") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/L") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/M") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/N") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/O") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/P") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/Q") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/R") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/S") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/T") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/U") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/V") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/W") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/X") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/Y") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras/Z") as GameObject);
        letterListOnlyMiddleRow = new ArrayList();
        letterListOnlyMiddleRow.Add(letterList[0]);
        letterListOnlyMiddleRow.Add(letterList[3]);
        letterListOnlyMiddleRow.Add(letterList[5]);
        letterListOnlyMiddleRow.Add(letterList[6]);
        letterListOnlyMiddleRow.Add(letterList[7]);
        letterListOnlyMiddleRow.Add(letterList[9]);
        letterListOnlyMiddleRow.Add(letterList[10]);
        letterListOnlyMiddleRow.Add(letterList[11]);
        letterListOnlyMiddleRow.Add(letterList[18]);
        dicctionaryRow = new Dictionary<string, int>();
        dicctionaryRow.Add("Q",1);
        dicctionaryRow.Add("W",1);
        dicctionaryRow.Add("E",1);
        dicctionaryRow.Add("R",1);
        dicctionaryRow.Add("T",1);
        dicctionaryRow.Add("Y",1);
        dicctionaryRow.Add("U",1);
        dicctionaryRow.Add("I",1);
        dicctionaryRow.Add("O",1);
        dicctionaryRow.Add("P",1);
        dicctionaryRow.Add("A",2);
        dicctionaryRow.Add("S",2);
        dicctionaryRow.Add("D",2);
        dicctionaryRow.Add("F",2);
        dicctionaryRow.Add("G",2);
        dicctionaryRow.Add("H",2);
        dicctionaryRow.Add("J",2);
        dicctionaryRow.Add("K",2);
        dicctionaryRow.Add("L",2);
        dicctionaryRow.Add("Z",3);
        dicctionaryRow.Add("X",3);
        dicctionaryRow.Add("C",3);
        dicctionaryRow.Add("V",3);
        dicctionaryRow.Add("B",3);
        dicctionaryRow.Add("N",3);
        dicctionaryRow.Add("M",3);

        foreach (GameObject letter in letterList)
        {
            Debug.Log(letter.name);
        }

        //Load in dicctionary for the levels
        dicctionaryList = new Dictionary<string, GameObject>();
        foreach (GameObject letter in letterList)
        {
            dicctionaryList.Add(letter.name, letter);
        }
    }

    IEnumerator MoveObject(GameObject thisMovingObject, float repeatRate, Rigidbody2D rb2D)
    {
        while (thisMovingObject != null)
        {
            rb2D.MovePosition(rb2D.position + velocity);
            yield return new WaitForSeconds(repeatRate);
        }
    }

    IEnumerator CreateObjectRandom()
    {
        while (true)
        {
            Rigidbody2D rb2D;
            int random;
            GameObject letterFromList;
            if(firstTime){
                firstTime = false;
                random = UnityEngine.Random.Range(0, letterListOnlyMiddleRow.Count);
                letterFromList = (GameObject)letterListOnlyMiddleRow[random];
            }
            else {
                random = UnityEngine.Random.Range(0, letterList.Count);
                letterFromList = (GameObject)letterList[random];
            }
            Debug.Log(dicctionaryRow[letterFromList.name]);
            Debug.Log(letterFromList.name);
            movingObject = Instantiate(letterFromList, new Vector3(letterFromList.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            rb2D = movingObject.AddComponent<Rigidbody2D>();
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            //InvokeRepeating("MoveObject", 0.05f, 0.05f);
            velocity = new Vector2(0, -downspeed);
            StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
            lastLetter = letterFromList.name;
            //Empieza el problema
            if(dicctionaryRow[lastLetter]==2){
            if(dicctionaryRow[letterFromList.name]==2)yield return new WaitForSeconds(spawningSpeed);
            else if(dicctionaryRow[letterFromList.name]==1)yield return new WaitForSeconds(spawningSpeed-0.320f);
            else yield return new WaitForSeconds(spawningSpeed+0.320f);
            } else
            if(dicctionaryRow[lastLetter]==1){
            if(dicctionaryRow[letterFromList.name]==2)yield return new WaitForSeconds(spawningSpeed+0.320f);
            else if(dicctionaryRow[letterFromList.name]==1)yield return new WaitForSeconds(spawningSpeed);
            else yield return new WaitForSeconds(spawningSpeed+0.640f);
            } else
            if(dicctionaryRow[lastLetter]==3){
            if(dicctionaryRow[letterFromList.name]==2)yield return new WaitForSeconds(spawningSpeed-0.320f);
            else if(dicctionaryRow[letterFromList.name]==1)yield return new WaitForSeconds(spawningSpeed-0.640f);
            else yield return new WaitForSeconds(spawningSpeed);
            } 
        }
    }

    IEnumerator CreateObjectRandomMiddleRow()
    {
        while (true)
        {
            Rigidbody2D rb2D;
            int random = UnityEngine.Random.Range(0, letterListOnlyMiddleRow.Count);
            GameObject letterFromList = (GameObject)letterListOnlyMiddleRow[random];
            movingObject = Instantiate(letterFromList, new Vector3(letterFromList.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            rb2D = movingObject.AddComponent<Rigidbody2D>();
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            //InvokeRepeating("MoveObject", 0.05f, 0.05f);
            velocity = new Vector2(0, -downspeed);
            StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    void TestLevel()
    {
        //CreateLevel();
        ReadLevel();
        StartCoroutine(PlayLevel());
    }

    //Deprecated for now
    void ExportLevel()
    {
        using (StreamWriter writer = new StreamWriter("./Assets/Levels/level.csv"))
        {
            foreach (LevelKey levelKey in levelList)
            writer.WriteLine(levelKey.ToString());
        }
    }

    void ReadLevel(){
        levelList = new ArrayList();
        foreach (string line in File.ReadLines(levelPath, Encoding.UTF8))
        {
            string[] words = line.Split(';');
            LevelKey key = new LevelKey(words[0],Int32.Parse(words[1]),Int32.Parse(words[2]));
            levelList.Add(key);
        }
    }

    private IEnumerator PlayLevel()
    {
        foreach (LevelKey levelKey in levelList)
        {
            Rigidbody2D rb2D;
            GameObject letter = dicctionaryList[levelKey.getKey()];
            movingObject = Instantiate(letter, new Vector3(letter.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            rb2D = movingObject.AddComponent<Rigidbody2D>();
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            velocity = new Vector2(0, -downspeed);
            StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
            yield return new WaitForSeconds((float)(levelKey.getTimeToWait() / 1000.0));
        }
    }

}
