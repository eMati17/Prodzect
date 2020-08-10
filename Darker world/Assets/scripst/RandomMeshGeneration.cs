using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeshGeneration : MonoBehaviour
{
    [SerializeField] GameObject MeshGenerate = null;
    public int width = 10;
    public int height = 10;
    Vector3[] points;
    [Tooltip("You can manipulate density of terrain.")] public float scale = 20;
    float xOffset;
    float yOffset;
    [Tooltip("It's a percent of respawning terrain. Higher number equal to less terrain.")] [Range(0f, 1f)] public float chance = .5f;
    List<Vector3> MeshHere;
    EdgeCollider2D coll2d;
    Vector2[] edgeColl2d;
    List<Vector3> dontPutHere;
    List<Vector3> putHere;
    List<Vector3> dontHereMesh;
    Vector3[] background;

    void Start()
    {
        width--;
        height--;
        
        points = new Vector3[(width + 1 ) * (height + 1)];

        int i = 0;
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                points[i] = new Vector3(x, y, 0);
                i++;
            }   
        }

        xOffset = Random.Range(0,9999);
        yOffset = Random.Range(0,9999);
        MeshHere = new List<Vector3>();
        dontHereMesh = new List<Vector3>();
        for (int a = 0; a < points.Length; a++)
        {
            if (Mathf.PerlinNoise(points[a].x / width * scale + xOffset, points[a].y / height * scale + yOffset) > chance)
            {
                MeshGenerate.GetComponent<meshScript>().CreateMeshAndPlaceItToWorld(points[a], true, true);

                if ((points[a] == new Vector3(0, points[a].y, 0)) || (points[a] == new Vector3(width, points[a].y, 0)) || (points[a] == new Vector3(points[a].x, 0, 0)) || (points[a] == new Vector3(points[a].x, height, 0)))
                {
                    MeshHere.Add(points[a]);
                }
            }
            else 
            {
                dontHereMesh.Add(points[a]);
            }
        }

        coll2d = GetComponent<EdgeCollider2D>();

        edgeColl2d = new Vector2[5]
        {
            new Vector2(-2, -2),
            new Vector2(-2, height+2),
            new Vector2(width+2, height+2),
            new Vector2(width+2, -2),
            new Vector2(-2, -2)
        };
        coll2d.points = edgeColl2d;

        dontPutHere = new List<Vector3>();

        for (int p = 0; p < MeshHere.Count; p++)
        {
            if ((MeshHere[p] != new Vector3(0, 0)) && (MeshHere[p] != new Vector3(width, height)) && (MeshHere[p] != new Vector3(0, height)) && (MeshHere[p] != new Vector3(width, 0)))
            {
                if (MeshHere[p] == new Vector3(0, MeshHere[p].y, 0))
                {
                    dontPutHere.Add(MeshHere[p] - new Vector3(1, -1));
                    dontPutHere.Add(MeshHere[p] - new Vector3(1, 1));
                    dontPutHere.Add(MeshHere[p] - new Vector3(1, 0));
                }
                else if (MeshHere[p] == new Vector3(MeshHere[p].x, height, 0))
                {
                    dontPutHere.Add(MeshHere[p] + new Vector3(0, 1));
                    dontPutHere.Add(MeshHere[p] + new Vector3(-1, 1));
                    dontPutHere.Add(MeshHere[p] + new Vector3(1, 1));
                }
                else if (MeshHere[p] == new Vector3(width, MeshHere[p].y, 0))
                {
                    dontPutHere.Add(MeshHere[p] + new Vector3(1, 0));
                    dontPutHere.Add(MeshHere[p] + new Vector3(1, 1));
                    dontPutHere.Add(MeshHere[p] + new Vector3(1, -1));
                }
                else if (MeshHere[p] == new Vector3(MeshHere[p].x, 0, 0))
                {
                    dontPutHere.Add(MeshHere[p] - new Vector3(0, 1));
                    dontPutHere.Add(MeshHere[p] - new Vector3(1, 1));
                    dontPutHere.Add(MeshHere[p] - new Vector3(-1, 1));
                }
            }
        }

        putHere = new List<Vector3>();
        bool trueOrFalse = false;

        for (int z = 0; z <= height; z++)
        {
            trueOrFalse = false;
            for (int t = 0; t < dontPutHere.Count; t++)
            {
                if (dontPutHere[t] == new Vector3(-1, z))
                {
                    trueOrFalse = true;
                }
            }
            if (trueOrFalse == false)
            {
                putHere.Add(new Vector3(-1, z));
            }
        }

        for (int z = -1; z <= width; z++)
        {
            trueOrFalse = false;
            for (int t = 0; t < dontPutHere.Count; t++)
            {
                if (dontPutHere[t] == new Vector3(z, height + 1, 0))
                {
                    trueOrFalse = true;
                }
            }
            if (trueOrFalse == false)
            {
                putHere.Add(new Vector3(z, height + 1, 0));
            }
        }

        for (int z = -1; z <= height; z++)
        {
            trueOrFalse = false;
            for (int t = 0; t < dontPutHere.Count; t++)
            {
                if (dontPutHere[t] == new Vector3(width + 1, z, 0))
                {
                    trueOrFalse = true;
                }
            }
            if (trueOrFalse == false)
            {
                putHere.Add(new Vector3(width + 1, z, 0));
            }
        }

        for (int z = -1; z <= width; z++)
        {
            trueOrFalse = false;
            for (int t = 0; t < dontPutHere.Count; t++)
            {
                if (dontPutHere[t] == new Vector3(z, -1, 0))
                {
                    trueOrFalse = true;
                }
            }
            if (trueOrFalse == false)
            {
                putHere.Add(new Vector3(z, -1, 0));
            }
        }

        for (int j = 0; j < putHere.Count; j++)
        {
            MeshGenerate.GetComponent<meshScript>().CreateMeshAndPlaceItToWorld(putHere[j], true, true);
        }
        MeshGenerate.GetComponent<meshScript>().CreateMeshAndPlaceItToWorld(new Vector3(width + 1, height + 1), true, true);

        for (int z = 0; z < dontHereMesh.Count; z++)
        {
            MeshGenerate.GetComponent<meshScript>().CreateMeshAndPlaceItToWorld(dontHereMesh[z], false, false);
        }
        for (int z = 0; z < dontPutHere.Count; z++)
        {
            MeshGenerate.GetComponent<meshScript>().CreateMeshAndPlaceItToWorld(dontPutHere[z], false, false);
        }

        background = new Vector3[2 * (height + 3) + 2 * (width + 3)];

        for (int mn = 0; mn < (height + 3); mn++)
        {
            background[mn] = new Vector3(-2, mn - 1);
        }

        for (int mn = 0; mn < (height + 3); mn++)
        {
            background[mn + height + 3] = new Vector3(width + 2, mn - 1);
        }

        for (int mn = 0; mn < (width + 3); mn++)
        {
            background[mn + height + 3 + height + 3] = new Vector3(mn - 1, -2);
        }

        for (int mn = 0; mn < (width + 3); mn++)
        {
            background[mn + height + 3 + height + 3 + width + 3] = new Vector3(mn - 1, height +2);
        }

        for (int xd = 0; xd < background.Length; xd++)
        {
            MeshGenerate.GetComponent<meshScript>().CreateMeshAndPlaceItToWorld(background[xd], false, true);
        }

    }
}
