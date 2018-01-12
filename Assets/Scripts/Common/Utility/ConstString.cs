
public static class ConstDirectry {

	public const string DirPrefabs = "Prefabs/";
	public const string DirPrefabsHit = DirPrefabs + "Hit/";

    public const string DirPrefabsHitPlayer = DirPrefabsHit + "Player/";
	public const string DirPrefabsHitMediumEnemy = DirPrefabsHit + "Enemy_Medium/";
	public const string DirPrefabsHitEnemyMin = DirPrefabsHit + "Enemy_Mini/";

	public const string DirPrefabsEnemy = DirPrefabs + "Enemy/";
    public const string DirPrefabsPlayerChar = DirPrefabs + "PlayerCharacter/";
    public const string DirPopup = DirPrefabs + "Popup/";
	public const string DirIcons = DirPopup + "Icons/";
	public const string DirScene = "Scene/ProductScene/";
	public const string DirSceneDebug = "Scene/DebugScene/";

	public const string DirParticle = "Particles/";
	public const string DirParticleEdo = DirParticle + "Edo/";
}

public static class ConstActionHitData {
	public const string Action = "testAction";
	public const string ActionSuction = "testActionSuction";

	public const string ActionEnemyMin1 = "EnemyMini_Attack1";
	public const string ActionEnemyPop = "EnemyMini_Attack1";

	public const string ActionBossFire = "BossFire";
	public const string EnemyEgg = "EventEnemyEgg";
	public const string PopEnemy = "BossPopEnemy";

	public const string ActionHeroWeak1 = "HeroAttack_Light1";
    public const string ActionHeroWeak2 = "HeroAttack_Light2";
    public const string ActionHeroWeak3 = "HeroAttack_Light3";
    public const string ActionHeroStrong = "HeroAttack_Strong";

    public const string ActionHeelWeak1 = "HeelAttack_Light1";
    public const string ActionHeelWeak2 = "HeelAttack_Light2";
    public const string ActionHeelWeak3 = "HeelAttack_Light3";
    public const string ActionHeelStrong = "HeelAttack_Strong";

    public const string ActionSpecialistWeak1 = "SpecialistAttack_Light1";
    public const string ActionSpecialistWeak2 = "SpecialistAttack_Light2";
    public const string ActionSpecialistWeak3 = "SpecialistAttack_Light3";
    public const string ActionSpecialistStrong = "SpecialistAttack_Strong";


	public const string ActionEnemyHeroWeak1 = "Enemy_HeroAttack_Light1";
	public const string ActionEnemyHeroWeak2 = "Enemy_HeroAttack_Light2";
	public const string ActionEnemyHeroWeak3 = "Enemy_HeroAttack_Light3";

	public const string ActionEnemyHeelWeak1 = "Enemy_HeelAttack_Light1";
	public const string ActionEnemyHeelWeak2 = "Enemy_HeelAttack_Light2";
	public const string ActionEnemyHeelWeak3 = "Enemy_HeelAttack_Light3";
}

public static class ConstString {

//	public const string EnemyEgg = "EventEnemyEgg;
}

public static class ConstEffects {

	public const string Happiness = "HappinessNotes";
	public const string Disappointed = "DisappointedMark";

	public const string FireLanding = "FlareParticle";

	public const string SublimitedFog = "SublimatedFog";
	public const string Conffeti = "Conffeti";

	public const string DestroyBoss = "DestroyBossEffect";

	public const string StandardHittedEffect = "StandardHittedEffect";
	public const string HeroHit = "Efk_HeroHit";
	public const string ViranHit = "Efk_ViranHit";
	public const string Hitted = "Efk_Hit";

    public const string Buff = "Efk_Buff";
    public const string Dash = "Efk_Dash";
    public const string Explode = "Efk_Explode";
    public const string Flash = "Efk_Flash";
    public const string Hate = "Efk_Hate";
    public const string Sand = "Efk_Sand";
    public const string Water = "Efk_Water";
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

	public const int StatusMax = SpecialistCharacter + 1;
}