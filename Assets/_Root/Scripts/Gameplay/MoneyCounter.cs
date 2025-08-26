using Sirenix.OdinInspector;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] Transform fWheel;
    [SerializeField] Transform bWheel;

    public float timer;

    private void Start()
    {

    }

    private void Update()
    {
        if((fWheel.position.y - bWheel.position.y)>.85f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
        if (timer > 1f)
        {
            timer = 0f;
            GameManager.Instance.commandManager.AddMoney(1);
        }
    }

}
