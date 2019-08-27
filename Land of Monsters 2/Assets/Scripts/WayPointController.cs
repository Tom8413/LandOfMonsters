using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetwaypoint;
    private int targetWayPointIndex;
    private float minDistance = 0.1f;
    private float lastWayPointIndex;

    private float movmentspeed = 0.50f;
    private float rotationSpeed = 10.00f;

    // Start is called before the first frame update
    void Start()
    {
        lastWayPointIndex = waypoints.Count - 1;
        targetwaypoint = waypoints[targetWayPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float movmentStep = movmentspeed * Time.deltaTime;
        float rotationStep = rotationSpeed - Time.deltaTime;

        //vectro3 przechowuje kierunek w którym będziemy poruszać się do wroga
        Vector3 directionToTarget = targetwaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        //obiekt obraca się do wroga w pewnym przedziale czasu
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        float distance = Vector3.Distance(transform.position, targetwaypoint.position);
        CheckDistanceToWayPoint(distance);

        transform.position = Vector3.MoveTowards(transform.position, targetwaypoint.position, movmentStep);
    }

    void CheckDistanceToWayPoint(float currenDistance)
    {
        if(currenDistance <= minDistance)
        {
            targetWayPointIndex++;
            UpdateTargetWayPoint();
        }
    }

    //określamy do którego targetu udajemy się
    void UpdateTargetWayPoint()
    {
        if(targetWayPointIndex > lastWayPointIndex)
        {
            targetWayPointIndex = 0;
        }
        targetwaypoint = waypoints[targetWayPointIndex];
    }
}
