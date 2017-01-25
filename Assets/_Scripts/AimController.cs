using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour {

    public ScoreController Scorer;
    public AudioSource AimAS;

    private float controlFactor = 0.4f;
    private float killRotFactor = 0.6f;

    private Quaternion spin = Quaternion.identity;
    private bool killingRot = false;

	// Use this for initialization
	void Start () {
	}

    private void UpdateGetInput(out float x, out float y, out float z, out float k)
    {
        if (Scorer.IsRunning)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            z = Input.GetAxis("Roll");
            k = Input.GetAxisRaw("KillRotation");
        }
        else
        {
            x = 0;
            y = 0;
            z = 0;
            k = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        float x, y, z, k;
        UpdateGetInput(out x, out y, out z, out k);
        bool rcsInput = (x != 0 || y != 0 || z != 0);
        bool killRot = k != 0;

        Quaternion q = Quaternion.identity;

        /*
        if (killRot)
        {
            killingRot = true;
        }
        else if (rcsInput)
        {
            killingRot = false;
            q = RotInput(x, y, z);
        }

        if (killingRot)
        {
            q = KillRotQ();
            if (q == Quaternion.identity)
            {
                killingRot = false;
            }
        }
        */

        if (rcsInput)
        {
            killingRot = false;
            q = RotInput(x, y, z);
        }
        else
        {
            float angle;
            Vector3 axis;
            spin.ToAngleAxis(out angle, out axis);
            if (angle != 0)
            {
                killingRot = true;
            }
        }

        if (killingRot)
        {
            q = KillRotQ();
            float angle;
            Vector3 axis;
            q.ToAngleAxis(out angle, out axis);
            if (angle != 0)
            {
                killingRot = false;
            }
        }

        spin = q * spin;
        transform.rotation *= Quaternion.Lerp(Quaternion.identity, spin, killRotFactor * Time.deltaTime);

        UpdateSounds(q);
    }

    private Quaternion RotInput(float x, float y, float z)
    {
        Quaternion q = Quaternion.identity;
        if (x != 0)
        {
            q *= Quaternion.AngleAxis(controlFactor * x, Vector3.up);
        }
        if (y != 0)
        {
            q *= Quaternion.AngleAxis(controlFactor * y, Vector3.right);
        }
        if (z != 0)
        {
            q *= Quaternion.AngleAxis(controlFactor * -z, Vector3.forward);
        }
        return q;
    }

    private Quaternion KillRotQ()
    {
        //       Quaternion q = Quaternion.Slerp(Quaternion.identity, Quaternion.Inverse(spin), Time.deltaTime);
        float angle;
        Vector3 axis;
        spin.ToAngleAxis(out angle, out axis);
        Quaternion dest = Quaternion.AngleAxis(0, axis);
        Quaternion q = Quaternion.Slerp(dest, Quaternion.Inverse(spin), Time.deltaTime);
        return q;
    }

    private void UpdateSounds(Quaternion q)
    {
        if (q != Quaternion.identity)
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
