using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public GameObject TargetPosition;

    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetPosition.transform.position, 0.1f);
    }
}
