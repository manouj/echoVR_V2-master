using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(MeshFilter))]
public class GenerateSeabed : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    System.Random rand = new System.Random();

    double randAmount = 10;
    int size = 65;
    static int verticesInATriangle = 3;

    double[,] landscapeArr;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        landscapeArr = new double[size, size];
        MakeMeshData();
        CreateMesh();
    }

    private void MakeMeshData()
    {
        ZeroLandscapeArray();

        GenerateTerrain(0, 0, size - 1, size - 1, randAmount);

        FillVerticesArray();
        FillTriangleArray();
    }

    private void ZeroLandscapeArray()
    {
        for (int j = 0; j < landscapeArr.GetLength(0); j++)
            for (int i = 0; i < landscapeArr.GetLength(1); i++)
                landscapeArr[i, j] = 0;
    }

    private void FillVerticesArray()
    {
        vertices = new Vector3[size * size];
        for (int j = 0; j < landscapeArr.GetLength(0); j++)
        {
            for (int i = 0; i < landscapeArr.GetLength(1); i++)
            {
                vertices[j * landscapeArr.GetLength(1) + i] = new Vector3(i - size / 2, (float)landscapeArr[i, j] - 10, j - size / 2);
            }
        }
    }

    private void FillTriangleArray()
    {
        triangles = new int[(size - 1) * (size - 1) * 2 * verticesInATriangle];
        for (int j = 0; j < landscapeArr.GetLength(0) - 1; j++)
        {
            for (int i = 0; i < landscapeArr.GetLength(1) - 1; i++)
            {
                triangles[i * verticesInATriangle * 2 + j * (landscapeArr.GetLength(0) - 1) * 2 * verticesInATriangle] = i + j * landscapeArr.GetLength(0);
                triangles[i * verticesInATriangle * 2 + j * (landscapeArr.GetLength(0) - 1) * 2 * verticesInATriangle + 1] = i + (j + 1) * landscapeArr.GetLength(0);
                triangles[i * verticesInATriangle * 2 + j * (landscapeArr.GetLength(0) - 1) * 2 * verticesInATriangle + 2] = i + 1 + j * landscapeArr.GetLength(0);


                triangles[i * verticesInATriangle * 2 + j * (landscapeArr.GetLength(0) - 1) * 2 * verticesInATriangle + 3] = i + 1 + j * landscapeArr.GetLength(0);
                triangles[i * verticesInATriangle * 2 + j * (landscapeArr.GetLength(0) - 1) * 2 * verticesInATriangle + 4] = i + (j + 1) * landscapeArr.GetLength(0);
                triangles[i * verticesInATriangle * 2 + j * (landscapeArr.GetLength(0) - 1) * 2 * verticesInATriangle + 5] = (i + 1) + (j + 1) * landscapeArr.GetLength(0);
            }
        }
    }

    private void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    private Vector2 CalcMidpoint(int x, int y, int x2, int y2)
    {
        int midPointX = (int)((x2 - x) / 2 + x);
        int midPointY = (int)((y2 - y) / 2 + y);
        return new Vector2(midPointX, midPointY);
    }

    private void DiamondStep(int x, int y, int x2, int y2, double randVal)
    {
        double avg = landscapeArr[x, y] + landscapeArr[x2, y] + landscapeArr[x2, y2] + landscapeArr[x, y2];
        avg = avg / 4;
        Vector2 midPoint = CalcMidpoint(x, y, x2, y2);
        landscapeArr[(int)midPoint[0], (int)midPoint[1]] = avg + (rand.NextDouble() - 0.5) * randVal;
    }

    private void AvgForSquareStep(int x, int y, int sizeDiamond, double randVal)
    {
        int numToAvg = 0;
        double avg = 0;
        if (x + sizeDiamond <= size - 1)
        {
            avg = landscapeArr[x + sizeDiamond, y];
            numToAvg += 1;
        }
        if (x - sizeDiamond >= 0)
        {
            avg += landscapeArr[x - sizeDiamond, y];
            numToAvg += 1;
        }
        if (y - sizeDiamond >= 0)
        {
            avg += landscapeArr[x, y - sizeDiamond];
            numToAvg += 1;
        }
        if (y + sizeDiamond <= size - 1)
        {
            avg += landscapeArr[x, y + sizeDiamond];
            numToAvg += 1;
        }
        avg = avg / 4;
        landscapeArr[x, y] = avg + ((rand.NextDouble()) - 0.5) * randVal;
    }

    private void SquareStep(int x, int y, int x2, int y2, double randVal)
    {
        Vector2 midPoint = CalcMidpoint(x, y, x2, y2);
        int sizeOfDiamond = (int)((x2 - x) / 2);
        int midDiamondX1 = (int)midPoint[0] - sizeOfDiamond;
        int midDiamondY1 = (int)midPoint[1];
        AvgForSquareStep(midDiamondX1, midDiamondY1, sizeOfDiamond, randVal);
        midDiamondX1 = (int)midPoint[0] + sizeOfDiamond;
        midDiamondY1 = (int)midPoint[1];
        AvgForSquareStep(midDiamondX1, midDiamondY1, sizeOfDiamond, randVal);
        midDiamondX1 = (int)midPoint[0];
        midDiamondY1 = (int)midPoint[1] - sizeOfDiamond;
        AvgForSquareStep(midDiamondX1, midDiamondY1, sizeOfDiamond, randVal);
        midDiamondX1 = (int)midPoint[0];
        midDiamondY1 = (int)midPoint[1] + sizeOfDiamond;
        AvgForSquareStep(midDiamondX1, midDiamondY1, sizeOfDiamond, randVal);
    }
    private void GenerateTerrain(int x, int y, int x2, int y2, double randVal)
    {
        Vector2 midPoint = CalcMidpoint(x, y, x2, y2);
        DiamondStep(x, y, x2, y2, randVal);
        SquareStep(x, y, x2, y2, randVal);
        int sizeOfSquare = (x2 - x) / 2;
        double newRandVal = randVal / 2;
        if (sizeOfSquare > 1)
        {
            int newX = (int)(midPoint[0] - sizeOfSquare);
            int newY = (int)(midPoint[1] - sizeOfSquare);
            int newX2 = (int)(midPoint[0]);
            int newY2 = (int)(midPoint[1]);
            GenerateTerrain(newX, newY, newX2, newY2, newRandVal);
            newX = (int)(midPoint[0] - sizeOfSquare);
            newY = (int)(midPoint[1]);
            newX2 = (int)(midPoint[0]);
            newY2 = (int)(midPoint[1] + sizeOfSquare);
            GenerateTerrain(newX, newY, newX2, newY2, newRandVal);
            newX = (int)(midPoint[0]);
            newY = (int)(midPoint[1]);
            newX2 = (int)(midPoint[0] + sizeOfSquare);
            newY2 = (int)(midPoint[1] + sizeOfSquare);
            GenerateTerrain(newX, newY, newX2, newY2, newRandVal);
            newX = (int)(midPoint[0]);
            newY = (int)(midPoint[1] - sizeOfSquare);
            newX2 = (int)(midPoint[0] + sizeOfSquare);
            newY2 = (int)(midPoint[1]);
            GenerateTerrain(newX, newY, newX2, newY2, newRandVal);
        }
    }
}
