using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FOV : MonoBehaviour
    {
        //Create variables for create a field of view
        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;

        public float meshRes;
        Mesh mesh;
        public MeshFilter meshFilter;
        public List<Ray> listOfCurrentRays;

        public List<Transform> visiblePrey = new List<Transform>();

        public LayerMask obstacleMask;
        public LayerMask preyMask;

        void Start()
        {
            //initialize mesh and list of rays for ray (FOV) detection
            mesh = new Mesh();
            mesh.name = "View Mesh";
            meshFilter.mesh = mesh;
            listOfCurrentRays = new List<Ray>();
        }

        public List<Transform> FindVisiblePrey()
        {
            //clear list of visible prey
            visiblePrey.Clear();

            //find objects in circular view radius of predator object
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, preyMask);

            //for objects in view radius
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                //if the object falls within the view angle (FOV) of predator
                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    //get the distance to object and check if its not an obstacle (meaning its a prey) and add it to prey list
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        visiblePrey.Add(target);
                    }
                }
            }

            //return prey list
            return visiblePrey;
        }


        public Vector3 DirectionFromAngle(float angleInDeg)
        {
            //convert angle into a direction vector
            return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
        }

        public bool EnemyDetected()
        {
            //for each of the rays
            for (int i = 0; i < listOfCurrentRays.Count; i++)
            {
                //if any ray hits and that hit ray is a predator return true, else return false
                if (listOfCurrentRays[i].hit)
                {
                    if (listOfCurrentRays[i].collider != null && listOfCurrentRays[i].collider.tag == "predator")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        void LateUpdate()
        {
            DrawFOV();
        }

        public Vector3 GetNewDirection()
        {
            //get a random integer between 0 and the list of the ray count
            System.Random random = new System.Random();
            int i = random.Next(listOfCurrentRays.Count);
            int newEndPoint = i;
            Quaternion rotation;

            //iterate from the current ray to the end of the ray list to find a ray that has not been hit
            // and return the direction associate with the point of that ray to move in a direction that is clear
            while (i < listOfCurrentRays.Count)
            {
                if (!listOfCurrentRays[i].hit)
                {
                    rotation = Quaternion.LookRotation((listOfCurrentRays[i].point - transform.position).normalized);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f);
                    return (listOfCurrentRays[i].point - transform.position).normalized;
                }
                i++;
            }

            //if reached end of list start from beginning of list and go to original 'i' value, repeating the same
            //process as aboe
            if (i == listOfCurrentRays.Count)
            {
                i = 0;
                while (i < newEndPoint)
                {
                    if (!listOfCurrentRays[i].hit)
                    {
                        rotation = Quaternion.LookRotation((listOfCurrentRays[i].point - transform.position).normalized);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f);
                        return (listOfCurrentRays[i].point - transform.position).normalized;
                    }
                    i++;
                }
            }

            //if all rays are hit rotate either right or left and return a zero vector
            RotateMore();
            return Vector3.zero;
        }

        private void RotateMore()
        {
            //get a random variable
            System.Random random = new System.Random();

            //rotate the transform either to the left or right
            Quaternion rotation = Quaternion.LookRotation(transform.forward);
            rotation *= Quaternion.Euler(0, (random.Next(0, 1) * 2 - 1) * 100, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f);
        }

        //FOV method from video
        void DrawFOV()
        {
            listOfCurrentRays.Clear();
            int rayCount = Mathf.RoundToInt(viewAngle * meshRes);
            float stepAngleSize = viewAngle / rayCount;

            List<Vector3> viewPoints = new List<Vector3>();

            for (int i = 0; i <= rayCount; i++)
            {
                float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                Ray ray = GetRay(angle);
                Debug.DrawLine(transform.position, ray.point, Color.red);

                listOfCurrentRays.Add(ray);
                viewPoints.Add(ray.point);
            }

            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;

            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

                if (i < (vertexCount - 2))
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

        //Ray method from video
        Ray GetRay(float angle)
        {
            Vector3 direction = DirectionFromAngle(angle);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, viewRadius))
            {
                return new Ray(true, hit.point, hit.distance, angle, hit.collider);
            }
            else
            {
                return new Ray(false, transform.position + direction * viewRadius, viewRadius, angle, null);
            }
        }

        //Ray struct from video
        public struct Ray
        {
            public bool hit;
            public Vector3 point;
            public float distance;
            public float angle;
            public Collider collider;

            public Ray(bool hit, Vector3 point, float distance, float angle, Collider collider)
            {
                this.hit = hit;
                this.point = point;
                this.distance = distance;
                this.angle = angle;
                this.collider = collider;
            }
        }
    }
