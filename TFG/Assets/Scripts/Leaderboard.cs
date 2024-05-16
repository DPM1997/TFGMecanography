using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Text;


public class PlayerInfo : IComparable<PlayerInfo>
{
    public string name;
    public int score;

    public static string levelPath;

    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public int CompareTo(PlayerInfo other)
    {
        // Comparar por puntuación, y si son iguales, comparar por nombre
        if (score == other.score)
        {
            return name.CompareTo(other.name);
        }
        return other.score.CompareTo(score); // Orden descendente por puntuación
    }
    
    public override string ToString(){
        return name+';'+score;
    }  
}
public class Leaderboard : MonoBehaviour
{
    SortedSet<PlayerInfo> collectedStats;
    // Start is called before the first frame update
    void Start()
    {
        collectedStats = new SortedSet<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SubmitUser(int score)
    {
        ImportLeatherBoard();
        PlayerInfo stats = new PlayerInfo(PlayerPrefs.GetString("User", "default"), score);
        collectedStats.Add(stats);
        ExportLeaderBoard();
    }

    void ImportLeatherBoard()
    {
        string route;
        if (ScriptBajada.randomMode == true)
        {
            route = "./Assets/LeaderBoard/random-" + ScriptBajada.dificulty.ToString() + ".csv";
        }
        else
        {
            String levelPath = ScriptBajada.levelPath;
            string level = levelPath.Split('/').Last().Split('.').First();
            route = "./Assets/LeaderBoard/random-" + level + ".csv";
        }
        try
        {
            foreach (string line in File.ReadLines(route, Encoding.UTF8))
            {
                string[] words = line.Split(';');
                PlayerInfo player = new PlayerInfo(words[0], Int32.Parse(words[1]));
                collectedStats.Add(player);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("No existe el documento");
        }

    }

    void ExportLeaderBoard()
    {
        string route;
        if (ScriptBajada.randomMode == true)
        {
            route = "./Assets/LeaderBoard/random-" + ScriptBajada.dificulty.ToString() + ".csv";
        }
        else
        {
            String levelPath = ScriptBajada.levelPath;
            string level = levelPath.Split('/').Last().Split('.').First();
            route = "./Assets/LeaderBoard/random-" + level + ".csv";
        }
        using (StreamWriter writer = new StreamWriter(route))
        {
            foreach (PlayerInfo player in collectedStats)
                writer.WriteLine(player.ToString());
        }
    }

}
