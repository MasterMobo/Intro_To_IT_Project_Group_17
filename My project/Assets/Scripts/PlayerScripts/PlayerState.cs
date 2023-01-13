using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles player state (invunerability, stamina, mana)
public class PlayerState : MonoBehaviour
{
    public Player player;

    public float maxStamina = 100f;
    public float _currentStamina;
    public float staminaRecoverySpeed = 0.5f;
    public float dashStaminaCost = 30f;
    public float runningStaminaCost = 2f;
    public float lowestStaminaToRun = 10f; // The lowest amount of stamina in order for the character to run (to prevent infinite running)
    public bool recoveringFromZeroStamina = false;


    public float invulnerableTimeWindow = 0.7f;
    float invulnerableTimeElapsed = 0f;
    public bool isInvulnerable = false;

    public float currentStamina
    {
        set
        {
            _currentStamina = value;
            if (_currentStamina > maxStamina)
            {
                _currentStamina = maxStamina;
            }
            else if (_currentStamina < 0)
            {
                _currentStamina = 0;
            }
        }
        get
        {
            return _currentStamina;
        }
    }


    public float maxMana = 100f;
    public float _currentMana;

    public float currentMana
    {
        set
        {
            _currentMana = value;
            if (_currentMana > maxMana)
            {
                _currentMana = maxMana;
            }
            else if (_currentMana < 0)
            {
                _currentMana = 0;
            }
        }
        get
        {
            return _currentMana;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        currentMana = maxMana;
    }


    // Update is called once per frame
    void Update()
    {
        CheckInvulnerability();
        RecoverStamina();
    }


    void RecoverStamina()
    {
        if (currentStamina == 0)
        {
            recoveringFromZeroStamina = true;
        }

        currentStamina += staminaRecoverySpeed;

        if (currentStamina > lowestStaminaToRun)
        {
            recoveringFromZeroStamina = false;
        }
    }


    void CheckInvulnerability()
    {
        if (isInvulnerable)
        {
            invulnerableTimeElapsed += Time.fixedDeltaTime;
        }

        if (invulnerableTimeElapsed > invulnerableTimeWindow)
        {
            isInvulnerable = false;
            invulnerableTimeElapsed = 0f;
        }
    }

}
