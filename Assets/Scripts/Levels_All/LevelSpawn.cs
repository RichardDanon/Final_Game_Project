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

    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> players = GameObject.FindGameObjectsWithTag("player").ToList();
        players.ForEach(player =>
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.transform.position = new Vector2(x, y);
        });
    }
}
