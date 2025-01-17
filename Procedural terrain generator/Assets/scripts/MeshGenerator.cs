﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator{
    public static MeshData GenerateTerrainMesh(float[,] heightMap,float heightMultiplier, AnimationCurve heightCurve){
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float TopLeftX = (width - 1)/-2f;
        float TopLeftZ = (height - 1)/-2f;
        MeshData meshData = new MeshData(width,height);
        int vertexIndex = 0;
        for(int y = 0;y<height;y++){
            for(int x = 0;x<width;x++){
                    meshData.vertices[vertexIndex] = new Vector3(TopLeftX+ x, heightCurve.Evaluate(heightMap[x,y]) * heightMultiplier,TopLeftZ - y);
                    meshData.uvs[vertexIndex] = new Vector2(x/(float)width,y/(float)height);
                    if(x< width-1 && y<height-1){
                        meshData.AddTriangles(vertexIndex,vertexIndex+width+1,vertexIndex+width);
                        meshData.AddTriangles(vertexIndex+width+1,vertexIndex,vertexIndex+1);
                    }
                    vertexIndex++;
            }
        }
        return  meshData;
    }
}
public class MeshData{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleindex = 0;
    public MeshData(int meshWidth, int meshHeight){
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
    }
    public void AddTriangles(int a,int b,int c){
        triangles[triangleindex] = a;
        triangles[triangleindex+1] = b;
        triangles[triangleindex+2] = c;
        triangleindex += 3;
    }
    public Mesh CreateMesh(){
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}