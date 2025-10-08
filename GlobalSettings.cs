using HKMirror;
using System.Collections.Generic;

namespace CharmReValance
{
    public class GlobalSettings
    {
		#region DefaultAbilitySettings
////////////////////////////////////////////////////////////////
//	DEFAULT ABILITY SETTINGS

//	Default Movement Settings
        private static readonly float defaultWalkSpeed = 6.0f;
        private static readonly float defaultSpeed = 8.3f;
        private static readonly float defaultDashCooldown = 0.6f;
		private static readonly float defaultDashSpeed = 20f;
        private static readonly float defaultShadeCloakCooldown = 1.5f;
		private static readonly float defaultCDashChargeTime = 0.8f;
		private static readonly int defaultCDashDamage = 25;
		
//	Default Damage & Focus Settings
        private static readonly float defaultIFrames = 1.3f;
        private static readonly float defaultDamageKnockback = 0.2f;
		private static readonly bool defaultIFramesNegateHazard = true;
        private static readonly float defaultFocusTime = 0.891f;
        private static readonly int defaultFocusHealing = 1;
		private static readonly int defaultFocusCost = 33;
		private static readonly int defaultMaximumOverheal = 1;

//	Default Nail Settings
		private static readonly int defaultNailDamageBase = 5;
		private static readonly int defaultNailDamageUpgrade = 3;
        private static readonly float defaultNailCooldown = 0.41f;
        private static readonly float defaultNailKnockback = 1.0f;
		private static readonly float defaultParryIFrames = 0.25f;
        private static readonly bool defaultNailRangeAffectsWallSlash = true;
        private static readonly int defaultSoulGain = 11;		
        private static readonly int defaultReserveSoulGain = 6;
		
//	Default Nail Art Settings
        private static readonly float defaultNailArtChargeTime = 1.35f;
		private static readonly float defaultGreatSlashDamage = 2.5f;
        private static readonly float defaultGreatSlashKnockback = 2.0f;
		private static readonly float defaultDashSlashDamage = 2.5f;
		private static readonly float defaultDashSlashKnockback = 2.0f;
		private static readonly float defaultCycloneSlashDamage = 1.25f;
        private static readonly float defaultCycloneSlashKnockback = 0.25f;

//	Default Spell Settings
        private static readonly int defaultSpellCost = 33;
        private static readonly int defaultVSDamage = 15;
        private static readonly int defaultSSDamage = 30;
        private static readonly int defaultHWDamage = 10;
        private static readonly int defaultASDamage = 15;
		private static readonly float defaultHWKnockback = 0.25f;
        private static readonly int defaultDiveDamage = 10;
        private static readonly int defaultDDiveDamage = 15;
        private static readonly int defaultDDarkDamageL = 25;
        private static readonly int defaultDDarkDamageR = 25;
        private static readonly int defaultDDarkDamageMega = 15;
        private static readonly float defaultVSSizeScaleX = 1.0f;
        private static readonly float defaultVSSizeScaleY = 1.0f;
        private static readonly float defaultSSSizeScaleX = 1.0f;
        private static readonly float defaultSSSizeScaleY = 1.0f;

//	Default Dream Nail Settings
        private static readonly int defaultDreamNailSoulGain = 33;
		private static readonly float defaultDreamNailRange = 1f;
		private static readonly float defaultDreamNailHeight = 1.4f;
		private static readonly int defaultEssenceChanceLow = 300;	//	vanilla 300
        private static readonly int defaultEssenceChanceHigh = 20;  //	vanilla 60

        #endregion

        #region DefaultCharmSettings
////////////////////////////////////////////////////////////////
//	DEFAULT CHARM SETTINGS
        #region General Changes
        private static readonly bool defaultReorderCharms = true;
        public static readonly int[] newInventoryCharmOrder =
        {
            0,	//	index 0 = 0 for efficiency
			37, 31, 16, 26, 32, 18, 20, 21, 03, 35,
            04, 12, 15, 06, 13, 25, 24, 23, 08, 09,
            28, 05, 14, 17, 10, 11, 19, 33, 29, 27,
            07, 34, 01, 22, 39, 40, 30, 38, 36, 02
        };
        public static readonly int[] oldInventoryCharmOrder =
        {
            0,	//	index 0 = 0 for efficiency
			02, 01, 04, 20, 19, 21, 31, 37, 03, 35,
            23, 24, 25, 33, 14, 15, 32, 18, 13, 06,
            12, 05, 11, 10, 22, 07, 34, 08, 09, 27,
            29, 17, 16, 28, 26, 39, 30, 38, 40, 36
        };
		public static readonly Dictionary<string,string> charmDesc = new Dictionary<string, string>()
        {
            {"CHARM_DESC_1", "A swarm will follow the bearer and gather up any loose Geo.\n\nIncreases the quantity and obedience of the bearer's followers."},
            {"SHOP_DESC_STALWARTSHELL", "Life in Hallownest can be tough, a battle of attrition that wears you down. This charm grants you more time to recover after taking damage and hardens one's carapace."},
            {"CHARM_DESC_4", "Builds resilience.\n\nThe bearer will generate a hardened shell if they do not take damage for some time. When recovering from damage, the bearer will remain invulnerable for longer."},
            {"CHARM_DESC_5", "Protects its bearer with a hard shell while focusing SOUL and allows healing beyond one's limit.\n\nThe shell is not indestructible and will shatter if it absorbs too much damage."},
            {"CHARM_DESC_6", "Embodies the fury and heroism that comes upon those who are about to die.\n\nIn moments of great danger, a fiery swell empowers the bearer's will, body, and SOUL."},
            {"CHARM_DESC_9", "Contains a living core that seeps precious lifeblood.\n\nConstantly feeds upon the bearer's SOUL, creating a self-reparing coating of lifeblood that protects the bearer."},
            {"CHARM_DESC_11", "Living charm born in the gut of a Flukemarm.\n\nTransforms the Vengeful Spirit spell into a horde of volatile baby flukes. Increases the ferocity of any who follow the bearer."},
            {"CHARM_DESC_12", "Senses the pain of its bearer and lashes out at the world around them.\n\nWhen taking damage, sprout thorny vines that administer equitable retribution to nearby foes."},
            {"CHARM_DESC_13", "Freely given by the Mantis Tribe to those they respect.\n\nGrants a modest increase in precision, speed, damage, and reach to the bearer's nail."},
            {"SHOP_DESC_NORECOIL", "I love the simple design on this little fellow! It's made out of a nice, sturdy material.\n\nWear it and you will recover easier and won't be getting knocked around so much."},
            {"CHARM_DESC_14", "A stable bearing reduces knockback and lends itself to heavy nail swings and harty tactics.\n\nReduces the SOUL needed to focus and recover from damage."},
            {"SHOP_DESC_ENEMYRECOILUP", "If you want more heft behind your blows, I have just the thing. With this charm equipped, every hit from your nail will feel like a kick from a full grown stag."},
            {"CHARM_DESC_15", "Formed from the nails of fallen warriors.\n\nIncreases the force of the bearer's nail, greatly increasing damage. The weight of the weapon is too much for those without a steady body."},
            {"CHARM_DESC_17", "Composed of living fungal matter. Scatters spores when exposed to SOUL.\n\nWhen focusing SOUL, emit a spore cloud that damages enemies based on the bearer's capacity for SOUL."},
            {"CHARM_DESC_26", "Contains the passion, skill and regrets of a Nailmaster.\n\nIncreases the bearer's mastery of Nail Arts, allowing them to focus their power faster and unleash stronger arts."},
            {"CHARM_DESC_28", "Reveals the form of Unn within the bearer. While focusing SOUL, the bearer will take on a new shape and can move freely to avoid enemies.\n\nIncreases the bearer's capacity for SOUL."},
            {"CHARM_DESC_30", "Transient charm created for those who wield the Dream Nail and collect Essence.\n\nThe bearer's Dream Nail charges faster but is easily corrupted by Nightmares."},
            {"CHARM_DESC_38", "Defensive charm once wielded by a tribe that could shape dreams.\n\nConjures a shield that follows behind the bearer and protects them from surprise attacks by repelling foes."},
            {"CHARM_DESC_39", "Silken charm containing a song of farewell, left by the Weavers who departed Hallownest for their old home.\n\nSummons weaverlings who pilfer SOUL from their foes."},
            {"CHARM_DESC_40_N", "Token commemorating the start of a friendship.\n\nContains a song of protection that grows stronger the more dire the threat against its bearer."},
        };
        #endregion

//	Baldur Shell
        private static readonly int defaultCharm5NotchCost = 2;
        private static readonly int defaultBaldurShellBlocks = 3;
        private static readonly float defaultBaldurShellKnockback = 1.8f;
		private static readonly bool defaultBaldurShellDeepFocusAffected = true;
		private static readonly int defaultBaldurShellDefendersCrestBlocks = 5;
		private static readonly bool defaultBaldurShellGreedShell = true;
        private static readonly int defaultBaldurShellGreedGeoLossRate = 20;
        private static readonly int defaultBaldurShellGreedGeoDrop = 20;
        private static readonly float defaultBaldurShellGreedDCGeoLossRate = 0.8f;
		private static readonly int defaultBaldurShellLifebloodHeartOverhealMaxIncrease = 1;

//	Carefree Melody
		private static readonly int defaultCharmCarefreeMelodyNotchCost = 3;
		private static readonly bool defaultCarefreeMelodyHeartHealing = true;
        private static readonly int defaultCarefreeMelodyChance = 60;
        //private static readonly int defaultCarefreeMelodyChanceAccel = 8;
		//private static readonly int defaultCarefreeMelodyGrubSongBonus = 9;

//	Dashmaster
        private static readonly int defaultCharm31NotchCost = 1;
        private static readonly bool defaultDashmasterDownwardDash = true;
        private static readonly float defaultDashmasterDashCooldown = 0.4f;
		private static readonly float defaultDashmasterShadeCloakCooldown = 1.0f;
		private static readonly int defaultDashmasterCDashDamage = 40;

//	Deep Focus
        private static readonly int defaultCharm34NotchCost = 2;
        private static readonly int defaultDeepFocusHealing = 2;
        private static readonly float defaultDeepFocusHealingTimeMult = 1.65f;
		private static readonly float defaultDeepFocusCDashDamageMult = 2f;
		private static readonly float defaultDeepFocusCDashTimeMult = 1.65f;

//	Defender's Crest
        private static readonly int defaultCharm10NotchCost = 1;
        private static readonly float defaultDefendersCrestDiscount = 0.8f;
		private static readonly float defaultDefendersCrestFrequency = 0.6f;
		private static readonly float defaultDefendersCrestDuration = 1.1f;
		private static readonly float defaultDefendersCrestRadius = 1f;
        private static readonly float defaultDefendersCrestDamage = 4.5f;
		private static readonly float defaultDefendersCrestSteadyBodyRadius = 1.2f;
		private static readonly float defaultDefendersCrestSteadyBodyDuration = 1.4f;
		private static readonly float defaultDefendersCrestFotFDamage = 6.0f;

//	Dream Wielder
        private static readonly int defaultCharm30NotchCost = 1;
        private static readonly int defaultDreamWielderSoulGain = 33;
        private static readonly int defaultDreamWielderEssenceChanceLow = 100;	//	vanilla 200
        private static readonly int defaultDreamWielderEssenceChanceHigh = 5;	//	vanilla 40

//	Dreamshield
        private static readonly int defaultCharm38NotchCost = 3;
		private static readonly int defaultDreamshieldOverheal = 1;
		private static readonly bool defaultDreamshieldBlocksBehind = true;
		private static readonly int defaultDreamshieldBaseDamage = 8;
		private static readonly int defaultDreamshieldDamageScaleRate = 100;
		private static readonly float defaultDreamshieldKnockback = 1.5f;
        private static readonly float defaultDreamshieldReformationTime = 6.4f;
        private static readonly float defaultDreamshieldSizeScale = 0.75f;
		private static readonly float defaultDreamshieldOverheadOffset = 30f;
		private static readonly float defaultDreamshieldTweenSpeed = 20f;
		private static readonly float defaultDreamshieldDefendersCrestReformTimeReduction = 1.2f;
		private static readonly float defaultDreamshieldDreamWielderReformTimeReduction = 1.6f;
		private static readonly int defaultDreamshieldLifebloodHeartOverhealMaxIncrease = 1;

//	Flukenest
        private static readonly int defaultCharm11NotchCost = 2;
		private static readonly float defaultFlukeLifetimeMin = 4f;	//	vanilla: 2f
		private static readonly float defaultFlukeLifetimeMax = 5f;	//	vanilla: 3f
        private static readonly int defaultFlukeDamage = 3;
        private static readonly int defaultFlukeShamanStoneDamage = 4;
		private static readonly int defaultFlukeFotFDamageIncrease = 2;
        private static readonly int defaultFlukenestVSFlukes = 9;
        private static readonly int defaultFlukenestSSFlukes = 16;
        private static readonly float defaultFlukeSizeMin = 0.7f;
        private static readonly float defaultFlukeSizeMax = 0.9f;
        private static readonly float defaultFlukeShamanStoneSizeMin = 0.9f;
        private static readonly float defaultFlukeShamanStoneSizeMax = 1.2f;
        private static readonly int defaultVolatileFlukeLv1ContactDamage = 8;
        private static readonly int defaultVolatileFlukeLv2ContactDamage = 13;
        private static readonly float defaultVolatileFlukeLv1CloudDamage = 19f;
        private static readonly float defaultVolatileFlukeLv2CloudDamage = 35f;
        private static readonly float defaultVolatileFlukeCloudDuration = 3.6f;
        private static readonly float defaultVolatileFlukeCloudRadius = 5.4f;
        private static readonly int defaultVolatileFlukeShamanStoneLv1ContactDamage = 10;
        private static readonly int defaultVolatileFlukeShamanStoneLv2ContactDamage = 18;
        private static readonly float defaultVolatileFlukeShamanStoneLv1CloudDamage = 26f;
        private static readonly float defaultVolatileFlukeShamanStoneLv2CloudDamage = 46f;
        private static readonly float defaultVolatileFlukeShamanStoneCloudDuration = 3.6f;
        private static readonly float defaultVolatileFlukeShamanStoneCloudRadius = 7.2f;

//	Fragile Charms
        private static readonly bool defaultFragileCharmsBreak = true;
		
	//	Greed
        private static readonly int defaultCharm24NotchCost = 1;
        private static readonly int defaultGreedGeoIncrease = 20;
		private static readonly bool defaultGreedSoulGain = true;
		
	//	Heart
        private static readonly int defaultCharm23NotchCost = 2;
		private static readonly int defaultHeartMasks = 2;
	
