using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class LevelKey
{
    string key; //Maybe in a future this element is a int or an enum for memory optimization
    int timeToAppear; //In miliseconds
    int timeToWait; //Time to wait for the next key

    public LevelKey(string key, int timeToAppear, int timeToWait)
    {
        this.key = key;
        this.timeToAppear = timeToAppear;
        this.timeToWait = timeToWait;
    }

    public override string ToString()
    {
        return "" + key + ";" + timeToAppear + ";" + timeToWait + "";
    }

    public string getKey()
    {
        return key;
    }

    public int getTimeToWait()
    {
        return timeToWait;
    }
}

public class KeyRandom
{
    float percentaje;

    string letra;

    public KeyRandom(string letra, float percentaje)
    {
        this.percentaje = percentaje;
        this.letra = letra;
    }

    public string getLetra()
    {
        return this.letra;
    }

    public float getPercentaje()
    {
        return this.percentaje;
    }
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

public class ScriptBajada : MonoBehaviour
{
    private ArrayList letterList;
    private Dictionary<string, GameObject> dicctionaryList;
    private Dictionary<string, int> dicctionaryRow;
    private ArrayList levelList;
    private ArrayList letterListOnlyMiddleRow;
    private List<KeyRandom> percentageList;
    private float sumTotalPercentageList;
    public GameObject topScreen;
    private GameObject movingObject;
    public float downspeed;
    public float spawnRate;
    public bool randomModeMusic;
    public bool spanish;
    private float spawningSpeed;
    private AudioClip backgroundMusic;
    private GameObject nextLetter;
    private bool firstTime;

