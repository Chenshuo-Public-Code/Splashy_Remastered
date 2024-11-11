using UnityEngine;

public class CameraController: MonoBehaviour
{
    public Transform Player;
    public float FollowSpeed = 5f;
    private Vector3 offset; 
   

    private void Start()
    {
        offset = transform.position - Player.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, Player.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeed*Time.deltaTime);
    }
}