	//	Strength
        private static readonly int defaultCharm25NotchCost = 2;
		private static readonly int defaultStrengthNailDamageIncrease = 3;

//	Fury of the Fallen
        private static readonly int defaultCharm6NotchCost = 2;
		private static readonly bool defaultFuryOfTheFallenOvercharmed = true;
		private static readonly float defaultFuryOfTheFallenThreshold = 0.499f;
        private static readonly int defaultFuryOfTheFallenNailDamageIncrease = 6;
		private static readonly float defaultFuryOfTheFallenNailCooldownReduction = 0.1f;
		private static readonly int defaultFuryOfTheFallenVSDamageIncrease = 10;
		private static readonly int defaultFuryOfTheFallenSSDamageIncrease = 10;
		private static readonly int defaultFuryOfTheFallenHWDamageIncrease = 5;
		private static readonly int defaultFuryOfTheFallenASDamageIncrease = 4;
		private static readonly int defaultFuryOfTheFallenDiveDamageIncrease = 5;
		private static readonly int defaultFuryOfTheFallenDDiveDamageIncrease = 5;
		private static readonly int defaultFuryOfTheFallenDDarkDamage1Increase = 5;
		private static readonly int defaultFuryOfTheFallenDDarkDamage2Increase = 0;

//	Gathering Swarm
        private static readonly int defaultCharm1NotchCost = 1;

//	Glowing Womb
        private static readonly int defaultCharm22NotchCost = 2;
		private static readonly bool defaultGlowingWombPiercing = true;
        private static readonly float defaultGlowingWombSpawnRate = 4f;
        private static readonly int defaultGlowingWombSpawnCost = 20;
        private static readonly int defaultGlowingWombSpawnTotal = 2;
        private static readonly int defaultGlowingWombDamage = 15;
        private static readonly float defaultGlowingWombGatheringSwarmSpawnRate = 2.8f;
        private static readonly int defaultGlowingWombGatheringSwarmCostReduction = 6;
        private static readonly int defaultGlowingWombGatheringSwarmSpawnTotal = 3;
		private static readonly int defaultGlowingWombGatheringSwarmDamageReduction = 3;
		private static readonly int defaultGlowingWombFotFDamageIncrease = 8;
		private static readonly int defaultGlowingWombFlukenestDamageIncrease = 5;
		private static readonly int defaultGlowingWombCarefreeMelodyCostReduction = 6;
        private static readonly int defaultGlowingWombDefendersCrestDamage = 1;
        private static readonly float defaultGlowingWombDefendersCrestDuration = 2.8f;
        private static readonly float defaultGlowingWombDefendersCrestRadius = 5.4f;
        private static readonly float defaultGlowingWombDefendersCrestCloudDamage = 19f;
        private static readonly float defaultGlowingWombDefendersCrestFotFDuration = 1.1f;

//	Grimmchild
		private static readonly int defaultCharmGrimmchildNotchCost = 2;
        private static readonly float defaultGrimmchildAttackTimer = 3.6f;
		private static readonly float defaultGrimmchildRange = 1.0f;
		private static readonly float defaultGrimmchildProjectileSpeed = 30f;	//	vanilla: 30f
		private static readonly float defaultGrimmchildProjectileSpread = 0f;	//	vanilla: 15f
		private static readonly bool defaultGrimmchildBypassTerrain = true;
        private static readonly int defaultGrimmchildDamage2 = 11;
        private static readonly int defaultGrimmchildDamage3 = 18;
        private static readonly int defaultGrimmchildDamage4 = 25;
		private static readonly float defaultGrimmchildGatheringSwarmAttackTimer = 2.4f;
		private static readonly int defaultGrimmchildFlukenestDamageIncrease = 7;
		private static readonly int defaultDreamNailBaseDamage = 8;
		private static readonly int defaultDreamNailDamageScaleRate = 100;		

//	Grubberfly's Elegy
        private static readonly int defaultCharm35NotchCost = 3;
        private static readonly float defaultGrubberflysElegyDamageScale = 1.0f;
		private static readonly bool defaultGrubberflysElegyRangeAffected = true;
		private static readonly bool defaultGrubberflysElegyFotFActivated = true;
        private static readonly bool defaultGrubberflysElegyJoniBeamDamageBool = true;
		private static readonly int defaultGrubberflysElegySoulCatcherSoulGain = 3;
		private static readonly int defaultGrubberflysElegySoulEaterSoulGain = 8;

//	Grubsong
        private static readonly int defaultCharm3NotchCost = 1;
        private static readonly int defaultGrubsongSoulGain = 11;
		private static readonly int defaultGrubsongShapeOfUnnSoulGainIncrease = 6;
        private static readonly int defaultGrubsongGrubberflysElegySoulGainIncrease = 16;

//	Heavy Blow
        private static readonly int defaultCharm15NotchCost = 2;
        private static readonly int defaultHeavyBlowNailDamageIncrease = 5;
		private static readonly int defaultHeavyBlowNailArtDamageIncrease = 3;
		private static readonly float defaultHeavyBlowNailCooldown = 0.57f;
        private static readonly int defaultHeavyBlowStagger = 3;
        private static readonly int defaultHeavyBlowStaggerCombo = 2;
		private static readonly int defaultHeavyBlowEnviroHits = 3;
        private static readonly float defaultHeavyBlowGreatSlashKnockback = 4.0f;
        private static readonly float defaultHeavyBlowDashSlashKnockback = 4.0f;
		private static readonly float defaultHeavyBlowSteadyBodyNailCooldownReduction = 0.1f;

//	Hiveblood
        private static readonly int defaultCharm29NotchCost = 3;
		private static readonly int defaultHivebloodRegenLimit = 1;
        private static readonly float defaultHivebloodCooldown = 8.0f;
		private static readonly int defaultHivebloodFragileHeartLimitIncrease = 2;
        private static readonly float defaultHivebloodCooldownDecceleration = 4.8f;
        private static readonly float defaultHivebloodJonisCooldown = 9.6f;
		private static readonly int defaultHivebloodLifebloodHeartLimitIncrease = 2;
        private static readonly float defaultHivebloodJonisCooldownDecceleration = 6.4f;

//	Joni's Blessing
        private static readonly int defaultCharm27NotchCost = 2;
		private static readonly int defaultJonisBlessingLifeblood = 3;

//	Kingsoul/Void Heart
        private static readonly int defaultCharmKingsoulNotchCost = 2;
        private static readonly int defaultKingsoulSoulGain = 3;
        private static readonly float defaultKingsoulRegenTickRate = 3.0f;
        private static readonly int defaultCharmVoidHeartNotchCost = 0;
		private static readonly bool defaultVoidHeartSoulRegen = true;

//	Lifeblood Core
        private static readonly int defaultCharm9NotchCost = 3;
        private static readonly int defaultLifebloodCoreLifeblood = 3;
		private static readonly float defaultLifebloodCoreCooldown = 9.6f;
		private static readonly int defaultLifebloodCoreCost = 33;
		//	Maximum Overheal = highest lifeblood since bench
		private static readonly int defaultLifebloodCoreSteadyBodyCost = 24;
		private static readonly int defaultLifebloodCoreSpellTwisterCost = 24;

//	Lifeblood Heart
        private static readonly int defaultCharm8NotchCost = 2;
        private static readonly int defaultLifebloodHeartLifeblood = 2;

//	Longnail
        private static readonly int defaultCharm18NotchCost = 2;
        private static readonly int defaultLongnailRangeIncrease = 25;

//	Mark of Pride
        private static readonly int defaultCharm13NotchCost = 3;
		private static readonly int defaultMarkOfPrideNailDamageIncrease = 2;
		private static readonly float defaultMarkOfPrideNailCooldownReduction = 0.08f;
        private static readonly int defaultMarkOfPrideRangeIncrease = 15;

//	Nailmaster's Glory
        private static readonly int defaultCharm26NotchCost = 1;
		private static readonly float defaultNailmastersGloryDamageIncrease = 1.1f;
        private static readonly float defaultNailmastersGloryChargeTime = 0.75f;
		private static readonly bool defaultNailmastersGloryMoPPiercing = true;

//	Quick Focus
        private static readonly int defaultCharm7NotchCost = 3;
        private static readonly float defaultQuickFocusFocusTime = 0.594f;
		private static readonly float defaultQuickFocusCDashTimeMult = 0.6f;

//	Quick Slash
        private static readonly int defaultCharm32NotchCost = 2;
        private static readonly float defaultQuickSlashNailCooldownReduction = 0.12f;

//	Shaman Stone
        private static readonly int defaultCharm19NotchCost = 3;
        private static readonly int defaultShamanStoneVSDamage = 20;
        private static readonly int defaultShamanStoneSSDamage = 35;
        private static readonly int defaultShamanStoneHWDamage = 14;
        private static readonly int defaultShamanStoneASDamage = 18;
        private static readonly int defaultShamanStoneDiveDamage = 15;
        private static readonly int defaultShamanStoneDDiveDamage = 20;
        private static readonly int defaultShamanStoneDDarkDamageL = 30;
        private static readonly int defaultShamanStoneDDarkDamageR = 30;
        private static readonly int defaultShamanStoneDDarkDamageMega = 15;
        private static readonly float defaultShamanStoneVSSizeScaleX = 1.3f;
        private static readonly float defaultShamanStoneVSSizeScaleY = 1.6f;
        private static readonly float defaultShamanStoneSSSizeScaleX = 1.3f;
        private static readonly float defaultShamanStoneSSSizeScaleY = 1.6f;

//	Shape of Unn
        private static readonly int defaultCharm28NotchCost = 2;
        private static readonly float defaultShapeOfUnnSpeed = 6f;
        private static readonly float defaultShapeOfUnnQuickFocusSpeed = 12f;
		private static readonly bool defaultShapeOfUnnAddsVessel = true;

//	Sharp Shadow
        private static readonly int defaultCharm16NotchCost = 3;
        private static readonly float defaultSharpShadowDamage = 1.8f;
        private static readonly float defaultSharpShadowDashSpeed = 28f;
		private static readonly bool defaultSharpShadowHurtBoxSize = true;
        private static readonly float defaultSharpShadowDashmasterDamage = 2.4f;
        private static readonly float defaultSharpShadowSprintmasterDashSpeed = 36f;
		private static readonly int defaultSharpShadowSoulCatcherSoulGain = 8;
		private static readonly int defaultSharpShadowSoulEaterSoulGain = 14;
		private static readonly float defaultSharpShadowStalwartShellIFrames = 0.25f;
		private static readonly float defaultSharpShadowVolume = 0.6f;

//	Soul Catcher
        private static readonly int defaultCharm20NotchCost = 1;
        private static readonly int defaultSoulCatcherSoulGain = 3;
        private static readonly int defaultSoulCatcherReserveSoulGain = 2;

//	Soul Eater
        private static readonly int defaultCharm21NotchCost = 3;
        private static readonly int defaultSoulEaterSoulGain = 8;
        private static readonly int defaultSoulEaterReserveSoulGain = 6;

//	Spell Twister
        private static readonly int defaultCharm33NotchCost = 2;
        private static readonly int defaultSpellTwisterSpellCost = 24;
		private static readonly int defaultSpellTwisterShapeOfUnnSpellCost = 22;
		private static readonly float defaultSpellTwisterDreamNailRange = 1.25f;

//	Spore Shroom
        private static readonly int defaultCharm17NotchCost = 1;
        private static readonly bool defaultSporeShroomDamageResetsCooldown = true;
        private static readonly float defaultSporeShroomCooldown = 0.25f;
		private static readonly float defaultSporeShroomRadiusBase = 1.0f;
		private static readonly float defaultSporeShroomRadiusMax = 1.4f;
        private static readonly float defaultSporeShroomCloudDurationBase = 4.1f;	//	no reserve
        private static readonly float defaultSporeShroomCloudDurationMax = 7.7f;	//	max reserve + unn
        private static readonly int defaultSporeShroomDamageBase = 15;	//	no reserve
        private static readonly int defaultSporeShroomDamageMax = 60;	//	max reserve + unn
		private static readonly float defaultSporeShroomDeepFocusRadiusMult = 1.35f;
        private static readonly float defaultSporeShroomDeepFocusDurationMult = 1.2f;
        private static readonly int defaultSporeShroomDefendersCrestDamageBase = 20;
        private static readonly int defaultSporeShroomDefendersCrestDamageMax = 80;

//	Sprintmaster
        private static readonly int defaultCharm37NotchCost = 1;
        private static readonly float defaultSprintmasterSpeed = 10.8f;
        private static readonly float defaultSprintmasterDashmasterSpeed = 12.0f;

//	Stalwart Shell
        private static readonly int defaultCharm4NotchCost = 2;
        private static readonly float defaultStalwartShellIFrames = 1.75f;
		private static readonly float defaultStalwartShellParryIFrames = 0.4f;
		private static readonly int defaultStalwartShellLifeblood = 1;
		private static readonly float defaultStalwartShellCooldown = 18.0f;
		private static readonly float defaultStalwartShellBaldurBlockIFrames = 3.25f;
		private static readonly float defaultStalwartShellDefendersCrestCooldownReduction = 3f;
		private static readonly float defaultStalwartShellLifebloodHeartCooldownReduction = 7f;
		private static readonly bool defaultStalwartShellJonisDamageReduction = true;
        //private static readonly float defaultStalwartShellRecoil = 0.2f;

//	Steady Body
        private static readonly int defaultCharm14NotchCost = 2;
		private static readonly int defaultSteadyBodyFocusCost = 24;
		private static readonly float defaultSteadyBodyKnockback = 0.08f;
		private static readonly bool defaultSteadyBodyNegateHardFall = true;
		private static readonly bool defaultSteadyBodyNegateNailRecoil = false;
		private static readonly int defaultSteadyBodyDefendersCrestImpactDamage = 25;
		private static readonly int defaultSteadyBodyShapeOfUnnFocusCost = 22;

//	Thorns of Agony
        private static readonly int defaultCharm12NotchCost = 1;
        private static readonly float defaultThornsOfAgonyBaseDamage = 7.57f;
		private static readonly float defaultThornsOfAgonyDamageIncrease = 1.93f;
		private static readonly float defaultThornsOfAgonyRadius = 0.8f;
		private static readonly float defaultThornsOfAgonyBaldurBlockDamage = 10.63f;
		private static readonly float defaultThornsOfAgonyBaldurBlockRadius = 0.3f;
		private static readonly float defaultThornsOfAgonyDefendersCrestDamage = 1.09f;
		private static readonly float defaultThornsOfAgonyFotFDamage = 2.71f;
		private static readonly float defaultThornsOfAgonyStalwartShellRadius = 0.3f;
		private static readonly float defaultThornsOfAgonySteadyBodyDamage = 0.67f;
		private static readonly float defaultThornsOfAgonySteadyBodyRadius = 0.15f;
		private static readonly bool defaultThornsOfAgonyFlukenestSpray = true;

//	Wayward Compass
        private static readonly int defaultCharm2NotchCost = 0;

//	Weaversong
        private static readonly int defaultCharm39NotchCost = 2;
        private static readonly int defaultWeaversongCount = 3;
        private static readonly int defaultWeaversongDamage = 2;
        private static readonly int defaultWeaversongSoulGain = 2;
        private static readonly float defaultWeaversongSpeedMin = 6f;
        private static readonly float defaultWeaversongSpeedMax = 10f;
		private static readonly int defaultWeaversongGatheringSwarmCount = 5;
		private static readonly int defaultWeaversongGatheringSwarmDamageReduction = 1;
		private static readonly int defaultWeaversongFlukenestDamageIncrease = 3;
		private static readonly int defaultWeaversongFlukenestSoulReduction = 1;
        private static readonly int defaultWeaversongGrubsongSoulGain = 1;
		private static readonly int defaultWeaversongCarefreeMelodyDamageIncrease = 1;
        private static readonly int defaultWeaversongCarefreeMelodySoulGain = 1;
        private static readonly int defaultWeaversongSprintmasterSpeedIncrease = 50;

		#endregion
		
////////////////////////////////////////////////////////////////
//	LOCAL SETTINGS
		#region ModSettings
        [BoolElement("Mod Settings", "Reorder Charms", "Position charms in inventory near their synergies")]
		public bool playersetReorderCharms = defaultReorderCharms;
		
		[ButtonElement("Mod Settings", "Reset Defaults", "")]
        public void ResetModSettings()
        {
			playersetReorderCharms = defaultReorderCharms;
        }
		#endregion

		#region AbilityMenu
////////////////////////////////////////////////////////////////
//	ABILITY MENUS

//	Movement Settings
        [InputFloatElement("Movement Settings", "Walk Speed", 0f, 36f)]
        public float regularWalkSpeed = defaultWalkSpeed;

        [InputFloatElement("Movement Settings", "Run Speed", 0f, 36f)]
        public float regularSpeed = defaultSpeed;

        [InputFloatElement("Movement Settings", "Dash Cooldown", 0f, 5f)]
        public float regularDashCooldown = defaultDashCooldown;

        [InputFloatElement("Movement Settings", "Dash Speed", 0f, 100f)]
        public float regularDashSpeed = defaultDashSpeed;

        [InputFloatElement("Movement Settings", "Shade Cloak Cooldown", 0f, 5f)]
        public float regularShadeCloakCooldown = defaultShadeCloakCooldown;

        [InputFloatElement("Movement Settings", "Crystal Dash Charge Time", 0.1f, 5f)]
		public float regularCDashChargeTime = defaultCDashChargeTime;

        [InputIntElement("Movement Settings", "Crystal Dash Damage", 0, 100)]
        public int regularCDashDamage = defaultCDashDamage;

        [ButtonElement("Movement Settings", "Reset Defaults", "")]
        public void ResetMovementSettings()
        {
			regularWalkSpeed = defaultWalkSpeed;
			regularSpeed = defaultSpeed;
			regularDashCooldown = defaultDashCooldown;
			regularDashSpeed = defaultDashSpeed;
			regularShadeCloakCooldown = defaultShadeCloakCooldown;
			regularCDashChargeTime = defaultCDashChargeTime;
			regularCDashDamage = defaultCDashDamage;
        }

//	Damage & Focus Settings
        [InputFloatElement("Damage & Focus Settings", "Invulnerable Time", 0.01f, 2f)]
        public float regularIFrames = defaultIFrames;
		
