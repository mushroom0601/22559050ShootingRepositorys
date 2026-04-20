using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    public Image[] bulletIcons;
    public GameObject reloadTextObject;

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        for (int i = 0; i < bulletIcons.Length; i++)
        {
            if (bulletIcons[i] == null)
                continue;

            bulletIcons[i].enabled = i < currentAmmo;
        }
    }

    public void SetReloadText(bool isActive)
    {
        if (reloadTextObject != null)
        {
            reloadTextObject.SetActive(isActive);
        }
    }
}