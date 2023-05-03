using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreTMP;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.playerScores.TryGetValue(SceneManager.GetActiveScene().name, out int hits))
        {
            GlobalVariables.playerScores[SceneManager.GetActiveScene().name] = GlobalVariables.numOfHitsForLvl;
        }

        scoreTMP.text = string.Join("\n", GlobalVariables.playerScores.Select(d => string.Format("{0}: \n Strokes {1}", d.Key, d.Value)));

    }
}
