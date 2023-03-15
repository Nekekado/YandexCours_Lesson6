using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private ModelVariants _modelVariants;

    public void SetMaterial(Material material)
    {
        Renderer renderer = _modelVariants.CurrentSelected.GetComponent<Renderer>();
        renderer.material = material;
    }
}
