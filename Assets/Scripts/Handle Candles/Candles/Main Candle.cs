using UnityEngine;
using UnityEngine.UI;

public class MainCandle : MonoBehaviour,InteractCandle
{
    public int CandleIndex {  get; set; }
    public bool isInteracted;
    public bool isInCollision;

    [Header("Room Objects")]
    [SerializeField] private GameObject candleLit;
    [SerializeField] private GameObject prevCandleBackground;
    [SerializeField] private GameObject newCandleBackground;
    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject blackLayer;
 

    public virtual void OnStartInteract()
    {
        candleLit.gameObject.SetActive(true);
        isInCollision = true;
    }

    public virtual void OnInteract()
    {
        Instantiate(newCandleBackground, prevCandleBackground.transform.position, Quaternion.identity);
        Instantiate(candle, candleLit.transform.position, Quaternion.identity);
        candleLit.gameObject.SetActive(false);
        Color color = blackLayer.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        blackLayer.GetComponent<SpriteRenderer>().color = color;
        Destroy(prevCandleBackground.gameObject);
    }

    public virtual void OnLeaveInteract()
    {
        candleLit.gameObject.SetActive(false);
        isInCollision = false;
    }

    private void Update()
    {
        if (isInCollision && !isInteracted && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Girdi");
            OnInteract();
            isInteracted = true;
        }
    }
}
