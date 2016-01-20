using UnityEngine;
using System.Collections;

public class TerrianManager : MonoBehaviour {


    public bool Slop = true;
    public int Distance = 50;
    public float Resoultion = 50;
    float Angle = 0;
    public int[] RandomAngles;
    public Material TopMaterial;
    public Material BodyMaterial;
    public Vector3 SizeOfPiece;

	// Use this for initialization
    public void Create()
    {
        if (!Slop)
        {
            Angle = 0;
        }
        else
        {
            //random terrian
            int rnd = Random.Range(RandomAngles[0], RandomAngles[1]);
            if (rnd == 0)
            {
                Angle = 0;
                Slop = false;
            }
            else
                Angle = rnd;
        }

        //Angle = 1;
        Debug.Log(Angle);
        Mesh mesh = null;
        GameObject gPiece;
        mesh = new Mesh();


        Vector3 lastPosition = Vector3.zero;
        float nextY = 0;
        float angle = 0;
        float random = 1;
        int counter = 0;
        Material[] materials = new Material[] { TopMaterial, BodyMaterial };
        for (int i = 1; i <= Distance; i++)
        {
            //set angle of terrain (repeatet)

            if (Slop)
            {
                    angle = Mathf.Sin((i / Angle)) * (random);
                    angle = Mathf.Clamp(angle, -0.6f, 1.2f);
            }


            //create angluar cupe
            mesh = new Mesh();
            mesh.subMeshCount = 2;
            Cupe.Create(Resoultion, 3, 2, angle, 50, ref mesh);

            //Initiallize mesh
            gPiece = new GameObject() { tag = "Terrain" };
            gPiece.AddComponent<MeshFilter>().mesh = mesh;
            MeshRenderer rend = gPiece.AddComponent<MeshRenderer>();
            rend.sharedMaterials = materials; //assign material 
            Bounds pieceBound = gPiece.AddComponent<MeshCollider>().bounds;
            gPiece.transform.SetParent(this.gameObject.transform);

            //assign position
            gPiece.transform.position = new Vector3(lastPosition.x + (pieceBound.max.x * 2), nextY, 0);
            lastPosition = gPiece.transform.position;
            nextY = lastPosition.y + angle;


            counter++;
            if (counter == 10)
            {
                counter = 0;
                random = Random.Range(0f, 2.5f);
            }
        }

        this.transform.localScale = new Vector3(1, 0.2f, 1);
        if (Angle <= 10)
            this.transform.position = new Vector3(-21.5f, -4.85f, 0);
        else if (Angle < 15)
            this.transform.position = new Vector3(-27.2f, -5.4f, 0);
        else if (Angle <= 15 || Angle < 18)
            this.transform.position = new Vector3(-35.8f, -5.4f, 0);
        else if (Angle >= 18)
            this.transform.position = new Vector3(-19.7f, -10f, 0);
    }

    



    static class Cupe
    {

        public static void Create(float width, int height, int depth, float sideHeight , float bodyHeight,ref Mesh mesh)
        {

            //vertices
            Vector3[] virtices = new Vector3[]
        {

            //front side
            new Vector3(-width * .5f, -height * .5f,-depth * .5f), //0
            new Vector3(-width * .5f, height * .5f,-depth * .5f), //1
            new Vector3(width * .5f, (height * .5f + sideHeight),-depth * .5f), //2
            new Vector3(width * .5f, (-height * .5f + sideHeight),-depth * .5f), //3


            //top side
            new Vector3(-width * .5f,height * .5f,-depth * .5f), //4
            new Vector3(-width * .5f, height * .5f,depth * .5f), //5
            new Vector3(width * .5f, (height * .5f + sideHeight),depth * .5f), //6
            new Vector3(width * .5f, (height * .5f + sideHeight),-depth * .5f), //7

            //front side (body)
            new Vector3(-width * .5f, (-height * .5f - bodyHeight),-depth * .5f), //0
            new Vector3(-width * .5f, (height * .5f) - height ,-depth * .5f), //1
            new Vector3(width * .5f, (height * .5f) - height + sideHeight,-depth * .5f), //2
            new Vector3(width * .5f, (-height * .5f + sideHeight - bodyHeight),-depth * .5f), //3

        };
            


            //triangles //MUST BE IN CLOCK-WISE
            int[] trianglesOne = new int[]
        {
          //front side
          0,1,2,2,3,0,
          
          //top side
          4,5,6,6,7,4,

            //front body side
          8,9,10,10,11,8,
        };

            int[] trianglesTwo = new int[]
        {
            //front body side
          8,9,10,10,11,8,
        };


            Vector2[] uvs = new Vector2[]
        {
            new Vector2(0,0), //0
            new Vector2(0,1), //1
            new Vector3(1,1), //2
            new Vector2(1,0), //3

            new Vector2(0,0), //0
            new Vector2(0,1), //1
            new Vector3(1,1), //2
            new Vector2(1,0), //3

            new Vector2(0,0), //0
            new Vector2(0,1), //1
            new Vector3(1,1), //2
            new Vector2(1,0), //3


        };

            mesh.vertices = virtices;
            mesh.SetTriangles(trianglesOne, 0); //material 0
            mesh.SetTriangles(trianglesTwo, 1); //material 1
            mesh.uv = uvs;
            mesh.Optimize();
            mesh.RecalculateNormals();

        }
    }

}
