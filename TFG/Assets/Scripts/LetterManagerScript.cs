using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEditor.Search;

/// <summary>
/// Data of the letter in a level
/// </summary>
public class LevelKey
{
    /// <summary>
    /// Letter
    /// </summary>
    string key;
    /// <summary>
    /// Time in miliseconds to spawn from the start
    /// </summary>
    int timeToAppear;
    /// <summary>
    /// Time in miliseconds to wait for the next key
    /// </summary>
    int timeToWait;
    /// <summary>
    /// Parameter constructor
    /// </summary>
    /// <param name="key">Letter</param>
    /// <param name="timeToAppear">Time in miliseconds to spawn from the start</param>
    /// <param name="timeToWait">Time in miliseconds to wait for the next key</param>
    public LevelKey(string key, int timeToAppear, int timeToWait)
    {
        this.key = key;
        this.timeToAppear = timeToAppear;
        this.timeToWait = timeToWait;
    }

    /// <summary>
    /// To string override
    /// </summary>
    /// <returns>key;timeToAppear;timeToWait</returns>
    public override string ToString()
    {
        return "" + key + ";" + timeToAppear + ";" + timeToWait + "";
    }

    /// <returns>current letter</returns>
    public string getKey()
    {
        return key;
    }

    /// <returns>current timeToWait</returns>
    public int getTimeToWait()
    {
        return timeToWait;
    }

    /// <returns>current timeToAppear</returns>
    public int getTimeToAppear()
    {
        return timeToAppear;
    }

    /// <param name="timeToWait">time to set</param>
    public void setTimeToWait(int timeToWait)
    {
        this.timeToWait = timeToWait;
    }

    /// <param name="timeToAppear">time to set</param>
    public void setTimeToAppear(int timeToAppear)
    {
        this.timeToAppear = timeToAppear;
    }

}

/// <summary>
/// Data of the letter in random mode with dicctionary
/// </summary>
public class KeyRandom
{
    /// <summary>
    /// Percentage individual of spawn
    /// </summary>
    public float percentaje { get; set; }
    /// <summary>
    /// Percentage cumulative of spawn
    /// </summary>
    public float cumulative { get; set; }
    /// <summary>
    /// Letter
    /// </summary>
    public string letter { get; set; }

    /// <summary>
    /// Parameterized constructors
    /// </summary>
    /// <param name="letter">Letter</param>
    /// <param name="percentaje">Percentage individual of spawn</param>
    /// <param name="cumulative">Percentage cumulative of spawn</param>
    public KeyRandom(string letter, float percentaje, float cumulative)
    {
        this.percentaje = percentaje;
        this.letter = letter;
        this.cumulative = cumulative;
    }

    /// <summary>
    /// Creates a KeyRandom and added to lista, the cumulative values is calculated from lista.
    /// </summary>
    /// <param name="name">Letter</param>
    /// <param name="percentage">Percentage individual of spawn</param>
    /// <param name="lista">List to add the KeyRandom</param>
    public static void Add(string name, float percentage, List<KeyRandom> lista)
    {
        float cumulative = (lista.Count == 0) ? percentage : lista[^1].cumulative + percentage;
        lista.Add(new KeyRandom(name, percentage, cumulative));
    }
}

/// <summary>
/// Enumerated with all the possibles difficulties
/// </summary>
public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

/// <summary>
/// Main script of the games, manages all the spawn of the letters and the gamemodes
/// </summary>
public class LetterManagerScript : MonoBehaviour
{

    /// <summary>
    /// Dictionary used in Level mode to load the objects
    /// </summary>
    private Dictionary<string, GameObject> dicctionaryList;
    /// <summary>
    /// List of letters in order used in Level mode
    /// </summary>
    private ArrayList levelList;
    /// <summary>
    /// List of letters that are used in Random mode without dicctionary
    /// </summary>
    private ArrayList letterList;
    /// <summary>
    /// List of letters that are used in Random mode with dicctionary.
    /// </summary>
    private List<KeyRandom> percentageList;
    /// <summary>
    /// Parameter used to calculate the random number in Random mode with dicctionary.
    /// </summary>
    private float sumTotalPercentageList;

    /// <summary>
    /// The last letter spawned, only used to check if the level is finished.
    /// </summary>
    private GameObject movingObject;
    /// <summary>
    /// Speed of the letters in the game.
    /// </summary>
    private float spawningSpeed;
    /// <summary>
    /// Clip of the background music in Random mode.
    /// </summary>
    private AudioClip backgroundMusic;
    /// <summary>
    /// Time in seconds that the first letter of a level is delayed
    /// </summary>
    private float delayed = 0;

    //Elements from the menu
    /// <summary>
    /// Position Y where the letters has to spawn.
    /// </summary>
    public GameObject topScreen;

