using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour {

    public StaminaController stamina;
    public Slider staminaBar;

    [SerializeField] private float MaxStamina;
    [SerializeField] private float StaminaRefillRate;


	// Use this for initialization
	void Start () {

        stamina = new StaminaController(MaxStamina, StaminaRefillRate);
        UpdateStaminaBar();
    }
	
	// Update is called once per frame
	void Update () {

        stamina.RefillStamina();
        Testing();
        DebugInfo();
        UpdateStaminaBar();

    }

    private void UpdateStaminaBar()
    {
        staminaBar.value = stamina.GetStaminaPercentage();
    }

    private void Testing()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            if (stamina.SingleSubstract(5))
            {
                Debug.Log("stamina eaten");
                
            }
            else
                Debug.Log("Not Enough Stamina");
        }
    }

    private void DebugInfo()
    {
        DebugPanel.Log("currentStamina", "Stamina", stamina.GetCurrentStamina());
        DebugPanel.Log("currentStaminaPercentage", "Stamina", stamina.GetStaminaPercentage());
    }

}
