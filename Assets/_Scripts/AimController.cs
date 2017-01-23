using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour {

    public Text rotationDir;
    public AudioSource AimAS;

    private float controlFactor = 0.5f;
    private float killRotFactor = 0.5f;
    private Rigidbody rb;
    private Quaternion spin = Quaternion.identity;
            
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Roll");
        // bool killRot = Input.GetAxisRaw("Fire2") == 1;

        // if (killRot)
        // {
        //     spin = Quaternion.Slerp(spin, Quaternion.identity, controlFactor);
        // }
        // else
        //        {
        Quaternion spin0 = spin;
            if (h != 0)
            {
                Quaternion q = Quaternion.Euler(0, controlFactor * h, 0);
                spin = q * spin;
            }
            if (v != 0)
            {
                Quaternion q = Quaternion.Euler(controlFactor * v, 0, 0);
                spin = q * spin;
            }
            if (z != 0)
            {
                Quaternion q = Quaternion.Euler(0, 0, controlFactor * -z);
                spin = q * spin;
            }
            if (h == 0 && v == 0 && z == 0 && spin != Quaternion.identity)
            {
                spin = Quaternion.Slerp(spin, Quaternion.identity, killRotFactor * Time.deltaTime);
            }
//        }

        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * spin, Time.deltaTime);

        rotationDir.text = transform.rotation.ToString();

        if (spin != spin0)
        {
            if (!AimAS.isPlaying)
            {
                AimAS.Play();
            }
        }
        else
        {
            if (AimAS.isPlaying)
            {
                AimAS.Pause();
            }
        }
    }

}
