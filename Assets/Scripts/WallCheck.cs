using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask layerMask;
    


    public bool LeftWallCheck() => CheckWall(leftWallCheck.position);
    public bool RightWallCheck() => CheckWall(rightWallCheck.position);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(leftWallCheck.position, new Vector3(size.x, size.y, 0));
        Gizmos.DrawWireCube(rightWallCheck.position, new Vector3(size.x, size.y, 0));
    }

    private bool CheckWall(Vector2 side)
    {
       
        Collider2D coll = Physics2D.OverlapBox(side, size, 0, layerMask);
        if (coll != null)
        {
            Debug.Log("Coll: "+coll.name);
            return true;
        }
        return false;
    }


}
