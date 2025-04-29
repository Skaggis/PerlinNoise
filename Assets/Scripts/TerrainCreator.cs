using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TerrainCreator : MonoBehaviour
{
    SpriteShapeController shape;
    public int scale;
    public int numberOfPoints = 67;
    //egen offsset
    public int distBetweenPoints = 3;
    public int plateau;
    private int plateauEnd = 10;

    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<SpriteShapeController>();
        
        //0 & 1 �r v�nster punkter, 2 & 3 h�ger punkter
        
        shape.spline.SetPosition(0, shape.spline.GetPosition(0) + Vector3.left * scale);
        shape.spline.SetPosition(1, shape.spline.GetPosition(1) + Vector3.left * scale);
        
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);

        //scale 100, num of points 67, distance between 3
        
        Vector3 nextYvalue;
        int plateau = 0;
        float xPos = shape.spline.GetPosition(1).x + distBetweenPoints;

        //s�tter ut punkter, random h�jdv�rde
        for (int i = 0; i < numberOfPoints; i++)
        {
            //i+1 och i+2 �r f�r att s�tta punkter p� �vre kanten av spline:en
            //s�tter ut punkter mellan punkt 1 och punkt 2
            //f�rsta v�rdet som "plussas p�" �r fr�n punkt 1 pos

            xPos = shape.spline.GetPosition(i+1).x + distBetweenPoints;

            Vector3 currentYvalue = new Vector3(xPos, 2 * Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0));

            if (plateau < plateauEnd)
            {
                xPos = shape.spline.GetPosition(i + 1).x + distBetweenPoints;
                nextYvalue = new Vector3(xPos, 2 * Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0));
                currentYvalue = nextYvalue;
                shape.spline.InsertPointAt(i + 2, currentYvalue);
                plateau++;

            }
            //random upp til 10
            else if (plateau == plateauEnd)
            {
                //sen ska Y-v�rdet uppdateras n�r counterMax n�tts 
             
                nextYvalue = new Vector3(xPos, 2);
                currentYvalue = nextYvalue;
                shape.spline.InsertPointAt(i + 2, currentYvalue);
                plateau++;
            }
            
            else if (plateau > plateauEnd)
            {
             
                nextYvalue = new Vector3(xPos, 2);
                currentYvalue = nextYvalue;
                shape.spline.InsertPointAt(i + 2, currentYvalue); //too close to neighbor
                plateau++;
            }

        }

        //h�rdare kurva
        for (int i = 2; i < numberOfPoints + 2; i++)
        {
            //TangentMode Continous �r en av tre tangents som g�r att v�lja i SpriteShapeControllern!
            shape.spline.SetTangentMode(i, ShapeTangentMode.Linear);
        }

        /*
        //mjukare kurva
        for (int i = 2;i < numberOfPoints + 2; i++)
        {
            //TangentMode Continous �r en av tre tangents som g�r att v�lja i SpriteShapeControllern!
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            //trigonometri, en rak tangent i x-led skapar en kurva tack vare continous mode
            shape.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }
        */
       
    }

    // Update is called once per frame

}
