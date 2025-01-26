using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public Transform transformprincipal;
    public Transform transform1;
    public Transform transform2;
    public Transform transform3;
    public Transform transform4;



    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            AssignTransformValues(transform1);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            AssignTransformValues(transform4);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            AssignTransformValues(transform2);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            AssignTransformValues(transform3);

        }
    }

    private void AssignTransformValues(Transform source)
    {
        transformprincipal.position = source.position;
        transformprincipal.rotation = source.rotation;
        transformprincipal.localScale = source.localScale;
    }
}