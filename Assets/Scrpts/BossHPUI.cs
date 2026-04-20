using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [Header("HP 아이콘 (순서대로 1,2,3)")]
    public Image[] hpIcons;

    // 초기 세팅 및 HP 갱신용
    public void SetHP(int currentHP, int maxHP)
    {
        if (hpIcons == null || hpIcons.Length == 0)
        {
            Debug.LogWarning("BossHPUI: hpIcons가 연결되지 않았습니다.");
            return;
        }

        for (int i = 0; i < hpIcons.Length; i++)
        {
            if (hpIcons[i] == null)
                continue;

            // currentHP보다 인덱스가 작으면 표시, 크면 숨김
            hpIcons[i].enabled = (i < currentHP);
        }
    }
}