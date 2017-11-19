
public static class ConstDirectry {

	public const string DirPrefabs = "Prefabs/";
	public const string DirPrefabsHit = DirPrefabs + "Hit/";
    public const string DirPrefabsHitHero = DirPrefabs + "Hit/Hero/";
    public const string DirPrefabsHitEnemyMin = DirPrefabsHit + "Enemy_Mini/";
	public const string DirPopup = DirPrefabs + "Popup/";
	public const string DirScene = "Scene/ProductScene/";
	public const string DirSceneDebug = "Scene/DebugScene/";
}

public static class ConstActionHitData {
	public const string Action = "testAction";
	public const string ActionSuction = "testActionSuction";

	public const string ActionEnemyMin1 = "EnemyMini_Attack1";

    public const string ActionHeroWeak1 = "HeroAttack_Light1";
    public const string ActionHeroWeak2 = "HeroAttack_Light2";
    public const string ActionHeroWeak3 = "HeroAttack_Light3";
    public const string ActionHeroStrong = "HeroAttack_Strong";
}

public static class ConstString {


}

public static class ConstScene {

	public const string IntroScene = "IntroductScene";
	public const string MainGameScene = "MainScene";
	public const string ResultScene = "ResultScene";
}


public static class ConstTags {

	public const string Player = "Player";
	public const string Enemy = "Enemy";
	public const string PlayerAttack = "PlayerAttack";
	public const string EnemyAttack = "EnemyAttack";
	public const string EnemyCheckPoint = "EnemyCheckPoint";
	public const string City = "CityArea";
    public const string HeroArea = "HeroArea";
    public const string HeelArea = "HeelArea";
    public const string SpecialistArea = "SpecialistArea";
	public const string RunAwayPoint = "RunAwayArea";
}

public static class ConstPlayerSta
{
    public const int NormalCharacter = 0;
    public const int HeroCharacter = 1;
    public const int HeelCharacter = 2;
    public const int SpecialistCharacter = 3;
    public const int MonsterCharacter = 4;
}