    //Static Elements, that are serialized from Menu
    /// <summary>
    /// Random mode boolean. True if Random mode
    /// </summary>
    public static bool randomMode = true;
    /// <summary>
    /// Difficulty of the game. Values can be Easy, Medium, Hard
    /// </summary>
    public static Difficulty dificulty = Difficulty.Easy;
    /// <summary>
    /// Path of the level
    /// </summary>
    public static string levelPath;
    /// <summary>
    /// If a dictionary of letters is used
    /// </summary>
    public static bool language = true;
    /// <summary>
    /// Only used if language is true. Selects if the dicctionary is English or Spanish
    /// </summary>
    public static bool english = true;
    /// <summary>
    /// See <see cref="ClickScript">
    /// </summary>
    [SerializeField] ClickScript click;

    /// <summary>
    /// Initialize variables and set all the data structures.
    /// </summary>
    private void Awake()
    {
        spawningSpeed = 1f;
        LoadGameObjects();
        if (language)
            if (english)
                createPercentajeEnglish();
            else
                createPercentajeSpanish();
        settingSpeedAndSpawnRate();
        if (!randomMode)
            ReadLevel();
        else
            backgroundMusic = (AudioClip)Resources.Load("Audio/Music/RandomLevel-38");


    }
    /// <summary>
    /// Start the game mode chose in the menu.
    /// </summary>
    private void Start()
    {
        if (randomMode)
        {
            SoundFXScript.instance.PlayAudio(backgroundMusic, 1f, MusicTypes.music, 0);
            if (language)
                StartCoroutine(CreateObjectLanguage());
            else
                StartCoroutine(CreateObjectRandomV3());
        }
        else
            TestLevel();
    }

