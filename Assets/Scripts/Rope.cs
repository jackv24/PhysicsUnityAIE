using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public int numLinks = 5;
    public float linkOffset = 1.0f;

    public float spring = 500.0f;

    void Start()
    {
        GameObject ball = Resources.Load<GameObject>("PhysicsSphere");

        GameObject anchor = (GameObject)Instantiate(ball, transform.position, transform.rotation);
        anchor.transform.parent = transform;
        anchor.name = "Anchor";
        anchor.GetComponent<Rigidbody>().isKinematic = true;

        GameObject[] links = new GameObject[numLinks];

        for(int i = 0; i < numLinks; i++)
        {
            links[i] = (GameObject)Instantiate(ball, transform.position - Vector3.up * linkOffset, transform.rotation);
            links[i].transform.parent = transform;
            links[i].name = "Link";
            links[i].GetComponent<Rigidbody>().isKinematic = false;

            SpringJoint joint = links[i].AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedBody = (i == 0 ? anchor : links[i - 1]).GetComponent<Rigidbody>();

            joint.anchor = Vector3.zero;
            joint.connectedAnchor = -Vector3.up;

            joint.spring = spring;
        }
    }
}
