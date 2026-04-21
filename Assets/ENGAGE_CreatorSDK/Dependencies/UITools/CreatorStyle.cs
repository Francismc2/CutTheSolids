using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public static class CreatorStyle
    {
        #region Table Constants
        public const int SELECT_WIDTH = 30;
        public const int ID_WIDTH = 60;
        public const int NAME_WIDTH = 180;
        public const int PRETTYNAME_WIDTH = 220;
        public const int DATE_WIDTH = 100;
        public const int SHORT_BUTTON_WIDTH = 60;
        public const int MEDIUM_BUTTON_WIDTH = 90;
        public const int LONG_BUTTON_WIDTH = 120;
        public const int EXTRALONG_BUTTON_WIDTH = 160;

        public const string ASC = "▲";
        public const string DESC = "▼";

        public static GUIStyle RowStyleEven => CreateStyle(background: ListEvenTone);
        public static GUIStyle RowStyleOdd => CreateStyle(background: ListOddTone);
        #endregion

        public static GUILayoutOption MaxExtraLongButton => GUILayout.MaxWidth(EXTRALONG_BUTTON_WIDTH);


        #region Color Constants

        #region Personal Theme
        private const float lightPersonal = 0.7608f;
        private const float mediumPersonal = 0.6667f;
        private const float darkPersonal = 0.6353f;

        private const float listOddPersonal = 0.8706f;
        private const float listEvenPersonal = 0.8471f;
        #endregion

        #region Professional Theme
        private const float lightPro = 0.2196f;
        private const float mediumPro = 0.1765f;
        private const float darkPro = 0.1608f;

        private const float listOddPro = 0.2353f;
        private const float listEvenPro = 0.2157f;
        #endregion

        public static Color LightTone => EditorGUIUtility.isProSkin ? new Color(lightPro, lightPro, lightPro) : new Color(lightPersonal, lightPersonal, lightPersonal);
        public static Color MediumTone => EditorGUIUtility.isProSkin ? new Color(mediumPro, mediumPro, mediumPro) : new Color(mediumPersonal, mediumPersonal, mediumPersonal);
        public static Color DarkTone => EditorGUIUtility.isProSkin ? new Color(darkPro, darkPro, darkPro) : new Color(darkPersonal, darkPersonal, darkPersonal);

        public static Color ListOddTone => EditorGUIUtility.isProSkin ? new Color(listOddPro, listOddPro, listOddPro) : new Color(listOddPersonal, listOddPersonal, listOddPersonal);
        public static Color ListEvenTone => EditorGUIUtility.isProSkin ? new Color(listEvenPro, listEvenPro, listEvenPro) : new Color(listEvenPersonal, listEvenPersonal, listEvenPersonal);
        public static Color Red => new Color(1f, 0.4588f, 0.4588f);
        public static Color Green => new Color(0.4039f, 1f, 0.4862f);
        public static Color Yellow => new Color(0.9921f, 0.8078f, 0.3490f);
        public static Color Blue => new Color(0.4784f, 0.8784f, 1f);

        // From Luke
        //Yellow: hex FDCE59
        //Green: hex 67FF7C
        //Blue: hex 7AE0FF
        //Red: hex FF7575

        #endregion

        public static GUIStyle CreateStyle(GUIStyle baseStyle = null, Color? foreground = null, Color? background = null)
        {
            var style = baseStyle != null ? new GUIStyle(baseStyle) : new GUIStyle();

            if (foreground.HasValue)
            {
                style.normal.textColor = foreground.Value;
            }

            if (background.HasValue)
            {
                var bgTexture = new Texture2D(1, 1);
                bgTexture.SetPixel(0, 0, background.Value);
                bgTexture.Apply();

                style.normal.background = bgTexture;
            }

            return style;
        }

    }
}
