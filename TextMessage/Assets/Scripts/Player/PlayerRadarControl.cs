using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRadarControl : MonoBehaviour
{
    private AlienStateControl _alienStateControl;
    [SerializeField]
    private float scale = 1f;
    [SerializeField]
    private float angle = 70f;
    [SerializeField]
    private float detectionRange = 50f;
    [SerializeField]
    private GameObject alienIcon;
    [SerializeField]
    private GameObject radarScreen;

    FakeRadioControl[] fakeRadioControls;
    private GameObject alienIconImg;

    private List<GameObject> icons = new List<GameObject>();
    private List<IconControl> iconControls = new List<IconControl>();
    private List<bool> isAlien = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        _alienStateControl = FindObjectOfType<AlienStateControl>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIcons();
    }

    private bool AlienInRange()
    {
        if (_alienStateControl == null)
            _alienStateControl = FindObjectOfType<AlienStateControl>();

        Vector3 relative_pos = new Vector3(_alienStateControl.transform.position.x, transform.position.y, _alienStateControl.transform.position.z);

        if (Vector3.AngleBetween(transform.position, 
            relative_pos) <= (angle / 180f) * Mathf.PI
            && Vector3.Distance(transform.position, relative_pos) <= detectionRange)
            return true;
        else
            return false;
    }

    private bool FakeRadarInRange(FakeRadioControl fakeRadioControl)
    {
        Vector3 relative_pos = new Vector3(fakeRadioControl.transform.position.x, transform.position.y, fakeRadioControl.transform.position.z);

        if (Vector3.AngleBetween(transform.position,
            relative_pos) <= (angle / 180f) * Mathf.PI
            && Vector3.Distance(transform.position, relative_pos) <= detectionRange)
            return true;
        else
            return false;
    }

    private void OpenRadarUI()
    {
        if (Input.GetMouseButtonDown(2))
        {
            // Open the UI
        }
    }

    private void UpdateIcons()
    {
        for (int i = 0;i < icons.Count;i++)
        {
            if (isAlien[i]) {
                if (AlienInRange())
                    UpdatePosition(i);
                else
                    icons[i].SetActive(false);
            }
            else {
                if (FakeRadarInRange(iconControls[i].GetComponent<FakeRadioControl>()))
                    UpdatePosition(i);
                else
                    icons[i].SetActive(false);
            }


        }
    }

    public void AddNewIcon(IconControl iconControl, bool alien = false)
    {
        iconControls.Add(iconControl);
        isAlien.Add(alien);

        if (alien && AlienInRange())
            icons.Add(DrawIcon(_alienStateControl.transform.position));
        else if (FakeRadarInRange(iconControl.GetComponent<FakeRadioControl>()))
            icons.Add(DrawIcon(iconControl.transform.position));
    }

    public bool RemoveIcon(IconControl iconControl)
    {
        for (int i = 0;i < iconControls.Count;i++)
            if (iconControls[i] == iconControl)
            {
                iconControls.RemoveAt(i);
                isAlien.RemoveAt(i);
                Destroy(icons[i]);
                icons.RemoveAt(i);
                return true;
            }

        return false;
    }

    private GameObject DrawIcon(Vector3 position)
    {
        Vector3 relative_pos = new Vector3(position.x, transform.position.y, position.z);
        float dist = Vector3.Distance(transform.position, relative_pos);
        float angle = Vector3.AngleBetween(transform.position, relative_pos);


        GameObject icon = Instantiate(alienIcon) as GameObject;
        icon.transform.SetParent(radarScreen.transform);
        float x = 0;
        float y = -radarScreen.GetComponent<RectTransform>().rect.height / 2 + dist * scale;

        icon.GetComponent<RectTransform>().anchoredPosition = new Vector3(x * Mathf.Cos(angle) - y * Mathf.Sin(angle), 
            x * Mathf.Sin(angle) + y * Mathf.Cos(angle));

        return icon;
    }

    private void UpdatePosition(int i)
    {
        icons[i].SetActive(true);

        Vector3 relative_pos = new Vector3(iconControls[i].transform.position.x, transform.position.y, iconControls[i].transform.position.z);
        float dist = Vector3.Distance(transform.position, relative_pos);
        float angle = Vector3.AngleBetween(transform.position, relative_pos);

        float x = 0;
        float y = -radarScreen.GetComponent<RectTransform>().rect.height / 2 + dist* scale;
        float xp = x * Mathf.Cos(angle) - y * Mathf.Sin(angle);
        float yp = x * Mathf.Sin(angle) + y * Mathf.Cos(angle);

        icons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x * Mathf.Cos(angle) - y * Mathf.Sin(angle),
            x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
    }

    private void WipeIcons()
    {
        foreach (GameObject icon in icons)
        {
            Destroy(icon);
        }

        icons.Clear();
    }
}
