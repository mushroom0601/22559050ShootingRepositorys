using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletObject;
    public Transform bulletFirePoint;

    [Header("Fire Setting")]
    public float fireCooldown = 0.2f;

    [Header("Ammo Setting")]
    public int maxAmmo = 5;
    public float reloadTime = 2f;

    private int currentAmmo;
    private float lastFireTime;
    private bool isReloading = false;

    [Header("UI")]
    public BulletUI bulletUI;

    private void Start()
    {
        currentAmmo = maxAmmo;

        if (bulletUI != null)
        {
            bulletUI.UpdateAmmoUI(currentAmmo, maxAmmo);
            bulletUI.SetReloadText(false);
        }
        else
        {
            Debug.LogWarning("BulletUI가 연결되지 않았습니다.");
        }
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            TryFire();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private void TryFire()
    {
        if (Time.time < lastFireTime + fireCooldown)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        Fire();
    }

    private void Fire()
    {
        if (bulletObject == null || bulletFirePoint == null)
        {
            Debug.LogError("BulletObject 또는 BulletFirePoint가 연결되지 않았습니다.");
            return;
        }

        lastFireTime = Time.time;
        currentAmmo--;

        GameObject bullet = Instantiate(bulletObject, bulletFirePoint.position, Quaternion.identity);

        if (bulletUI != null)
        {
            bulletUI.UpdateAmmoUI(currentAmmo, maxAmmo);
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;

        if (bulletUI != null)
        {
            bulletUI.SetReloadText(true);
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;

        if (bulletUI != null)
        {
            bulletUI.UpdateAmmoUI(currentAmmo, maxAmmo);
            bulletUI.SetReloadText(false);
        }
    }
}