using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
//Makes sure this script requires mesh filter as a dependency
public class MeshGeneration : MonoBehaviour
{
    Vector3[] newVertices;
    //represent corners of each mesh
    Vector2[] newUV;
    //uvs map texture values onto 3D objects
    int[] newTriangles;
    //define which vertices make up which triangle
    //contain ordering of triangles


    // Start is called before the first frame update
    void Start()
    {
        newVertices = new Vector3[4];
        //number of vertices
        newUV = new Vector2[4];
        //each vertex will have own uv value
        newTriangles = new int[6];
        //amount of triangles dependent on mesh
        //for a plane with 4 vertices, this will be 2
        //each triangle needs 3 values for each vertex

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        newVertices[0] = new Vector3(-1.0f, 0.0f, 1.0f);
        newVertices[1] = new Vector3(1.0f, 0.0f, 1.0f);
        newVertices[2] = new Vector3(-1.0f, 0.0f, -1.0f);
        newVertices[3] = new Vector3(1.0f, 0.0f, -1.0f);
        //4 coordinates of a square plane

        newUV[0] = new Vector2(0.0f, 0.0f);
        newUV[1] = new Vector2(1.0f, 0.0f);
        newUV[2] = new Vector2(0.0f, 1.0f);
        newUV[3] = new Vector2(1.0f, 1.0f);
        //UVs of texture

        newTriangles[0] = 0;
        newTriangles[1] = 1;
        newTriangles[2] = 3;
        //first triangle of plane

        newTriangles[3] = 0;
        newTriangles[4] = 3;
        newTriangles[5] = 2;
        //second triangle of plane

        mesh.vertices = newVertices;
        //assign vertices to mesh class attribute
        mesh.uv = newUV;
        //assign uvs to mesh class
        mesh.triangles = newTriangles;
        //assign triangles to mesh class
        mesh.RecalculateBounds();
        //recalculates bounding volume is mesh is modified
        mesh.RecalculateNormals();
        //recalculates normal vectors to mesh if modified
    }
}