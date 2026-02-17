using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float materializeTime;
    [SerializeField] private Material dissolveMaterial;
    
    private PlayerActions playerActions;
    

    private float dissolveAmount = 0f;
    private bool isDissolving = false;
    private bool isReappearing = false;

    private void Awake()
    {
        playerActions = new PlayerActions();

        playerActions.Dissolve.Dissolve.performed += ctx => StartDissolve();
        playerActions.Dissolve.Reappear.performed += ctx => StartReappear();
    }

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAmount += Time.deltaTime / materializeTime;
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            dissolveMaterial.SetFloat("_Dissolution_Amount", dissolveAmount);

            if (dissolveAmount >= 1f)
            {
                isDissolving = false;
            }
        }
        
        if (isReappearing)
        {
            dissolveAmount -= Time.deltaTime / materializeTime;
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            dissolveMaterial.SetFloat("_Dissolution_Amount", dissolveAmount);

            if (dissolveAmount <= 0f)
            {
                isReappearing = false;
            }
        }
    }
    
    private void StartDissolve()
    {
        isDissolving = true;
        isReappearing = false;
    }
    
    private void StartReappear()
    {
        isReappearing = true;
        isDissolving = false;
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
