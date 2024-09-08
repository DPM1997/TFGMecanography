using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplayScript : MonoBehaviour
{
    /// <summary>
    /// Components of the Prefab.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _rankText, _usernameText, _scoreText;

    /// <summary>
    /// Updates the prefab with the actual data. If the player has scored in this session, print the score in yellow.
    /// </summary>
    /// <param name="playerInfo">Name and score</param>
    /// <param name="rank">Rank</param>
    /// <param name="isNew">If the player has scored in this session</param>
    public void SetEntry(PlayerInfo playerInfo, int rank, bool isNew)
    {
        _rankText.text = rank.ToString();
        _usernameText.text = playerInfo.name;
        _scoreText.text = playerInfo.score.ToString();
        GetComponent<Image>().color = isNew ? Color.yellow : Color.white;
    }
}
