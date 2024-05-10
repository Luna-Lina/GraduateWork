using StarVelocity.Data;
using TMPro;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _userName;
    [SerializeField] private TMP_Text _userScore;

    public void SetData(FirebaseWrapper.PlayerData player)
    {
        _userName.text = player.playerName;
        _userScore.text = player.playerScore.ToString();
    }
}
