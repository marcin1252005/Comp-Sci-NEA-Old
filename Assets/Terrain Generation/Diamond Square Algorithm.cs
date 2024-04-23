using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
//create dependency on mesh filter
public class DiamondSquareAlgorithm : MonoBehaviour
{
    //mesh setup

    [Range(0.1f, 0.9f)]
    public float randomnessFactor = 0.5f;
    public int terrainDivisions;
    //divisions are the number of 'squares' per row
    [Range(0.1f,1000f)]
    public float size;
    // width/height of map within editor
    [Range(0.1f,100f)]
    public float height;
    //corresponding height values (for randomness)

    Vector3[] newVertices;
    //vertices of mesh
    int vertexCount;
    //number of total vertices

    // Start is called before the first frame update
    void Start()
    {
        //check if power of 2 and positive
        if (terrainDivisions <= 0 || (terrainDivisions & (terrainDivisions - 1)) != 0)
        {
            Debug.LogError("terrainDivisions is not a power of 2 and positive");
            return;
        }
        GenerateTerrain();
        //generate terrain upon loading scene
    }

    // Update is called once per frame
    void GenerateTerrain()
    {
        vertexCount = (terrainDivisions + 1) * (terrainDivisions + 1);
        //if n divisions, (n+1)^2 vertices
        newVertices = new Vector3[vertexCount];
        //mesh vertices = number of vertices
        Vector2[] newUV = new Vector2[vertexCount];
        //each vertex has corresponding uv
        int[] newTriangles = new int[terrainDivisions*terrainDivisions*6];

        float halfSide = size * 0.5f;
        //calculate halfside value
        float sizeDivisions = size / terrainDivisions;
        //calculate size of divisions

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int triangleOffset = 0;

        //for n divisions along x axis
        for (int i = 0; i <= terrainDivisions; i++)
        {
            //for n divisions along y axis
            //as terrain is square, same num of i and j vertices
            for (int j = 0; j <= terrainDivisions; j++)
            {
               //setting vertices 
                
                //allows for vertices to be represented as a 1D array
                //rather than 2D 
                //each index of 2D map assigned value in 1D array
                newVertices[i * (terrainDivisions + 1) + j] = new Vector3(-halfSide + j * sizeDivisions, 0.0f, halfSide - i * sizeDivisions);
               
               //setting uvs 
                
                //typecast to floats as UVs do not have to be ints
                newUV[i * (terrainDivisions + 1) + j] = new Vector2((float)i / terrainDivisions, (float)j / terrainDivisions);
                //dividing by divisions makes sure values are from 0 to 1
                
               //constructing trangles
                
                if(i < terrainDivisions && j < terrainDivisions)
                //last terrainDivision is the last vertex
                //on a row and column
                //so no need to construct triangle
                {
                    int topLeft = i * (terrainDivisions + 1) + + j;
                    //index of topLeft of triangle (current vertex)
                    int bottomLeft = (i+1)* (terrainDivisions + 1) + j;
                    //index of bottomLeft of triangle (next row)

                    newTriangles[triangleOffset] = topLeft;
                    newTriangles[triangleOffset + 1] = topLeft +1;
                    newTriangles[triangleOffset + 2] = bottomLeft +1;
                    //first triangle will be current vertex, then vertex to its right
                    //then vertex below it

                    newTriangles[triangleOffset + 3] = topLeft;
                    newTriangles[triangleOffset+4] = bottomLeft +1;
                    newTriangles[triangleOffset+5] = bottomLeft;
                    //the second triangle in the square will be the current vertex
                    //then vertex to its bottom right, then vertex below it

                    triangleOffset += 6;
                    //sets up offset for next iteration of for loop
                }
            }

        }

        //randomness
        
        
        newVertices[0].y = Random.Range(-height, height);
        newVertices[terrainDivisions].y =Random.Range(-height, height);
        newVertices[newVertices.Length-1-terrainDivisions].y = Random.Range(-height, height);
        newVertices[newVertices.Length-1].y = Random.Range(-height, height);
        //setting the corner values to a random number in range of height

        int numLoops = (int)Mathf.Log(terrainDivisions, 2);
        //squareWidth halves each iteration
        //therefore log(divisions) base 2 gives number of iterations
        int squareWidth = terrainDivisions;
        //current squareWidth 
        int numSquares = 1;
        //initially only one square on first iteration
            for (int i = 0; i < numLoops; i++)
        {
            int row = 0;
           //this is the nested for loop for the square
            for (int j = 0; j < numSquares; j++)
            {
                int column = 0;
                    for (int k = 0; k < numSquares; k++)
                {
                    calcDiamondSquare(row, column, squareWidth, height);
                    column += squareWidth;
                    //move right to next corner of square
                }
                row += squareWidth;
                //jump up to next corner of square
            }
            numSquares *= 2;
            //double number of squares
            //this means the halfsquares are covered in next iteration
            squareWidth /= 2;
            //half the width
            //again, ensures halfsquares are covered in next iteration
            height *= randomnessFactor;
        }


        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
        //assign all values to mesh class

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //recalculate bounding values and normal vectors
        //if mesh is modified
    }
    
    //main calculation
    void calcDiamondSquare(int row, int column, int size, float randomOffset)
    {
        //Diamond Step
        
        int halfSide = (int)(size * 0.5f);
        //typecast to int
        int topLeft = row * (terrainDivisions + 1) + column;
        //topLeft corner index, offset by column
        int bottomLeft = (row + size)*(terrainDivisions+1)+column;
        //bottomLeft corner index, jump down size of square and across by column

        int midpoint = (row + halfSide) * (terrainDivisions + 1) + (int)(column+halfSide);
        //midpoint is current row then jump down half size of current square
        //then offset to right by current column plus half size again
        newVertices[midpoint].y = 0.25f*(newVertices[topLeft].y + newVertices[bottomLeft].y + newVertices[bottomLeft + size].y + newVertices[topLeft + size].y)+Random.Range(-randomOffset,randomOffset);
        //avg value of corners + randomOffset

        //Square Step 
      
        newVertices[midpoint-halfSide].y = (newVertices[midpoint].y + newVertices[bottomLeft].y +newVertices[topLeft].y)/3 + Random.Range(-randomOffset, randomOffset);
        //middle left index
        newVertices[midpoint + halfSide].y = (newVertices[bottomLeft+size].y + newVertices[midpoint].y +newVertices[topLeft + size].y)/3 + Random.Range(-randomOffset, randomOffset);
        //middle right index
        newVertices[topLeft + halfSide].y = (newVertices[topLeft].y + newVertices[topLeft+size].y + newVertices[midpoint].y)/3 + Random.Range(-randomOffset,randomOffset);
        //top middle index
        newVertices[bottomLeft + halfSide].y = (newVertices[midpoint].y + newVertices[bottomLeft].y + newVertices[bottomLeft + size].y) / 3 + Random.Range(-randomOffset, randomOffset);
        //bottom middle index
    }
}