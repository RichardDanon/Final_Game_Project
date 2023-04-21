using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]
    private Text score;
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
        score.text = string.Join("\n", GlobalVariables.playerScores.Select(d => string.Format(" {0}: {1} Strokes", d.Key, d.Value)));
    }
}
