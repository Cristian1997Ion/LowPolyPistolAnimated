using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public Transform BulletSpawn;
    public Transform BulletPrefab;
    public Text AmmoText;

    private int Ammo = 12;
    [SerializeField]
    private int MaxAmmo = 48;
    [SerializeField]
    private int MagazineAmmo = 12;
    [SerializeField]
    private int MagazineMaxAmmo = 12;
    [SerializeField]
    private AudioClip ShootSound;
    [SerializeField]
    private AudioClip ReloadSound;
    [SerializeField]
    private AudioClip OutOfAmmoSound;

    private AudioSource speaker;
    private Animator anim;
    private bool IsReloading;
    private bool IsShooting;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();

        UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Wait for the action to finish
        if (IsShooting)
        {
            return;
        }


        // KLINK! No more bullets \_(^.^)_/
        if (MagazineAmmo == 0 && Ammo == 0)
        {
            speaker.clip = OutOfAmmoSound;
            speaker.Play();
            return;
        }

        if (MagazineAmmo == 0 && Ammo > 0)
        {
            Reload();
            return;
        }

        MagazineAmmo--;
        UpdateAmmoText();

        speaker.clip = ShootSound;
        speaker.Play();

        IsShooting = true;
        anim.SetBool("IsShooting", true);

       Instantiate(
            BulletPrefab,
            BulletSpawn.position,
            BulletSpawn.rotation
       );
    }

    void EndShoot()
    {
        IsShooting = false;
        anim.SetBool("IsShooting", IsShooting);
    }

    void Reload()
    {
        if (IsReloading)
        {
            return;
        }

        IsReloading = true;
        IsShooting = false;

        speaker.clip = ReloadSound;
        speaker.Play();

        anim.SetBool("IsReloading", IsReloading);
        anim.SetBool("IsShooting", IsShooting);
    }

    // An animation event is using this function to update the ammo variables
    void RecalculateAmmo()
    {
        IsReloading = false;
        anim.SetBool("IsReloading", IsReloading);

        int amount = Mathf.Min(Ammo, MagazineMaxAmmo);
        Ammo -= amount;
        MagazineAmmo = amount;

        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        AmmoText.text = MagazineAmmo + "/" + Ammo;
    }
}