        [InputFloatElement("Damage & Focus Settings", "Damage Recoil", 0.01f, 1f)]
        public float regularDamageKnockback = defaultDamageKnockback;
		
        [BoolElement("Damage & Focus Settings", "Negate Hazards", "Invulnerability negates damage from hazards")]
		public bool regularIFramesNegateHazard = defaultIFramesNegateHazard;
		
        [InputFloatElement("Damage & Focus Settings", "Focus Time", 0.033f, 2f)]
        public float regularFocusTime = defaultFocusTime;

        [InputIntElement("Damage & Focus Settings", "Focus Healing", 0, 20)]
        public int regularFocusHealing = defaultFocusHealing;

        [InputIntElement("Damage & Focus Settings", "Focus Cost", 0, 99)]
		public int regularFocusCost = defaultFocusCost;

        [InputIntElement("Damage & Focus Settings", "Maximum Overheal", 0, 20)]
		public int regularMaximumOverheal = defaultMaximumOverheal;

        [ButtonElement("Damage & Focus Settings", "Reset Defaults", "")]
        public void ResetDamageFocusSettings()
        {
			regularIFrames = defaultIFrames;
			regularDamageKnockback = defaultDamageKnockback;
			regularIFramesNegateHazard = defaultIFramesNegateHazard;
			regularFocusTime = defaultFocusTime;
			regularFocusHealing = defaultFocusHealing;
			regularFocusCost = defaultFocusCost;
			regularMaximumOverheal = defaultMaximumOverheal;
        }

//	Nail Settings
        [InputIntElement("Nail Settings", "Base Nail Damage", 0, 20)]
		public int regularNailDamageBase = defaultNailDamageBase;

        [InputIntElement("Nail Settings", "Nail Damage Upgrades", 0, 20)]
		public int regularNailDamageUpgrade = defaultNailDamageUpgrade;

        [InputFloatElement("Nail Settings", "Nail Cooldown", 0f, 2f)]
        public float regularNailCooldown = defaultNailCooldown;

        [InputFloatElement("Nail Settings", "Nail Knockback", 0f, 5f)]
        public float regularNailKnockback = defaultNailKnockback;

        [InputFloatElement("Nail Settings", "Parry IFrames", 0f, 5f)]
		public float regularParryIFrames = defaultParryIFrames;
		
        [BoolElement("Nail Settings", "Nail Range Affects Wall Slash", "")]
        public bool regularNailRangeAffectsWallSlash = defaultNailRangeAffectsWallSlash;

        [InputIntElement("Nail Settings", "Soul Gain", 0, 231)]
        public int regularSoulGain = defaultSoulGain;

        [InputIntElement("Nail Settings", "Reserve Soul Gain", 0, 231)]
        public int regularReserveSoulGain = defaultReserveSoulGain;

        [ButtonElement("Nail Settings", "Reset Defaults", "")]
        public void ResetNailSettings()
        {
			regularNailDamageBase = defaultNailDamageBase;
			regularNailDamageUpgrade = defaultNailDamageUpgrade;
			regularNailCooldown = defaultNailCooldown;
			regularNailKnockback = defaultNailKnockback;
			regularParryIFrames = defaultParryIFrames;
			regularNailRangeAffectsWallSlash = defaultNailRangeAffectsWallSlash;
			regularSoulGain = defaultSoulGain;
			regularReserveSoulGain = defaultReserveSoulGain;
        }

//	Nail Art Settings
        [InputFloatElement("Nail Art Settings", "Nail Art Charge Time", 0f, 5f)]
        public float regularNailArtChargeTime = defaultNailArtChargeTime;
		
        [InputFloatElement("Nail Art Settings", "Great Slash Damage", 0f, 5f)]
		public float regularGreatSlashDamage = defaultGreatSlashDamage;
		
        [InputFloatElement("Nail Art Settings", "Great Slash Knockback", 0f, 10f)]
        public float regularGreatSlashKnockback = defaultGreatSlashKnockback;
		
        [InputFloatElement("Nail Art Settings", "Dash Slash Damage", 0f, 5f)]
		public float regularDashSlashDamage = defaultDashSlashDamage;
		
        [InputFloatElement("Nail Art Settings", "Dash Slash Knockback", 0f, 10f)]
		public float regularDashSlashKnockback = defaultDashSlashKnockback;
		
        [InputFloatElement("Nail Art Settings", "Cyclone Slash Damage", 0f, 5f)]
		public float regularCycloneSlashDamage = defaultCycloneSlashDamage;
		
        [InputFloatElement("Nail Art Settings", "Cyclone Slash Knockback", 0f, 10f)]
        public float regularCycloneSlashKnockback = defaultCycloneSlashKnockback;


        [ButtonElement("Nail Art Settings", "Reset Defaults", "")]
        public void ResetNailArtSettings()
        {
			regularNailArtChargeTime = defaultNailArtChargeTime;
			regularGreatSlashDamage = defaultGreatSlashDamage;
			regularGreatSlashKnockback = defaultGreatSlashKnockback;
			regularDashSlashDamage = defaultDashSlashDamage;
			regularDashSlashKnockback = defaultDashSlashKnockback;
			regularCycloneSlashDamage = defaultCycloneSlashDamage;
			regularCycloneSlashKnockback = defaultCycloneSlashKnockback;
        }

//	Spell Settings
        [InputIntElement("Spell Settings", "Spell Cost", 0, 231)]
        public int regularSpellCost = defaultSpellCost;

        [InputIntElement("Spell Settings", "Vengeful Spirit Damage", 0, 100)]
        public int regularVSDamage = defaultVSDamage;

        [InputIntElement("Spell Settings", "Shade Soul Damage", 0, 100)]
        public int regularSSDamage = defaultSSDamage;

        [InputIntElement("Spell Settings", "Howling Wraiths Damage", 0, 100)]
        public int regularHWDamage = defaultHWDamage;

        [InputIntElement("Spell Settings", "Abyss Shriek", 0, 100)]
        public int regularASDamage = defaultASDamage;

        [InputFloatElement("Spell Settings", "Wraiths/Shriek Knockback", 0f, 10f)]
		public float regularHWKnockback = defaultHWKnockback;

        [InputIntElement("Spell Settings", "Dive Damage", 0, 100)]
        public int regularDiveDamage = defaultDiveDamage;

        [InputIntElement("Spell Settings", "Desolate Dive Damage", 0, 100)]
        public int regularDDiveDamage = defaultDDiveDamage;

        [InputIntElement("Spell Settings", "Descending Dark Left Damage", 0, 100)]
        public int regularDDarkDamageL = defaultDDarkDamageL;

        [InputIntElement("Spell Settings", "Descending Dark Right Damage", 0, 100)]
        public int regularDDarkDamageR = defaultDDarkDamageR;

        [InputIntElement("Spell Settings", "Descending Dark Final Damage", 0, 100)]
        public int regularDDarkDamageMega = defaultDDarkDamageMega;
		
        //[InputFloatElement("Spell Settings", "Vengeful Spirit X Scale", 0f, 5f)]
        public float regularVSSizeScaleX = defaultVSSizeScaleX;
		
        //[InputFloatElement("Spell Settings", "Vengeful Spirit Y Scale", 0f, 5f)]
        public float regularVSSizeScaleY = defaultVSSizeScaleY;
		
        //[InputFloatElement("Spell Settings", "Shade Soul X Scale", 0f, 5f)]
        public float regularSSSizeScaleX = defaultSSSizeScaleX;
		
        //[InputFloatElement("Spell Settings", "Shade Soul Y Scale", 0f, 5f)]
        public float regularSSSizeScaleY = defaultSSSizeScaleY;

        [ButtonElement("Spell Settings", "Reset Defaults", "")]
        public void ResetSpellSettings()
        {
            regularSpellCost = defaultSpellCost;
			regularVSDamage = defaultVSDamage;
            regularSSDamage = defaultSSDamage;
            regularHWDamage = defaultHWDamage;
            regularASDamage = defaultASDamage;
            regularDiveDamage = defaultDiveDamage;
            regularDDiveDamage = defaultDDiveDamage;
            regularDDarkDamageL = defaultDDarkDamageL;
            regularDDarkDamageR = defaultDDarkDamageR;
            regularDDarkDamageMega = defaultDDarkDamageMega;
            regularVSSizeScaleX = defaultVSSizeScaleX;
            regularVSSizeScaleY = defaultVSSizeScaleY;
            regularSSSizeScaleX = defaultSSSizeScaleX;
            regularSSSizeScaleY = defaultSSSizeScaleY;
        }

//	Dream Nail Settings
        [InputIntElement("Dream Nail Settings", "Soul Gain", 0, 231)]
        public int regularDreamNailSoulGain = defaultDreamNailSoulGain;
		
        [InputFloatElement("Dream Nail Settings", "Range", 0f, 5f)]
		public float regularDreamNailRange = defaultDreamNailRange;
		
        [InputFloatElement("Dream Nail Settings", "Height", 0f, 5f)]
		public float regularDreamNailHeight = defaultDreamNailHeight;
		
        [InputIntElement("Dream Nail Settings", "Essence Chance Low (1/X)", 0, 1000)]
		public int regularEssenceChanceLow = defaultEssenceChanceLow;

        [InputIntElement("Dream Nail Settings", "Essence Chance High (1/X)", 0, 1000)]
        public int regularEssenceChanceHigh = defaultEssenceChanceHigh;

        [ButtonElement("Dream Nail Settings", "Reset Defaults", "")]
        public void ResetDreamNailSettings()
        {
			regularDreamNailSoulGain = defaultDreamNailSoulGain;
			regularDreamNailRange = defaultDreamNailRange;
			regularDreamNailHeight = defaultDreamNailHeight;
			regularEssenceChanceLow = defaultEssenceChanceLow;
			regularEssenceChanceHigh = defaultEssenceChanceHigh;
        }
		
		#endregion

		#region CharmMenus
////////////////////////////////////////////////////////////////
//	CHARM MENUS

//	Baldur Shell
        [SliderIntElement("Baldur Shell", "Notch Cost", 0, 5)]
        public int charm5NotchCost = defaultCharm5NotchCost;

        [InputIntElement("Baldur Shell", "Blocks", 0, 10)]
        public int baldurShellBlocks = defaultBaldurShellBlocks;

        [InputFloatElement("Baldur Shell", "Enemy Knockback", 0f, 5f)]
        public float baldurShellKnockback = defaultBaldurShellKnockback;

        [InputIntElement("Baldur Shell", "Lifeblood Heart Overheal Max Increase", 0, 20)]
		public int baldurShellLifebloodHeartOverhealMaxIncrease = defaultBaldurShellLifebloodHeartOverhealMaxIncrease;

        [BoolElement("Baldur Shell", "Deep Focus Applies Overheal", "")]
		public bool baldurShellDeepFocusAffected = defaultBaldurShellDeepFocusAffected;

        [InputIntElement("Baldur Shell", "Defender's Crest Blocks", 0, 10)]
        public int baldurShellDefendersCrestBlocks = defaultBaldurShellDefendersCrestBlocks;

        [BoolElement("Baldur Shell", "Greed Geo Shell", "")]
		public bool baldurShellGreedShell = defaultBaldurShellGreedShell;

        [InputIntElement("Baldur Shell", "Geo Loss Rate", 0, 2000)]
        public int baldurShellGreedGeoLossRate = defaultBaldurShellGreedGeoLossRate;

        [InputIntElement("Baldur Shell", "Geo Dropped", 0, 2000)]
        public int baldurShellGreedGeoDrop = defaultBaldurShellGreedGeoDrop;

        [InputFloatElement("Baldur Shell", "Defender's Crest Geo Loss Rate", 0f, 1f)]
        public float baldurShellGreedDCGeoLossRate = defaultBaldurShellGreedDCGeoLossRate;

        [ButtonElement("Baldur Shell", "Reset Defaults", "")]
        public void ResetBaldurShell()
        {
			charm5NotchCost = defaultCharm5NotchCost;
			baldurShellBlocks = defaultBaldurShellBlocks;
			baldurShellKnockback = defaultBaldurShellKnockback;
			baldurShellLifebloodHeartOverhealMaxIncrease = defaultBaldurShellLifebloodHeartOverhealMaxIncrease;
			baldurShellDeepFocusAffected = defaultBaldurShellDeepFocusAffected;
			baldurShellDefendersCrestBlocks = defaultBaldurShellDefendersCrestBlocks;
			baldurShellGreedShell = defaultBaldurShellGreedShell;
			baldurShellGreedGeoLossRate = defaultBaldurShellGreedGeoLossRate;
			baldurShellGreedGeoDrop = defaultBaldurShellGreedGeoDrop;
            baldurShellGreedDCGeoLossRate = defaultBaldurShellGreedDCGeoLossRate;
        }

//	Carefree Melody
        [SliderIntElement("Carefree Melody", "Notch Cost", 0, 5)]
        public int charmCarefreeMelodyNotchCost = defaultCharmCarefreeMelodyNotchCost;

        [InputIntElement("Carefree Melody", "Max Block Chance (%)", 0, 100)]
        public int carefreeMelodyChance = defaultCarefreeMelodyChance;

        [BoolElement("Carefree Melody", "Fragile Heart Heal on Block", "")]
		public bool carefreeMelodyHeartHealing = defaultCarefreeMelodyHeartHealing;

        //[InputIntElement("Carefree Melody", "Chance Accelleration", 0, 99)]
        //public int carefreeMelodyChanceAccel = defaultCarefreeMelodyChanceAccel;

        //[InputIntElement("Carefree Melody", "Grubsong Bonus Chance", 0, 99)]
		//public int carefreeMelodyGrubSongBonus = defaultCarefreeMelodyGrubSongBonus;

        [ButtonElement("Carefree Melody", "Reset Defaults", "")]
        public void ResetCarefreeMelody()
        {
			charmCarefreeMelodyNotchCost = defaultCharmCarefreeMelodyNotchCost;
			carefreeMelodyHeartHealing = defaultCarefreeMelodyHeartHealing;
			carefreeMelodyChance = defaultCarefreeMelodyChance;
			//carefreeMelodyChanceAccel = defaultCarefreeMelodyChanceAccel;
			//carefreeMelodyGrubSongBonus = defaultCarefreeMelodyGrubSongBonus;

            charm40NotchCost = PlayerDataAccess.grimmChildLevel == 5 ? charmCarefreeMelodyNotchCost : charmGrimmchildNotchCost;
        }

//	Dashmaster
        [SliderIntElement("Dashmaster", "Notch Cost", 0, 5)]
        public int charm31NotchCost = defaultCharm31NotchCost;

        [BoolElement("Dashmaster", "Allow Downward Dash", "")]
        public bool dashmasterDownwardDash = defaultDashmasterDownwardDash;

        [InputFloatElement("Dashmaster", "Dash Cooldown", 0f, 10f)]
        public float dashmasterDashCooldown = defaultDashmasterDashCooldown;

        [InputFloatElement("Dashmaster", "Shade Cloak Cooldown", 0f, 10f)]
		public float dashmasterShadeCloakCooldown = defaultDashmasterShadeCloakCooldown;

        [InputIntElement("Dashmaster", "Crystal Dash Damage", 0, 100)]
		public int dashmasterCDashDamage = defaultDashmasterCDashDamage;

        [ButtonElement("Dashmaster", "Reset Defaults", "")]
        public void ResetDashmaster()
        {
			charm31NotchCost = defaultCharm31NotchCost;
			dashmasterDownwardDash = defaultDashmasterDownwardDash;
			dashmasterDashCooldown = defaultDashmasterDashCooldown;
			dashmasterShadeCloakCooldown = defaultDashmasterShadeCloakCooldown;
			dashmasterCDashDamage = defaultDashmasterCDashDamage;
        }

//	Deep Focus
        [SliderIntElement("Deep Focus", "Notch Cost", 0, 5)]
        public int charm34NotchCost = defaultCharm34NotchCost;

        [InputIntElement("Deep Focus", "Deep Focus Healing", 0, 20)]
        public int deepFocusHealing = defaultDeepFocusHealing;

        [InputFloatElement("Deep Focus", "Deep Focus Healing Time Multiplier", 1f, 3f)]
        public float deepFocusHealingTimeMult = defaultDeepFocusHealingTimeMult;

