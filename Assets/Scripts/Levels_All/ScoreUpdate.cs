using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreTMP;


    void Update()
    {
        //Update every frame for the number of hits done by the player
        if (GlobalVariables.playerScores.TryGetValue(SceneManager.GetActiveScene().name, out int hits))
        {
            GlobalVariables.playerScores[SceneManager.GetActiveScene().name] = GlobalVariables.numOfHitsForLvl;
        }

        scoreTMP.text = string.Join("\n", GlobalVariables.playerScores.Select(d => string.Format("{0}: \n Strokes {1}", d.Key, d.Value)));

    }
}
