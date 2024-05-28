using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEditor.Search;

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

    public int getTimeToAppear()
    {
        return timeToAppear;
    }

    public void setTimeToWait(int timeToWait){
        this.timeToWait = timeToWait;
    }

    public void setTimeToAppear(int timeToAppear){
        this.timeToAppear = timeToAppear;
    }
    
}

public class KeyRandom
{
    public float percentaje { get; set; }
    public float cumulative { get; set; }
    public string letra { get; set; }

    public KeyRandom(string letra, float percentaje)
    {
        this.percentaje = percentaje;
        this.letra = letra;
    }

    public KeyRandom(string letra, float percentaje, float cumulative)
    {
        this.percentaje = percentaje;
        this.letra = letra;
        this.cumulative = cumulative;
    }

    public static void Add(string name, float percentage, List<KeyRandom> lista)
    {
        float cumulative = (lista.Count == 0) ? percentage : lista[^1].cumulative + percentage;
        lista.Add(new KeyRandom(name, percentage, cumulative));
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

    //Static Elements, that are serialized from Menu
    public static bool randomMode = true;
    public static Difficulty dificulty = Difficulty.Easy;
    public static string levelPath;
    public static bool language = true;
    public static bool english = true;

    [SerializeField] Click click;
    void Awake()
    {
        spawningSpeed = 1f;
        LoadGameObjects();
        if (language)
            if (english)
                createPercentajeEnglish();
            else
                createPercentajeSpanish();
        settingSpeedAndSpawnRate();
        if(!randomMode)
            ReadLevel();
        else
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
            if (language)
                StartCoroutine(CreateObjectLanguage());
            else
                StartCoroutine(CreateObjectRandomV3());
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
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/A") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/B") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/C") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/D") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/E") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/F") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/G") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/H") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/I") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/J") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/K") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/L") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/M") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/N") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/O") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/P") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/Q") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/R") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/S") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/T") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/U") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/V") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/W") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/X") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/Y") as GameObject);
        letterList.Add((GameObject)Resources.Load("GameObjects/Letras2/Z") as GameObject);
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
        KeyRandom.Add("A", (float)0.120365034, percentageList);
        KeyRandom.Add("B", (float)0.013640730, percentageList);
        KeyRandom.Add("C", (float)0.044956772, percentageList);
        KeyRandom.Add("D", (float)0.056292027, percentageList);
        KeyRandom.Add("E", (float)0.131412104, percentageList);
        KeyRandom.Add("F", (float)0.006628242, percentageList);
        KeyRandom.Add("G", (float)0.009702209, percentageList);
        KeyRandom.Add("H", (float)0.006724304, percentageList);
        KeyRandom.Add("I", (float)0.060038425, percentageList);
        KeyRandom.Add("J", (float)0.042267051, percentageList);
        KeyRandom.Add("K", (float)0.000192123, percentageList);
        KeyRandom.Add("L", (float)0.047742555, percentageList);
        KeyRandom.Add("M", (float)0.030259366, percentageList);
        KeyRandom.Add("N", (float)0.064457253, percentageList);
        KeyRandom.Add("O", (float)0.083381364, percentageList);
        KeyRandom.Add("P", (float)0.024111431, percentageList);
        KeyRandom.Add("Q", (float)0.008453410, percentageList);
        KeyRandom.Add("R", (float)0.065994236, percentageList);
        KeyRandom.Add("S", (float)0.076657061, percentageList);
        KeyRandom.Add("T", (float)0.044476465, percentageList);
        KeyRandom.Add("U", (float)0.037752161, percentageList);
        KeyRandom.Add("V", (float)0.008645533, percentageList);
        KeyRandom.Add("W", (float)0.000096061, percentageList);
        KeyRandom.Add("X", (float)0.002113353, percentageList);
        KeyRandom.Add("Y", (float)0.008645533, percentageList);
        KeyRandom.Add("Z", (float)0.004995197, percentageList);
        sumTotalPercentageList = 0;
        foreach (var item in percentageList) sumTotalPercentageList += item.percentaje;
    }
    public void createPercentajeEnglish()
    {
        percentageList = new List<KeyRandom>();
        KeyRandom.Add("A", (float)0.8167, percentageList);
        KeyRandom.Add("B", (float)0.1492, percentageList);
        KeyRandom.Add("C", (float)0.2782, percentageList);
        KeyRandom.Add("D", (float)0.4253, percentageList);
        KeyRandom.Add("E", (float)1.2702, percentageList);
        KeyRandom.Add("F", (float)0.2228, percentageList);
        KeyRandom.Add("G", (float)0.2015, percentageList);
        KeyRandom.Add("H", (float)0.6094, percentageList);
        KeyRandom.Add("I", (float)0.6966, percentageList);
        KeyRandom.Add("J", (float)0.0253, percentageList);
        KeyRandom.Add("K", (float)0.1772, percentageList);
        KeyRandom.Add("L", (float)0.4025, percentageList);
        KeyRandom.Add("M", (float)0.2406, percentageList);
        KeyRandom.Add("N", (float)0.6749, percentageList);
        KeyRandom.Add("O", (float)0.7507, percentageList);
        KeyRandom.Add("P", (float)0.1929, percentageList);
        KeyRandom.Add("Q", (float)0.0095, percentageList);
        KeyRandom.Add("R", (float)0.5987, percentageList);
        KeyRandom.Add("S", (float)0.6327, percentageList);
        KeyRandom.Add("T", (float)0.9056, percentageList);
        KeyRandom.Add("U", (float)0.2758, percentageList);
        KeyRandom.Add("V", (float)0.0978, percentageList);
        KeyRandom.Add("W", (float)0.2360, percentageList);
        KeyRandom.Add("X", (float)0.0250, percentageList);
        KeyRandom.Add("Y", (float)0.1974, percentageList);
        KeyRandom.Add("Z", (float)0.0074, percentageList);
        sumTotalPercentageList = 0;
        foreach (var item in percentageList) sumTotalPercentageList += item.percentaje;
    }

    public int letterToArrayIndex(string letter)
    {
        return (int)letter.ToCharArray()[0] - (int)'A' + 1;
    }

    IEnumerator CreateObjectRandomV3()
    {
        Debug.Log("CreateObjectRandomV3");
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

    IEnumerator CreateObjectLanguage()
    {
        Debug.Log("CreateObjectLanguage");
        while (true)
        {
            float random;
            GameObject actualLetter;
            //Debug.Log("First Time" + firstTime);
            random = UnityEngine.Random.Range(0.0f, sumTotalPercentageList);
            int index = BinarySearch(random, percentageList);
            actualLetter = (GameObject)letterList[index];
            movingObject = Instantiate(actualLetter, new Vector3(actualLetter.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    private int BinarySearch(float value, List<KeyRandom> lista)
    {
        int low = 0;
        int high = lista.Count - 1;

        while (low < high)
        {
            int mid = (low + high) / 2;
            if (value < lista[mid].cumulative)
                high = mid;
            else
                low = mid + 1;
        }

        return low;
    }

    private void settingSpeedAndSpawnRate()
    {
        if (dificulty == Difficulty.Easy)
        {
            Movement.speed = 3f;
            spawningSpeed = 1.6f;
        }
        if (dificulty == Difficulty.Medium)
        {
            Movement.speed = 4f;
            spawningSpeed = 0.8f;
        }
        if (dificulty == Difficulty.Hard)
        {
            Movement.speed = 5f;
            spawningSpeed = 0.4f;
        }

    }
    void TestLevel()
    {
        SoundFXScript.instance.PlayAudio(backgroundMusic, 1f, MusicTypes.music);
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

    public static void ConvertTimeToWait(string path, bool timeToWait){
        List<LevelKey> levelListAux = new List<LevelKey>();
        bool firstLine = true;
        foreach (string line in File.ReadLines(path, Encoding.UTF8))
        {
            if(firstLine){
            firstLine=false;
            }else{
            string[] words = line.Split(';');
            LevelKey key = new LevelKey(words[0], Int32.Parse(words[1]), Int32.Parse(words[2]));
            levelListAux.Add(key);
            }
        }
        int accumulatedValue=0;
        for(int i = 0; i < levelListAux.Count; i++){
        if(timeToWait){
            levelListAux[i].setTimeToAppear(accumulatedValue);
            accumulatedValue+=levelListAux[i].getTimeToWait();
        }
        else{
            if (i < levelListAux.Count - 1){
                int nextValue = levelListAux[i+1].getTimeToAppear();
                levelListAux[i].setTimeToWait(nextValue-levelListAux[i].getTimeToAppear());
            }
        }
        }
        using (StreamWriter writer = new StreamWriter("./Assets/Levels/level.csv"))
        {
            foreach (LevelKey levelKey in levelListAux)
                writer.WriteLine(levelKey.ToString());
        }
    }

    void ReadLevel()
    {
        levelList = new ArrayList();
        bool firstLine = true;
        foreach (string line in File.ReadLines(levelPath, Encoding.UTF8))
        {
            if(firstLine){
            firstLine=false;
            string[] words = line.Split(';');
            backgroundMusic = (AudioClip)Resources.Load(words[0]);
            Debug.Log(words[1]);
            Movement.speed= float.Parse(words[1]);
            }else{
            string[] words = line.Split(';');
            LevelKey key = new LevelKey(words[0], Int32.Parse(words[1]), Int32.Parse(words[2]));
            levelList.Add(key);
            }
        }
    }

    private IEnumerator PlayLevel()
    {
        foreach (LevelKey levelKey in levelList)
        {
            GameObject letter = dicctionaryList[levelKey.getKey()];
            movingObject = Instantiate(letter, new Vector3(letter.transform.position.x, topScreen.transform.position.y, 0), Quaternion.identity);
            //velocity = new Vector2(0, -downspeed);
            //StartCoroutine(MoveObject(movingObject, 0.05f, rb2D));
            yield return new WaitForSeconds((float)(levelKey.getTimeToWait() / 1000.0));
        }
        //After that, when the movingObject is deleted that means that the level is finshed
        StartCoroutine(CheckIfObjectIsDestroyed());
    }
    private IEnumerator CheckIfObjectIsDestroyed(){
        while(movingObject!=null){
            yield return new WaitForSeconds(1.0f);
        }
        click.EndLevel("HAS GANADO");
    }
}
