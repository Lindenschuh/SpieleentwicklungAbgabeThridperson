using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public ThirdPersonCameraMovement CameraT;
    public float WeaponRange;
    public float BulletsPerSecound;
    public LayerMask mask;
    private float cooldown;
    public int WeaponDMG = 10;

    public ParticleSystem Sparks;
    public ParticleSystem Smoke;
    private Light pLight;
    private ParticleSystem.MainModule mainSmoke;
    private AudioSource AS;

    private bool isOnFire;

    private void Start()
    {
        pLight = Smoke.GetComponentInChildren<Light>();
        AS = GetComponent<AudioSource>();
        mainSmoke = Smoke.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (CameraT.IsZooming)
        {
            transform.eulerAngles = new Vector3(CameraT.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        pLight.gameObject.SetActive(false);
        if (Time.time > cooldown)
        {
            isOnFire = false;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Smoke.Play();
                AS.Play();
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                isOnFire = true;
                Ray ray = new Ray(CameraT.transform.position, CameraT.transform.forward);
                Debug.DrawRay(ray.origin, ray.direction * WeaponRange);
                RaycastHit hit;

                Sparks.Play();
                AS.loop = true;
                mainSmoke.loop = true;
                pLight.gameObject.SetActive(true);
                if (Physics.Raycast(ray, out hit, WeaponRange))
                {
                    Destroyable des = hit.collider.GetComponent<Destroyable>();
                    if (des != null)
                    {
                        des.GetDMG(WeaponDMG);
                    }
                }
                cooldown = Time.time + (1f / BulletsPerSecound);
            }

            if (!isOnFire)
            {
                AS.Stop();
                Sparks.Stop();
                mainSmoke.loop = false;
            }
        }
    }
}