        [InputFloatElement("Deep Focus", "Deep Focus Crystal Dash Damage Multiplier", 1f, 3f)]
		public float deepFocusCDashDamageMult = defaultDeepFocusCDashDamageMult;

        [InputFloatElement("Deep Focus", "Deep Focus Crystal Dash Time Multiplier", 1f, 5f)]
		public float deepFocusCDashTimeMult = defaultDeepFocusCDashTimeMult;

        [ButtonElement("Deep Focus", "Reset Defaults", "")]
        public void ResetDeepFocus()
        {
            charm34NotchCost = defaultCharm34NotchCost;
            deepFocusHealing = defaultDeepFocusHealing;
            deepFocusHealingTimeMult = defaultDeepFocusHealingTimeMult;
			deepFocusCDashDamageMult = defaultDeepFocusCDashDamageMult;
			deepFocusCDashTimeMult = defaultDeepFocusCDashTimeMult;
        }

//	Defender's Crest
        [SliderIntElement("Defender's Crest", "Notch Cost", 0, 5)]
        public int charm10NotchCost = defaultCharm10NotchCost;

        [InputFloatElement("Defender's Crest", "Leg Eater Discount (%)", 0f, 1f)]
        public float defendersCrestDiscount = defaultDefendersCrestDiscount;

        [InputFloatElement("Defender's Crest", "Cloud Spawn Frequency", 0.01f, 5f)]
        public float defendersCrestFrequency = defaultDefendersCrestFrequency;

        [InputFloatElement("Defender's Crest", "Cloud Duration", 0f, 5f)]
		public float defendersCrestDuration = defaultDefendersCrestDuration;
		
        [InputFloatElement("Defender's Crest", "Cloud Radius", 0f, 2f)]
		public float defendersCrestRadius = defaultDefendersCrestRadius;

        [InputFloatElement("Defender's Crest", "Cloud Damage", 0f, 20f)]
        public float defendersCrestDamage = defaultDefendersCrestDamage;
		
        [InputFloatElement("Defender's Crest", "Steady Body Cloud Radius", 0f, 5f)]
		public float defendersCrestSteadyBodyRadius = defaultDefendersCrestSteadyBodyRadius;
		
        [InputFloatElement("Defender's Crest", "Steady Body Cloud Duration Increase", 0f, 5f)]
		public float defendersCrestSteadyBodyDuration = defaultDefendersCrestSteadyBodyDuration;

        [InputFloatElement("Defender's Crest", "FotF Cloud Damage", 0f, 20f)]
		public float defendersCrestFotFDamage = defaultDefendersCrestFotFDamage;

        [ButtonElement("Defender's Crest", "Reset Defaults", "")]
        public void ResetDefendersCrest()
        {
			charm10NotchCost = defaultCharm10NotchCost;
			defendersCrestDiscount = defaultDefendersCrestDiscount;
			defendersCrestFrequency = defaultDefendersCrestFrequency;
			defendersCrestDuration = defaultDefendersCrestDuration;
			defendersCrestRadius = defaultDefendersCrestRadius;
			defendersCrestDamage = defaultDefendersCrestDamage;
			defendersCrestSteadyBodyRadius = defaultDefendersCrestSteadyBodyRadius;
			defendersCrestSteadyBodyDuration = defaultDefendersCrestSteadyBodyDuration;
			defendersCrestFotFDamage = defaultDefendersCrestFotFDamage;
        }

//	Dream Wielder
        [SliderIntElement("Dream Wielder", "Notch Cost", 0, 5)]
        public int charm30NotchCost = defaultCharm30NotchCost;

        [InputIntElement("Dream Wielder", "Dream Nail Soul Gain", 0, 231)]
        public int dreamWielderSoulGain = defaultDreamWielderSoulGain;

        [InputIntElement("Dream Wielder", "Essence Chance Low (1/X)", 1, 1000)]
        public int dreamWielderEssenceChanceLow = defaultDreamWielderEssenceChanceLow;

        [InputIntElement("Dream Wielder", "Essence Chance High (1/X)", 1, 1000)]
        public int dreamWielderEssenceChanceHigh = defaultDreamWielderEssenceChanceHigh;

        [ButtonElement("Dream Wielder", "Reset Defaults", "")]
        public void ResetDreamWielder()
        {
			charm30NotchCost = defaultCharm30NotchCost;
			dreamWielderSoulGain = defaultDreamWielderSoulGain;
			dreamWielderEssenceChanceLow = defaultDreamWielderEssenceChanceLow;
			dreamWielderEssenceChanceHigh = defaultDreamWielderEssenceChanceHigh;
        }

//	Dreamshield
        [SliderIntElement("Dreamshield", "Notch Cost", 0, 5)]
        public int charm38NotchCost = defaultCharm38NotchCost;

        // [InputIntElement("Dreamshield", "Dream Nail Overheal", 0, 5)]
		public int dreamshieldOverheal = defaultDreamshieldOverheal;

        [BoolElement("Dreamshield", "Blocks Attacks from Behind", "")]
        public bool dreamshieldBlocksBehind = defaultDreamshieldBlocksBehind;

        [InputIntElement("Dreamshield", "Contact Damage", 0, 100)]
        public int dreamshieldBaseDamage = defaultDreamshieldBaseDamage;
		
        [InputIntElement("Dreamshield", "+1 Damage per X Essense", 0, 1000)]
		public int dreamshieldDamageScaleRate = defaultDreamshieldDamageScaleRate;

        [InputFloatElement("Dreamshield", "Contact Knockback", 0f, 5f)]
		public float dreamshieldKnockback = defaultDreamshieldKnockback;

        [InputFloatElement("Dreamshield", "Shield Reformation Time", 0f, 60f)]
        public float dreamshieldReformationTime = defaultDreamshieldReformationTime;

        //[InputFloatElement("Dreamshield", "Size Scale", 0f, 3f)]
        public float dreamshieldSizeScale = defaultDreamshieldSizeScale;

        //[InputFloatElement("Dreamshield", "Overhead Offset", 0f, 90f)]
		public float dreamshieldOverheadOffset = defaultDreamshieldOverheadOffset;

        //[InputFloatElement("Dreamshield", "Tween Speed", 0f, 100f)]
		public float dreamshieldTweenSpeed = defaultDreamshieldTweenSpeed;

        [InputFloatElement("Dreamshield", "Defender's Crest Shield Reformation Time Reduction", 0f, 60f)]
        public float dreamshieldDefendersCrestReformTimeReduction = defaultDreamshieldDefendersCrestReformTimeReduction;

        [InputFloatElement("Dreamshield", "Dream Wielder Shield Reformation Time Reduction", 0f, 60f)]
        public float dreamshieldDreamWielderReformTimeReduction = defaultDreamshieldDreamWielderReformTimeReduction;

        // [InputIntElement("Dreamshield", "Lifeblood Heart Overheal Max Increase", 0, 5)]
        public int dreamshieldLifebloodHeartOverhealMaxIncrease = defaultDreamshieldLifebloodHeartOverhealMaxIncrease;

        [ButtonElement("Dreamshield", "Reset Defaults", "")]
        public void ResetDreamshield()
        {
			charm38NotchCost = defaultCharm38NotchCost;
			dreamshieldOverheal = defaultDreamshieldOverheal;
            dreamshieldBlocksBehind = defaultDreamshieldBlocksBehind;
			dreamshieldBaseDamage = defaultDreamshieldBaseDamage;
			dreamshieldDamageScaleRate = defaultDreamshieldDamageScaleRate;
			dreamshieldKnockback = defaultDreamshieldKnockback;
			dreamshieldReformationTime = defaultDreamshieldReformationTime;
			dreamshieldSizeScale = defaultDreamshieldSizeScale;
			dreamshieldOverheadOffset = defaultDreamshieldOverheadOffset;
			dreamshieldTweenSpeed = defaultDreamshieldTweenSpeed;
            dreamshieldDefendersCrestReformTimeReduction = defaultDreamshieldDefendersCrestReformTimeReduction;
			dreamshieldDreamWielderReformTimeReduction = defaultDreamshieldDreamWielderReformTimeReduction;
            dreamshieldLifebloodHeartOverhealMaxIncrease = defaultDreamshieldLifebloodHeartOverhealMaxIncrease;
        }

//	Flukenest
        [SliderIntElement("Flukenest", "Notch Cost", 0, 5)]
        public int charm11NotchCost = defaultCharm11NotchCost;

        [InputFloatElement("Flukenest", "Fluke Min Lifetime", 0.1f, 20f)]
		public float flukeLifetimeMin = defaultFlukeLifetimeMin;

        [InputFloatElement("Flukenest", "Fluke Max Lifetime", 0.1f, 20f)]
		public float flukeLifetimeMax = defaultFlukeLifetimeMax;

        [InputIntElement("Flukenest", "Fluke Damage", 0, 10)]
        public int flukeDamage = defaultFlukeDamage;

        [InputIntElement("Flukenest", "FotF Damage Increase", 0, 10)]
		public int flukeFotFDamageIncrease = defaultFlukeFotFDamageIncrease;

        [InputIntElement("Flukenest", "Vengeful Spirit Flukes", 0, 36)]
        public int flukenestVSFlukes = defaultFlukenestVSFlukes;

        [InputIntElement("Flukenest", "Shade Soul Flukes", 0, 64)]
        public int flukenestSSFlukes = defaultFlukenestSSFlukes;

        //[InputFloatElement("Flukenest", "Fluke Min Size", 0f, 3f)]
        public float flukeSizeMin = defaultFlukeSizeMin;

        //[InputFloatElement("Flukenest", "Fluke Max Size", 0f, 3f)]
        public float flukeSizeMax = defaultFlukeSizeMax;

        [InputIntElement("Flukenest", "Shaman Stone Fluke Damage", 0, 10)]
        public int flukeShamanStoneDamage = defaultFlukeShamanStoneDamage;

        //[InputFloatElement("Flukenest", "Shaman Stone Fluke Min Size", 0f, 3f)]
        public float flukeShamanStoneSizeMin = defaultFlukeShamanStoneSizeMin;

        //[InputFloatElement("Flukenest", "Shaman Stone Fluke Max Size", 0f, 3f)]
        public float flukeShamanStoneSizeMax = defaultFlukeShamanStoneSizeMax;

        [InputIntElement("Flukenest", "Volatile Fluke Lv1 Contact Damage", 0, 100)]
        public int volatileFlukeLv1ContactDamage = defaultVolatileFlukeLv1ContactDamage;

        [InputIntElement("Flukenest", "Volatile Fluke Lv2 Contact Damage", 0, 100)]
        public int volatileFlukeLv2ContactDamage = defaultVolatileFlukeLv2ContactDamage;

        [InputFloatElement("Flukenest", "Volatile Fluke Lv1 Cloud Damage", 0f, 100f)]
        public float volatileFlukeLv1CloudDamage = defaultVolatileFlukeLv1CloudDamage;

        [InputFloatElement("Flukenest", "Volatile Fluke Lv2 Cloud Damage", 0f, 100f)]
        public float volatileFlukeLv2CloudDamage = defaultVolatileFlukeLv2CloudDamage;

        [InputFloatElement("Flukenest", "Volatile Fluke Cloud Duration", 0f, 6f)]
        public float volatileFlukeCloudDuration = defaultVolatileFlukeCloudDuration;
		
        [InputFloatElement("Flukenest", "Volatile Fluke Cloud Radius", 0.01f, 10f)]
        public float volatileFlukeCloudRadius = defaultVolatileFlukeCloudRadius;

        [InputIntElement("Flukenest", "VF Shaman Stone Lv1 Damage", 0, 100)]
        public int volatileFlukeShamanStoneLv1ContactDamage = defaultVolatileFlukeShamanStoneLv1ContactDamage;

        [InputIntElement("Flukenest", "VF Shaman Stone Lv2 Damage", 0, 100)]
        public int volatileFlukeShamanStoneLv2ContactDamage = defaultVolatileFlukeShamanStoneLv2ContactDamage;

        [InputFloatElement("Flukenest", "VF Shaman Stone Lv 1 Cloud Damage", 0f, 100f)]
        public float volatileFlukeShamanStoneLv1CloudDamage = defaultVolatileFlukeShamanStoneLv1CloudDamage;

        [InputFloatElement("Flukenest", "VF Shaman Stone Lv 2 Cloud Damage", 0f, 100f)]
        public float volatileFlukeShamanStoneLv2CloudDamage = defaultVolatileFlukeShamanStoneLv2CloudDamage;

        [InputFloatElement("Flukenest", "VF Shaman Stone Cloud Duration", 0f, 6f)]
        public float volatileFlukeShamanStoneCloudDuration = defaultVolatileFlukeShamanStoneCloudDuration;
		
        [InputFloatElement("Flukenest", "VF Shaman Stone Cloud Radius", 0.01f, 10f)]
        public float volatileFlukeShamanStoneCloudRadius = defaultVolatileFlukeShamanStoneCloudRadius;

        [ButtonElement("Flukenest", "Reset Defaults", "")]
        public void ResetFlukenest()
        {
			charm11NotchCost = defaultCharm11NotchCost;
			flukeLifetimeMin = defaultFlukeLifetimeMin;
			flukeLifetimeMax = defaultFlukeLifetimeMax;
			flukeDamage = defaultFlukeDamage;
			flukeFotFDamageIncrease = defaultFlukeFotFDamageIncrease;
			flukenestVSFlukes = defaultFlukenestVSFlukes;
			flukenestSSFlukes = defaultFlukenestSSFlukes;
			flukeSizeMin = defaultFlukeSizeMin;
			flukeSizeMax = defaultFlukeSizeMax;
			flukeShamanStoneDamage = defaultFlukeShamanStoneDamage;
			flukeShamanStoneSizeMin = defaultFlukeShamanStoneSizeMin;
			flukeShamanStoneSizeMax = defaultFlukeShamanStoneSizeMax;
			volatileFlukeLv1ContactDamage = defaultVolatileFlukeLv1ContactDamage;
			volatileFlukeLv2ContactDamage = defaultVolatileFlukeLv2ContactDamage;
			volatileFlukeLv1CloudDamage = defaultVolatileFlukeLv1CloudDamage;
            volatileFlukeLv2CloudDamage = defaultVolatileFlukeLv2CloudDamage;
            volatileFlukeCloudDuration = defaultVolatileFlukeCloudDuration;
			volatileFlukeCloudRadius = defaultVolatileFlukeCloudRadius;
			volatileFlukeShamanStoneLv1ContactDamage = defaultVolatileFlukeShamanStoneLv1ContactDamage;
			volatileFlukeShamanStoneLv2ContactDamage = defaultVolatileFlukeShamanStoneLv2ContactDamage;
            volatileFlukeShamanStoneLv1CloudDamage = defaultVolatileFlukeShamanStoneLv1CloudDamage;
            volatileFlukeShamanStoneLv2CloudDamage = defaultVolatileFlukeShamanStoneLv2CloudDamage;
            volatileFlukeShamanStoneCloudDuration = defaultVolatileFlukeShamanStoneCloudDuration;
			volatileFlukeShamanStoneCloudRadius = defaultVolatileFlukeShamanStoneCloudRadius;
        }

//	Fragile Charms
        [BoolElement("Fragile Charms", "Fragiles Break On Death", "")]
        public bool fragileCharmsBreak = defaultFragileCharmsBreak;
		
	//	Greed
        [SliderIntElement("Fragile Charms", "Greed Notch Cost", 0, 5)]
        public int charm24NotchCost = defaultCharm24NotchCost;

        [InputIntElement("Fragile Charms", "Greed Geo Increase (%)", 0, 500)]
        public int greedGeoIncrease = defaultGreedGeoIncrease;

        [BoolElement("Fragile Charms", "Greed + Soul Catcher/Eater Soul Gain", "")]
		public bool greedSoulGain = defaultGreedSoulGain;
		
	//	Heart
        [SliderIntElement("Fragile Charms", "Heart Notch Cost", 0, 5)]
        public int charm23NotchCost = defaultCharm23NotchCost;

        [InputIntElement("Fragile Charms", "Heart Masks", 0, 5)]
		public int heartMasks = defaultHeartMasks;
	
	//	Strength
        [SliderIntElement("Fragile Charms", "Strength Notch Cost", 0, 5)]
        public int charm25NotchCost = defaultCharm25NotchCost;

        [InputIntElement("Fragile Charms", "Strength Damage Increase", 0, 20)]
        public int strengthNailDamageIncrease = defaultStrengthNailDamageIncrease;

