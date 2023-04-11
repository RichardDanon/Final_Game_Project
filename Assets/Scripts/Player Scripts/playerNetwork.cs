using Unity.Netcode;
using UnityEngine;

public class playerNetwork : NetworkBehaviour
{

    //Variables
    [SerializeField]
    private GameObject ball;

    private int previousLength = 0;

    //private NetworkVariable<Dictionary<int, Color>> colors = new(new Dictionary<int, Color>());

    private GameObject[] players;



    // Start is called before the first frame update
    void Start()

    {

        SpriteRenderer sprite = ball.GetComponent<SpriteRenderer>();
        Color color = Random.ColorHSV();
        sprite.color = color;
        //colors.Value.Add(this.GetInstanceID(), color);

    }

    private void Update()
    {
        /*
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length <= previousLength)
            foreach (GameObject p in players)
            {
                if (colors.Value.TryGetValue(p.GetInstanceID(), out Color color))
                {

                    p.GetComponent<SpriteRenderer>().color = color;
                }


            }
        previousLength = players.Length;
        */
    }
}
