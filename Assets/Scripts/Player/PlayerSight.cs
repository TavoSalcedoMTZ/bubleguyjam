using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    public Vector3[] sightPositions = new Vector3[4]; 
    public ShotSight actualShotSight;
    public Transform shotSightTransform;

    public void Start()
    {
        SetSight(ShotSight.Right);
    }

    public void SetSight(ShotSight newSight)
    {
        int index = (int)newSight; 
        if (index >= 0 && index < sightPositions.Length)
        {
            shotSightTransform.localPosition = sightPositions[index];
            actualShotSight = newSight;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetSight(ShotSight.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetSight(ShotSight.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetSight(ShotSight.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetSight(ShotSight.Right);
        }
    }
}