        [ButtonElement("Fragile Charms", "Reset Defaults", "")]
        public void ResetFragileCharms()
        {
			fragileCharmsBreak = defaultFragileCharmsBreak;
			charm24NotchCost = defaultCharm24NotchCost;
			greedGeoIncrease = defaultGreedGeoIncrease;
			greedSoulGain = defaultGreedSoulGain;
			charm23NotchCost = defaultCharm23NotchCost;
			heartMasks = defaultHeartMasks;
			charm25NotchCost = defaultCharm25NotchCost;
			strengthNailDamageIncrease = defaultStrengthNailDamageIncrease;
        }

//	Fury of the Fallen
        [SliderIntElement("Fury of the Fallen", "Notch Cost", 0, 5)]
        public int charm6NotchCost = defaultCharm6NotchCost;

        [BoolElement("Fury of the Fallen", "Active While Overcharmed", "")]
		public bool furyOfTheFallenOvercharmed = defaultFuryOfTheFallenOvercharmed;
		
        [InputFloatElement("Fury of the Fallen", "Active Health Threshold (%)", 0f, 1f)]
		public float furyOfTheFallenThreshold = defaultFuryOfTheFallenThreshold;

        [InputIntElement("Fury of the Fallen", "Nail Damage Increase", 0, 50)]
        public int furyOfTheFallenNailDamageIncrease = defaultFuryOfTheFallenNailDamageIncrease;
		
        [InputFloatElement("Fury of the Fallen", "Nail Cooldown Reduction", 0f, 1f)]
		public float furyOfTheFallenNailCooldownReduction = defaultFuryOfTheFallenNailCooldownReduction;

        [InputIntElement("Fury of the Fallen", "Vengeful Spirit Damage Increase", 0, 50)]
		public int furyOfTheFallenVSDamageIncrease = defaultFuryOfTheFallenVSDamageIncrease;
		
        [InputIntElement("Fury of the Fallen", "Shade Soul Damage Increase", 0, 50)]
		public int furyOfTheFallenSSDamageIncrease = defaultFuryOfTheFallenSSDamageIncrease;
		
        [InputIntElement("Fury of the Fallen", "Howling Wraiths Damage Increase", 0, 50)]
		public int furyOfTheFallenHWDamageIncrease = defaultFuryOfTheFallenHWDamageIncrease;
		
        [InputIntElement("Fury of the Fallen", "Abyss Shriek Damage Increase", 0, 50)]
		public int furyOfTheFallenASDamageIncrease = defaultFuryOfTheFallenASDamageIncrease;
		
        [InputIntElement("Fury of the Fallen", "Dive Damage Increase", 0, 50)]
		public int furyOfTheFallenDiveDamageIncrease = defaultFuryOfTheFallenDiveDamageIncrease;
		
        [InputIntElement("Fury of the Fallen", "Desolate Dive Damage Increase", 0, 50)]
		public int furyOfTheFallenDDiveDamageIncrease = defaultFuryOfTheFallenDDiveDamageIncrease;
		
        [InputIntElement("Fury of the Fallen", "Descending Dark First Damage Increase", 0, 50)]
		public int furyOfTheFallenDDarkDamage1Increase = defaultFuryOfTheFallenDDarkDamage1Increase;
		
        [InputIntElement("Fury of the Fallen", "Descending Dark Second Damage Increase", 0, 50)]
		public int furyOfTheFallenDDarkDamage2Increase = defaultFuryOfTheFallenDDarkDamage2Increase;
		
        [ButtonElement("Fury of the Fallen", "Reset Defaults", "")]
        public void ResetFuryOfTheFallen()
        {
			charm6NotchCost = defaultCharm6NotchCost;
			furyOfTheFallenOvercharmed = defaultFuryOfTheFallenOvercharmed;
			furyOfTheFallenThreshold = defaultFuryOfTheFallenThreshold;
            furyOfTheFallenNailDamageIncrease = defaultFuryOfTheFallenNailDamageIncrease;
			furyOfTheFallenNailCooldownReduction = defaultFuryOfTheFallenNailCooldownReduction;
			furyOfTheFallenVSDamageIncrease = defaultFuryOfTheFallenVSDamageIncrease;
			furyOfTheFallenSSDamageIncrease = defaultFuryOfTheFallenSSDamageIncrease;
			furyOfTheFallenHWDamageIncrease = defaultFuryOfTheFallenHWDamageIncrease;
			furyOfTheFallenASDamageIncrease = defaultFuryOfTheFallenASDamageIncrease;
			furyOfTheFallenDiveDamageIncrease = defaultFuryOfTheFallenDiveDamageIncrease;
			furyOfTheFallenDDiveDamageIncrease = defaultFuryOfTheFallenDDiveDamageIncrease;
			furyOfTheFallenDDarkDamage1Increase = defaultFuryOfTheFallenDDarkDamage1Increase;
			furyOfTheFallenDDarkDamage2Increase = defaultFuryOfTheFallenDDarkDamage2Increase;
        }

//	Gathering Swarm
        [SliderIntElement("Gathering Swarm", "Notch Cost", 0, 5)]
        public int charm1NotchCost = defaultCharm1NotchCost;

        [ButtonElement("Gathering Swarm", "Reset Defaults", "")]
        public void ResetGatheringSwarm()
        {
            charm1NotchCost = defaultCharm1NotchCost;
        }

//	Glowing Womb
        [SliderIntElement("Glowing Womb", "Notch Cost", 0, 5)]
        public int charm22NotchCost = defaultCharm22NotchCost;

        [BoolElement("Glowing Womb", "Hatchlings Pierce Armor", "")]
		public bool glowingWombPiercing = defaultGlowingWombPiercing;
		
        [InputFloatElement("Glowing Womb", "Spawn Rate", 0f, 10f)]
        public float glowingWombSpawnRate = defaultGlowingWombSpawnRate;

        [InputIntElement("Glowing Womb", "Spawn Cost", 0, 99)]
        public int glowingWombSpawnCost = defaultGlowingWombSpawnCost;

        [InputIntElement("Glowing Womb", "Maximum Hatchlings", 0, 12)]
        public int glowingWombSpawnTotal = defaultGlowingWombSpawnTotal;

        [InputIntElement("Glowing Womb", "Impact Damage", 0, 100)]
        public int glowingWombDamage = defaultGlowingWombDamage;

        [InputFloatElement("Glowing Womb", "Gathering Swarm Spawn Rate", 0f, 10f)]
        public float glowingWombGatheringSwarmSpawnRate = defaultGlowingWombGatheringSwarmSpawnRate;
		
        [InputIntElement("Glowing Womb", "Gathing Swarm Spawn Cost Reduction", 0, 99)]
        public int glowingWombGatheringSwarmCostReduction = defaultGlowingWombGatheringSwarmCostReduction;
		
        [InputIntElement("Glowing Womb", "Gathering Swarm Max Hatchlings", 0, 12)]
        public int glowingWombGatheringSwarmSpawnTotal = defaultGlowingWombGatheringSwarmSpawnTotal;
		
        [InputIntElement("Glowing Womb", "Gathering Swarm Impact Damage Reduction", 0, 50)]
		public int glowingWombGatheringSwarmDamageReduction = defaultGlowingWombGatheringSwarmDamageReduction;
		
        [InputIntElement("Glowing Womb", "Flukenest Impact Damage Increase", 0, 50)]
		public int glowingWombFlukenestDamageIncrease = defaultGlowingWombFlukenestDamageIncrease;
		
        [InputIntElement("Glowing Womb", "Carefree Melody Spawn Cost Reduction", 0, 99)]
		public int glowingWombCarefreeMelodyCostReduction = defaultGlowingWombCarefreeMelodyCostReduction;
		
        [InputIntElement("Glowing Womb", "FotF Impact Damage Increase", 0, 50)]
        public int glowingWombFotFDamageIncrease = defaultGlowingWombFotFDamageIncrease;
		
        [InputIntElement("Glowing Womb", "Defender's Crest Impact Damage", 0, 100)]
        public int glowingWombDefendersCrestDamage = defaultGlowingWombDefendersCrestDamage;

        [InputFloatElement("Glowing Womb", "Defender's Crest Cloud Duration", 0f, 10f)]
        public float glowingWombDefendersCrestDuration = defaultGlowingWombDefendersCrestDuration;

        [InputFloatElement("Glowing Womb", "Defender's Crest Cloud Radius", 0f, 5f)]
        public float glowingWombDefendersCrestRadius = defaultGlowingWombDefendersCrestRadius;

        [InputFloatElement("Glowing Womb", "Defender's Crest Cloud Damage", 0f, 50f)]
        public float glowingWombDefendersCrestCloudDamage = defaultGlowingWombDefendersCrestCloudDamage;
		
        [InputFloatElement("Glowing Womb", "DC + FotF Cloud Duration", 0f, 10f)]
        public float glowingWombDefendersCrestFotFDuration = defaultGlowingWombDefendersCrestFotFDuration;

        [ButtonElement("Glowing Womb", "Reset Defaults", "")]
        public void ResetGlowingWomb()
        {
			charm22NotchCost = defaultCharm22NotchCost;
			glowingWombPiercing = defaultGlowingWombPiercing;
			glowingWombSpawnRate = defaultGlowingWombSpawnRate;
			glowingWombSpawnCost = defaultGlowingWombSpawnCost;
			glowingWombSpawnTotal = defaultGlowingWombSpawnTotal;
			glowingWombDamage = defaultGlowingWombDamage;
			glowingWombGatheringSwarmSpawnRate = defaultGlowingWombGatheringSwarmSpawnRate;
			glowingWombGatheringSwarmCostReduction = defaultGlowingWombGatheringSwarmCostReduction;
			glowingWombGatheringSwarmSpawnTotal = defaultGlowingWombGatheringSwarmSpawnTotal;
			glowingWombGatheringSwarmDamageReduction = defaultGlowingWombGatheringSwarmDamageReduction;
			glowingWombFlukenestDamageIncrease = defaultGlowingWombFlukenestDamageIncrease;
			glowingWombCarefreeMelodyCostReduction = defaultGlowingWombCarefreeMelodyCostReduction;
			glowingWombFotFDamageIncrease = defaultGlowingWombFotFDamageIncrease;
			glowingWombDefendersCrestDamage = defaultGlowingWombDefendersCrestDamage;
			glowingWombDefendersCrestDuration = defaultGlowingWombDefendersCrestDuration;
			glowingWombDefendersCrestRadius = defaultGlowingWombDefendersCrestRadius;
			glowingWombDefendersCrestCloudDamage = defaultGlowingWombDefendersCrestCloudDamage;
			glowingWombDefendersCrestFotFDuration = defaultGlowingWombDefendersCrestFotFDuration;
        }

//	Grimmchild
        [SliderIntElement("Grimmchild", "Notch Cost", 0, 5)]
        public int charmGrimmchildNotchCost = defaultCharmGrimmchildNotchCost;

        [InputFloatElement("Grimmchild", "Attack Cooldown", 0f, 10f)]
        public float grimmchildAttackTimer = defaultGrimmchildAttackTimer;

        [InputFloatElement("Grimmchild", "Attack Range", 0f, 10f)]
		public float grimmchildRange = defaultGrimmchildRange;

        [InputFloatElement("Grimmchild", "Projectile Speed", 0f, 100f)]
		public float grimmchildProjectileSpeed = defaultGrimmchildProjectileSpeed;
		
        [InputFloatElement("Grimmchild", "Projectile Spread", 0f, 180f)]
		public float grimmchildProjectileSpread = defaultGrimmchildProjectileSpread;

        [BoolElement("Grimmchild", "Attack Ignores Terrain", "")]
		public bool grimmchildBypassTerrain = defaultGrimmchildBypassTerrain;

        [InputIntElement("Grimmchild", "Level 2 Damage", 0, 50)]
        public int grimmchildDamage2 = defaultGrimmchildDamage2;

        [InputIntElement("Grimmchild", "Level 3 Damage", 0, 50)]
        public int grimmchildDamage3 = defaultGrimmchildDamage3;

        [InputIntElement("Grimmchild", "Level 4 Damage", 0, 50)]
        public int grimmchildDamage4 = defaultGrimmchildDamage4;

        [InputFloatElement("Grimmchild", "Gathering Swarm Attack Cooldown", 0f, 10f)]
		public float grimmchildGatheringSwarmAttackTimer = defaultGrimmchildGatheringSwarmAttackTimer;

        [InputIntElement("Grimmchild", "Flukenest Damage Increase", 0, 50)]
		public int grimmchildFlukenestDamageIncrease = defaultGrimmchildFlukenestDamageIncrease;

        [InputIntElement("Grimmchild", "Dream Nail Base Damage", 0, 100)]
        public int grimmchildDreamNailBaseDamage = defaultDreamNailBaseDamage;
		
        [InputIntElement("Grimmchild", "+1 Damage per X Essense", 0, 1000)]
		public int grimmchildDreamNailDamageScaleRate = defaultDreamNailDamageScaleRate;

        public int charm40NotchCost = defaultCharmGrimmchildNotchCost;

        [ButtonElement("Grimmchild", "Reset Defaults", "")]
        public void ResetGrimmchild()
        {
			charmGrimmchildNotchCost = defaultCharmGrimmchildNotchCost;
			grimmchildAttackTimer = defaultGrimmchildAttackTimer;
			grimmchildRange = defaultGrimmchildRange;
			grimmchildProjectileSpeed = defaultGrimmchildProjectileSpeed;
			grimmchildProjectileSpread = defaultGrimmchildProjectileSpread;
			grimmchildBypassTerrain = defaultGrimmchildBypassTerrain;
			grimmchildDamage2 = defaultGrimmchildDamage2;
			grimmchildDamage3 = defaultGrimmchildDamage3;
			grimmchildDamage4 = defaultGrimmchildDamage4;
			grimmchildGatheringSwarmAttackTimer = defaultGrimmchildGatheringSwarmAttackTimer;
			grimmchildFlukenestDamageIncrease = defaultGrimmchildFlukenestDamageIncrease;
			grimmchildDreamNailBaseDamage = defaultDreamNailBaseDamage;
			grimmchildDreamNailDamageScaleRate = defaultDreamNailDamageScaleRate;

			charm40NotchCost = PlayerDataAccess.grimmChildLevel == 5 ? charmCarefreeMelodyNotchCost : charmGrimmchildNotchCost;
        }

//	Grubberfly's Elegy
        [SliderIntElement("Grubberfly's Elegy", "Notch Cost", 0, 5)]
        public int charm35NotchCost = defaultCharm35NotchCost;

        [InputFloatElement("Grubberfly's Elegy", "Beam Damage", 0f, 5f)]
        public float grubberflysElegyDamageScale = defaultGrubberflysElegyDamageScale;

        [BoolElement("Grubberfly's Elegy", "Nail Range Affects Beam Range", "")]
		public bool grubberflysElegyRangeAffected = defaultGrubberflysElegyRangeAffected;
		
        [BoolElement("Grubberfly's Elegy", "Fire Beam While Fury Active", "")]
		public bool grubberflysElegyFotFActivated = defaultGrubberflysElegyFotFActivated;

        [BoolElement("Grubberfly's Elegy", "Fire Beam With Joni's Blessing", "While above base max health.")]
        public bool grubberflysElegyJoniBeamDamageBool = defaultGrubberflysElegyJoniBeamDamageBool;

        [InputIntElement("Grubberfly's Elegy", "Soul Catcher Soul Gain", 0, 231)]
		public int grubberflysElegySoulCatcherSoulGain = defaultGrubberflysElegySoulCatcherSoulGain;

        [InputIntElement("Grubberfly's Elegy", "Soul Eater Soul Gain", 0, 231)]
		public int grubberflysElegySoulEaterSoulGain = defaultGrubberflysElegySoulEaterSoulGain;

        [ButtonElement("Grubberfly's Elegy", "Reset Defaults", "")]
        public void ResetGrubberflysElegy()
        {
			charm35NotchCost = defaultCharm35NotchCost;
			grubberflysElegyDamageScale = defaultGrubberflysElegyDamageScale;
			grubberflysElegyRangeAffected = defaultGrubberflysElegyRangeAffected;
			grubberflysElegyFotFActivated = defaultGrubberflysElegyFotFActivated;
			grubberflysElegyJoniBeamDamageBool = defaultGrubberflysElegyJoniBeamDamageBool;
			grubberflysElegySoulCatcherSoulGain = defaultGrubberflysElegySoulCatcherSoulGain;
			grubberflysElegySoulEaterSoulGain = defaultGrubberflysElegySoulEaterSoulGain;
        }

//	Grubsong
        [SliderIntElement("Grubsong", "Notch Cost", 0, 5)]
        public int charm3NotchCost = defaultCharm3NotchCost;

