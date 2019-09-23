using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public float distance;
    public Camera cam;

    public GameObject spawnCube;
    public GameObject spawnSphere;


    public GameObject handCube;
    public GameObject handSphere;
    bool cubeEnable;
    bool sphereEnable;
    bool spawn;
    bool remove;
    Ray ray;
    RaycastHit hit;



    private void Start()
    {
        spawn = false;
        cubeEnable = false;
        sphereEnable = false;
    }
    void Update()
    {
        ray = cam.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

        handCube.GetComponent<MeshRenderer>().enabled = cubeEnable;
        handSphere.GetComponent<MeshRenderer>().enabled = sphereEnable;

        CraftCube();
        CraftSphere();
        RemoveObject();

    }
    void CraftCube()
    {
        if (spawn && cubeEnable)   // spawn cube
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                spawn = false;
                if (hit.collider.tag == "ground")
                {
                    int scale = 3;
               /*     float fault = hit.point.x / Mathf.Round(hit.point.x);

                    if (hit.point.x / scale > 0.5f )
                    {
                        float xPos = Mathf.Round(hit.point.x / scale) * scale;
                        Mathf.Round(hit.point.z);
                    }
                    else
                    {
                        Mathf.Floor(hit.point.x);
                        Mathf.Floor(hit.point.z);
                    }
                    */
                    float xPos = Mathf.Round(hit.point.x / scale) * scale;
                    float zPos = Mathf.Round(hit.point.z / scale) * scale;

                    Vector3 pos = new Vector3(xPos, 0, zPos);
                    Instantiate(spawnCube, pos, Quaternion.identity);
                }

            }
        }
    }
    void CraftSphere()
    {
        if (spawn && sphereEnable) // spawn sphere
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                spawn = false;
                if (hit.collider.tag == "Basement")
                {
                    if (hit.transform.childCount < 1)
                    {
                        Vector3 pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.75f, hit.collider.transform.position.z);
                        GameObject sphere = Instantiate(spawnSphere, pos, Quaternion.identity);
                        sphere.transform.parent = hit.transform;
                    }
                }
            }
        }
    }
    void RemoveObject()
    {
        if (remove)
        {
            remove = false;
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.tag == "Basement")
                {
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "Furniture")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    public void HandCube()
    {
        cubeEnable = !cubeEnable;
        if (sphereEnable == true) { handSphere.GetComponent<MeshRenderer>().enabled = false; sphereEnable = false; }
        spawn = false;
    }

    public void HandSphere()
    {
        sphereEnable = !sphereEnable;
        if (cubeEnable == true) { handCube.GetComponent<MeshRenderer>().enabled = false; cubeEnable = false; }
        spawn = false;
    }

    public void Craft( )
    {
        if (handCube.GetComponent<MeshRenderer>().enabled  == true)
        {
            spawn = true;
        }
        else if (handSphere.GetComponent<MeshRenderer>().enabled == true)
        {
            spawn = true;
        }
        else
        {
            spawn = false;
        }
    }

    public void Remove()
    {
        remove = true;
    }
}
