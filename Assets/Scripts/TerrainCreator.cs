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
        
        //0 & 1 är vänster punkter, 2 & 3 höger punkter
        
        shape.spline.SetPosition(0, shape.spline.GetPosition(0) + Vector3.left * scale);
        shape.spline.SetPosition(1, shape.spline.GetPosition(1) + Vector3.left * scale);
        
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);

        //scale 100, num of points 67, distance between 3
        
        Vector3 nextYvalue;
        int plateau = 0;
        float xPos = shape.spline.GetPosition(1).x + distBetweenPoints;

        //sätter ut punkter, random höjdvärde
        for (int i = 0; i < numberOfPoints; i++)
        {
            //i+1 och i+2 är för att sätta punkter på övre kanten av spline:en
            //sätter ut punkter mellan punkt 1 och punkt 2
            //första värdet som "plussas på" är från punkt 1 pos

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
                //sen ska Y-värdet uppdateras när counterMax nåtts 
             
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

        //hårdare kurva
        for (int i = 2; i < numberOfPoints + 2; i++)
        {
            //TangentMode Continous är en av tre tangents som går att välja i SpriteShapeControllern!
            shape.spline.SetTangentMode(i, ShapeTangentMode.Linear);
        }

        /*
        //mjukare kurva
        for (int i = 2;i < numberOfPoints + 2; i++)
        {
            //TangentMode Continous är en av tre tangents som går att välja i SpriteShapeControllern!
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            //trigonometri, en rak tangent i x-led skapar en kurva tack vare continous mode
            shape.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }
        */
       
    }

    // Update is called once per frame

}
