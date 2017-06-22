using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : MonoBehaviour
{
    public int numLinks = 10;
    public float linkOffset = 1.0f;

    public float spring = 5000.0f;
    public float damping = 5000f;

    public Vector3 xAxis = new Vector3(1, 0, 0);
    public Vector3 yAxis = new Vector3(0, 0, 1);

    void Start()
    {
        GameObject ball = Resources.Load<GameObject>("PhysicsSphere");

        GameObject[,] links = new GameObject[numLinks, numLinks];

        for(int i = 0; i < numLinks; i++)
        {
            for (int j = 0; j < numLinks; j++)
            {
                links[i, j] = (GameObject)Instantiate(ball, transform.position + new Vector3(linkOffset * i, 0, linkOffset * j), transform.rotation);
                links[i, j].transform.parent = transform;
                links[i, j].name = "Link " + i + "_" + j;
                links[i, j].GetComponent<Rigidbody>().isKinematic = false;

                if (i != 0)
                {
                    SpringJoint joint = links[i, j].AddComponent<SpringJoint>();
                    //joint.autoConfigureConnectedAnchor = false;
                    joint.connectedBody = links[i - 1, j].GetComponent<Rigidbody>();

                    joint.anchor = Vector3.zero;
                    joint.connectedAnchor = -xAxis;

                    joint.spring = spring;
                    joint.damper = damping;
                }

                if (j != 0)
                {
                    SpringJoint joint = links[i, j].AddComponent<SpringJoint>();
                    //joint.autoConfigureConnectedAnchor = false;
                    joint.connectedBody = links[i, j - 1].GetComponent<Rigidbody>();

                    joint.anchor = Vector3.zero;
                    joint.connectedAnchor = -yAxis;

                    joint.spring = spring;
                    joint.damper = damping;
                }
            }
        }
    }
}
