using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{

    Dictionary<string, List<string>> worlds;
    List<string> worldsList;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text[] worldsText;
    [SerializeField] Leaderboard leaderboard;
    [SerializeField] MainMenu menu;
    [SerializeField] TMP_Text durationValue, scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        worldsList = new List<string>(worlds.Keys);
        //CargarObjetos de Niveles
        //TODO Comprobar si hay menos de 3 mundos
        worldsText[0].text = worldsList[worldsList.Count - 1];
        worldsText[1].text = worldsList[0];
        if(worldsList.Count!=1)
            worldsText[2].text = worldsList[1];
        else
            worldsText[2].text = worldsList[0];
        levelText.text = worlds[worldsText[1].text][0];
        loadLevelMetadata();
    }

    private void LoadData()
    {
        string folderPath = "./Assets/Levels";
        string[] levels = Directory.GetFiles(folderPath, "*.csv");
        worlds = new Dictionary<string, List<string>>();
        foreach (var level in levels)
        {
            // Dividir el string por '-'
            string fileName = Path.GetFileName(level);
            string[] parts = fileName.Split('-');
            string key = parts[0];
            string[] levelpath = parts[1].Split('.');
            string levelName = levelpath[0];

            // Si el diccionario no contiene la clave, añadirla con una lista vacía
            if (!worlds.ContainsKey(key))
            {
                worlds[key] = new List<string>();
            }
            // Añadir el string a la lista correspondiente en el diccionario  
            worlds[key].Add(levelName);
        }
    }

    public void changeWorld(bool forward)
    {
        if (forward)
        {
            int actualIndex = worldsList.IndexOf(worldsText[2].text);
            worldsText[0].text = worldsText[1].text;
            worldsText[1].text = worldsText[2].text;
            if (actualIndex == worldsList.Count - 1)
                worldsText[2].text = worldsList[0];
            else
                worldsText[2].text = worldsList[actualIndex + 1];
            levelText.text = worlds[worldsText[1].text][0];
        }
        else
        {
            int actualIndex = worldsList.IndexOf(worldsText[0].text);
            worldsText[2].text = worldsText[1].text;
            worldsText[1].text = worldsText[0].text;
            if (actualIndex == 0)
                worldsText[0].text = worldsList[worldsList.Count - 1];
            else
                worldsText[0].text = worldsList[actualIndex - 1];
            levelText.text = worlds[worldsText[1].text][0];
        }
        loadLevelMetadata();
    }


    public void changeLevel(bool forward)
    {
        List<string> world = worlds[worldsText[1].text];
        int actualIndex = world.IndexOf(levelText.text);
        if (forward)
            if (actualIndex == world.Count - 1)
                levelText.text = world[0];
            else
                levelText.text = world[actualIndex + 1];
        else
            if (actualIndex == 0)
            levelText.text = world[world.Count - 1];
        else
            levelText.text = world[actualIndex - 1];
        loadLevelMetadata();
    }

    private void loadLevelMetadata()
    {
        List<PlayerInfo> playerInfos = leaderboard.ImportLeatherBoardLevel(worldsText[1].text +'-'+ levelText.text);
        //TODO Meterse en nivel y cargar datos de Duración
        string user = PlayerPrefs.GetString("User", "default");
        foreach (PlayerInfo player in playerInfos)
            if (player.name == user)
                scoreValue.text = player.score + "";

    }

    public void loadLevel(){
        menu.levelMode("./Assets/Levels/"+worldsText[1].text+levelText.text+".csv");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
