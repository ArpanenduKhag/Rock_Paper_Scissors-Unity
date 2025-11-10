using UnityEngine;

public class PlayerChoice : MonoBehaviour
{
    public string choiceName; 

    public void OnClickChoice()
    {
        GameManager.Instance.PlayerChoice(choiceName);
    }
}
