using UnityEngine;

public class ChromaVideoMaterialInstance : MonoBehaviour
{
    [SerializeField] private bool useCustomKeyColor;
    [SerializeField] private Color customKeyColor = Color.green;

    private new Renderer renderer;

    private void Awake(){}

    public void SetCustomKeyColor(Color color){}
}
