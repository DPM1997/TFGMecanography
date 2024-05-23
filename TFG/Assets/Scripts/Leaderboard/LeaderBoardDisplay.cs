using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText, _usernameText, _scoreText;

    public void SetEntry(PlayerInfo playerInfo, int rank, bool isNew)
    {
        _rankText.text = rank.ToString();
        _usernameText.text = playerInfo.name;
        _scoreText.text = playerInfo.score.ToString();
        GetComponent<Image>().color = isNew ? Color.yellow : Color.white;
    }
}
