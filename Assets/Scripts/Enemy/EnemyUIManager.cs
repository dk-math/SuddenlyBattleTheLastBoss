using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;
    // public Slider staminaSlider;

    public void Init(EnemyManager enemyManager) {
        hpSlider.maxValue = enemyManager.maxHp;
        hpSlider.value = enemyManager.maxHp;
        // staminaSlider.maxValue = playerManager.maxStamina;
        // staminaSlider.value = playerManager.maxStamina;
    }

    public void UpdateHP(int hp) {
        hpSlider.value = hp;
        // hpSlider.DOValue(hp, 0.5f);
    }

    // public void UpdateStamina(int stamina) {
    //     hpSlider.value = hp;
    //     // staminaSlider.DOValue(stamina, 0.5f);
    // }
}