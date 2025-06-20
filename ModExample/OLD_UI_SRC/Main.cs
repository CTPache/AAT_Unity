
using UnityEngine;
public class Main
{
    public void ModMain()
    {
        Font font = Resources.Load<Font>("FOT-SeuratProN-M"); // Load the font from Resources/yourFont
        if (font != null)
        {
            ReplaceFont.instance.default_font_data_ = font; // Assign the font to the Text component
        }
        else
        {
            Debug.LogError("Font not found!"); // Handle the case where the font is not found
        }
    }
}
