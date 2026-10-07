using UnityEngine;

[RequireComponent(typeof(Controller))]
public class GenderSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] playerForms;

    private int activeFormID;
    private float changeTime;
    private bool changeGender;
    private Controller _controller;
    private PlayerData _data;

    void Start()
    {
        activeFormID = 0;
        changeTime = 0;
        _controller = GetComponent<Controller>();
        GetFormData();
    }

    // Update is called once per frame
    void Update()
    {
        GetFormData();
        GetActiveForm();

        changeGender = _controller.input.RetrieveGenderSwitchInput();

        changeTime += Time.deltaTime;

        if(changeGender && changeTime >= _data.changeRecharge)
        {
            SwitchGender();
            changeTime = 0;
        }
    }

    public void SwitchGender()
    {
        switch (activeFormID)
        {
            case 0:
                playerForms[activeFormID].SetActive(false);
                InstantiateGenderPuff(_data.genderSwitchPuff);
                activeFormID++;
                playerForms[activeFormID].SetActive(true);
                break;
            case 1:
                playerForms[activeFormID].SetActive(false);
                InstantiateGenderPuff(_data.genderSwitchPuff);
                activeFormID--;
                playerForms[activeFormID].SetActive(true);
                break;
        }
    }

    public void SetActiveFormID(int activeID)
    {
        activeFormID = activeID;
    }

    public void InstantiateGenderPuff(GameObject puffParticle)
    {
        Instantiate(puffParticle, transform.position, Quaternion.identity);
    }

    public GameObject GetActiveForm()
    {
        return playerForms[activeFormID];
    }

    public Animator GetActiveAnimator()
    {
        return playerForms[activeFormID].GetComponent<Animator>();
    }

    public bool GetCanChange()
    {
        return changeGender;
    }

    public float GetChangeTime()
    {
        return changeTime;
    }

    private void GetFormData()
    {
        _data = playerForms[activeFormID].GetComponent<PlayerFormData>()._data;
    }
}
