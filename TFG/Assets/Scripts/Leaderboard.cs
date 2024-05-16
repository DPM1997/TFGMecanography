using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;


public class PlayerInfo: IComparable<PlayerInfo>{
    public string name;
    public int score;

    public static string levelPath;

    public PlayerInfo(string name, int score){
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
}
public class Leaderboard : MonoBehaviour
{
    SortedSet<PlayerInfo> collectedStats;
    [SerializeField] TMP_Text score_text;
    // Start is called before the first frame update
    void Start()
    {
        collectedStats = new SortedSet<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SubmitUser(){
        PlayerInfo stats = new PlayerInfo(PlayerPrefs.GetString("User", "default"),Int32.Parse(score_text.text));
        collectedStats.Add(stats);
    }

    void ImportLeatherBoard(){
        
    }

    void ExportLeaderBoard()
    {
        using (StreamWriter writer = new StreamWriter("./Assets/Levels/level.csv"))
        {
            foreach (PlayerInfo player in collectedStats)
                writer.WriteLine(player.ToString());
        }
    }

}
