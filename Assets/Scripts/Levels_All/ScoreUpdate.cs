using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        scoreTMP.text = string.Join("\n", GlobalVariables.playerScores.Select(d => string.Format("<align=left>{0}: {1} Strokes</align>", d.Key, d.Value)));
    }
}
