/// <summary>
/// 初期化が必要な(カードの)クラスが実装するインターフェース
/// </summary>
public interface ICardInitializer
{
    void Initialize(bool isPlayer, ICardInfo cardInfo);
}