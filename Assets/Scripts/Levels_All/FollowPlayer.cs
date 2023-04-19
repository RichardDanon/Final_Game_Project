using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 playerTransform;
    public int depth = -1;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform + new Vector3(0, 0, depth);
        }
    }

    public void setTarget(Vector3 target)
    {
        playerTransform = target;
    }
}