using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    float prevPos;
    void Start()
    {
        prevPos = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 1.25f, 0f));

        if (prevPos > transform.localPosition.y || transform.localPosition.y == 25)
        {
            prevPos = transform.localPosition.y;
            transform.localPosition -= new Vector3(0, 1f / 2, 0);
        }
        if (transform.localPosition.y > prevPos || transform.localPosition.y == 10)
        {
            prevPos = transform.localPosition.y;
            transform.localPosition += new Vector3(0, 1f / 2, 0);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            Destroy(this.gameObject);
            Character player = (Character)other.GetComponent(typeof(Character));
            player.keyObtained = true;
        }
    }
}