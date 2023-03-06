using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    [SerializeField]
    Transform CubePrefab;

    [SerializeField]
    int GridX, GridY;

    [SerializeField]
    Camera cam;

    private void Start()
    {
        MakeGridNOdd();

        int iter = 1;

        for (int f = 1; f <= GridY; f++)
        {
            for (int i = GridX; i > 0; i--)
            {
                CubeScript cube = Instantiate(CubePrefab, new Vector3((GridX - i) - GridX/2, (GridY - f) - GridY/2, 0), Quaternion.identity, transform).GetComponent<CubeScript>();

                cube.setText(iter.ToString());

                iter++;
            }
        }

        cam.transform.position = new Vector3(0,0,-(10 + GridX/1.5f));
    }

    void MakeGridNOdd()
    {
        if (GridX % 2 == 0)
            GridX -= 1;

        if (GridY % 2 == 0)
            GridY -= 1;
    }
}