        [InputIntElement("Grubsong", "Soul Gain", 0, 231)]
        public int grubsongSoulGain = defaultGrubsongSoulGain;

        [InputIntElement("Grubsong", "Shape of Unn Soul Gain", 0, 231)]
		public int grubsongShapeOfUnnSoulGainIncrease = defaultGrubsongShapeOfUnnSoulGainIncrease;

        [InputIntElement("Grubsong", "Grubberfly's Elegy Soul Gain", 0, 231)]
        public int grubsongGrubberflysElegySoulGainIncrease = defaultGrubsongGrubberflysElegySoulGainIncrease;

        [ButtonElement("Grubsong", "Reset Defaults", "")]
        public void ResetGrubsong()
        {
			charm3NotchCost = defaultCharm3NotchCost;
			grubsongSoulGain = defaultGrubsongSoulGain;
			grubsongShapeOfUnnSoulGainIncrease = defaultGrubsongShapeOfUnnSoulGainIncrease;
			grubsongGrubberflysElegySoulGainIncrease = defaultGrubsongGrubberflysElegySoulGainIncrease;
        }

//	Heavy Blow
        [SliderIntElement("Heavy Blow", "Notch Cost", 0, 5)]
        public int charm15NotchCost = defaultCharm15NotchCost;

        [InputIntElement("Heavy Blow", "Damage Increase", 0, 20)]
        public int heavyBlowNailDamageIncrease = defaultHeavyBlowNailDamageIncrease;

        [InputFloatElement("Heavy Blow", "Nail Cooldown Increase", 0f, 2f)]
		public float heavyBlowNailCooldownIncrease = defaultHeavyBlowNailCooldown;
		
        [InputIntElement("Heavy Blow", "Stagger Reduction", 0, 20)]
        public int heavyBlowStagger = defaultHeavyBlowStagger;

        [InputIntElement("Heavy Blow", "Combo Stagger Reduction", 0, 20)]
        public int heavyBlowStaggerCombo = defaultHeavyBlowStaggerCombo;

        [InputIntElement("Heavy Blow", "Additional Nail Art Damage Increase", 0, 20)]
        public int heavyBlowNailArtDamageIncrease = defaultHeavyBlowNailArtDamageIncrease;

        [InputIntElement("Heavy Blow", "Environmental Hit Multiplier", 0, 20)]
		public int heavyBlowEnviroHits = defaultHeavyBlowEnviroHits;

        [InputFloatElement("Heavy Blow", "Great Slash Knockback", 0f, 10f)]
        public float heavyBlowGreatSlashKnockback = defaultHeavyBlowGreatSlashKnockback;

        [InputFloatElement("Heavy Blow", "Dash Slash Knockback", 0f, 10f)]
        public float heavyBlowDashSlashKnockback = defaultHeavyBlowDashSlashKnockback;

        [InputFloatElement("Heavy Blow", "Steady Body Nail Cooldown Reduction", 0f, 2f)]
		public float heavyBlowSteadyBodyNailCooldownReduction = defaultHeavyBlowSteadyBodyNailCooldownReduction;

        [ButtonElement("Heavy Blow", "Reset Defaults", "")]
        public void ResetHeavyBlow()
        {
			charm15NotchCost = defaultCharm15NotchCost;
			heavyBlowNailDamageIncrease = defaultHeavyBlowNailDamageIncrease;
			heavyBlowNailCooldownIncrease = defaultHeavyBlowNailCooldown;
			heavyBlowStagger = defaultHeavyBlowStagger;
			heavyBlowStaggerCombo = defaultHeavyBlowStaggerCombo;
			heavyBlowNailArtDamageIncrease = defaultHeavyBlowNailArtDamageIncrease;
			heavyBlowEnviroHits = defaultHeavyBlowEnviroHits;
			heavyBlowGreatSlashKnockback = defaultHeavyBlowGreatSlashKnockback;
			heavyBlowDashSlashKnockback = defaultHeavyBlowDashSlashKnockback;
			heavyBlowSteadyBodyNailCooldownReduction = defaultHeavyBlowSteadyBodyNailCooldownReduction;
        }

//	Hiveblood
        [SliderIntElement("Hiveblood", "Notch Cost", 0, 5)]
        public int charm29NotchCost = defaultCharm29NotchCost;

        [InputIntElement("Hiveblood", "Regen Limit", 0, 20)]
		public int hivebloodRegenLimit = defaultHivebloodRegenLimit;
		
        [InputFloatElement("Hiveblood", "Regen Cooldown", 0f, 60f)]
        public float hivebloodCooldown = defaultHivebloodCooldown;

        [InputIntElement("Hiveblood", "Fragile Heart Regen Limit Increase", 0, 20)]
		public int hivebloodFragileHeartLimitIncrease = defaultHivebloodFragileHeartLimitIncrease;
		
        [InputFloatElement("Hiveblood", "Regen Cooldown Decceleration", 0f, 60f)]
        public float hivebloodCooldownDecceleration = defaultHivebloodCooldownDecceleration;

        [InputFloatElement("Hiveblood", "Joni's Blessing Regen Cooldown", 0f, 60f)]
        public float hivebloodJonisCooldown = defaultHivebloodJonisCooldown;

        [InputIntElement("Hiveblood", "Lifeblood Heart Regen Limit Increase", 0, 20)]
		public int hivebloodLifebloodHeartLimitIncrease = defaultHivebloodLifebloodHeartLimitIncrease;

        [InputFloatElement("Hiveblood", "Joni's Blessing Regen Cooldown Decceleration", 0f, 60f)]
        public float hivebloodJonisCooldownDecceleration = defaultHivebloodJonisCooldownDecceleration;

        [ButtonElement("Hiveblood", "Reset Defaults", "")]
        public void ResetHiveblood()
        {
			charm29NotchCost = defaultCharm29NotchCost;
			hivebloodRegenLimit = defaultHivebloodRegenLimit;
			hivebloodCooldown = defaultHivebloodCooldown;
			hivebloodFragileHeartLimitIncrease = defaultHivebloodFragileHeartLimitIncrease;
			hivebloodCooldownDecceleration = defaultHivebloodCooldownDecceleration;
			hivebloodJonisCooldown = defaultHivebloodJonisCooldown;
			hivebloodLifebloodHeartLimitIncrease = defaultHivebloodLifebloodHeartLimitIncrease;
			hivebloodJonisCooldownDecceleration = defaultHivebloodJonisCooldownDecceleration;
        }

//	Joni's Blessing
        [SliderIntElement("Joni's Blessing", "Notch Cost", 0, 5)]
        public int charm27NotchCost = defaultCharm27NotchCost;

        [InputIntElement("Joni's Blessing", "Lifeblood", 0, 20)]
		public int jonisBlessingLifeblood = defaultJonisBlessingLifeblood;
		
        [ButtonElement("Joni's Blessing", "Reset Defaults", "")]
        public void ResetJonisBlessing()
        {
			charm27NotchCost = defaultCharm27NotchCost;
			jonisBlessingLifeblood = defaultJonisBlessingLifeblood;
        }

//	Kingsoul/Void Heart
        [SliderIntElement("Kingsoul/Void Heart", "Kingsoul Notch Cost", 0, 5)]
        public int charmKingsoulNotchCost = defaultCharmKingsoulNotchCost;

        [InputIntElement("Kingsoul/Void Heart", "Soul Gain", 0, 231)]
        public int kingsoulSoulGain = defaultKingsoulSoulGain;

        [InputFloatElement("Kingsoul/Void Heart", "Regen Tick Rate", 0f, 60f)]
        public float kingsoulRegenTickRate = defaultKingsoulRegenTickRate;

        [SliderIntElement("Kingsoul/Void Heart", "Void Heart Notch Cost", 0, 5)]
        public int charmVoidHeartNotchCost = defaultCharmVoidHeartNotchCost;

        [BoolElement("Kingsoul/Void Heart", "Void Heart Regens Soul", "")]
		public bool voidHeartSoulRegen = defaultVoidHeartSoulRegen;

        public int charm36NotchCost = defaultCharmKingsoulNotchCost;

        [ButtonElement("Kingsoul/Void Heart", "Reset Defaults", "")]
        public void ResetKingsoulVoidHeart()
        {
			charmKingsoulNotchCost = defaultCharmKingsoulNotchCost;
			kingsoulSoulGain = defaultKingsoulSoulGain;
			kingsoulRegenTickRate = defaultKingsoulRegenTickRate;
			charmVoidHeartNotchCost = defaultCharmVoidHeartNotchCost;
			voidHeartSoulRegen = defaultVoidHeartSoulRegen;

            charm36NotchCost = PlayerDataAccess.royalCharmState == 4 ? charmVoidHeartNotchCost : charmKingsoulNotchCost;
        }

//	Lifeblood Core
        [SliderIntElement("Lifeblood Core", "Notch Cost", 0, 5)]
        public int charm9NotchCost = defaultCharm9NotchCost;

        [InputIntElement("Lifeblood Core", "Lifeblood", 0, 12)]
        public int lifebloodCoreLifeblood = defaultLifebloodCoreLifeblood;

        [InputFloatElement("Lifeblood Core", "Regen Cooldown", 0f, 60f)]
		public float lifebloodCoreCooldown = defaultLifebloodCoreCooldown;
		
        [InputIntElement("Lifeblood Core", "Regen Cost", 0, 231)]
		public int lifebloodCoreCost = defaultLifebloodCoreCost;
		
        [InputIntElement("Lifeblood Core", "Steady Body Regen Cost", 0, 231)]
		public int lifebloodCoreSteadyBodyCost = defaultLifebloodCoreSteadyBodyCost;
		
        [InputIntElement("Lifeblood Core", "Spell Twister Regen Cost", 0, 231)]
		public int lifebloodCoreSpellTwisterCost = defaultLifebloodCoreSpellTwisterCost;

        [ButtonElement("Lifeblood Core", "Reset Defaults", "")]
        public void ResetLifebloodCore()
        {
			charm9NotchCost = defaultCharm9NotchCost;
			lifebloodCoreLifeblood = defaultLifebloodCoreLifeblood;
			lifebloodCoreCooldown = defaultLifebloodCoreCooldown;
			lifebloodCoreCost = defaultLifebloodCoreCost;
			lifebloodCoreSteadyBodyCost = defaultLifebloodCoreSteadyBodyCost;
			lifebloodCoreSpellTwisterCost = defaultLifebloodCoreSpellTwisterCost;
        }

//	Lifeblood Heart
        [SliderIntElement("Lifeblood Heart", "Notch Cost", 0, 5)]
        public int charm8NotchCost = defaultCharm8NotchCost;

        [InputIntElement("Lifeblood Heart", "Lifeblood Heart Masks", 0, 12)]
        public int lifebloodHeartLifeblood = defaultLifebloodHeartLifeblood;

        [ButtonElement("Lifeblood Heart", "Reset Defaults", "")]
        public void ResetLifebloodHeart()
        {
            charm8NotchCost = defaultCharm8NotchCost;
            lifebloodHeartLifeblood = defaultLifebloodHeartLifeblood;
        }

//	Longnail
        [SliderIntElement("Longnail", "Notch Cost", 0, 5)]
        public int charm18NotchCost = defaultCharm18NotchCost;

        [InputIntElement("Longnail", "Nail Range Increase (%)", 0, 500)]
        public int longnailRangeIncrease = defaultLongnailRangeIncrease;

        [ButtonElement("Longnail", "Reset Defaults", "")]
        public void ResetLongnail()
        {
			charm18NotchCost = defaultCharm18NotchCost;
			longnailRangeIncrease = defaultLongnailRangeIncrease;
        }

//	Mark of Pride
        [SliderIntElement("Mark of Pride", "Notch Cost", 0, 5)]
        public int charm13NotchCost = defaultCharm13NotchCost;

        [InputIntElement("Mark of Pride", "Nail Damage Increase", 0, 20)]
        public int markOfPrideNailDamageIncrease = defaultMarkOfPrideNailDamageIncrease;

        [InputFloatElement("Mark of Pride", "Nail Cooldown Reduction", 0.01f, 5f)]
		public float markOfPrideNailCooldownReduction = defaultMarkOfPrideNailCooldownReduction;
		
        [InputIntElement("Mark of Pride", "Nail Range Increase (%)", 0, 500)]
        public int markOfPrideRangeIncrease = defaultMarkOfPrideRangeIncrease;

        [ButtonElement("Mark of Pride", "Reset Defaults", "")]
        public void ResetMarkOfPride()
        {
			charm13NotchCost = defaultCharm13NotchCost;
			markOfPrideNailDamageIncrease = defaultMarkOfPrideNailDamageIncrease;
			markOfPrideNailCooldownReduction = defaultMarkOfPrideNailCooldownReduction;
			markOfPrideRangeIncrease = defaultMarkOfPrideRangeIncrease;
        }

//	Nailmaster's Glory
        [SliderIntElement("Nailmaster's Glory", "Notch Cost", 0, 5)]
        public int charm26NotchCost = defaultCharm26NotchCost;

        [InputFloatElement("Nailmaster's Glory", "Nail Art Damage Increase", 0.01f, 5f)]
		public float nailmastersGloryDamageIncrease = defaultNailmastersGloryDamageIncrease;
		
        [InputFloatElement("Nailmaster's Glory", "Nail Art Charge Time", 0.01f, 5f)]
        public float nailmastersGloryChargeTime = defaultNailmastersGloryChargeTime;
		
        [BoolElement("Nailmaster's Glory", "Mark of Pride Shield Breaker", "Should nail arts pierce shields while Mark of Pride is equipped?")]
		public bool nailmastersGloryMoPPiercing = defaultNailmastersGloryMoPPiercing;

        [ButtonElement("Nailmaster's Glory", "Reset Defaults", "")]
        public void ResetNailmastersGlory()
        {
			charm26NotchCost = defaultCharm26NotchCost;
			nailmastersGloryDamageIncrease = defaultNailmastersGloryDamageIncrease;
			nailmastersGloryChargeTime = defaultNailmastersGloryChargeTime;
			nailmastersGloryMoPPiercing = defaultNailmastersGloryMoPPiercing;
        }

//	Quick Focus
        [SliderIntElement("Quick Focus", "Notch Cost", 0, 5)]
        public int charm7NotchCost = defaultCharm7NotchCost;

        [InputFloatElement("Quick Focus", "Focus Time", 0.033f, 2f)]
        public float quickFocusFocusTime = defaultQuickFocusFocusTime;

        [InputFloatElement("Quick Focus", "Crystal Dash Charge Time Multiplier", 0.033f, 2f)]
		public float quickFocusCDashTimeMult = defaultQuickFocusCDashTimeMult;

        [ButtonElement("Quick Focus", "Reset Defaults", "")]
        public void ResetQuickFocus()
        {
            charm7NotchCost = defaultCharm7NotchCost;
            quickFocusFocusTime = defaultQuickFocusFocusTime;
			quickFocusCDashTimeMult = defaultQuickFocusCDashTimeMult;
        }

//	Quick Slash
        [SliderIntElement("Quick Slash", "Notch Cost", 0, 5)]
        public int charm32NotchCost = defaultCharm32NotchCost;

        [InputFloatElement("Quick Slash", "Nail Cooldown Reduction", 0f, 2f)]
        public float quickSlashNailCooldownReduction = defaultQuickSlashNailCooldownReduction;

        [ButtonElement("Quick Slash", "Reset Defaults", "")]
        public void ResetQuickSlash()
        {
			charm32NotchCost = defaultCharm32NotchCost;
			quickSlashNailCooldownReduction = defaultQuickSlashNailCooldownReduction;
        }

//	Shaman Stone
        [SliderIntElement("Shaman Stone", "Notch Cost", 0, 5)]
        public int charm19NotchCost = defaultCharm19NotchCost;

        [InputIntElement("Shaman Stone", "Vengeful Spirit Damage", 0, 100)]
        public int shamanStoneVSDamage = defaultShamanStoneVSDamage;

        [InputIntElement("Shaman Stone", "Shade Soul Damage", 0, 100)]
        public int shamanStoneSSDamage = defaultShamanStoneSSDamage;