    //Static Elements, that are serialized from Menu
    public static bool randomMode = true;
    public static Difficulty dificulty = Difficulty.Hard;
    public static string levelPath;
    void Awake()
    {
        spawningSpeed = 1f;
        firstTime = true;
        LoadGameObjects();
        createPercentajeSpanish();
        backgroundMusic = (AudioClip)Resources.Load("Audio/Music/RandomLevel-38");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (randomMode)
        {
            if (randomModeMusic)
            {
                SoundFXScript.instance.PlayAudio(backgroundMusic, 1f, MusicTypes.music);
            }
            StartCoroutine(CreateObjectRandomV3());
            //StartCoroutine(CreateObjectRandom());
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
        dicctionaryRow.Add("Q", 1);
        dicctionaryRow.Add("W", 1);
        dicctionaryRow.Add("E", 1);
        dicctionaryRow.Add("R", 1);
        dicctionaryRow.Add("T", 1);
        dicctionaryRow.Add("Y", 1);
        dicctionaryRow.Add("U", 1);
        dicctionaryRow.Add("I", 1);
        dicctionaryRow.Add("O", 1);
        dicctionaryRow.Add("P", 1);
        dicctionaryRow.Add("A", 2);
        dicctionaryRow.Add("S", 2);
        dicctionaryRow.Add("D", 2);
        dicctionaryRow.Add("F", 2);
        dicctionaryRow.Add("G", 2);
        dicctionaryRow.Add("H", 2);
        dicctionaryRow.Add("J", 2);
        dicctionaryRow.Add("K", 2);
        dicctionaryRow.Add("L", 2);
        dicctionaryRow.Add("Z", 3);
        dicctionaryRow.Add("X", 3);
        dicctionaryRow.Add("C", 3);
        dicctionaryRow.Add("V", 3);
        dicctionaryRow.Add("B", 3);
        dicctionaryRow.Add("N", 3);
        dicctionaryRow.Add("M", 3);

        //Load in dicctionary for the levels
        dicctionaryList = new Dictionary<string, GameObject>();
        foreach (GameObject letter in letterList)
        {
            dicctionaryList.Add(letter.name, letter);
        }
    }

    public void createPercentajeSpanish()
    {
        percentageList = new List<KeyRandom>();
        percentageList.Add(new KeyRandom("A", (float)0.120365034));
        percentageList.Add(new KeyRandom("B", (float)0.013640730));
        percentageList.Add(new KeyRandom("C", (float)0.044956772));
        percentageList.Add(new KeyRandom("D", (float)0.056292027));
        percentageList.Add(new KeyRandom("E", (float)0.131412104));
        percentageList.Add(new KeyRandom("F", (float)0.006628242));
        percentageList.Add(new KeyRandom("G", (float)0.009702209));
        percentageList.Add(new KeyRandom("H", (float)0.006724304));
        percentageList.Add(new KeyRandom("I", (float)0.060038425));
        percentageList.Add(new KeyRandom("J", (float)0.042267051));
        percentageList.Add(new KeyRandom("K", (float)0.000192123));
        percentageList.Add(new KeyRandom("L", (float)0.047742555));
        percentageList.Add(new KeyRandom("M", (float)0.030259366));
        percentageList.Add(new KeyRandom("N", (float)0.064457253));
        percentageList.Add(new KeyRandom("O", (float)0.083381364));
        percentageList.Add(new KeyRandom("P", (float)0.024111431));
        percentageList.Add(new KeyRandom("Q", (float)0.008453410));
        percentageList.Add(new KeyRandom("R", (float)0.065994236));
        percentageList.Add(new KeyRandom("S", (float)0.076657061));
        percentageList.Add(new KeyRandom("T", (float)0.044476465));
        percentageList.Add(new KeyRandom("U", (float)0.037752161));
        percentageList.Add(new KeyRandom("V", (float)0.008645533));
        percentageList.Add(new KeyRandom("W", (float)0.000096061));
        percentageList.Add(new KeyRandom("X", (float)0.002113353));
        percentageList.Add(new KeyRandom("Y", (float)0.008645533));
        percentageList.Add(new KeyRandom("Z", (float)0.004995197));
        sumTotalPercentageList = 0;
        foreach (var item in percentageList) sumTotalPercentageList += item.getPercentaje();
    }
    
    public int letterToArrayIndex(string letter)
    {
        return (int)letter.ToCharArray()[0] - (int)'A' + 1;
    }

    /*
    IEnumerator MoveObject(GameObject thisMovingObject, float repeatRate, Rigidbody2D rb2D)
    {
        while (thisMovingObject != null)
        {
            rb2D.MovePosition(rb2D.position + velocity);
            yield return new WaitForSeconds(repeatRate);
        }
    }
    */
    IEnumerator CreateObjectRandomV3()
    {
        //Definir la velocidad según la dificultad
        settingSpeedAndSpawnRate();
        //Cambiar funcion para que se calcule la siguiente no la anterior
        while (true)
        {
            int random;
            GameObject actualLetter;
            //Debug.Log("First Time" + firstTime);
            random = UnityEngine.Random.Range(0, letterList.Count);
            actualLetter = (GameObject)letterList[random];
            movingObject = Instantiate(actualLetter, new Vector3(actualLetter.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    private void settingSpeedAndSpawnRate()
    {
        if (dificulty == Difficulty.Easy)
        {
            Movement.speed=3f;
            spawningSpeed = 1.6f;
        }
        if (dificulty == Difficulty.Medium)
        {
            Movement.speed =4f;
            spawningSpeed = 0.8f;
        }
        if (dificulty == Difficulty.Hard)
        {
            Movement.speed=5f;
            spawningSpeed = 0.4f;
        }
        
    }

    IEnumerator CreateObjectRandomV2()
    {
        //Definir la velocidad según la dificultad

        settingSpeedAndSpawnRate();
        //Cambiar funcion para que se calcule la siguiente no la anterior
        while (true)
        {
            Rigidbody2D rb2D;
            int random;
            GameObject actualLetter;
            if (firstTime)
            {
                firstTime = false;
                //Debug.Log("First Time" + firstTime);
                random = UnityEngine.Random.Range(0, letterListOnlyMiddleRow.Count);
                actualLetter = (GameObject)letterListOnlyMiddleRow[random];
                random = UnityEngine.Random.Range(0, letterList.Count);
                nextLetter = (GameObject)letterList[random];
            }
            else
            {
                //Debug.Log("First Time" + firstTime);
                random = UnityEngine.Random.Range(0, letterList.Count);
                actualLetter = nextLetter;
                nextLetter = (GameObject)letterList[random];
            }
            movingObject = Instantiate(actualLetter, new Vector3(actualLetter.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            rb2D = movingObject.AddComponent<Rigidbody2D>();
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            //velocity = new Vector2(0, -downspeed);
            //StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));

            if (dicctionaryRow[actualLetter.name] == 2)
            {
                if (dicctionaryRow[nextLetter.name] == 2) yield return new WaitForSeconds(spawningSpeed);
                else if (dicctionaryRow[nextLetter.name] == 1) yield return new WaitForSeconds(spawningSpeed + spawnRate);
                else yield return new WaitForSeconds(spawningSpeed - spawnRate);
            }
            else
            if (dicctionaryRow[actualLetter.name] == 1)
            {
                if (dicctionaryRow[nextLetter.name] == 2) yield return new WaitForSeconds(spawningSpeed - spawnRate);
                else if (dicctionaryRow[nextLetter.name] == 1) yield return new WaitForSeconds(spawningSpeed);
                else yield return new WaitForSeconds(spawningSpeed - spawnRate * 2);
            }
            else
            if (dicctionaryRow[actualLetter.name] == 3)
            {
                if (dicctionaryRow[nextLetter.name] == 2) yield return new WaitForSeconds(spawningSpeed + spawnRate);
                else if (dicctionaryRow[nextLetter.name] == 1) yield return new WaitForSeconds(spawningSpeed + 0.640f);
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
            //velocity = new Vector2(0, -downspeed);
            //StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
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

    void ReadLevel()
    {
        levelList = new ArrayList();
        foreach (string line in File.ReadLines(levelPath, Encoding.UTF8))
        {
            string[] words = line.Split(';');
            LevelKey key = new LevelKey(words[0], Int32.Parse(words[1]), Int32.Parse(words[2]));
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
            //velocity = new Vector2(0, -downspeed);
            //StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
            yield return new WaitForSeconds((float)(levelKey.getTimeToWait() / 1000.0));
        }
    }

}
