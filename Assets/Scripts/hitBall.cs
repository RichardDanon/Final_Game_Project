using Unity.Netcode;
using UnityEngine;

public class hitBall : NetworkBehaviour
{
    [SerializeField]
    private float maxDragLength = 0.1f;


    private bool isMoving;

    private Rigidbody2D rb2d;

    LineRenderer lineRenderer;





    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();


        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;


    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {


            if (rb2d.velocity.magnitude < 0.05f)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }


            if (!isMoving)
            {

                Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float dist = Mathf.Clamp(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), 0, maxDragLength);
                Vector3 endPos = (Vector2)transform.position + (dir.normalized * dist);
                lineRenderer.SetPosition(1, endPos);


                lineRenderer.SetPosition(0, this.gameObject.transform.position);
                Debug.Log("Not Moving");


                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    float force = (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) * 100 / maxDragLength);
                    rb2d.AddForce(-(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * force * 10);
                }
            }



        }
    }
}
