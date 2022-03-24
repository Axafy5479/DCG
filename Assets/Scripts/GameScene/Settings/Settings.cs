using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�����s�ݒ�
/// </summary>
[CreateAssetMenu(menuName ="Settings")]
public class Settings : ScriptableObject
{
    #region Singleton
    private static Settings instance;
    public static Settings I => instance ??= Resources.Load<Settings>("Settings");
    #endregion


    /// <summary>
    /// �f�o�b�O���[�h�t���O
    /// On�̎��̓f�o�b�O�@�\���g�p�\
    /// </summary>
    public bool DebugMode => debugMode;
    [SerializeField] private bool debugMode;


}
