using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float materializeTime;
    [SerializeField] private Material dissolveMaterial;
    
    private PlayerActions playerActions;
    
    void Update()
    {
        float dissolveAmount = 0f;
        while (dissolveAmount < 1f)
        {
            dissolveAmount += Time.deltaTime / materializeTime;
            dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }
}
