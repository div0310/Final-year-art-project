using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followtest : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    public Transform point;
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navmeshagent.destination = point.position;
    }
}
