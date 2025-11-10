using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject resultPanel;

    private void Start()
    {
        mainPanel.SetActive(true);
        resultPanel.SetActive(false);
    }

    public void ShowResultPanel(bool show)
    {
        resultPanel.SetActive(show);
    }
}
