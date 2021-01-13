using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBombControl : MonoBehaviour
{
    private EquipmentControl _equipmentControl;

    [SerializeField]
    private GameObject stunBombPrefab;
    private bool startStunBomb;

    [SerializeField]
    private GameObject fakeRadarPrefab;
    private bool startFakeRadar;

    public enum Weapon
    {
        StunBomb,
        FakeRadar
    }

    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        _equipmentControl = GetComponent<EquipmentControl>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (weapon)
        {
            case Weapon.StunBomb:
                if (CanMakeStunBomb())
                {
                    if (Input.GetKey(KeyCode.F) && !startStunBomb)
                        StartCoroutine(MakeStunBomb());
                }
                break;
            case Weapon.FakeRadar:
                if (CanMakeFakeRadar())
                {
                    if (Input.GetKey(KeyCode.F) && !startFakeRadar)
                        StartCoroutine(MakeFakeRadar());
                }
                break;
            default:
                break;
        }
    }

    private bool CanMakeStunBomb()
    {
        if (_equipmentControl.ContainItem("Metal")
            && _equipmentControl.ContainItem("Oil")
            && _equipmentControl.ContainItem("Heat Sensor"))
            return true;

        return false;
    }

    IEnumerator MakeStunBomb()
    {
        startStunBomb = true;
        //Remove item first
        _equipmentControl.RemoveItem("Metal");
        _equipmentControl.RemoveItem("Oil");
        _equipmentControl.RemoveItem("Heat Sensor");
        yield return new WaitForSeconds(4);
        startStunBomb = false;
        GameObject bomb = Instantiate(stunBombPrefab) as GameObject;
        bomb.transform.position = transform.position;
    }

    private bool CanMakeFakeRadar()
    {
        if (_equipmentControl.ContainItem("Metal")
            && _equipmentControl.ContainItem("Battery")
            && _equipmentControl.ContainItem("Heat Sensor")
            && _equipmentControl.ContainItem("Noise Sensor"))
            return true;

        return false;
    }

    IEnumerator MakeFakeRadar()
    {
        startFakeRadar = true;
        //Remove item first
        _equipmentControl.RemoveItem("Metal");
        _equipmentControl.RemoveItem("Battery");
        _equipmentControl.RemoveItem("Heat Sensor");
        _equipmentControl.RemoveItem("Noise Sensor");
        yield return new WaitForSeconds(4);
        startFakeRadar = false;
        GameObject fakeRadar = Instantiate(fakeRadarPrefab) as GameObject;
        fakeRadar.transform.position = transform.position;
    }

    private void ChangeWeapon()
    {

    }
}
