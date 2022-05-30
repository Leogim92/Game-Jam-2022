using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] PlayerController player = null;
    [SerializeField] BossController boss = null;
    [Space]
    [SerializeField] TextMeshProUGUI playerHealth = null;
    [SerializeField] TextMeshProUGUI bossHealth = null;
    [Space]
    [SerializeField] Transform winFightBox = null;
    [SerializeField] Transform loseFightBox = null;

    AudioManager audioManager;
    bool fightDone;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        playerHealth.text = player.Health.ToString();
        bossHealth.text = boss.BossHP.ToString();

        if (!fightDone)
        {
            if(player.Health <= 0)
            {
                loseFightBox.gameObject.SetActive(true);
                fightDone = true;
            }
            else if(boss.BossHP <= 0)
            {
                winFightBox.gameObject.SetActive(true);
                fightDone = true;
                audioManager.StopBackgroundMusic();
                audioManager.PlayFightOutro();
            }
        }
    }
    public void WinFight()
    {
        audioManager.PlayButtonSound();
        FindObjectOfType<AudioManager>().SetMusic(AudioManager.Music.Death);
        LoadingManager.LoadLastScene();
    }
    public void RestartFight()
    {
        audioManager.PlayButtonSound();
        player.Heal();
        boss.Heal();
        loseFightBox.gameObject.SetActive(false);
        fightDone = false;
    }
}
