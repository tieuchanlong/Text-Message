using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphview _dialogueGraphview;
    private string _fileName = "New Narrative";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
    }

    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap{anchored = true};
        miniMap.SetPosition(new Rect(10, 30, 200, 140));
        _dialogueGraphview.Add(miniMap);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_dialogueGraphview);
    }

    private void ConstructGraphView()
    {
        _dialogueGraphview = new DialogueGraphview
        {
            name = "Dialogue Graph"
        };

        _dialogueGraphview.StretchToParentSize();
        rootVisualElement.Add(_dialogueGraphview);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue); // change file name when change in editor
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(clickEvent: () => RequestDataOperation(true)) { text = "Save Data"});
        toolbar.Add(new Button(clickEvent: () => RequestDataOperation(false)) { text = "Load Data" });

        var nodeCreateButton = new Button(() =>
        {
            _dialogueGraphview.CreateNode("Dialogue Node");
        });

        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name.", "OK");
        }

        var saveUtility = GraphSaveUtility.GetInstance(_dialogueGraphview);

        if (save)
            saveUtility.SaveGraph(_fileName);
        else
            saveUtility.LoadGraph(_fileName);
    }
}
