using NUnit.Framework;
using OurUtils;
using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    public class TemplateUIManager : MonoBehaviour
    {
        [Header("Parent of UI")]
        [SerializeField] Transform _baseLayerParent;
        [SerializeField] Transform _tabLayerParent;
        [SerializeField] Transform _popUpLayerParent;

        [Header("Base Layer UI Prefab")]
        [SerializeField] TemplateUIController _aaaUIControllerPrefab;
        [SerializeField] TemplateUIController _bbbUIControllerPrefab;

        private TemplateUIController _aaaUIController;
        private TemplateUIController _bbbUIController;

        private List<IUIController> _baseLayerUIs = new();
        private IUIController _currentBaseUI;

        public TemplateUIController AaaUIController { get => _aaaUIController; set => _aaaUIController = value; }
        public TemplateUIController BbbUIController { get => _bbbUIController; set => _bbbUIController = value; }

        void Start()
        {
            _aaaUIController = Instantiate(_aaaUIControllerPrefab, _baseLayerParent);
            _bbbUIController = Instantiate(_bbbUIControllerPrefab, _baseLayerParent);

            _baseLayerUIs = new()
            {
                _aaaUIController,
                _bbbUIController,
            };
        }

        public void SetBaseScreen(IUIController incomingUI)
        {
            if (_currentBaseUI == null)
            {
                _currentBaseUI = incomingUI;
                _currentBaseUI.OnActiveUI();
            }
            else
            {
                if (_currentBaseUI != incomingUI)
                {
                    _currentBaseUI.OnDeactiveUI();
                    _currentBaseUI = incomingUI;
                    _currentBaseUI.OnActiveUI();
                }
            }
        }
    }
}