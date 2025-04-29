
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] public int height = 256;
    [SerializeField] public int width = 256;
    [SerializeField]  public float scale = 20f;
    [SerializeField] public float offsetX = 100f;
    [SerializeField] public float offsetY = 100f;

    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    //textur skapas fr�n scratch
    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        //generate perlin noise map for texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x,y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }
    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        //noiset returnerar en float fr�n koordinater x och y
        //"x" och "y" �r pixelkoord, dvs heltal, m�ste g�as omovan f�r att kunna returnera gr�skala/noise
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        //gr�skala
        return new Color(sample, sample, sample);

    }
}
