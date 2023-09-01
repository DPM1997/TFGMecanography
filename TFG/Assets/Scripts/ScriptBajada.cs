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
    private ArrayList levelList;
    private ArrayList levelListOnlyMiddleRow;
    public GameObject topScreen;
    private GameObject movingObject;
    public float downspeed;
    public bool randomMode;
    public bool randomModeMusic;
    private float spawningSpeed;
    public string levelPath;
    private Vector2 velocity;
    private AudioClip backgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
        spawningSpeed = 1f;
        LoadGameObjects();
        if (randomMode){
            if(randomModeMusic){
                backgroundMusic = (AudioClip)Resources.Load("Music/RandomLevel-38");
                spawningSpeed=1.6f;
                SoundFXScript.instance.PlayAudio(backgroundMusic, 1f);
            }
            //StartCoroutine(CreateObjectRandom());
            StartCoroutine(CreateObjectRandomMiddleRow());
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
        levelListOnlyMiddleRow = new ArrayList();
        levelListOnlyMiddleRow.Add(letterList[0]);
        levelListOnlyMiddleRow.Add(letterList[3]);
        levelListOnlyMiddleRow.Add(letterList[5]);
        levelListOnlyMiddleRow.Add(letterList[6]);
        levelListOnlyMiddleRow.Add(letterList[7]);
        levelListOnlyMiddleRow.Add(letterList[9]);
        levelListOnlyMiddleRow.Add(letterList[10]);
        levelListOnlyMiddleRow.Add(letterList[11]);
        levelListOnlyMiddleRow.Add(letterList[18]);

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
            int random = UnityEngine.Random.Range(0, letterList.Count);
            GameObject letterFromList = (GameObject)letterList[random];
            movingObject = Instantiate(letterFromList, new Vector3(letterFromList.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            rb2D = movingObject.AddComponent<Rigidbody2D>();
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            //InvokeRepeating("MoveObject", 0.05f, 0.05f);
            velocity = new Vector2(0, -downspeed);
            StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    IEnumerator CreateObjectRandomMiddleRow()
    {
        while (true)
        {
            Rigidbody2D rb2D;
            int random = UnityEngine.Random.Range(0, levelListOnlyMiddleRow.Count);
            GameObject letterFromList = (GameObject)levelListOnlyMiddleRow[random];
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
