using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ThirdPersonCharacter character;
    private Camera cam;
    private NavMeshAgent agent;
    private RaycastHit hitInfo;
    private Ray ray;

    private bool crouch = false;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                agent.destination = hitInfo.point;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Move(Vector3.zero, crouch, true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = crouch ? false : true;
        }
        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, crouch, false);
        else
            character.Move(Vector3.zero, crouch, false);
    }
}
