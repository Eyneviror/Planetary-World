using  UnityEngine;
using  System;
using Random = UnityEngine.Random;

//TODO: No asset usage.
public class ThunderEffect : MonoBehaviour
{
    [SerializeField] private float randomForce;
    
    private LineRenderer line;
    private Vector3[] cashedPoints;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        cashedPoints = new Vector3[line.positionCount];
        for (int i = 0; i < cashedPoints.Length; i++)
        {
            cashedPoints[i] = line.GetPosition(i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < cashedPoints.Length; i++)
        {
            line.SetPosition(i, new Vector3(cashedPoints[i].x,
                cashedPoints[i].y+Random.Range(-randomForce,randomForce),
                cashedPoints[i].z));
        }
    }
}
