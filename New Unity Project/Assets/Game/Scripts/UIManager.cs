using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _coin;
    [SerializeField]
    private Text _goal;
    [SerializeField]
    private Text _maxGoal;

    private int _destructed = 0;
    [SerializeField]
    private int _maxDestructed = 4;

    [SerializeField]
    private GameObject _popUpMsg;

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void CoinPickedUp()
    {
        _coin.SetActive(true);
    }

    public void CoinGiven()
    {
        _coin.SetActive(false);
    }

    public void UpdateScore()
    {
        _destructed++;
        _goal.text = "Destructed: " + _destructed;
        _maxGoal.text = " Out of " + _maxDestructed;
    }

    public void PopUpMsg()
    {
        _popUpMsg.SetActive(true);
        StartCoroutine(PopUp());

    }

    IEnumerator PopUp()
    {
        yield return new WaitForSeconds(2.0f);
        _popUpMsg.SetActive(false);
    }
}
