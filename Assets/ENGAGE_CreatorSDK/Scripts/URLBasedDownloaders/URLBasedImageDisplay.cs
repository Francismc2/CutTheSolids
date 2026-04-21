using UnityEngine;

public class URLBasedImageDisplay : MonoBehaviour
{
    [Header("Behaviour Settings")]
    public bool loadImageOnStart;
    public string url;
    public bool reformatDisplayTransform;
    public bool hideDisplayTransformWhenLoading;
    public bool showIsDownloadingObject;
    public GameObject _isDownloadingObject;
    public Transform _displayTransform;
    public Renderer[] _imageRenderers;

    public void DownloadImage(string imgUrl)
    {
    }

}
