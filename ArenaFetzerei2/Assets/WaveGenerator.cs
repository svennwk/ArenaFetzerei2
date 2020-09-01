using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    
    public float power = 3;
    public float scale = 1;
    public float timeScale = 1;

    private float offsetX;
    private float offsetY;
    private MeshFilter mf;

    void Start() {
        mf = GetComponent<MeshFilter>();
        GenerateNoise();
    }

    void Update() {
        
        GenerateNoise();
        offsetX += Time.deltaTime * timeScale;
        offsetY += Time.deltaTime * timeScale;

    }

    void GenerateNoise() {
        Vector3[] vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * power;

        }

        mf.mesh.vertices = vertices;
    }

    float CalculateHeight (float x, float y) {

        float xCord = x * scale + offsetX;
        float yCord = y * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }

}