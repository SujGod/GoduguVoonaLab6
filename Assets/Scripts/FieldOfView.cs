using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [SerializeField] private float fov;
    [SerializeField] private int rayCount;
    [SerializeField] private float viewDistance;
    private float angleIncrease;
    private List<Ray> raycasts;
    private Mesh mesh;
    [SerializeField] private bool FieldOfViewToggle; 

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        raycasts = new List<Ray>();
        /*viewPoints = new List<Vector3>();*/
        //if (this.transform.tag == "Prey")
        //{
        //    transform.GetComponent<Prey>().fieldOfView = this;
        //}
        /*else*//* if (this.transform.tag == "Predator")
        {
            transform.GetComponent<Predator>().fieldOfView = this;
        }*/


        UpdateFieldOfView();
    }

    void Update()
    {
        UpdateFieldOfView();
    }



    // Update is called once per frame

    void UpdateFieldOfView()
    {
        raycasts.Clear();
        angleIncrease = fov / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();


        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - fov / 2 + angleIncrease * i;
            Ray rayObj = GetRayObj(angle);
            raycasts.Add(rayObj);
            viewPoints.Add(rayObj.point);
        }

        if (FieldOfViewToggle)
        {
            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[rayCount + 2];
            int[] triangles = new int[rayCount * 3];

            vertices[0] = Vector3.zero;

            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
        
    }

    Vector3 DirFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));
    }

    Ray GetRayObj(float angle)
    {
        Vector3 dir = DirFromAngle(angle);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, DirFromAngle(angle), out hit, viewDistance))
        {
            return new Ray(true, hit.point, hit.collider);
        } else
        {
            return new Ray(false, transform.position + dir * viewDistance, null);
        }
    }

    

    public bool AreAllRaysHit()
    {
        int rayHitCount = 0;
        for (int i = 0; i < raycasts.Count; i++)
        {
            if (raycasts[i].hit)
            {
                rayHitCount++;
            }
        }

        if (rayHitCount == raycasts.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetNewDirection()
    {
        System.Random random = new System.Random();
        int startIndex = random.Next(raycasts.Count);
        while (raycasts[startIndex].hit)
        {
            if (startIndex == raycasts.Count - 1)
            {
                startIndex = -1;
            }
            startIndex++;
        }

        return (raycasts[startIndex].point - transform.position).normalized;

    }

    public bool AreAnyMiddleRaysHit()
    {
        int firstQuarter = raycasts.Count / 4;
        int thirdQuarter = 3 * firstQuarter;
        for (int i = firstQuarter; i < thirdQuarter; i++)
        {
            if (raycasts[i].hit)
            {
                return true;
            }
        }
        return false;
        
    }

    public bool IsPreyInVision()
    {
        for (int i = 0; i < raycasts.Count; i++)
        {
            if (raycasts[i].other != null)
            {
                if (raycasts[i].other.CompareTag("Character"))
                {
                    return true;
                }
            }

        }

        return false;
    }

    public bool IsPredInVision()
    {
        for (int i = 0; i < raycasts.Count; i++)
        {
            if (raycasts[i].other != null)
            {
                if (raycasts[i].other.CompareTag("Predator"))
                {
                    return true;
                }
            }

        }

        return false;
    }

    public Collider WhichPrey()
    {
        for (int i = 0; i < raycasts.Count; i++)
        {
            if (raycasts[i].other != null)
            {
                if (raycasts[i].other.CompareTag("Character"))
                {
                    return raycasts[i].other;
                }
            }
        }

        return null;
    }

    public Collider WhichPred()
    {
        for (int i = 0; i < raycasts.Count; i++)
        {
            if (raycasts[i].other != null)
            {
                if (raycasts[i].other.CompareTag("Predator"))
                {
                    return raycasts[i].other;
                }
            }
        }

        return null;
    }

}







