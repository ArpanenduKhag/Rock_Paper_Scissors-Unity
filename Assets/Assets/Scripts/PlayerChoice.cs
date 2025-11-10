using UnityEngine;

public class PlayerChoice : MonoBehaviour
{
    public string choiceName; // Rock, Paper, or Scissors

    public void OnClickChoice()
    {
        GameManager.Instance.PlayerChoice(choiceName);
    }
}
