using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        GameObject canvasObject = new GameObject();
        canvasObject.name = "Canvas";
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        GraphicRaycaster raycaster = canvasObject.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CreateEventSystem();

        return canvasObject;
    }

    private GameObject CreateEventSystem() {
        GameObject eventSystemObject = new GameObject();
        eventSystemObject.name = "EventSystem";
        eventSystemObject.AddComponent<EventSystem>();
        eventSystemObject.AddComponent<StandaloneInputModule>();
        eventSystemObject.AddComponent<TouchInputModule>();
        return eventSystemObject;
    }

    private GameObject CreateButton(string label, string textureName, UnityAction action) {
        GameObject buttonObject = new GameObject();
        buttonObject.name = "Button";
        
        Sprite sprite = Sprite.Create(
                Resources.Load<Texture2D>(textureName), 
                new Rect(0, 0, 256, 256), 
                new Vector2(.5f, .5f));
        Image image = buttonObject.AddComponent<Image>();
        image.sprite = sprite;

        Button button = buttonObject.AddComponent<Button>();
        button.onClick.AddListener(action);
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        //TODO figure out how to layout properly!
        //rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 100, 256);
        //rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 100, 256);
        

        GameObject text = CreateText(label);
        Attach(child: text, parent: buttonObject);

        return buttonObject;

    }

    private GameObject CreateText(string label)
    {
        GameObject textObject = new GameObject();
        textObject.name = "Text";

        Text text = textObject.AddComponent<Text>();
        text.text = label;
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        return textObject;
    }

    public static void Attach(GameObject child, GameObject parent) {
        child.transform.SetParent(parent.transform);
    }
}
