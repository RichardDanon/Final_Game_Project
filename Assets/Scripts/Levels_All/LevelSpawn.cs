using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    [SerializeField]
    private float x, y;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        players.ForEach(player =>
        {
            player.GetComponent<playerNetwork>().IsLevelCompleted = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.transform.position = new Vector2(x, y);
            //coroutine for time of boxcollider is trigger with players only
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
