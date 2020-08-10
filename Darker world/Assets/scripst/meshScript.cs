using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshScript : MonoBehaviour
{
    public Mesh mesh = null;
    float offset = .5f;
    PolygonCollider2D coll2d;
    
    public Vector3 x;

    int meshIndex = 0;
    int pathIndex = 0;

    [HideInInspector] public List<Vector3> vertices;
    [HideInInspector] public List<int> triangles;
    [HideInInspector] public List<Vector2> uvs;
    [HideInInspector] public Vector2[] coll2dPoints;

    [Tooltip("Type here number of texturs, which are in current mesh material.")] public int numberOfBlock = 1;


    void Awake()
    {       
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        coll2d = GetComponent<PolygonCollider2D>();
    }

    public void CreateMeshAndPlaceItToWorld(Vector3 quadPlace, bool collider, bool wantUvs)
    {
        //// SET VERTICES ////
        /// This one below add new points base on Vector3 quadPlace. Our quad has pivot point in the center.

        vertices.AddRange(new List<Vector3>() { new Vector3(quadPlace.x - offset, quadPlace.y - offset), new Vector3(quadPlace.x + offset, quadPlace.y - offset), new Vector3(quadPlace.x - offset, quadPlace.y + offset), new Vector3(quadPlace.x + offset, quadPlace.y + offset) });

        //// SET TRIANGLES ////

        triangles.AddRange(new List<int>() { 0 + meshIndex, 3 + meshIndex, 1 + meshIndex, 0 + meshIndex, 2 + meshIndex, 3 + meshIndex });

        //// APPLY VERTICES & TRIANGLES ////

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        //// SET & APPLY UVS ////

        int randomIntUV;
        if (wantUvs == true)
        {
            randomIntUV = Random.Range(2, numberOfBlock + 1);
        }
        else
        {
            randomIntUV = 1;
        }
        float offx = (float)randomIntUV / (float)numberOfBlock;
        float zeroZero = /*-.25f*/ -(1f/numberOfBlock) + offx;

        uvs.AddRange(new List<Vector2>() { new Vector2(zeroZero, 0), new Vector2(offx, 0), new Vector2(zeroZero, 1), new Vector2(offx, 1) });
        mesh.SetUVs(0, uvs);

        //// SET & APPLY POLYGON COLLIDER 2D ////
        if (collider == true)
        {
            coll2dPoints = new Vector2[]
                {
                (Vector2)vertices[0 + meshIndex],
                (Vector2)vertices[1 + meshIndex],
                (Vector2)vertices[3 + meshIndex],
                (Vector2)vertices[2 + meshIndex]
                };
            coll2d.pathCount = 1 + pathIndex;

            coll2d.SetPath(0 + pathIndex, coll2dPoints);
        }
        //// SOME MATH ////

        meshIndex += 4;
        pathIndex++;
    }

}
