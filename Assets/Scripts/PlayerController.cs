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

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                agent.destination = hitInfo.point;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Move(Vector3.zero, crouch, true);
            agent.ResetPath();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = crouch ? false : true;
        }
        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, crouch, false);
        else
            character.Move(Vector3.zero, crouch, false);
    }
}
