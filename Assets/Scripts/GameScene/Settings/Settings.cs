using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム実行設定
/// </summary>
[CreateAssetMenu(menuName ="Settings")]
public class Settings : ScriptableObject
{
    #region Singleton
    private static Settings instance;
    public static Settings I => instance ??= Resources.Load<Settings>("Settings");
    #endregion


    /// <summary>
    /// デバッグモードフラグ
    /// Onの時はデバッグ機能が使用可能
    /// </summary>
    public bool DebugMode => debugMode;
    [SerializeField] private bool debugMode;


}
