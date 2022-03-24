
/// <summary>
/// 
/// ※自動生成されたスクリプト。変更厳禁※
/// 
/// Abilityクラスに関してDependency Injectionを行うためのクラス
/// GameInfoパッケージ内にAbilityのDBを作成したいが、生身のクラスを公開するとアビリティーのRun()メソッドが公開されてしまう。
/// そのためGameInfoパッケージには読み取り専用のIAbilityInfoのリストを定義し、
/// Dependency Injectionにより完成させる。
/// </summary>
public class Ability_DI
{
    public Ability_DI()
    {
		AbilityLocatorInfo.Reflesh();

		AbilityLocatorInfo.I.Register(Ability_Sunny_675920.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Marisa_559213.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Reimu_925862.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Shanghai_667977.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Horai_976763.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Luna_542018.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Star_775671.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_ClownPiece_814032.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_GreatFairy_810247.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Alice_376247.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Komachi_917097.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Cirno_934591.GetAbilityBook());
		AbilityLocatorInfo.I.Register(Ability_Frandle_213725.GetAbilityBook());
		

    }


}