        [InputIntElement("Shaman Stone", "Howling Wraiths Damage", 0, 100)]
        public int shamanStoneHWDamage = defaultShamanStoneHWDamage;

        [InputIntElement("Shaman Stone", "Abyss Shriek Damage", 0, 100)]
        public int shamanStoneASDamage = defaultShamanStoneASDamage;

        [InputIntElement("Shaman Stone", "Dive Damage", 0, 100)]
        public int shamanStoneDiveDamage = defaultShamanStoneDiveDamage;

        [InputIntElement("Shaman Stone", "Desolate Dive Damage", 0, 100)]
        public int shamanStoneDDiveDamage = defaultShamanStoneDDiveDamage;

        [InputIntElement("Shaman Stone", "Descending Dark Left Damage", 0, 100)]
        public int shamanStoneDDarkDamageL = defaultShamanStoneDDarkDamageL;

        [InputIntElement("Shaman Stone", "Descending Dark Right Damage", 0, 100)]
        public int shamanStoneDDarkDamageR = defaultShamanStoneDDarkDamageR;

        [InputIntElement("Shaman Stone", "Descending Dark Final Damage", 0, 100)]
        public int shamanStoneDDarkDamageMega = defaultShamanStoneDDarkDamageMega;

        //[InputFloatElement("Shaman Stone", "Vengeful Spirit X Scale", 0f, 5f)]
        public float shamanStoneVSSizeScaleX = defaultShamanStoneVSSizeScaleX;

        //[InputFloatElement("Shaman Stone", "Vengeful Spirit Y Scale", 0f, 5f)]
        public float shamanStoneVSSizeScaleY = defaultShamanStoneVSSizeScaleY;

        //[InputFloatElement("Shaman Stone", "Shade Soul X Scale", 0f, 5f)]
        public float shamanStoneSSSizeScaleX = defaultShamanStoneSSSizeScaleX;

        //[InputFloatElement("Shaman Stone", "Shade Soul Y Scale", 0f, 5f)]
        public float shamanStoneSSSizeScaleY = defaultShamanStoneSSSizeScaleY;

        [ButtonElement("Shaman Stone", "Reset Defaults", "")]
        public void ResetShamanStone()
        {
            charm19NotchCost = defaultCharm19NotchCost;
            shamanStoneVSDamage = defaultShamanStoneVSDamage;
            shamanStoneSSDamage = defaultShamanStoneSSDamage;
            shamanStoneHWDamage = defaultShamanStoneHWDamage;
            shamanStoneASDamage = defaultShamanStoneASDamage;
            shamanStoneDiveDamage = defaultShamanStoneDiveDamage;
            shamanStoneDDiveDamage = defaultShamanStoneDDiveDamage;
            shamanStoneDDarkDamageL = defaultShamanStoneDDarkDamageL;
            shamanStoneDDarkDamageR = defaultShamanStoneDDarkDamageR;
            shamanStoneDDarkDamageMega = defaultShamanStoneDDarkDamageMega;
            shamanStoneVSSizeScaleX = defaultShamanStoneVSSizeScaleX;
            shamanStoneVSSizeScaleY = defaultShamanStoneVSSizeScaleY;
            shamanStoneSSSizeScaleX = defaultShamanStoneSSSizeScaleX;
            shamanStoneSSSizeScaleY = defaultShamanStoneSSSizeScaleY;
        }

//	Shape of Unn
        [SliderIntElement("Shape of Unn", "Notch Cost", 0, 5)]
        public int charm28NotchCost = defaultCharm28NotchCost;

        [InputFloatElement("Shape of Unn", "Slug Speed", 0f, 36f)]
        public float shapeOfUnnSpeed = defaultShapeOfUnnSpeed;

        [InputFloatElement("Shape of Unn", "Quick Focus Slug Speed", 0f, 36f)]
        public float shapeOfUnnQuickFocusSpeed = defaultShapeOfUnnQuickFocusSpeed;

        [BoolElement("Shape of Unn", "Add Soul Reserve Vessel", "")]
		public bool shapeOfUnnAddsVessel = defaultShapeOfUnnAddsVessel;

        [ButtonElement("Shape of Unn", "Reset Defaults", "")]
        public void ResetShapeOfUnn()
        {
            charm28NotchCost = defaultCharm28NotchCost;
            shapeOfUnnSpeed = defaultShapeOfUnnSpeed;
            shapeOfUnnQuickFocusSpeed = defaultShapeOfUnnQuickFocusSpeed;
			shapeOfUnnAddsVessel = defaultShapeOfUnnAddsVessel;
        }

//	Sharp Shadow
        [SliderIntElement("Sharp Shadow", "Notch Cost", 0, 5)]
        public int charm16NotchCost = defaultCharm16NotchCost;

        [InputFloatElement("Sharp Shadow", "Dash Damage (%)", 0f, 5f)]
        public float sharpShadowDamage = defaultSharpShadowDamage;
		
        [InputFloatElement("Sharp Shadow", "Dash Speed", 0f, 75f)]
        public float sharpShadowDashSpeed = defaultSharpShadowDashSpeed;
		
        [BoolElement("Sharp Shadow", "Increase Hurt Box Size", "")]
		public bool sharpShadowHurtBoxSize = defaultSharpShadowHurtBoxSize;
        
        [InputFloatElement("Sharp Shadow", "Dashmaster Damage (%)", 0f, 5f)]
        public float sharpShadowDashmasterDamage = defaultSharpShadowDashmasterDamage;
		
        [InputFloatElement("Sharp Shadow", "Sprintmaster Dash Speed", 0f, 75f)]
        public float sharpShadowSprintmasterDashSpeed = defaultSharpShadowSprintmasterDashSpeed;
		
        [InputIntElement("Sharp Shadow", "Soul Catcher Soul Gain", 0, 231)]
		public int sharpShadowSoulCatcherSoulGain = defaultSharpShadowSoulCatcherSoulGain;
		
        [InputIntElement("Sharp Shadow", "Soul Eater Soul Gain", 0, 231)]
		public int sharpShadowSoulEaterSoulGain = defaultSharpShadowSoulEaterSoulGain;
		
        [InputFloatElement("Sharp Shadow", "Stalwart Shell IFrames", 0f, 2f)]
		public float sharpShadowStalwartShellIFrames = defaultSharpShadowStalwartShellIFrames;

        [InputFloatElement("Sharp Shadow", "Volume", 0f, 2f)]
		public float sharpShadowVolume = defaultSharpShadowVolume;

        [ButtonElement("Sharp Shadow", "Reset Defaults", "")]
        public void ResetSharpShadow()
        {
			charm16NotchCost = defaultCharm16NotchCost;
			sharpShadowDamage = defaultSharpShadowDamage;
			sharpShadowDashSpeed = defaultSharpShadowDashSpeed;
			sharpShadowHurtBoxSize = defaultSharpShadowHurtBoxSize;
			sharpShadowDashmasterDamage = defaultSharpShadowDashmasterDamage;
			sharpShadowSprintmasterDashSpeed = defaultSharpShadowSprintmasterDashSpeed;
			sharpShadowSoulCatcherSoulGain = defaultSharpShadowSoulCatcherSoulGain;
			sharpShadowSoulEaterSoulGain = defaultSharpShadowSoulEaterSoulGain;
			sharpShadowStalwartShellIFrames = defaultSharpShadowStalwartShellIFrames;
			sharpShadowVolume = defaultSharpShadowVolume;
        }

//	Soul Catcher
        [SliderIntElement("Soul Catcher", "Notch Cost", 0, 5)]
        public int charm20NotchCost = defaultCharm20NotchCost;

        [InputIntElement("Soul Catcher", "Soul Gain", 0, 231)]
        public int soulCatcherSoulGain = defaultSoulCatcherSoulGain;

        [InputIntElement("Soul Catcher", "Reserve Soul Gain", 0, 231)]
        public int soulCatcherReserveSoulGain = defaultSoulCatcherReserveSoulGain;

        [ButtonElement("Soul Catcher", "Reset Defaults", "")]
        public void ResetSoulCatcher()
        {
			charm20NotchCost = defaultCharm20NotchCost;
			soulCatcherSoulGain = defaultSoulCatcherSoulGain;
			soulCatcherReserveSoulGain = defaultSoulCatcherReserveSoulGain;
        }

//	Soul Eater
        [SliderIntElement("Soul Eater", "Notch Cost", 0, 5)]
        public int charm21NotchCost = defaultCharm21NotchCost;

        [InputIntElement("Soul Eater", "Soul Gain", 0, 231)]
        public int soulEaterSoulGain = defaultSoulEaterSoulGain;

        [InputIntElement("Soul Eater", "Reserve Soul Gain", 0, 231)]
        public int soulEaterReserveSoulGain = defaultSoulEaterReserveSoulGain;

        [ButtonElement("Soul Eater", "Reset Defaults", "")]
        public void ResetSoulEater()
        {
            charm21NotchCost = defaultCharm21NotchCost;
            soulEaterSoulGain = defaultSoulEaterSoulGain;
            soulEaterReserveSoulGain = defaultSoulEaterReserveSoulGain;
        }

//	Spell Twister
        [SliderIntElement("Spell Twister", "Notch Cost", 0, 5)]
        public int charm33NotchCost = defaultCharm33NotchCost;

        [InputIntElement("Spell Twister", "Spell Cost", 0, 99)]
        public int spellTwisterSpellCost = defaultSpellTwisterSpellCost;

        [InputIntElement("Spell Twister", "Shape of Unn Spell Cost", 0, 99)]
		public int spellTwisterShapeOfUnnSpellCost = defaultSpellTwisterShapeOfUnnSpellCost;

        [InputFloatElement("Spell Twister", "Dream Nail Range", 0, 5f)]
		public float spellTwisterDreamNailRange = defaultSpellTwisterDreamNailRange;

        [ButtonElement("Spell Twister", "Reset Defaults", "")]
        public void ResetSpellTwister()
        {
			charm33NotchCost = defaultCharm33NotchCost;
			spellTwisterSpellCost = defaultSpellTwisterSpellCost;
			spellTwisterShapeOfUnnSpellCost = defaultSpellTwisterShapeOfUnnSpellCost;
			spellTwisterDreamNailRange = defaultSpellTwisterDreamNailRange;
        }

//	Spore Shroom
        [SliderIntElement("Spore Shroom", "Notch Cost", 0, 5)]
        public int charm17NotchCost = defaultCharm17NotchCost;

        [BoolElement("Spore Shroom", "Damage Resets Cooldown", "")]
        public bool sporeShroomDamageResetsCooldown = defaultSporeShroomDamageResetsCooldown;

        [InputFloatElement("Spore Shroom", "Cloud Cooldown", 0f, 20f)]
        public float sporeShroomCooldown = defaultSporeShroomCooldown;
		
        [InputFloatElement("Spore Shroom", "Cloud Radius Base", 0f, 5f)]
		public float sporeShroomRadiusBase = defaultSporeShroomRadiusBase;
		
        [InputFloatElement("Spore Shroom", "Cloud Radius Max", 0f, 5f)]
		public float sporeShroomRadiusMax = defaultSporeShroomRadiusMax;

        [InputFloatElement("Spore Shroom", "Cloud Duration Base", 0f, 10f)]
        public float sporeShroomCloudDurationBase = defaultSporeShroomCloudDurationBase;

        [InputFloatElement("Spore Shroom", "Cloud Duration Max", 0f, 10f)]
        public float sporeShroomCloudDurationMax = defaultSporeShroomCloudDurationMax;

        [InputIntElement("Spore Shroom", "Cloud Damage Base", 0, 100)]
        public int sporeShroomDamageBase = defaultSporeShroomDamageBase;

        [InputIntElement("Spore Shroom", "Cloud Damage Max", 0, 100)]
        public int sporeShroomDamageMax = defaultSporeShroomDamageMax;
		
        [InputFloatElement("Spore Shroom", "Deep Focus Radius Mult", 0f, 2f)]
		public float sporeShroomDeepFocusRadiusMult = defaultSporeShroomDeepFocusRadiusMult;

        [InputFloatElement("Spore Shroom", "Deep Focus Duration Mult", 0f, 2f)]
        public float sporeShroomDeepFocusDurationMult = defaultSporeShroomDeepFocusDurationMult;

        [InputIntElement("Spore Shroom", "Defender's Crest Damage Base", 0, 100)]
        public int sporeShroomDefendersCrestDamageBase = defaultSporeShroomDefendersCrestDamageBase;

        [InputIntElement("Spore Shroom", "Defender's Crest Damage Max", 0, 100)]
        public int sporeShroomDefendersCrestDamageMax = defaultSporeShroomDefendersCrestDamageMax;

        [ButtonElement("Spore Shroom", "Reset Defaults", "")]
        public void ResetSporeShroom()
        {
			charm17NotchCost = defaultCharm17NotchCost;
			sporeShroomDamageResetsCooldown = defaultSporeShroomDamageResetsCooldown;
			sporeShroomCooldown = defaultSporeShroomCooldown;
			sporeShroomRadiusBase = defaultSporeShroomRadiusBase;
			sporeShroomRadiusMax = defaultSporeShroomRadiusMax;
			sporeShroomCloudDurationBase = defaultSporeShroomCloudDurationBase;
			sporeShroomCloudDurationMax = defaultSporeShroomCloudDurationMax;
			sporeShroomDamageBase = defaultSporeShroomDamageBase;
			sporeShroomDamageMax = defaultSporeShroomDamageMax;
			sporeShroomDeepFocusRadiusMult = defaultSporeShroomDeepFocusRadiusMult;
			sporeShroomDeepFocusDurationMult = defaultSporeShroomDeepFocusDurationMult;
			sporeShroomDefendersCrestDamageBase = defaultSporeShroomDefendersCrestDamageBase;
			sporeShroomDefendersCrestDamageMax = defaultSporeShroomDefendersCrestDamageMax;
        }

//	Sprintmaster
        [SliderIntElement("Sprintmaster", "Notch Cost", 0, 5)]
        public int charm37NotchCost = defaultCharm37NotchCost;

        [InputFloatElement("Sprintmaster", "Sprint Speed", 0f, 36f)]
        public float sprintmasterSpeed = defaultSprintmasterSpeed;

        [InputFloatElement("Sprintmaster", "Dashmaster Sprint Speed", 0f, 36f)]
        public float sprintmasterDashmasterSpeed = defaultSprintmasterDashmasterSpeed;

        [ButtonElement("Sprintmaster", "Reset Defaults", "")]
        public void ResetSprintmaster()
        {
			charm37NotchCost = defaultCharm37NotchCost;
			sprintmasterSpeed = defaultSprintmasterSpeed;
			sprintmasterDashmasterSpeed = defaultSprintmasterDashmasterSpeed;
        }

//	Stalwart Shell
        [SliderIntElement("Stalwart Shell", "Notch Cost", 0, 5)]
        public int charm4NotchCost = defaultCharm4NotchCost;

        [InputFloatElement("Stalwart Shell", "Damage Invulnerable Time", 0f, 5f)]
        public float stalwartShellIFrames = defaultStalwartShellIFrames;

        [InputFloatElement("Stalwart Shell", "Parry Invulnerable Time", 0f, 5f)]
		public float stalwartShellParryIFrames = defaultStalwartShellParryIFrames;
		
        [InputIntElement("Stalwart Shell", "Lifeblood", 0, 20)]
		public int stalwartShellLifeblood = defaultStalwartShellLifeblood;

        [InputFloatElement("Stalwart Shell", "Regen Cooldown", 0f, 60f)]
		public float stalwartShellCooldown = defaultStalwartShellCooldown;

        [InputFloatElement("Stalwart Shell", "Baldur Shell Block IFrames", 0f, 5f)]
        public float stalwartShellBaldurBlockIFrames = defaultStalwartShellBaldurBlockIFrames;

        [InputFloatElement("Stalwart Shell", "Defender's Crest Cooldown Reduction", 0f, 60f)]
		public float stalwartShellDefendersCrestCooldownReduction = defaultStalwartShellDefendersCrestCooldownReduction;

        [InputFloatElement("Stalwart Shell", "Lifeblood Heart Cooldown Reduction", 0f, 60f)]
		public float stalwartShellLifebloodHeartCooldownReduction = defaultStalwartShellLifebloodHeartCooldownReduction;
		
        [BoolElement("Stalwart Shell", "Joni's Blessing Damage Reduction", "While Joni's Blessing is equipped, incoming damage reduced by one, to a minimum of one.")]
		public bool stalwartShellJonisDamageReduction = defaultStalwartShellJonisDamageReduction;

