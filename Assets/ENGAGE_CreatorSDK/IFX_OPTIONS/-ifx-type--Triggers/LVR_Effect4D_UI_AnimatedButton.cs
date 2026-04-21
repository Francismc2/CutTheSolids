using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LVR_Effect4D_UI_AnimatedButton : MonoBehaviour {

    public RectTransform[] rectTransforms;
    public Button button;
    public SkinnedMeshRenderer blendRenderer;
    public Animator buttonAnimator;
    public Text text;
    int smallBlendIndex = -1;
    int bigBlendIndex = -1;

    //private void OnEnable()
    //{
    //    StartCoroutine(SetUpButton());
    //}

    //public void GoLoad(string option)
    //{
    //    StartCoroutine(SetUpButton());
    //}

    //IEnumerator SetUpButton()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (blendRenderer != null)
    //        {
    //            if (text != null)
    //            {
    //                float length = (float)text.text.Length;
    //                foreach (RectTransform rect in rectTransforms)
    //                {
    //                    if(rect)
    //                        rect.sizeDelta = new Vector2(Mathf.Clamp(length * 40f, 40, 700), 150);
    //                }
    //                float value = Mathf.InverseLerp(19f, 1f, length);
    //                float shapeWeight = value * 200f - 100f;
    //                float goBig = Mathf.Max(shapeWeight, 1f);
    //                float goSmall = Mathf.Abs(Mathf.Min(shapeWeight, -1f));
    //                goBig = Mathf.Min(goBig, 99);
    //                goSmall = Mathf.Min(goSmall, 99);
    //                blendRenderer.SetBlendShapeWeight(0, goBig);
    //                blendRenderer.SetBlendShapeWeight(1, goSmall);
    //            }
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    //public void OnClickButton()
    //{
    //    if (buttonAnimator)
    //    {
    //        buttonAnimator.SetTrigger("PressButton");
    //    }

    //    if (GetComponent<EffectScriptGame>())
    //    {
    //        if (button)
    //        {
    //            if (button.GetComponent<Image>())
    //                button.GetComponent<Image>().raycastTarget = false;
    //            if (button.GetComponent<Text>())
    //                button.GetComponent<Text>().raycastTarget = false;
    //        }
    //        Invoke("ClickButtonResult", 0.05f);
    //    }
    //}

    //void ClickButtonResult()
    //{
    //    ENG_IGM_Run4DEffects.instance.LaunchTriggeredEffects(GetComponent<EffectScriptGame>().uniqueID);

    //    if (button)
    //    {
    //        if (button.GetComponent<Image>())
    //            button.GetComponent<Image>().raycastTarget = true;
    //        if (button.GetComponent<Text>())
    //            button.GetComponent<Text>().raycastTarget = true;
    //    }
    //}

}
