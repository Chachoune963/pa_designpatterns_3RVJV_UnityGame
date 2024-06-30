using System.Collections;
using UnityEngine;

public class MainUiPresenter : MonoBehaviour, Subscriber<int>
{
    [SerializeField] private RectTransform fillerBar;
    [SerializeField] private PlayerController player;

    private float fillerBarWidth;
    
    private void Start()
    {
        player.CurrentHealth.Subscribe(this);

        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return null;
        fillerBarWidth = fillerBar.sizeDelta.x;
    }

    public void Notify(int value)
    { 
        var healthCoef = (float)value / player.MaxHealth;
        Debug.Log(healthCoef);

        fillerBar.sizeDelta = new Vector2(healthCoef * fillerBarWidth, fillerBar.sizeDelta.y);
    }
}