        //[InputFloatElement("Stalwart Shell", "Stalwart Recoil Time", 0f, 1.0f)]
        //public float stalwartShellRecoil = defaultStalwartShellRecoil;

        [ButtonElement("Stalwart Shell", "Reset Defaults", "")]
        public void ResetStalwartShell()
        {
			charm4NotchCost = defaultCharm4NotchCost;
			stalwartShellIFrames = defaultStalwartShellIFrames;
			stalwartShellParryIFrames = defaultStalwartShellParryIFrames;
			stalwartShellLifeblood = defaultStalwartShellLifeblood;
			stalwartShellCooldown = defaultStalwartShellCooldown;
			stalwartShellBaldurBlockIFrames = defaultStalwartShellBaldurBlockIFrames;
			stalwartShellDefendersCrestCooldownReduction = defaultStalwartShellDefendersCrestCooldownReduction;
			stalwartShellLifebloodHeartCooldownReduction = defaultStalwartShellLifebloodHeartCooldownReduction;
			stalwartShellJonisDamageReduction = defaultStalwartShellJonisDamageReduction;
			//stalwartShellRecoil = defaultStalwartShellRecoil;
        }

//	Steady Body
        [SliderIntElement("Steady Body", "Notch Cost", 0, 5)]
        public int charm14NotchCost = defaultCharm14NotchCost;
		
        [InputIntElement("Steady Body", "Focus Cost", 0, 231)]
		public int steadyBodyFocusCost = defaultSteadyBodyFocusCost;
		
        [InputFloatElement("Steady Body", "Damage Knockback", 0f, 2f)]
		public float steadyBodyKnockback = defaultSteadyBodyKnockback;
		
        [BoolElement("Steady Body", "Negate Hard Falls", "")]
		public bool steadyBodyNegateHardFall = defaultSteadyBodyNegateHardFall;
		
        [BoolElement("Steady Body", "Negate Nail Recoil", "")]
		public bool steadyBodyNegateNailRecoil = defaultSteadyBodyNegateNailRecoil;

        [InputIntElement("Steady Body", "Defender's Crest Impact Damage", 0, 200)]
		public int steadyBodyDefendersCrestImpactDamage = defaultSteadyBodyDefendersCrestImpactDamage;
		
        [InputIntElement("Steady Body", "Shape of Unn Focus Cost", 0, 231)]
		public int steadyBodyShapeOfUnnFocusCost = defaultSteadyBodyShapeOfUnnFocusCost;

        [ButtonElement("Steady Body", "Reset Defaults", "")]
        public void ResetSteadyBody()
        {
			charm14NotchCost = defaultCharm14NotchCost;
			steadyBodyFocusCost = defaultSteadyBodyFocusCost;
			steadyBodyKnockback = defaultSteadyBodyKnockback;
			steadyBodyNegateHardFall = defaultSteadyBodyNegateHardFall;
			steadyBodyNegateNailRecoil = defaultSteadyBodyNegateNailRecoil;
			steadyBodyDefendersCrestImpactDamage = defaultSteadyBodyDefendersCrestImpactDamage;
			steadyBodyShapeOfUnnFocusCost = defaultSteadyBodyShapeOfUnnFocusCost;
        }

//	Thorns of Agony
        [SliderIntElement("Thorns of Agony", "Notch Cost", 0, 5)]
        public int charm12NotchCost = defaultCharm12NotchCost;

        [InputFloatElement("Thorns of Agony", "Thorns Damage", 0f, 50f)]
        public float thornsOfAgonyBaseDamage = defaultThornsOfAgonyBaseDamage;

        [InputFloatElement("Thorns of Agony", "+X Damage per Mask", 0f, 2f)]
		public float thornsOfAgonyDamageIncrease = defaultThornsOfAgonyDamageIncrease;

        [InputFloatElement("Thorns of Agony", "Thorns Radius", 0f, 2f)]
		public float thornsOfAgonyRadius = defaultThornsOfAgonyRadius;

        [InputFloatElement("Thorns of Agony", "Baldur Shell Block Damage (flat)", 0f, 50f)]
		public float thornsOfAgonyBaldurBlockDamage = defaultThornsOfAgonyBaldurBlockDamage;

        [InputFloatElement("Thorns of Agony", "Baldur Shell Block Radius Increase (%)", 0f, 1f)]
		public float thornsOfAgonyBaldurBlockRadius = defaultThornsOfAgonyBaldurBlockRadius;

        [InputFloatElement("Thorns of Agony", "Defender's Crest +X Dmg/Mask", 0f, 2f)]
		public float thornsOfAgonyDefendersCrestDamage = defaultThornsOfAgonyDefendersCrestDamage;

        [InputFloatElement("Thorns of Agony", "Fury of the Fallen +X Dmg/Mask", 0f, 5f)]
		public float thornsOfAgonyFotFDamage = defaultThornsOfAgonyFotFDamage;

        [InputFloatElement("Thorns of Agony", "Stalwart Shell Radius Increase (%)", 0f, 1f)]
		public float thornsOfAgonyStalwartShellRadius = defaultThornsOfAgonyStalwartShellRadius;

        [InputFloatElement("Thorns of Agony", "Steady Body +X Dmg/Mask", 0f, 2f)]
		public float thornsOfAgonySteadyBodyDamage = defaultThornsOfAgonySteadyBodyDamage;

        [InputFloatElement("Thorns of Agony", "Steady Body Radius Increase (%)", 0f, 1f)]
		public float thornsOfAgonySteadyBodyRadius = defaultThornsOfAgonySteadyBodyRadius;

        [BoolElement("Thorns of Agony", "Flukenest Spray", "")]
		public bool thornsOfAgonyFlukenestSpray = defaultThornsOfAgonyFlukenestSpray;

        [ButtonElement("Thorns of Agony", "Reset Defaults", "")]
        public void ResetThornsOfAgony()
        {
			charm12NotchCost = defaultCharm12NotchCost;
			thornsOfAgonyBaseDamage = defaultThornsOfAgonyBaseDamage;
			thornsOfAgonyDamageIncrease = defaultThornsOfAgonyDamageIncrease;
			thornsOfAgonyRadius = defaultThornsOfAgonyRadius;
			thornsOfAgonyBaldurBlockDamage = defaultThornsOfAgonyBaldurBlockDamage;
			thornsOfAgonyBaldurBlockRadius = defaultThornsOfAgonyBaldurBlockRadius;
			thornsOfAgonyDefendersCrestDamage = defaultThornsOfAgonyDefendersCrestDamage;
			thornsOfAgonyFotFDamage = defaultThornsOfAgonyFotFDamage;
			thornsOfAgonyStalwartShellRadius = defaultThornsOfAgonyStalwartShellRadius;
			thornsOfAgonySteadyBodyDamage = defaultThornsOfAgonySteadyBodyDamage;
			thornsOfAgonySteadyBodyRadius = defaultThornsOfAgonySteadyBodyRadius;
			thornsOfAgonyFlukenestSpray = defaultThornsOfAgonyFlukenestSpray;
        }

//	Wayward Compass
        [SliderIntElement("Wayward Compass", "Notch Cost", 0, 5)]
        public int charm2NotchCost = defaultCharm2NotchCost;

        [ButtonElement("Wayward Compass", "Reset Defaults", "")]
        public void ResetWaywardCompass()
        {
            charm2NotchCost = defaultCharm2NotchCost;
        }

//	Weaversong
        [SliderIntElement("Weaversong", "Notch Cost", 0, 5)]
        public int charm39NotchCost = defaultCharm39NotchCost;

        [InputIntElement("Weaversong", "Weaverling Count", 1, 12)]
        public int weaversongCount = defaultWeaversongCount;

        [InputIntElement("Weaversong", "Contact Damage", 0, 50)]
        public int weaversongDamage = defaultWeaversongDamage;

        [InputIntElement("Weaversong", "Soul Gain", 0, 231)]
        public int weaversongSoulGain = defaultWeaversongSoulGain;

        [InputFloatElement("Weaversong", "Minimum Speed", 0f, 36f)]
        public float weaversongSpeedMin = defaultWeaversongSpeedMin;

        [InputFloatElement("Weaversong", "Maximum Speed", 0f, 36f)]
        public float weaversongSpeedMax = defaultWeaversongSpeedMax;

        [InputIntElement("Weaversong", "Gathering Swarm Weaverling Count", 1, 12)]
		public int weaversongGatheringSwarmCount = defaultWeaversongGatheringSwarmCount;

        [InputIntElement("Weaversong", "Gathering Swarm Damage Reduction", 0, 50)]
		public int weaversongGatheringSwarmDamageReduction = defaultWeaversongGatheringSwarmDamageReduction;

        [InputIntElement("Weaversong", "Flukenest Damage Increase", 0, 50)]
		public int weaversongFlukenestDamageIncrease = defaultWeaversongFlukenestDamageIncrease;

        [InputIntElement("Weaversong", "Flukenest Soul Reduction", 0, 231)]
		public int weaversongFlukenestSoulReduction = defaultWeaversongFlukenestSoulReduction;

        [InputIntElement("Weaversong", "Grubsong Soul Gain", 0, 231)]
        public int weaversongGrubsongSoulGain = defaultWeaversongGrubsongSoulGain;

        [InputIntElement("Weaversong", "Carefree Melody Damage Increase", 0, 50)]
		public int weaversongCarefreeMelodyDamageIncrease = defaultWeaversongCarefreeMelodyDamageIncrease;

        [InputIntElement("Weaversong", "Carefree Melody Soul Gain", 0, 231)]
        public int weaversongCarefreeMelodySoulGain = defaultWeaversongCarefreeMelodySoulGain;

        [InputIntElement("Weaversong", "Sprintmaster Speed Increase (%)", 0, 500)]
        public int weaversongSprintmasterSpeedIncrease = defaultWeaversongSprintmasterSpeedIncrease;

        [ButtonElement("Weaversong", "Reset Defaults", "")]
        public void ResetWeaversong()
        {
			charm39NotchCost = defaultCharm39NotchCost;
			weaversongCount = defaultWeaversongCount;
			weaversongDamage = defaultWeaversongDamage;
			weaversongSoulGain = defaultWeaversongSoulGain;
			weaversongSpeedMin = defaultWeaversongSpeedMin;
			weaversongSpeedMax = defaultWeaversongSpeedMax;
			weaversongGatheringSwarmCount = defaultWeaversongGatheringSwarmCount;
			weaversongGatheringSwarmDamageReduction = defaultWeaversongGatheringSwarmDamageReduction;
			weaversongFlukenestDamageIncrease = defaultWeaversongFlukenestDamageIncrease;
			weaversongFlukenestSoulReduction = defaultWeaversongFlukenestSoulReduction;
			weaversongGrubsongSoulGain = defaultWeaversongGrubsongSoulGain;
			weaversongCarefreeMelodyDamageIncrease = defaultWeaversongCarefreeMelodyDamageIncrease;
			weaversongCarefreeMelodySoulGain = defaultWeaversongCarefreeMelodySoulGain;
            weaversongSprintmasterSpeedIncrease = defaultWeaversongSprintmasterSpeedIncrease;
        }

        #endregion

////////////////////////////////////////////////////////////////
//	RESET METHODS
        #region ResetMethods
        //	FIX CHARM NOTCHES
        public void FixCharmNotches()
        {
            charm36NotchCost = PlayerDataAccess.royalCharmState == 4 ? charmVoidHeartNotchCost : charmKingsoulNotchCost;
            charm40NotchCost = PlayerDataAccess.grimmChildLevel == 5 ? charmCarefreeMelodyNotchCost : charmGrimmchildNotchCost;

            PlayerDataAccess.charmSlotsFilled = 0;

            if (PlayerDataAccess.equippedCharms.Contains(1)) PlayerDataAccess.charmSlotsFilled += charm1NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(2)) PlayerDataAccess.charmSlotsFilled += charm2NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(3)) PlayerDataAccess.charmSlotsFilled += charm3NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(4)) PlayerDataAccess.charmSlotsFilled += charm4NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(5)) PlayerDataAccess.charmSlotsFilled += charm5NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(6)) PlayerDataAccess.charmSlotsFilled += charm6NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(7)) PlayerDataAccess.charmSlotsFilled += charm7NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(8)) PlayerDataAccess.charmSlotsFilled += charm8NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(9)) PlayerDataAccess.charmSlotsFilled += charm9NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(10)) PlayerDataAccess.charmSlotsFilled += charm10NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(11)) PlayerDataAccess.charmSlotsFilled += charm11NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(12)) PlayerDataAccess.charmSlotsFilled += charm12NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(13)) PlayerDataAccess.charmSlotsFilled += charm13NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(14)) PlayerDataAccess.charmSlotsFilled += charm14NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(15)) PlayerDataAccess.charmSlotsFilled += charm15NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(16)) PlayerDataAccess.charmSlotsFilled += charm16NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(17)) PlayerDataAccess.charmSlotsFilled += charm17NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(18)) PlayerDataAccess.charmSlotsFilled += charm18NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(19)) PlayerDataAccess.charmSlotsFilled += charm19NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(20)) PlayerDataAccess.charmSlotsFilled += charm20NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(21)) PlayerDataAccess.charmSlotsFilled += charm21NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(22)) PlayerDataAccess.charmSlotsFilled += charm22NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(23)) PlayerDataAccess.charmSlotsFilled += charm23NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(24)) PlayerDataAccess.charmSlotsFilled += charm24NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(25)) PlayerDataAccess.charmSlotsFilled += charm25NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(26)) PlayerDataAccess.charmSlotsFilled += charm26NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(27)) PlayerDataAccess.charmSlotsFilled += charm27NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(28)) PlayerDataAccess.charmSlotsFilled += charm28NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(29)) PlayerDataAccess.charmSlotsFilled += charm29NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(30)) PlayerDataAccess.charmSlotsFilled += charm30NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(31)) PlayerDataAccess.charmSlotsFilled += charm31NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(32)) PlayerDataAccess.charmSlotsFilled += charm32NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(33)) PlayerDataAccess.charmSlotsFilled += charm33NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(34)) PlayerDataAccess.charmSlotsFilled += charm34NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(35)) PlayerDataAccess.charmSlotsFilled += charm35NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(36)) PlayerDataAccess.charmSlotsFilled += charm36NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(37)) PlayerDataAccess.charmSlotsFilled += charm37NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(38)) PlayerDataAccess.charmSlotsFilled += charm38NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(39)) PlayerDataAccess.charmSlotsFilled += charm39NotchCost;
            if (PlayerDataAccess.equippedCharms.Contains(40)) PlayerDataAccess.charmSlotsFilled += charm40NotchCost;

            PlayerDataAccess.overcharmed = (PlayerDataAccess.charmSlotsFilled > PlayerDataAccess.charmSlots);
        }

//	RESET ALL DEFAULTS
        public void ResetAllDefaults()
        {
			ResetModSettings();
			ResetMovementSettings();
			ResetDamageFocusSettings();
			ResetNailSettings();
			ResetNailArtSettings();
			ResetSpellSettings();
			ResetDreamNailSettings();
			ResetBaldurShell();
			ResetCarefreeMelody();
			ResetDashmaster();
			ResetDeepFocus();
			ResetDefendersCrest();
			ResetDreamWielder();
			ResetDreamshield();
			ResetFlukenest();
			ResetFragileCharms();
			ResetFuryOfTheFallen();
			ResetGatheringSwarm();
			ResetGlowingWomb();
			ResetGrimmchild();
			ResetGrubberflysElegy();
			ResetGrubsong();
			ResetHeavyBlow();
			ResetHiveblood();
			ResetJonisBlessing();
			ResetKingsoulVoidHeart();
			ResetLifebloodCore();
			ResetLifebloodHeart();
			ResetLongnail();
			ResetMarkOfPride();
			ResetNailmastersGlory();
			ResetQuickFocus();
			ResetQuickSlash();
			ResetShamanStone();
			ResetShapeOfUnn();
			ResetSharpShadow();
			ResetSoulCatcher();
			ResetSoulEater();
			ResetSpellTwister();
			ResetSporeShroom();
			ResetSprintmaster();
			ResetStalwartShell();
			ResetSteadyBody();
			ResetThornsOfAgony();
			ResetWaywardCompass();
			ResetWeaversong();
			FixCharmNotches();
        }
		#endregion
    }
}
