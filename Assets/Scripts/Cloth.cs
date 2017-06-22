using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : MonoBehaviour
{
    public int numRows = 20;
    public int numColumns = 20;
    public float linkOffset = 1.0f;

    public float spring = 5000.0f;
    public float damping = 5000f;

    public Vector3 xAxis = new Vector3(1, 0, 0);
    public Vector3 yAxis = new Vector3(0, 0, 1);

    public bool showBalls = false;

    private GameObject[,] links;

    private MeshFilter meshFilter;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void Start()
    {
        GameObject ball = Resources.Load<GameObject>("PhysicsSphere");

        links = new GameObject[numColumns, numRows];

        for(int i = 0; i < numColumns; i++)
        {
            for (int j = 0; j < numRows; j++)
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

        if(meshFilter)
        {
            meshFilter.mesh = new Mesh();

            UpdateCloth();

            // TODO set up UV coordinates

            // set up the topology
            int[] triangles = new int[(numRows - 1) * (numColumns - 1) * 12];
            int k = 0;

            for (int i = 0; i < numColumns - 1; i++)
            {
                for (int j = 0; j < numRows - 1; j++)
                {
                    triangles[k] = i + j * numColumns; k++;
                    triangles[k] = (i + 1) + j * numColumns; k++;
                    triangles[k] = i + (j + 1) * numColumns; k++;
                    triangles[k] = i + (j + 1) * numColumns; k++;
                    triangles[k] = (i + 1) + j * numColumns; k++;
                    triangles[k] = (i + 1) + (j + 1) * numColumns; k++;
                    // do the triangles both ways for two-sided rendering
                    triangles[k] = i + j * numColumns; k++;
                    triangles[k] = i + (j + 1) * numColumns; k++;
                    triangles[k] = (i + 1) + j * numColumns; k++;
                    triangles[k] = (i + 1) + j * numColumns; k++;
                    triangles[k] = i + (j + 1) * numColumns; k++;
                    triangles[k] = (i + 1) + (j + 1) * numColumns; k++;
                }
            }
            meshFilter.mesh.triangles = triangles;
        }
    }

    void Update()
    {
        UpdateCloth();
    }

    void UpdateCloth()
    {
        if (meshFilter)
        {
            Vector3[] vertices = new Vector3[numRows * numColumns];
            Vector3[] normals = new Vector3[numRows * numColumns];
            for (int i = 0; i < numColumns; i++)
            {
                for (int j = 0; j < numRows; j++)
                {
                    vertices[i + j * numColumns] = links[i, j].transform.position - transform.position;
                    links[i, j].GetComponent<MeshRenderer>().enabled = showBalls;
                }
            }
            for (int i = 0; i < numColumns; i++)
            {
                for (int j = 0; j < numRows; j++)
                {
                    Vector3 left = i == 0 ? vertices[i + j * numColumns] : vertices[i - 1 + j * numColumns];
                    Vector3 right = i == numColumns - 1 ? vertices[i + j * numColumns] : vertices[i + 1 + j * numColumns];
                    Vector3 down = j == 0 ? vertices[i + j * numColumns] : vertices[i + (j - 1) * numColumns];
                    Vector3 up = j == numRows - 1 ? vertices[i + j * numColumns] : vertices[i + (j + 1) * numColumns];
                    normals[i + j * numColumns] = Vector3.Cross(right - left, up - down);
                    normals[i + j * numColumns].Normalize();
                }
            }
            meshFilter.mesh.vertices = vertices;
            meshFilter.mesh.normals = normals;
            meshFilter.mesh.RecalculateBounds();
        }
    }
}
