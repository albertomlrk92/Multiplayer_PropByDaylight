using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform healthFill;

    private PlayerManager player;
    private ControllerPlayerMovement controller;


    public void SetPlayer(PlayerManager _player)
    {
        player = _player;
        controller = player.GetComponent<ControllerPlayerMovement>();
        
    }


    void Update()
    {
        SetHealthAmount(player.GetHealthPct());
    }

    void SetHealthAmount(float _amount)
    {
        healthFill.localScale = new Vector3(1f, _amount, 1f);
    }
}
