using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Belonging : MonoBehaviour, ICheckable
{
    [SerializeField] private Sprite belongingImage;
    [SerializeField] private string belongingText;
    protected PlayerController playerReference => 
        GameManager.instance.blackBoard.GetValue("PlayerController",out PlayerController _playerController) ? _playerController : null;

    [Header("Conditions")]
    public bool inCollision;
    public bool isChecked;
    public virtual void OnEnterCheck()
    {
        Debug.Log("OnEnterCheck");
        if (!isChecked)
        {
            UIController.instance.indicator.gameObject.SetActive(true);
        }
       
        inCollision = true;
    }

    public virtual void OnLeaveCheck()
    {
        Debug.Log("OnLeaveCheck");
        if (!isChecked)
        {
            UIController.instance.indicator.gameObject.SetActive(false);
            inCollision = false;
        }
       
      
    }

    private void Update()
    {
        if (inCollision)
        {
            Debug.Log("OnStayCheck");
            if (Input.GetKeyDown(KeyCode.E) && !isChecked)
            {

                UIController.instance.panel.gameObject.SetActive(true);
                UIController.instance.panel.transform.Find("BelongingText").GetComponent<TextMeshProUGUI>().text = belongingText;
                UIController.instance.panel.transform.Find("BelongingImage").GetComponent<Image>().sprite = belongingImage;
                playerReference.isInInteraction = true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Checked");
                isChecked = true;
                playerReference.isInInteraction = false;
                OnCheck();
            }
        }
        
       
    }

    public void OnCheck()
    {
        UIController.instance.indicator.gameObject.SetActive(false);
        UIController.instance.panel.gameObject.SetActive(false);
    }
}