    /// <summary>
    /// Loads the letters for the Random mode without dicctionary
    /// </summary>
    private void LoadGameObjects()
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
        //Load in dicctionary for the levels
        dicctionaryList = new Dictionary<string, GameObject>();
        foreach (GameObject letter in letterList)
        {
            dicctionaryList.Add(letter.name, letter);
        }
    }

    /// <summary>
    /// Load the spanish dicctionary for the Random mode with dicctionary
    /// </summary>
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

    /// <summary>
    /// Load the english dicctionary for the Random mode with dicctionary
    /// </summary>
    public void createPercentajeEnglish()
    {
        percentageList = new List<KeyRandom>();
        KeyRandom.Add("A", (float)0.08167, percentageList);
        KeyRandom.Add("B", (float)0.01492, percentageList);
        KeyRandom.Add("C", (float)0.02782, percentageList);
        KeyRandom.Add("D", (float)0.04253, percentageList);
        KeyRandom.Add("E", (float)0.12702, percentageList);
        KeyRandom.Add("F", (float)0.02228, percentageList);
        KeyRandom.Add("G", (float)0.02015, percentageList);
        KeyRandom.Add("H", (float)0.06094, percentageList);
        KeyRandom.Add("I", (float)0.06966, percentageList);
        KeyRandom.Add("J", (float)0.00253, percentageList);
        KeyRandom.Add("K", (float)0.01772, percentageList);
        KeyRandom.Add("L", (float)0.04025, percentageList);
        KeyRandom.Add("M", (float)0.02406, percentageList);
        KeyRandom.Add("N", (float)0.06749, percentageList);
        KeyRandom.Add("O", (float)0.07507, percentageList);
        KeyRandom.Add("P", (float)0.01929, percentageList);
        KeyRandom.Add("Q", (float)0.00095, percentageList);
        KeyRandom.Add("R", (float)0.05987, percentageList);
        KeyRandom.Add("S", (float)0.06327, percentageList);
        KeyRandom.Add("T", (float)0.09056, percentageList);
        KeyRandom.Add("U", (float)0.02758, percentageList);
        KeyRandom.Add("V", (float)0.00978, percentageList);
        KeyRandom.Add("W", (float)0.02360, percentageList);
        KeyRandom.Add("X", (float)0.00250, percentageList);
        KeyRandom.Add("Y", (float)0.01974, percentageList);
        KeyRandom.Add("Z", (float)0.00074, percentageList);
        sumTotalPercentageList = 0;
        foreach (var item in percentageList) sumTotalPercentageList += item.percentaje;
    }
    
    /// <summary>
    /// Spawner of letters for the Random mode without dicctionary
    /// </summary>
    IEnumerator CreateObjectRandomV3()
    {
        while (true)
        {
            int random;
            GameObject actualLetter;
            random = UnityEngine.Random.Range(0, letterList.Count);
            actualLetter = (GameObject)letterList[random];
            movingObject = Instantiate(actualLetter, new Vector3(actualLetter.transform.position.x, topScreen.transform.position.y, -0.2f), Quaternion.identity);
            yield return new WaitForSeconds(spawningSpeed);
        }
    }
    /// <summary>
    /// Spawner of letters for the Random mode with dicctionary
    /// </summary>
    IEnumerator CreateObjectLanguage()
    {
        while (true)
        {
            float random;
            GameObject actualLetter;
            random = UnityEngine.Random.Range(0.0f, sumTotalPercentageList);
            int index = BinarySearch(random, percentageList);
            actualLetter = (GameObject)letterList[index];
            movingObject = Instantiate(actualLetter, new Vector3(actualLetter.transform.position.x, topScreen.transform.position.y, -0.2f), Quaternion.identity);
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    /// <summary>
    /// Binary search with 	O(log n), also know as logarithmic search 
    /// </summary>
    /// <param name="value">Value to search, usually a random number</param>
    /// <param name="lista">Key list of the Random mode</param>
    /// <returns>Index of the list</returns>
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

    /// <summary>
    /// Set the parameters of each difficulty
    /// </summary>
    private void settingSpeedAndSpawnRate()
    {
        if (dificulty == Difficulty.Easy)
        {
            MovementScript.speed = 3f;
            spawningSpeed = 1.6f;
        }
        if (dificulty == Difficulty.Medium)
        {
            MovementScript.speed = 4f;
            spawningSpeed = 0.8f;
        }
        if (dificulty == Difficulty.Hard)
        {
            MovementScript.speed = 5f;
            spawningSpeed = 0.4f;
        }

    }
    /// <summary>
    /// Plays the music of the level and then start the level
    /// </summary>
    void TestLevel()
    {
        SoundFXScript.instance.PlayAudio(backgroundMusic, 1f, MusicTypes.music, delayed);
        StartCoroutine(PlayLevel());
    }

    /// <summary>
    /// Read the .csv file and loads it's data in levelList. Also saves speed and music path.
    /// </summary>
    void ReadLevel()
    {
        levelList = new ArrayList();
        bool firstLine = true;
        foreach (string line in File.ReadLines(levelPath, Encoding.UTF8))
        {
            if (firstLine)
            {
                firstLine = false;
                string[] words = line.Split(';');
                backgroundMusic = (AudioClip)Resources.Load(words[0]);
                MovementScript.speed = float.Parse(words[1]);
                delayed = float.Parse(words[3]);
            }
            else
            {
                string[] words = line.Split(';');
                LevelKey key = new LevelKey(words[0], Int32.Parse(words[1]), Int32.Parse(words[2]));
                levelList.Add(key);
            }
        }
    }
    /// <summary>
    /// Spawner of letters for the Level mode.
    /// </summary>
    private IEnumerator PlayLevel()
    {
        foreach (LevelKey levelKey in levelList)
        {
            if (levelKey.getKey() != "None")
            {
                GameObject letter = dicctionaryList[levelKey.getKey()];
                movingObject = Instantiate(letter, new Vector3(letter.transform.position.x, topScreen.transform.position.y, -0.2f), Quaternion.identity);
            }
            yield return new WaitForSeconds((float)(levelKey.getTimeToWait() / 1000.0));
        }
        //After that, when the movingObject is deleted that means that the level is finshed
        StartCoroutine(CheckIfObjectIsDestroyed());
    }
    /// <summary>
    /// Function called when there is no more letters in levelList. Every second checks if the last letter is destroyed.
    /// If so, shows the GameOver submenu
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckIfObjectIsDestroyed()
    {
        while (movingObject != null)
        {
            yield return new WaitForSeconds(1.0f);
        }
        click.EndLevel("YOU WIN");
    }

    /// <summary>
    /// Used in debug mode, calculate the timeToWait or the timeToAppear of a level.
    /// </summary>
    /// <param name="path">Path to the level</param>
    /// <param name="timeToWait">The value that is in the level. If true, calculates timeToAppear et viceversa</param>
    public static void ConvertTimeToWait(string path, bool timeToWait)
    {
        List<LevelKey> levelListAux = new List<LevelKey>();
        bool firstLine = true;
        foreach (string line in File.ReadLines(path, Encoding.UTF8))
        {
            if (firstLine)
            {
                firstLine = false;
            }
            else
            {
                string[] words = line.Split(';');
                LevelKey key = new LevelKey(words[0], Int32.Parse(words[1]), Int32.Parse(words[2]));
                levelListAux.Add(key);
            }
        }
        int accumulatedValue = 0;
        for (int i = 0; i < levelListAux.Count; i++)
        {
            if (timeToWait)
            {
                levelListAux[i].setTimeToAppear(accumulatedValue);
                accumulatedValue += levelListAux[i].getTimeToWait();
            }
            else
            {
                if (i < levelListAux.Count - 1)
                {
                    int nextValue = levelListAux[i + 1].getTimeToAppear();
                    levelListAux[i].setTimeToWait(nextValue - levelListAux[i].getTimeToAppear());
                }
            }
        }
        // Export the level
        using (StreamWriter writer = new StreamWriter("./Assets/Levels/level.csv"))
        {
            foreach (LevelKey levelKey in levelListAux)
                writer.WriteLine(levelKey.ToString());
        }
    }
}
