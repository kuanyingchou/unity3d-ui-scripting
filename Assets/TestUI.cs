using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestUI : MonoBehaviour {

	public void Start () {
        GameObject button = CreateButton(
            label: "OK!", 
            textureName: "button", 
            action: () => Debug.Log("OK!"));
        GameObject canvas = CreateCanvas();
        Attach(child: button, parent: canvas);
        
	}

    private GameObject CreateCanvas() {
        GameObject o = new GameObject();
        o.name = "Canvas";
        Canvas canvas = o.AddComponent<Canvas>();
        CanvasScaler scaler = o.AddComponent<CanvasScaler>();
        GraphicRaycaster raycaster = o.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        return o;
    }

    private GameObject CreateButton(string label, string textureName, UnityAction action) {
        GameObject o = new GameObject();
        o.name = "Button";
        
        Sprite sprite = Sprite.Create(
                Resources.Load<Texture2D>(textureName), 
                new Rect(0, 0, 256, 256), 
                new Vector2(.5f, .5f));
        Image image = o.AddComponent<Image>();
        image.sprite = sprite;

        

        Button button = o.AddComponent<Button>();
        button.onClick.AddListener(action);
        GameObject text = CreateText(label);
        Attach(child: text, parent: o);
        return o;

    }

    private GameObject CreateText(string label)
    {
        GameObject o = new GameObject();
        o.name = "Text";
        Text text = o.AddComponent<Text>();
        text.text = label;
        Font arial = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.font = arial;

        return o;
    }

    public static void Attach(GameObject child, GameObject parent) {
        child.transform.SetParent(parent.transform);
    }
}
