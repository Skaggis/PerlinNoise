using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProceduralGen : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] public int scale;
    [SerializeField] public GameObject dirt;

    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)
        {
            //instantierar dirt på x-pos
            Instantiate(dirt, new Vector2(x, 0), Quaternion.identity);

            /*
            for (int y = 0; y < height; y++)
            {
                Instantiate(dirt, new Vector2(x, (Mathf.PerlinNoise(Random.Range(5f, 15f)))));

                //new Vector3(xPos, Mathf.PerlinNoise(i * Random.Range(5f, 15f), 0));
                GetNoiseValueAsInt();
                Instantiate(dirt, new Vector2());
            }
            */
        }
    }
    /*
    void GetNoiseValueAsInt(int x, int y)
    {
        float ytemp = Mathf.PerlinNoise(x, Random.Range(5f, 15f));


        //parentesen gör om?
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        //noiset returnerar en float från koordinater x och y
        //"x" och "y" är pixelkoord, dvs heltal, måste göas omovan för att kunna returnera gråskala/noise
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        //gråskala
        return new Color(sample, sample, sample);

    }
    */
}
