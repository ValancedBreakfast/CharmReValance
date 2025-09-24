using GlobalEnums;
using HKMirror;
using HKMirror.Hooks.ILHooks;
using HKMirror.Reflection.SingletonClasses;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using MonoMod.Cil;
using Satchel;
using SFCore;
using SFCore.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharmReValance
{
    public class CharmReValance : Mod, ICustomMenuMod, ILocalSettings<LocalSettings>
    {
////////////////////////////////////////////////////////////////
//	BOILERPLATE
        #region Boilerplate
        private static CharmReValance? _instance;
        internal static CharmReValance Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(CharmReValance)} was never constructed");
                }
                return _instance;
            }
        }
        public static LocalSettings LS { get; private set; } = new();
        public void OnLoadLocal(LocalSettings s) => LS = s;
        public LocalSettings OnSaveLocal() => LS;
        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();
        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? _) => ModMenu.CreateMenuScreen(modListMenu);
        public bool ToggleButtonInsideMenu => false;
        public CharmReValance() : base("CharmReValance")
        {
            _instance = this;
        }
        #endregion


////////////////////////////////////////////////////////////////
//	CUSTOM VARIABLES
        #region Custom Variables
        private bool charmsReordered = false;
        private float nailCooldown = LS.regularNailCooldown;
        private float cdashChargeTime = LS.regularCDashChargeTime;

        private bool baldurShellBlocked = false;
        private int baldurGreedShellBlocks = 0;
        private GameObject _smallGeoPrefab;
        private GameObject _mediumGeoPrefab;
        private GameObject _largeGeoPrefab;
        private float carefreeMelodyChance = 0f;
        private GameObject dreamshield = null;
        private Rotate dreamshieldRotation = null;
        private bool dreamshieldBlock = false;
		private bool furyActive = false;
        private int grubSoulGain = LS.grubsongSoulGain;
        private bool hivebloodRegenMore = false;
        private int hivebloodRegenLimit = 1;
        private int hivebloodLocalMaxHP = PlayerDataAccess.maxHealth;
        private int hivebloodRegenTimes = 0;
        private int hivebloodRegensLeft = 0;
        private List<PlayMakerFSM> hivebloodJoniMasks = new List<PlayMakerFSM>();
        private int lifebloodCoreCost = LS.lifebloodCoreCost;
        private float lifebloodCoreTimer = 0f;
        private int lifebloodCoreMax = LS.lifebloodCoreLifeblood;
        private bool shapeOfUnnVessel = PlayerDataAccess.equippedCharm_28;
        private bool stalwartShellRegen = false;
        private float stalwartShellTimer = 0f;
        private bool meteorDrop = false;
        #endregion

////////////////////////////////////////////////////////////////
//  DEBUG FUNCTIONS
        #region Debug Functions
        private void SlashToLog(AttackDirection direction)
        {
            if (direction.ToString() == "upward")
            {
                Log("--- Slash Log ---");
                //Log("nailCooldown = " + nailCooldown);
                PlayerDataAccess.heartPieces = 3;
                PlayerDataAccess.slyShellFrag1 = false;
                PlayerDataAccess.slyShellFrag2 = false;
                PlayerDataAccess.slyShellFrag3 = false;
                PlayerDataAccess.slyShellFrag4 = false;
                PlayerDataAccess.vesselFragments = 2;
                PlayerDataAccess.slyVesselFrag1 = false;
                PlayerDataAccess.slyVesselFrag2 = false;

                //Log("health = " + PlayerDataAccess.health); //  current white masks (always 1 w/ jonis)
                //Log("healthBlue = " + PlayerDataAccess.healthBlue); //  current blue masks (including jonis)
                //Log("joniHealthBlue = " + PlayerDataAccess.joniHealthBlue); //  full health masks -1 (0 w/o jonis)
                //Log("damagedBlue = " + PlayerDataAccess.damagedBlue);   //  marked true after losing a blue mask, but then stays true forever?
                //Log("maxHealth = " + PlayerDataAccess.maxHealth);   //  current max health (always 1 w/ jonis)
                //Log("maxHealthBase = " + PlayerDataAccess.maxHealthBase);   //  non-charm max white masks
                //Log("maxHealthCap = " + PlayerDataAccess.maxHealthCap); //  always 9. was able to manually change in save file
                //Log("prevHealth = " + PlayerDataAccess.prevHealth); //  on bench, gets set to maxHealth before maxHealth is recalculated

                //Log("heartPieceCollected = " + PlayerDataAccess.heartPieceCollected);   //  has a mask shard ever been collected
                //Log("heartPieceMax = " + PlayerDataAccess.heartPieceMax);   //  flag when game thinks you have all mask shards
                //Log("heartPieces = " + PlayerDataAccess.heartPieces);   //  number of shards of current unfinished mask (0-3)

                //Log("MPCharge = " + PlayerDataAccess.MPCharge); //  current soul in primary vessel
                //Log("MPReserve = " + PlayerDataAccess.MPReserve);   //  current sum soul in all reserve vessels
                //Log("MPReserveCap = " + PlayerDataAccess.MPReserveCap); //  set to 99. was able to manually change in save file
                //Log("MPReserveMax = " + PlayerDataAccess.MPReserveMax); //  current max soul in reserve vessels
                //Log("maxMP = " + PlayerDataAccess.maxMP);   //  set to 99. resets to 99 when soul is gained

                //Log("vesselFragmentCollected = " + PlayerDataAccess.vesselFragmentCollected);   //  has a vessel fragment ever been collected
                //Log("vesselFragmentMax = " + PlayerDataAccess.vesselFragmentMax);   //  flag when game thinks you have all vessel fragments
                //Log("vesselFragments = " + PlayerDataAccess.vesselFragments);   //  number of fragments of current unfinished vessel (0-2)

                //ChangeInventoryCharmOrder();
            }
        }
        private void DebugHitInstance(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            Log("HitInstance " + hitInstance.Source.name + " dealing " + hitInstance.DamageDealt + " damage");
            Log("hitInstance.Source.gameObject.transform.parent.name: " + hitInstance.Source.gameObject.transform.parent.name);

            orig(self, hitInstance);
        }

        #endregion


////////////////////////////////////////////////////////////////
//	INITIALIZE
        public override void Initialize()
        {
            Log("Initializing");

////////////////////////////////////////////////////////////////
//  DEBUG HOOKS
            #region Debug Hooks
            //ModHooks.AfterAttackHook += SlashToLog;
            //On.HealthManager.TakeDamage += DebugHitInstance;
            #endregion

////////////////////////////////////////////////////////////////
//  ABILITY HOOKS
            #region Ability Hooks

//  Movement 
            On.HeroController.CharmUpdate += CrystalDashChargeTime;
            On.HutongGames.PlayMaker.Actions.iTweenMoveBy.OnEnter += SDCrystalGenTween;
            On.HutongGames.PlayMaker.Actions.TakeDamage.OnEnter += CrystalDashDamage;

//  Damage & Focus
            ModHooks.AfterTakeDamageHook += IFramesNegateHazardDamage;
            On.HutongGames.PlayMaker.Actions.SetFloatValue.OnEnter += FocusSpeed;
            On.HutongGames.PlayMaker.Actions.SetIntValue.OnEnter += FocusHealAmount;

//  Nail
            ModHooks.GetPlayerIntHook += RegularNailDamage;
            On.HeroController.DoAttack += CalculateNailCooldown;
            ILHeroController.orig_DoAttack += SetNailCooldown;
            IL.HeroController.Attack += SetNailDuration;
            On.HutongGames.PlayMaker.Actions.TakeDamage.OnEnter += SetKnockbacks;

//  Nail Art
            On.HutongGames.PlayMaker.Actions.TakeDamage.OnEnter += NailArtDamage;

//  Spells

//  Dream Nail

            #endregion

////////////////////////////////////////////////////////////////
//  CHARM HOOKS
            #region Charm Hooks

//  Inventory Charm Order
            On.HutongGames.PlayMaker.Actions.DestroyAllChildren.OnEnter += SetInvCharmOrder;

//	Notch Costs			
            On.GameManager.CalculateNotchesUsed += ChangeNotchCosts;

//  Charm Descriptions
            ModHooks.LanguageGetHook += ChangeCharmDescriptions;

//	Baldur Shell
            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += BaldurShellContFocus;
            On.PlayerData.MaxHealth += BaldurShellBlocks;
            On.BeginRecoil.OnEnter += BaldurShellKnockback;
            On.HeroController.AddHealth += BaldurShellOverheal;
            On.HutongGames.PlayMaker.Actions.PlayerDataIntAdd.OnEnter += BaldurShellGreedShell;
            GetGeoPrefabs();

//	Carefree Melody
            On.HeroController.TakeDamage += CarefreeMelodyBlockAndHeal;
            IL.HeroController.TakeDamage += CarefreeMelodyFixedChance;

//	Dashmaster
            IL.HeroController.HeroDash += DashmasterChanges;

//	Defender's Crest
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += DungCloudSettings;
            On.HutongGames.PlayMaker.Actions.SetPosition.OnEnter += DungCloudScaling;
            On.HutongGames.PlayMaker.Actions.SpawnObjectFromGlobalPoolOverTime.OnUpdate += CloudFrequency;
            IL.ShopItemStats.OnEnable += DefendersCrestCostReduction;

//	Dream Wielder
            IL.EnemyDreamnailReaction.RecieveDreamImpact += DreamWielderSoul;
            IL.EnemyDeathEffects.EmitEssence += DreamWielderEssence;
            On.HutongGames.PlayMaker.Actions.ActivateGameObject.OnEnter += DreamNailSize;
            On.HutongGames.PlayMaker.Actions.Tk2dPlayAnimation.OnEnter += DreamNailSpriteSize;

//	Dreamshield
            On.EnemyDreamnailReaction.RecieveDreamImpact += DreamshieldOverheal;
            On.HutongGames.PlayMaker.Actions.SendEventByName.OnEnter += DreamshieldDamageAndKnockback;
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += DreamshieldReformationTime;

            On.PlayMakerFSM.OnEnable += DreamshieldObjectPrep;
            On.HeroController.Update += DreamshieldFollowBehind;
            On.HeroController.TakeDamage += DreamshieldBlockDetection;
            ModHooks.AfterTakeDamageHook += DreamshieldBlocks;
            IL.HeroController.TakeDamage += DreamshieldBlockNoHitEffect;
            On.HutongGames.PlayMaker.Actions.SetScale.OnEnter += DreamshieldScaling;

            //On.HutongGames.PlayMaker.Actions.SendEvent.OnEnter += DreamshieldDebugLogging;
            On.PlayMakerFSM.OnEnable += DreamshieldFSMPrep;

//	Flukenest
            On.HutongGames.PlayMaker.Actions.FlingObjectsFromGlobalPool.OnEnter += FlukeCount;
            On.HealthManager.TakeDamage += FlukenestDCContactDamage;
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += FlukenestDefendersCrestDurationAndDamage;
            IL.SpellFluke.OnEnable += FlukenestEnableHook;

//	Fragile Charms
            On.HutongGames.PlayMaker.Actions.PlayerDataBoolTest.OnEnter += FragileCharmsBreak;
            IL.HealthManager.Die += GreedGeoIncrease;
            On.HeroController.AddGeo += GeoGrantsSoul;
            ILHeroController.orig_CharmUpdate += SetFragileHeartMasks;
            ModHooks.GetPlayerIntHook += StrengthNailDamageIncrease;
            On.HutongGames.PlayMaker.Actions.FloatMultiply.OnEnter += IgnoreVanillaStrengthDamage;

//	Fury of the Fallen
            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += FotFHPRequirements;
            On.HeroController.NearBench += FotFActiveOvercharmed;
            On.HutongGames.PlayMaker.Actions.SetFsmFloat.OnEnter += FotFIgnoreVanillaNailScaling;
            On.HutongGames.PlayMaker.Actions.FloatMultiply.OnEnter += FotFIgnoreVanillaNailArtScaling;
			On.NailSlash.SetFury += SetFuryActive;
            On.HutongGames.PlayMaker.Actions.BoolAllTrue.OnEnter += FotFIgnoreJonis;
            On.HutongGames.PlayMaker.Actions.PlayerDataBoolTest.OnEnter += FotFIgnoreJonis2;

//	Glowing Womb
            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += HatchlingSpawnRequirements;
            On.KnightHatchling.OnEnable += HatchlingDamage;
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += GlowingWombDungCloudDamage;
            On.HutongGames.PlayMaker.Actions.SetScale.OnEnter += GlowingWombDungRadius;
            IL.KnightHatchling.OnEnable += HatchlingFotFSettings;

//	Grimmchild
            On.HutongGames.PlayMaker.Actions.SetFloatValue.OnEnter += GrimmchildAttackTimer;
            On.HutongGames.PlayMaker.Actions.RandomFloat.OnEnter += GrimmchildAttackTimer2;
            On.HutongGames.PlayMaker.Actions.SetIntValue.OnEnter += GrimmchildDamage;
            On.HutongGames.PlayMaker.Actions.FireAtTarget.OnEnter += GrimmchildProjectileSpeed;
            On.HutongGames.PlayMaker.Actions.CallMethodProper.OnEnter += GrimmchildDetectRadius;
            On.PlayMakerFSM.OnEnable += RemoveGrimmballTerrainCollision;

//	Grubberfly's Elegy
            IL.HeroController.Attack += GrubberflysShootBeam;
            ModHooks.GetPlayerIntHook += GrubberflysElegyDamage;
            On.HeroController.TakeDamage += GrubberflysElegyAllowJoniBeam;
            On.HutongGames.PlayMaker.Actions.SetVelocity2d.OnEnter += GrubberflyBeamVelocity;
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += GrubberflyBeamDuration;
            On.HealthManager.TakeDamage += GrubberflysSoulGain;
            On.HutongGames.PlayMaker.Actions.FloatMultiply.OnEnter += IgnoreVanillaGEFotFDamage;
            IL.HeroController.Attack += IgnoreVanillaGEMoPSize;

//	Grubsong
            ModHooks.CharmUpdateHook += GrubsongSetSoulGain;
            IL.HeroController.TakeDamage += GrubsongSoulChanges;

//	Heavy Blow
			ModHooks.GetPlayerIntHook += HeavyBlowNailDamageIncrease;
            On.HutongGames.PlayMaker.Actions.IntOperator.OnEnter += HeavyBlowStagger;
            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += HeavyBlowComboStaggerFix;
            On.HutongGames.PlayMaker.Actions.IntOperator.OnEnter += HeavyBlowEnviroHit;
            On.HutongGames.PlayMaker.Actions.IntSwitch.OnEnter += HeavyBlowOneWayHit;
            On.HutongGames.PlayMaker.Actions.FlingObjectsFromGlobalPool.OnEnter += HeavyBlowGeoRockFling;

//	Hiveblood
            On.HeroController.MaxHealth += SetHivebloodValues;
            On.HutongGames.PlayMaker.Actions.FloatCompare.OnEnter += HivebloodCooldowns;
            On.HeroController.AddHealth += HivebloodUpdateOnHeal;
            On.PlayerData.TakeHealth += HivebloodUpdateOnDamage;
            On.HutongGames.PlayMaker.FsmState.OnEnter += HivebloodCanRegenMore;
            On.PlayMakerFSM.OnEnable += EditBlueHealthHiveFSM;
            On.HutongGames.PlayMaker.Actions.SendEventByName.OnEnter += HivebloodUpdateOnJonisRegen;

//	Joni's Blessing
            ILHeroController.orig_CharmUpdate += JonisBlessingLifeblood;

//	Kingsoul
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += KingsoulTimer;
            On.HutongGames.PlayMaker.Actions.SendMessageV2.DoSendMessage += KingsoulSoul;
            On.PlayMakerFSM.OnEnable += VoidHeartSoulRegen;

//	Lifeblood Heart/Core
            ModHooks.BlueHealthHook += SetBaseLifeblood;
            ModHooks.HeroUpdateHook += LifebloodCoreRegen;
            ILPlayerData.orig_UpdateBlueHealth += IgnoreVanillaBlueHearts;

//	Longnail/Mark of Pride
            On.PlayMakerFSM.OnEnable += OnFsmEnable;
            On.HutongGames.PlayMaker.Actions.SendMessage.OnEnter += WallSlashSizeScale;
            IL.NailSlash.StartSlash += NailSlashSizeScale;
            ModHooks.GetPlayerIntHook += MarkOfPrideNailDamageIncrease;

//	Nailmaster's Glory
            ILHeroController.orig_CharmUpdate += SetNailArtChargeTime;
            On.HealthManager.TakeDamage += NailArtPiercingSoulGain;

//	Quick/Deep Focus
            On.HutongGames.PlayMaker.Actions.FloatMultiply.OnEnter += DeepFocusScaling;

//	Quick Slash

//	Shaman Stone
            On.HutongGames.PlayMaker.Actions.SetScale.OnEnter += ShamanStoneVengefulSpiritScaling;
            On.HutongGames.PlayMaker.Actions.FloatMultiply.OnEnter += ShamanStoneShadeSoulScaling;
            On.HutongGames.PlayMaker.Actions.SetFsmInt.OnEnter += ShamanStoneDamage;
            On.HutongGames.PlayMaker.Actions.FloatCompare.OnEnter += ShamanStoneQMegaDamage;

//	Shape of Unn
            On.HutongGames.PlayMaker.Actions.SetFloatValue.OnEnter += SlugSpeeds;
            ModHooks.GetPlayerIntHook += ShapeOfUnnReserveIncrease;
            ModHooks.SetPlayerIntHook += ShapeOfUnnVesselGainFix;
            ModHooks.CharmUpdateHook += ShapeOfUnnUpdateVesselUI;

//	Sharp Shadow
            On.HutongGames.PlayMaker.Actions.FloatMultiplyV2.OnEnter += SharpShadowDashMasterDamageScaling;
            On.PlayMakerFSM.OnEnable += SharpShadowHurtBoxSize;
            On.HutongGames.PlayMaker.Actions.TakeDamage.OnEnter += SharpShadowDamage;
            ILHeroController.OrigDashVector += SetSharpShadowDashSpeed;
            On.HealthManager.TakeDamage += SharpShadowSoulGain;
            On.HeroController.CancelDash += SharpShadowStalwartIFrames;
            IL.HeroController.HeroDash += SharpShadowVolume;

//	Soul Catcher/Eater
            IL.HeroController.SoulGain += SoulCharmChanges;

//	Spell Twister
            On.HutongGames.PlayMaker.Actions.IntCompare.OnEnter += SpellTwisterSpellCosts;

//	Spore Shroom
            On.HutongGames.PlayMaker.Actions.Wait.OnEnter += SporeShroomDamageDuration;
            On.HutongGames.PlayMaker.Actions.SetScale.OnEnter += SporeShroomRadius;
            On.HutongGames.PlayMaker.Actions.ActivateGameObject.OnEnter += SporeShroomVisuals;
            //On.HutongGames.PlayMaker.FsmState.OnEnter += SporeShroomDisableDungCloud;
            On.HutongGames.PlayMaker.Actions.SetBoolValue.OnEnter += SporeShroomDamageReset;

//	Sprintmaster
            IL.HeroController.Move += SprintmasterSpeed;

//	Stalwart Shell
            ILHeroController.StartRecoil += SetDamageIFrames;
            On.HeroController.CharmUpdate += SetParryIFrames;
            ModHooks.HeroUpdateHook += StalwartShellRegenTimer;
            ModHooks.AfterTakeDamageHook += StalwartShellReactivate;
            On.HutongGames.PlayMaker.Actions.PlayerDataIntAdd.OnEnter += BaldurShellBlockFlag;
            On.PlayerData.TakeHealth += StalwartShellJonisDamageReduction;

//	Steady Body
            ModHooks.CharmUpdateHook += SteadyBodyFocusCost;
            ILHeroController.orig_Update += SetDamageKnockback;
            On.HeroController.ShouldHardLand += SteadyBodyNegateHardFall;
            On.HeroController.TakeDamage += SteadyBodyMeteorDrop;

//	Thorns of Agony
            On.HutongGames.PlayMaker.Actions.SetFsmInt.OnEnter += ThornsOfAgonyDamage;
            On.HutongGames.PlayMaker.Actions.ActivateGameObject.OnEnter += ThornsOfAgonyRadius;

//	Weaversong
            On.HutongGames.PlayMaker.Actions.IntOperator.OnEnter += WeaverlingAttack;
            On.HutongGames.PlayMaker.Actions.CallMethodProper.OnEnter += IgnoreVanillaWeaversongSoulGain;
            On.HutongGames.PlayMaker.Actions.RandomFloat.OnEnter += WeaversongSpeeds;
            On.HutongGames.PlayMaker.Actions.SetFloatValue.OnEnter += WeaversongSprintmasterSpeed;
            On.PlayMakerFSM.OnEnable += WeaverlingSpawner;
            #endregion

            Log("Initialized");
        }


        private bool stopMovingMyComments = true;

////////////////////////////////////////////////////////////////
//	CHANGES

////////////////////////////////////////////////////////////////
//  ABILITY CHANGES
        #region Ability Changes

//  Movement Changes
        #region Movement Changes
        private void CrystalDashChargeTime(On.HeroController.orig_CharmUpdate orig, HeroController self)
        {
            cdashChargeTime = LS.regularCDashChargeTime
                * (PlayerDataAccess.equippedCharm_7 ? LS.quickFocusCDashTimeMult : 1f)
                * (PlayerDataAccess.equippedCharm_34 ? LS.deepFocusCDashTimeMult : 1f);
            self.gameObject.LocateMyFSM("Superdash").GetFsmFloatVariable("Charge Time").Value = cdashChargeTime;
            //Log("Crystal Dash charge time: " + cdashChargeTime);

            orig(self);
        }
        private void SDCrystalGenTween(On.HutongGames.PlayMaker.Actions.iTweenMoveBy.orig_OnEnter orig, iTweenMoveBy self)
        {
            if (self.Fsm.Name == "superdash_crystal_gen" && PlayerDataAccess.equippedCharm_34)
            {
                self.time = cdashChargeTime;
            }

            orig(self);
        }
        private void CrystalDashDamage(On.HutongGames.PlayMaker.Actions.TakeDamage.orig_OnEnter orig, TakeDamage self)
        {
            if ((self.Fsm.GameObject.name == "SuperDash Damage" || self.Fsm.GameObject.name == "SD Burst") && self.Fsm.Name == "damages_enemy")
            {
                int damage = (int)Math.Round((PlayerDataAccess.equippedCharm_31 ? LS.dashmasterCDashDamage : LS.regularCDashDamage)
                    * (PlayerDataAccess.equippedCharm_34 ? LS.deepFocusCDashDamageMult : 1f), MidpointRounding.AwayFromZero);
                self.DamageDealt = damage;
            }

            orig(self);
        }
        #endregion

//  Damage & Focus Changes
        #region Damage & Focus Changes
        private int IFramesNegateHazardDamage(int hazardType, int damageAmount)
        {
            //  Negate hazard damage while you have i-frames
            if (HeroController.instance.cState.invulnerable && hazardType == 2)
            {
                //Log("Hazard damage negated by i-frames");
                return 0;
            }

            return damageAmount;
        }
        private void FocusSpeed(On.HutongGames.PlayMaker.Actions.SetFloatValue.orig_OnEnter orig, SetFloatValue self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name == "Set Focus Speed")
            {
                if (self.floatValue.Name == "Time Per MP Drain UnCH")
                {
                    self.floatValue.Value = LS.regularFocusTime / (float)(PlayerDataAccess.focusMP_amount);
                }
                else if (self.floatValue.Name == "Time Per MP Drain CH")
                {
                    self.floatValue.Value = LS.quickFocusFocusTime / (float)(PlayerDataAccess.focusMP_amount);
                }
            }

            orig(self);
        }
        private void FocusHealAmount(On.HutongGames.PlayMaker.Actions.SetIntValue.orig_OnEnter orig, SetIntValue self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name.StartsWith("Set HP Amount"))
            {
                if (self.State.ActiveActionIndex == 0)
                {
                    self.intValue.Value = LS.regularFocusHealing;
                }
                else if (self.State.ActiveActionIndex == 2)
                {
                    self.intValue.Value = LS.deepFocusHealing;
                }
            }

            orig(self);
        }
        #endregion

//  Nail Changes
        #region Nail Changes
        private int RegularNailDamage(string name, int orig)
        {
            if (name == "nailDamage")
            {
                orig = LS.regularNailDamageBase + (LS.regularNailDamageUpgrade * PlayerDataAccess.nailSmithUpgrades);
            }

            return orig;
        }
        private void CalculateNailCooldown(On.HeroController.orig_DoAttack orig, HeroController self)
        {
            nailCooldown = PlayerDataAccess.equippedCharm_15 ? LS.heavyBlowNailCooldownIncrease : LS.regularNailCooldown;
            List<float> cooldownReductions = new List<float>();
            if (PlayerDataAccess.equippedCharm_15 && PlayerDataAccess.equippedCharm_14)
                cooldownReductions.Add(LS.heavyBlowSteadyBodyNailCooldownReduction);
            if (PlayerDataAccess.equippedCharm_32)
                cooldownReductions.Add(LS.quickSlashNailCooldownReduction);
            if (PlayerDataAccess.equippedCharm_13)
                cooldownReductions.Add(LS.markOfPrideNailCooldownReduction);
            if (furyActive)
                cooldownReductions.Add(LS.furyOfTheFallenNailCooldownReduction);
            for (int i = 0; i < cooldownReductions.Count; i++)
            {
                nailCooldown -= (cooldownReductions[i] / (float)(i + 1));
            }
            //Log("Nail Cooldown: " + nailCooldown);

            orig(self);
        }
        private void SetNailCooldown(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("ATTACK_COOLDOWN_TIME_CH"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => nailCooldown);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("ATTACK_COOLDOWN_TIME"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => nailCooldown);
        }
        private void SetNailDuration(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("ATTACK_DURATION_CH"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => (nailCooldown + 0.39f) * 7f / 16f);    //  d = (c + 39) * 7 / 16

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("ATTACK_DURATION"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => (nailCooldown + 0.39f) * 7f / 16f);    //  d = (c + 39) * 7 / 16
        }
        private void SetKnockbacks(On.HutongGames.PlayMaker.Actions.TakeDamage.orig_OnEnter orig, TakeDamage self)
        {
            if (self.Fsm.GameObject.name.StartsWith("Hit ") && self.Fsm.Name == "damages_enemy")
            {
                //  Set scream knockback
                if (self.Fsm.GameObject.transform.parent.name.StartsWith("Scr Heads"))
                {
                    self.MagnitudeMultiplier = LS.regularHWKnockback;
                }
                //  Set cyclone slash knockback
                else if (self.Fsm.GameObject.transform.parent.parent.name.StartsWith("Cyclone "))
                {
                    self.MagnitudeMultiplier = LS.regularCycloneSlashKnockback;
                    //Log("Cyclone Slash knockback modified.");
                }
            }
            else if (self.Fsm.GameObject.name.StartsWith("Great ") && self.Fsm.Name == "damages_enemy")
            {
                //  Set great slash knockback
                self.MagnitudeMultiplier = (PlayerDataAccess.equippedCharm_15 ? LS.heavyBlowGreatSlashKnockback : LS.regularGreatSlashKnockback);
                //Log("Great Slash knockback modified.");
            }
            else if (self.Fsm.GameObject.name.StartsWith("Dash ") && self.Fsm.Name == "damages_enemy")
            {
                //  Set dash slash knockback
                self.MagnitudeMultiplier = (PlayerDataAccess.equippedCharm_15 ? LS.heavyBlowDashSlashKnockback : LS.regularDashSlashKnockback);
                //Log("Dash Slash knockback modified.");
            }

            orig(self);
        }
        #endregion

//  Nail Art Changes
        #region Nail Art Changes
        private void NailArtDamage(On.HutongGames.PlayMaker.Actions.TakeDamage.orig_OnEnter orig, TakeDamage self)
        {
            if (self.Fsm.Name == "damages_enemy")// && self.AttackType.Value == 0)
            {
                int nailDamage = PlayerDataAccess.nailDamage + (PlayerDataAccess.equippedCharm_15 ? LS.heavyBlowNailArtDamageIncrease : 0);
                float dmgMult = PlayerDataAccess.equippedCharm_26 ? LS.nailmastersGloryDamageIncrease : 1f;
                if (self.Fsm.GameObject.name == "Great Slash")
                {
                    self.DamageDealt = (int)Math.Round(nailDamage * LS.regularGreatSlashDamage * dmgMult, MidpointRounding.AwayFromZero);
                    if (LS.nailmastersGloryMoPPiercing && PlayerDataAccess.equippedCharm_26 && PlayerDataAccess.equippedCharm_13)
                        self.AttackType = 2;
                    else
                        self.AttackType = 0;
                }
                else if (self.Fsm.GameObject.name == "Dash Slash")
                {
                    self.DamageDealt = (int)Math.Round(nailDamage * LS.regularDashSlashDamage * dmgMult, MidpointRounding.AwayFromZero);
                    if (LS.nailmastersGloryMoPPiercing && PlayerDataAccess.equippedCharm_26 && PlayerDataAccess.equippedCharm_13)
                        self.AttackType = 2;
                    else
                        self.AttackType = 0;
                }
                else if (self.Fsm.GameObject.name.StartsWith("Hit "))
                {
                    var grandparent = self.Fsm.GameObject.transform.parent.parent;
                    if (grandparent != null)
                    {
                        if (grandparent.name == "Cyclone Slash")
                        {
                            self.DamageDealt = (int)Math.Round(nailDamage * LS.regularCycloneSlashDamage * dmgMult, MidpointRounding.AwayFromZero);
                            if (LS.nailmastersGloryMoPPiercing && PlayerDataAccess.equippedCharm_26 && PlayerDataAccess.equippedCharm_13)
                                self.AttackType = 2;
                            else
                                self.AttackType = 0;
                        }
                    }
                }
            }

            orig(self);
        }
        #endregion

//  Spell Changes
        #region Spell Changes
        #endregion

//  Dream Nail Changes
        #region Dream Nail Changes
        #endregion

        #endregion

////////////////////////////////////////////////////////////////
//  CHARM CHANGES
        #region Charm Changes

//  Inventory Charm Order
        #region Inventory Charm Order
        private void SetInvCharmOrder(On.HutongGames.PlayMaker.Actions.DestroyAllChildren.orig_OnEnter orig, DestroyAllChildren self)
        {
            if (self.Fsm.GameObject.name == "Charms" && self.Fsm.Name == "UI Charms" && self.State.Name == "Build Equipped"
                && charmsReordered != LS.playersetReorderCharms)
            {
                ChangeInventoryCharmOrder();
            }

            orig(self);
        }
        private void ChangeInventoryCharmOrder()
        {
            if (charmsReordered != LS.playersetReorderCharms)
            {
                var charmOrder = LocalSettings.oldInventoryCharmOrder;
                if (LS.playersetReorderCharms)
                {
                    charmOrder = LocalSettings.newInventoryCharmOrder;
                }
                var charmsInv = GameCameras.instance.hudCamera.gameObject.transform.Find("Inventory/Charms");
                if (charmsInv != null)
                {
                    for (int i = 1; i < charmOrder.Length; i++)
                    {
                        //Log("i = " + i);
                        var bb = charmsInv.transform.Find("Backboards/BB " + i.ToString()).GetComponent<InvCharmBackboard>();
                        //Log("bb orig charm num: " + bb.charmNum);
                        bb.charmNum = charmOrder[i];
                        bb.charmNumString = bb.charmNum.ToString();
                        bb.gotCharmString = "gotCharm_" + bb.charmNumString;
                        bb.newCharmString = "newCharm_" + bb.charmNumString;
                        //Log("bb new charm num: " + bb.charmNum);

                        var charm = charmsInv.transform.Find("Collected Charms/" + charmOrder[i].ToString()).transform;
                        //Log("charm go: " + charm.name);
                        //Log("charm orig pos: " + charm.localPosition.x + ", " + charm.localPosition.y + ", " + charm.localPosition.z);
                        int row = (int)((i - 1) / 10);
                        float y = -8.34f - (1.46f * row);
                        int col = ((i - 1) % 10);
                        float x = -7.92f + ((row % 2) * 0.91f) + (col * 1.5f);
                        charm.localPosition = new Vector3(x, y, -0.001f);
                        //Log("charm at row " + row + ", col " + col + " new pos: " + x + ", " + y);
                    }
                    charmsReordered = LS.playersetReorderCharms;
                    Log("Charms Inventory Reordered");
                }
                else Log("Couldn't find Inventory/Charms");
            }
        }
        #endregion

//	Notch Costs
        #region Notch Cost Changes
        private void ChangeNotchCosts(On.GameManager.orig_CalculateNotchesUsed orig, GameManager self)
        {
            LS.FixCharmNotches();

            PlayerDataAccess.charmCost_1 = LS.charm1NotchCost;
            PlayerDataAccess.charmCost_2 = LS.charm2NotchCost;
            PlayerDataAccess.charmCost_3 = LS.charm3NotchCost;
            PlayerDataAccess.charmCost_4 = LS.charm4NotchCost;
            PlayerDataAccess.charmCost_5 = LS.charm5NotchCost;
            PlayerDataAccess.charmCost_6 = LS.charm6NotchCost;
            PlayerDataAccess.charmCost_7 = LS.charm7NotchCost;
            PlayerDataAccess.charmCost_8 = LS.charm8NotchCost;
            PlayerDataAccess.charmCost_9 = LS.charm9NotchCost;
            PlayerDataAccess.charmCost_10 = LS.charm10NotchCost;
            PlayerDataAccess.charmCost_11 = LS.charm11NotchCost;
            PlayerDataAccess.charmCost_12 = LS.charm12NotchCost;
            PlayerDataAccess.charmCost_13 = LS.charm13NotchCost;
            PlayerDataAccess.charmCost_14 = LS.charm14NotchCost;
            PlayerDataAccess.charmCost_15 = LS.charm15NotchCost;
            PlayerDataAccess.charmCost_16 = LS.charm16NotchCost;
            PlayerDataAccess.charmCost_17 = LS.charm17NotchCost;
            PlayerDataAccess.charmCost_18 = LS.charm18NotchCost;
            PlayerDataAccess.charmCost_19 = LS.charm19NotchCost;
            PlayerDataAccess.charmCost_20 = LS.charm20NotchCost;
            PlayerDataAccess.charmCost_21 = LS.charm21NotchCost;
            PlayerDataAccess.charmCost_22 = LS.charm22NotchCost;
            PlayerDataAccess.charmCost_23 = LS.charm23NotchCost;
            PlayerDataAccess.charmCost_24 = LS.charm24NotchCost;
            PlayerDataAccess.charmCost_25 = LS.charm25NotchCost;
            PlayerDataAccess.charmCost_26 = LS.charm26NotchCost;
            PlayerDataAccess.charmCost_27 = LS.charm27NotchCost;
            PlayerDataAccess.charmCost_28 = LS.charm28NotchCost;
            PlayerDataAccess.charmCost_29 = LS.charm29NotchCost;
            PlayerDataAccess.charmCost_30 = LS.charm30NotchCost;
            PlayerDataAccess.charmCost_31 = LS.charm31NotchCost;
            PlayerDataAccess.charmCost_32 = LS.charm32NotchCost;
            PlayerDataAccess.charmCost_33 = LS.charm33NotchCost;
            PlayerDataAccess.charmCost_34 = LS.charm34NotchCost;
            PlayerDataAccess.charmCost_35 = LS.charm35NotchCost;
            PlayerDataAccess.charmCost_36 = LS.charm36NotchCost;
            PlayerDataAccess.charmCost_37 = LS.charm37NotchCost;
            PlayerDataAccess.charmCost_38 = LS.charm38NotchCost;
            PlayerDataAccess.charmCost_39 = LS.charm39NotchCost;
            PlayerDataAccess.charmCost_40 = LS.charm40NotchCost;
        }
        #endregion

//  Charm Descriptions
        #region Charm Descriptions
        private string ChangeCharmDescriptions(string key, string sheetTitle, string orig)
        {
            if (sheetTitle == "UI" && LocalSettings.charmDesc.ContainsKey(key))
            {
                //Log("Language value modified");
                return LocalSettings.charmDesc[key];
            }

            return orig;
        }
        #endregion

//	Baldur Shell
        #region Baldur Shell Changes
        //  Allow continued focusing if overheal is possible
        private void BaldurShellContFocus(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, IntCompare self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name == "Full HP?"
                && PlayerDataAccess.equippedCharm_5 && PlayerDataAccess.blockerHits > 0)
            {
                int hp = PlayerDataAccess.health + PlayerDataAccess.healthBlue;
                int maxHP = PlayerDataAccess.maxHealth + LS.regularMaximumOverheal
                    + (PlayerDataAccess.equippedCharm_8 ? LS.baldurShellLifebloodHeartOverhealMaxIncrease : 0);
                //Log("HP: " + hp + " / " + maxHP);
                //Log("Baldur Shell allowed continued focusing.");

                if (hp < maxHP && PlayerDataAccess.MPCharge >= PlayerDataAccess.focusMP_amount)
                {
                    self.Fsm.SetState("Focus");
                }
            }

            orig(self);
        }
        private void BaldurShellBlocks(On.PlayerData.orig_MaxHealth orig, PlayerData self)
        {
            orig(self);
            
            if (PlayerDataAccess.equippedCharm_5)
            {
                baldurGreedShellBlocks = 0;
                self.blockerHits = (PlayerDataAccess.equippedCharm_10 ? LS.baldurShellDefendersCrestBlocks : LS.baldurShellBlocks);
                var BaldurShellFSM = HeroController.instance.gameObject.transform.Find("Charm Effects/Blocker Shield").gameObject.LocateMyFSM("Control");
                if (!BaldurShellFSM.GetFsmState("Blocker Hit").HasFinishedTransition())
                {
                    BaldurShellFSM.AddFsmTransition("Blocker Hit", "FINISHED", "Impact End");
                }
            }
        }
        private void BaldurShellKnockback(On.BeginRecoil.orig_OnEnter orig, BeginRecoil self)
        {
            if (self.Fsm.GameObject.name.StartsWith("Hit ") && self.Fsm.Name == "push_enemy" && self.State.Name == "Send Event")
            {
                self.attackMagnitude = 2f * LS.baldurShellKnockback;
            }
            orig(self);
        }
        private void BaldurShellOverheal(On.HeroController.orig_AddHealth orig, HeroController self, int amount)
        {
            int healAmount = (LS.baldurShellDeepFocusAffected && PlayerDataAccess.equippedCharm_34 ? LS.deepFocusHealing : LS.regularFocusHealing);
            int overhealMax = LS.regularMaximumOverheal
                + (PlayerDataAccess.equippedCharm_8 ? LS.baldurShellLifebloodHeartOverhealMaxIncrease : 0);
            int addBlueMasks = healAmount + PlayerDataAccess.health - PlayerDataAccess.maxHealth;

            orig(self, amount);

            //Log("Baldur Shell blocks remaining: " + PlayerDataAccess.blockerHits);
            if (PlayerDataAccess.equippedCharm_5 && PlayerDataAccess.blockerHits > 0)
            {
                for (int i = 0; i < addBlueMasks; i++)
                {
                    if (PlayerDataAccess.healthBlue < overhealMax)
                    {
                        EventRegister.SendEvent("ADD BLUE HEALTH");
                        //Log("Lifeblood should have been added. Did it work?");
                    }
                }
            }
        }
        private void BaldurShellGreedShell(On.HutongGames.PlayMaker.Actions.PlayerDataIntAdd.orig_OnEnter orig, PlayerDataIntAdd self)
        {
            if (self.Fsm.GameObject.name == "Blocker Shield" && self.Fsm.Name == "Control" && self.State.Name == "Blocker Hit"
                && LS.baldurShellGreedShell && PlayerDataAccess.equippedCharm_24)
            {
                baldurGreedShellBlocks++;
                //Log("Greed Shell blocks: " + baldurGreedShellBlocks);

                int geoLoss = 0;
                for (int i = baldurGreedShellBlocks; i > 0; i--)
                {
                    geoLoss += i;
                    //Log("geoLoss increased to " + geoLoss);
                }
                geoLoss *= LS.baldurShellGreedGeoLossRate;
                if (PlayerDataAccess.equippedCharm_10)
                    geoLoss = Mathf.CeilToInt((float)geoLoss * LS.baldurShellGreedDCGeoLossRate);
                //Log("Greed Shell geo loss: " + geoLoss);

                if (PlayerDataAccess.geo >= geoLoss && geoLoss > 0)
                {
                    HeroControllerR.TakeGeo(geoLoss);
                    //Log("Greed Shell blocked hit.");

                    int geoDrop = Math.Min(LS.baldurShellGreedGeoDrop, geoLoss);
                    ScatterGeoFromHero(geoDrop);

                    //Log("Blocker hits: " + PlayerDataAccess.blockerHits);
                    if (PlayerDataAccess.blockerHits <= 1)
                    {
                        return;
                    }
                }
            }

            orig(self);
        }
        public void GetGeoPrefabs()
        {
            Log("Getting geo prefabs.");
            foreach (var o in ObjectPool.instance.startupPools)
            {
                if (_smallGeoPrefab == null && o.prefab.name == "Geo Small")
                {
                    _smallGeoPrefab = o.prefab;
                    _smallGeoPrefab.SetActive(false);
                    UnityEngine.Object.DontDestroyOnLoad(_smallGeoPrefab);
                }
                else if (_mediumGeoPrefab == null && o.prefab.name == "Geo Med")
                {
                    _mediumGeoPrefab = o.prefab;
                    _mediumGeoPrefab.SetActive(false);
                    UnityEngine.Object.DontDestroyOnLoad(_mediumGeoPrefab);
                }
                else if (_largeGeoPrefab == null && o.prefab.name == "Geo Large")
                {
                    _largeGeoPrefab = o.prefab;
                    _largeGeoPrefab.SetActive(false);
                    UnityEngine.Object.DontDestroyOnLoad(_largeGeoPrefab);
                }

                if (_smallGeoPrefab != null && _mediumGeoPrefab != null && _largeGeoPrefab != null)
                    break;
            }
            //Log(_smallGeoPrefab != null ? "Small geo prefab found." : "Small geo prefab not found.");
            //Log(_mediumGeoPrefab != null ? "Medium geo prefab found." : "Medium geo prefab not found.");
            //Log(_largeGeoPrefab != null ? "Large geo prefab found." : "Large geo prefab not found.");
        }
        private void ScatterGeoFromHero(int amount)
        {
            //Log("Scattering " + amount + " geo from hero.");
            float angleMin = 20f;
            float angleMax = 160f;
            float speedMin = 15f;
            float speedMax = 30f;

            int numSmall = amount;   //  Worth 1 geo
            int numMed = 0;     //  Worth 5 geo
            int numLarge = 0;   //  Worth 25 geo
            
            GameObject[] gameObjects = FlingUtils.SpawnAndFling(new FlingUtils.Config
            {
                Prefab = _smallGeoPrefab,
                AmountMin = numSmall,
                AmountMax = numSmall,
                SpeedMin = speedMin,
                SpeedMax = speedMax,
                AngleMin = angleMin,
                AngleMax = angleMax
            }, HeroController.instance.transform, Vector3.zero);
            //Log(gameObjects.Length + " small geo dropped.");
            gameObjects = FlingUtils.SpawnAndFling(new FlingUtils.Config
            {
                Prefab = _mediumGeoPrefab,
                AmountMin = numMed,
                AmountMax = numMed,
                SpeedMin = speedMin,
                SpeedMax = speedMax,
                AngleMin = angleMin,
                AngleMax = angleMax
            }, HeroController.instance.transform, Vector3.zero);
            //Log(gameObjects.Length + " medium geo dropped.");
            gameObjects = FlingUtils.SpawnAndFling(new FlingUtils.Config
            {
                Prefab = _largeGeoPrefab,
                AmountMin = numLarge,
                AmountMax = numLarge,
                SpeedMin = speedMin,
                SpeedMax = speedMax,
                AngleMin = angleMin,
                AngleMax = angleMax
            }, HeroController.instance.transform, Vector3.zero);
            //Log(gameObjects.Length + " large geo dropped.");
        }
        private void BaldurShellBlockFlag(On.HutongGames.PlayMaker.Actions.PlayerDataIntAdd.orig_OnEnter orig, PlayerDataIntAdd self)
        {
            if (self.Fsm.GameObject.name == "Blocker Shield" && self.Fsm.Name == "Control" && self.State.Name == "Blocker Hit")
            {
                //  Flag that a block occurred    —    Referenced by other effects
                baldurShellBlocked = true;
                GameManager.instance.StartCoroutine(BaldurShellBlock());
                //Log("Baldur Shell blocked");
            }

            orig(self);
        }
        #endregion

//	Carefree Melody
        #region Carefree Melody Changes
        private void CarefreeMelodyBlockAndHeal(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, CollisionSide damageSide, int damageAmount, int hazardType)
        {
            //  Calculate chance to block
            int currentHP = PlayerDataAccess.health
                + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.healthBlue - LS.jonisBlessingLifeblood : 0);
            if (HeroControllerR.CanTakeDamage() && PlayerDataAccess.equippedCharm_40 && PlayerDataAccess.grimmChildLevel == 5)
            {
                carefreeMelodyChance = Mathf.Max(Mathf.Pow(9 - currentHP, 3) * LS.carefreeMelodyChance / 512f, 0f);
                HeroControllerR.hitsSinceShielded = 1;
                //Log("Carefree Melody chance to block: " + carefreeMelodyChance);
            }
            else
                carefreeMelodyChance = 0;

            orig(self, go, damageSide, damageAmount, hazardType);

            //  Synergy w/ Fragile Heart allows Carefree Melody a chance to heal when it blocks
            if (PlayerDataAccess.equippedCharm_23)
            {
                // Only trigger when Carefree Melody blocks
                GameObject shield = HeroController.instance.carefreeShield;
                if (shield != null && shield.activeSelf)
                {
                    if (UnityEngine.Random.Range(1, 101) <= carefreeMelodyChance)
                    {
                        if ((PlayerDataAccess.health < PlayerDataAccess.maxHealth && !PlayerDataAccess.equippedCharm_27) || 
                            (PlayerDataAccess.equippedCharm_29 && PlayerDataAccess.equippedCharm_27 && PlayerDataAccess.healthBlue < PlayerDataAccess.joniHealthBlue))
                        {
                            HeroController.instance.AddHealth(1);
                        }
                        else if (PlayerDataAccess.equippedCharm_27 && PlayerDataAccess.healthBlue < PlayerDataAccess.joniHealthBlue)
                        {
                            EventRegister.SendEvent("ADD BLUE HEALTH");
                        }
                    }

                    //  Grubsong soul gain when Carefree Melody blocks
                    if (PlayerDataAccess.equippedCharm_3)
                    {
                        HeroController.instance.AddMPCharge(grubSoulGain);
                    }
                }
            }
        }
        private void CarefreeMelodyFixedChance(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(
                i => i.MatchLdcI4(1),
                i => i.MatchLdcI4(100)
            );
            cursor.GotoNext();
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(hundred => 101);

            cursor.TryGotoNext(i => i.MatchLdcR4(10));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(chance => (int)carefreeMelodyChance);

            //cursor.TryGotoNext(
            //    i => i.MatchLdcI4(0),
            //    i => i.MatchStfld("int32", "HeroController::hitsSinceShielded")
            //);
            //cursor.GotoNext();
            //cursor.GotoNext();
            //cursor.EmitDelegate<Func<int, int>>(zero => 1);

            //cursor.TryGotoNext(
            //    i => i.MatchStfld("int32", "HeroController::hitsSinceShielded"),
            //    i => i.MatchLdcI4(1)
            //);
            //cursor.GotoNext();
            //cursor.GotoNext();
            //cursor.EmitDelegate<Func<int, int>>(one => 0);
        }
        #endregion

//	Dashmaster
        #region Dashmaster Changes
        private void DashmasterChanges(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);

            // Downward Dashing
            cursor.TryGotoNext(i => i.MatchLdstr("equippedCharm_31"));
            cursor.GotoNext();
            cursor.GotoNext();
            cursor.EmitDelegate<Func<bool, bool>>(downDash =>  downDash && LS.dashmasterDownwardDash);

            // Dashmaster Dash Cooldown
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("DASH_COOLDOWN_CH"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(cooldown => LS.dashmasterDashCooldown);

            // Regular Dash Cooldown
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("DASH_COOLDOWN"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(cooldown => LS.regularDashCooldown);

            // Shade Cloak Cooldown
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("SHADOW_DASH_COOLDOWN"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(cooldown => (PlayerDataAccess.equippedCharm_31 ? LS.dashmasterShadeCloakCooldown : LS.regularShadeCloakCooldown));
        }
        #endregion

//	Deep Focus
        #region Deep Focus Changes
        private void DeepFocusScaling(On.HutongGames.PlayMaker.Actions.FloatMultiply.orig_OnEnter orig, FloatMultiply self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name == "Deep Focus Speed")
            {
                self.multiplyBy.Value = LS.deepFocusHealingTimeMult;
            }

            orig(self);
        }
        #endregion

//	Defender's Crest
        #region Defender's Crest Changes
        private void DefendersCrestCostReduction(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdcR4(0.8f));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(Discount => 100f - ((float)(LS.defendersCrestDiscount / 100f)));
        }
        private void DungCloudSettings(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            if (self.Fsm.GameObject.name == "Knight Dung Trail(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Wait")
            {
                // Duration & Visuals
                float dungDuration = (PlayerDataAccess.equippedCharm_14 ? LS.defendersCrestSteadyBodyDuration : LS.defendersCrestDuration);
                self.time.Value = dungDuration;
                self.Fsm.GameObject.transform.Find("Pt Normal").GetComponent<ParticleSystem>().startLifetime = dungDuration * 1.25f;    //  visual doesn't last as long as hit box

                // Damage Rate
                float dungDamageRate = (furyActive ? LS.defendersCrestFotFDamageRate : LS.defendersCrestDamageRate);
                self.Fsm.GameObject.GetComponent<DamageEffectTicker>().damageInterval = dungDamageRate;
            }

            orig(self);
        }
        private void DungCloudScaling(On.HutongGames.PlayMaker.Actions.SetPosition.orig_OnEnter orig, SetPosition self)
        {
            orig(self);

            if (self.Fsm.GameObject.name == "Knight Dung Trail(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Init")
            {
                float dungScaling = (PlayerDataAccess.equippedCharm_14 ? LS.defendersCrestSteadyBodyRadius : LS.defendersCrestRadius);
                self.Fsm.GameObject.transform.localScale = new Vector3(dungScaling, dungScaling, 0);
            }
        }
        private void CloudFrequency(On.HutongGames.PlayMaker.Actions.SpawnObjectFromGlobalPoolOverTime.orig_OnUpdate orig, SpawnObjectFromGlobalPoolOverTime self)
        {
            if (self.Fsm.GameObject.name == "Dung" && self.Fsm.Name == "Control" && self.State.Name == "Equipped")
            {
                self.frequency.Value = LS.defendersCrestFrequency;
            }

            orig(self);
        }
        #endregion

//	Dream Wielder
        #region Dream Wielder Changes
        private void DreamWielderSoul(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);

            // Regular Soul
            cursor.TryGotoNext(i => i.MatchLdcI4(33));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.regularDreamNailSoulGain);

            // Charm Soul
            cursor.TryGotoNext(i => i.MatchLdcI4(66));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.dreamWielderSoulGain);
        }
        private void DreamWielderEssence(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);

            // High Chance w/ Dream Wielder
            cursor.TryGotoNext(i => i.MatchLdcI4(40));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(chanceLow => LS.dreamWielderEssenceChanceHigh);

            // Low Chance w/ Dream Wielder
            cursor.TryGotoNext(i => i.MatchLdcI4(200));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(chanceHigh => LS.dreamWielderEssenceChanceLow);

            // Low Chance w/o Dream Wielder
            cursor.TryGotoNext(i => i.MatchLdcI4(300));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(chanceLow => LS.regularEssenceChanceLow);

            // High Chance w/o Dream Wielder
            cursor.TryGotoNext(i => i.MatchLdcI4(60));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(chanceHigh => LS.regularEssenceChanceHigh);
        }
        private void DreamNailSize(On.HutongGames.PlayMaker.Actions.ActivateGameObject.orig_OnEnter orig, ActivateGameObject self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Dream Nail" && self.State.Name == "Slash")
            {
                float dnScale = (PlayerDataAccess.equippedCharm_30 && PlayerDataAccess.equippedCharm_33 ? LS.dreamWielderSpellTwisterRange : LS.regularDreamNailRange);
                self.gameObject.GameObject.Value.SetScale(dnScale, dnScale);
            }

            orig(self);
        }
        private void DreamNailSpriteSize(On.HutongGames.PlayMaker.Actions.Tk2dPlayAnimation.orig_OnEnter orig, Tk2dPlayAnimation self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Dream Nail" && self.State.Name == "Slash")
            {
                float dnScale = (PlayerDataAccess.equippedCharm_30 && PlayerDataAccess.equippedCharm_33 ? LS.dreamWielderSpellTwisterRange : LS.regularDreamNailRange);
                self.gameObject.GameObject.Value.SetScale(dnScale, dnScale);
            }

            orig(self);
        }
        #endregion

//	Dreamshield
//  TODO: needs to block or reflect crystal hunter projectiles
        #region Dreamshield Changes
        private void DreamshieldOverheal(On.EnemyDreamnailReaction.orig_RecieveDreamImpact orig, EnemyDreamnailReaction self)
        {
            orig(self);

            if (PlayerDataAccess.equippedCharm_38)
            {
                int overhealMax = LS.regularMaximumOverheal
                    + (PlayerDataAccess.equippedCharm_8 ? LS.dreamshieldLifebloodHeartOverhealMaxIncrease : 0);
                if (PlayerDataAccess.healthBlue < overhealMax)
                {
                    EventRegister.SendEvent("ADD BLUE HEALTH");
                    //Log("Lifeblood should have been added. Did it work?");
                }
            }
        }
        private void DreamshieldDamageAndKnockback(On.HutongGames.PlayMaker.Actions.SendEventByName.orig_OnEnter orig, SendEventByName self)
        {
            if (self.Fsm.GameObject.name == "Shield" && self.Fsm.Name == "Shield Hit" &&
                (self.State.Name == "Hit" || self.State.Name == "Parent?" || self.State.Name == "G Parent?"))
            {
                //  Disable vanilla damage
                self.State.DisableAction(6);
                self.State.DisableAction(8);

                if (self.State.Name == "Hit")
                {
                    //  Apply damage via HitTaker instead
                    HitTaker.Hit(self.eventTarget.gameObject.GameObject.Value, new HitInstance
                    {
                        Source = dreamshield,
                        AttackType = AttackTypes.Spell,
                        CircleDirection = true,
                        DamageDealt = LS.dreamshieldBaseDamage + (int)(PlayerDataAccess.dreamOrbs / LS.dreamshieldDamageScaleRate),
                        Direction = 0f,
                        IgnoreInvulnerable = false,
                        MagnitudeMultiplier = LS.dreamshieldKnockback,
                        MoveAngle = 0f,
                        MoveDirection = true,
                        Multiplier = 1f,
                        SpecialType = SpecialTypes.None,
                        IsExtraDamage = false
                    }, 3);
                }
            }

            orig(self);
        }
        private void DreamshieldReformationTime(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            if (self.Fsm.GameObject.name == "Shield" && self.Fsm.Name == "Shield Hit" && self.State.Name == "Break")
            {
                self.time.Value = (PlayerDataAccess.equippedCharm_10 ? LS.dreamshieldDefendersCrestReformTime : LS.dreamshieldReformationTime);
            }

            orig(self);
        }
        private void DreamshieldObjectPrep(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.gameObject.name == "Orbit Shield(Clone)" && self.FsmName == "Control")
            {
                //  Prep shield to recieve custom position and rotation instructions
                dreamshield = self.gameObject;
                Satchel.FsmUtil.DisableAction(self, "Follow", 3);
                dreamshieldRotation = Satchel.FsmUtil.GetAction<Rotate>(self, "Follow", 4);
                dreamshieldRotation.zAngle = 0;
                //Log("Stored dreamshield: " + dreamshield.name);
                //Log("Halted dreamshield position and rotation.");
            }
            else if (self.gameObject.name == "Orbit Shield(Clone)" && self.FsmName == "Focus Speedup")
            {
                //  Disable vanilla rotation
                foreach (FsmState state in self.Fsm.States)
                {
                    for (int i = 0; i < state.Actions.Length; i++)
                    {
                        state.DisableAction(i);
                    }
                }
            }
        }
        private void DreamshieldFollowBehind(On.HeroController.orig_Update orig, HeroController self)
        {
            orig(self);

            if (PlayerDataAccess.equippedCharm_38 && dreamshield != null)
            {
                //  Set shield facing
                int facing = (HeroController.instance.cState.facingRight ? 1 : -1);

                //  Set shield scale
                dreamshield.transform.GetChild(1).SetScaleMatching(-LS.dreamshieldSizeScale);

                //  Set shield position lower relative to knight
                dreamshield.transform.SetPosition2D(HeroController.instance.transform.GetPositionX() + (facing * 0.3f), HeroController.instance.transform.GetPositionY() - 0.35f);

                //  Set rotation based on player inputs
                float currentRotation = dreamshield.transform.eulerAngles.z;
                float targetRotation;
                if (PlayerDataAccess.atBench)
                    targetRotation = 82;
                else if (HeroController.instance.vertical_input != 0f)
                    targetRotation = (90 + (facing * LS.dreamshieldOverheadOffset));
                else
                    targetRotation = (90 + (facing * 90f));
                float rotationSpeed = (targetRotation - currentRotation) * LS.dreamshieldTweenSpeed;
                if (rotationSpeed > -5f && rotationSpeed < 5f) rotationSpeed = 0f;
                dreamshieldRotation.zAngle = rotationSpeed;

                //  Set shield distance from knight
                dreamshield.transform.GetChild(1).transform.localPosition = new Vector3(LS.dreamshieldSizeScale * 2f, facing * 0.15f, 0f);
                
                //  Reduce particle emission
                dreamshield.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<ParticleSystem>().emissionRate = 7;

                //Log("Affected shield scale, rotation, and local position.");
                return;
            }
        }
        private void DreamshieldBlockDetection(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, CollisionSide damageSide, int damageAmount, int hazardType)
        {
            if (HeroControllerR.CanTakeDamage()
                && !(self.damageMode == DamageMode.HAZARD_ONLY && hazardType == 1)
                && !(self.cState.shadowDashing && hazardType == 1)
                && !(self.parryInvulnTimer > 0f && hazardType == 1))
            {
                if (LS.dreamshieldBlocksBehind && PlayerDataAccess.equippedCharm_38 && HeroController.instance.vertical_input == 0f
                    && dreamshield.transform.FindChild("Shield").gameObject.LocateMyFSM("Shield Hit").Fsm.ActiveStateName != "Break"
                    && ((self.cState.facingRight && damageSide == CollisionSide.left)
                    || (!self.cState.facingRight && damageSide == CollisionSide.right)))
                {
                    dreamshieldBlock = true;
                    hazardType = 0; //  flagging hazardType to bypass Carefree Melody and Baldur Shell checks
                    GameManager.instance.StartCoroutine(DreamshieldBlocked());
                    //Log("Hero facing " + (self.cState.facingRight ? "right" : "left"));
                    //Log("hit from " + damageSide.ToString() + " by hazard type " + hazardType);
                    //Log("dreamshield should block this attack");
                }
            }

            orig(self, go, damageSide, damageAmount, hazardType);
        }
        private int DreamshieldBlocks(int hazardType, int damageAmount)
        {
            if (dreamshieldBlock)
            {
                dreamshieldBlock = false;
                var shield = dreamshield.transform.FindChild("Shield").gameObject;
                shield.LocateMyFSM("Shield Hit").SetState("Tink");
                return 0;
            }

            return damageAmount;
        }
        private void DreamshieldBlockNoHitEffect(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(
                i => i.MatchLdcI4(1),
                i => i.MatchStloc(0)
            );
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(boolTrue =>
            {
                if (dreamshieldBlock)
                    return 0;   //  false
                return boolTrue;
            });

            cursor.TryGotoNext(
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(3)
            );
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(boolFalse =>
            {
                if (dreamshieldBlock)
                    return 1;   //  true
                return boolFalse;
            });
        }
        private void DreamshieldScaling(On.HutongGames.PlayMaker.Actions.SetScale.orig_OnEnter orig, SetScale self)
        {
            if (self.Fsm.GameObject.name == "Shield" && self.Fsm.Name == "Shield Hit" && self.State.Name == "Dreamwielder?")
            {
                if (self.State.ActiveActionIndex == 0)
                {
                    self.x.Value = -LS.dreamshieldSizeScale;
                    self.y.Value = LS.dreamshieldSizeScale;
                }
                else if (self.State.ActiveActionIndex == 1)
                {
                    self.x.Value = LS.dreamshieldSizeScale;
                    self.y.Value = LS.dreamshieldSizeScale;
                }
                else if (self.State.ActiveActionIndex == 3)
                {
                    self.x.Value = -LS.dreamshieldSizeScale;
                    self.y.Value = LS.dreamshieldSizeScale;
                }
                else if (self.State.ActiveActionIndex == 4)
                {
                    self.x.Value = LS.dreamshieldSizeScale;
                    self.y.Value = LS.dreamshieldSizeScale;
                }
            }

            orig(self);
        }
        private void DreamshieldDebugLogging(On.HutongGames.PlayMaker.Actions.SendEvent.orig_OnEnter orig, SendEvent self)
        {
            orig(self);

            if (self.Fsm.GameObjectName == "Shield" && self.Fsm.Name == "Shield Hit" && self.State.Name == "Type")
            {
                Log("Collider: " + self.Fsm.Variables.GameObjectVariables.GetValue(0).ToString());
                Log("Tag: " + self.Fsm.Variables.StringVariables.GetValue(0));
                Log("Layer: " + self.Fsm.Variables.IntVariables.GetValue(0));
            }
        }
        //  Disable elements of the vanilla Dreamshield
        private void DreamshieldFSMPrep(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            if (self.Fsm.GameObject.name == "Shield" && self.Fsm.Name == "Shield Hit")
            {
                //  Ignore Geo Collisions
                var state = Satchel.FsmUtil.GetState(self, "Type");
                if (state.Actions.Length == 7)
                {
                    var ignoreGeo = Satchel.FsmUtil.GetAction<StringCompare>(state, 4);
                    ignoreGeo.compareTo = "Geo";
                    Satchel.FsmUtil.InsertAction(state, ignoreGeo, 4);
                    //Log("Dreamshield modified to ignore geo.");
                }

                //  Remove Slash Responder
                state = Satchel.FsmUtil.GetState(self, "Idle");
                if (state.Transitions.Length > 1)
                    Satchel.FsmUtil.RemoveTransition(state, "SLASH");
            }

            orig(self);
        }
        #endregion

//	Flukenest
        #region Flukenest Changes
        private void FlukeCount(On.HutongGames.PlayMaker.Actions.FlingObjectsFromGlobalPool.orig_OnEnter orig, FlingObjectsFromGlobalPool self)
        {
            // Vengeful Spirit
            if(self.Fsm.GameObject.name == "Fireball Top(Clone)" && self.Fsm.Name == "Fireball Cast" && self.State.Name == "Flukes")
            {
                self.spawnMin.Value = LS.flukenestVSFlukes;
                self.spawnMax.Value = LS.flukenestVSFlukes;
            }

            // Shade Soul
            else if (self.Fsm.GameObject.name == "Fireball2 Top(Clone)" && self.Fsm.Name == "Fireball Cast" && self.State.Name == "Flukes")
            {
                self.spawnMin.Value = LS.flukenestSSFlukes;
                self.spawnMax.Value = LS.flukenestSSFlukes;
            }

            orig(self);
        }
        private void FlukenestDCContactDamage(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            if (PlayerDataAccess.equippedCharm_11 && PlayerDataAccess.equippedCharm_10)
            {
                if (hitInstance.Source.gameObject.transform.parent.name == "Spell Fluke Dung Lv1(Clone)")
                {
                    hitInstance.DamageDealt = (PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneVSDamage : LS.flukenestDefendersCrestVSDamage);
                }
                else if (hitInstance.Source.gameObject.transform.parent.name == "Spell Fluke Dung Lv2(Clone)")
                {
                    hitInstance.DamageDealt = (PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneSSDamage : LS.flukenestDefendersCrestSSDamage);
                }
            }

            orig(self, hitInstance);
        }
        private void FlukenestDefendersCrestDurationAndDamage(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            //Log(self.Fsm.GameObject.name + "-" + self.Fsm.Name + "   " + self.State.Name);
            if (self.Fsm.GameObject.name.StartsWith("Spell Fluke Dung") && self.Fsm.Name == "Control" && self.State.Name == "Blow")
            {
                float cloudDuration = (PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneDuration : LS.flukenestDefendersCrestDuration);
                //Log("BLOW Original duration: " + self.time.Value);
                self.time.Value = cloudDuration + 1f;
                self.Fsm.GameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;    //  stop moving after the explosion
                //Log("BLOW Modified duration: " + self.time.Value);

                //  Cloud Visuals
                ParticleSystem cloudPT = self.Fsm.Variables.GetFsmGameObject("Dung Cloud").Value.transform.Find("Pt Normal").GetComponent<ParticleSystem>();
                //ParticleSystem cloudPT = self.Fsm.GameObject.transform.Find("Pt Normal").GetComponent<ParticleSystem>();
                float cloudRadius = (PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneRadius : LS.flukenestDefendersCrestRadius);
                //Log("Original startLifetime: " + cloudPT.startLifetime);
                //Log("Original startSpeed: " + cloudPT.startSpeed);
                cloudPT.startLifetime = cloudDuration + 0.4f;
                cloudPT.startSpeed = 40f * (cloudRadius / 6f);
                //Log("Modified startLifetime: " + cloudPT.startLifetime);
                //Log("Modified startSpeed: " + cloudPT.startSpeed);
            }
            else if (self.Fsm.GameObject.name.StartsWith("Knight Dung Cloud") && self.Fsm.Name == "Control" && self.State.Name == "Collider On")
            {
                // Duration & Visuals
                float cloudDuration = (PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneDuration : LS.flukenestDefendersCrestDuration);
                //Log("COLLIDER Original duration: " + self.time.Value);
                self.time.Value = cloudDuration;
                //Log("COLLIDER Modified duration: " + self.time.Value);

                // Cloud Radius
                CircleCollider2D bigFlukeCollider = self.Fsm.GameObject.GetComponent<UnityEngine.CircleCollider2D>();
                //Log("Original cloud radius: " + bigFlukeCollider.radius);
                bigFlukeCollider.radius = (PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneRadius : LS.flukenestDefendersCrestRadius);
                //Log("Modified cloud radius: " + bigFlukeCollider.radius);

                // Damage Rates
                DamageEffectTicker bigFLukeDET = self.Fsm.GameObject.GetComponent<DamageEffectTicker>();
                //Log("Original damage rate: " + bigFLukeDET.damageInterval);
                bigFLukeDET.SetDamageInterval(PlayerDataAccess.equippedCharm_19 ? LS.flukenestDCShamanStoneDamageRate : LS.flukenestDefendersCrestDamageRate);
                bigFLukeDET.extraDamageType = (PlayerDataAccess.fireballLevel == 2 ? ExtraDamageTypes.Dung2 : ExtraDamageTypes.Dung);
                //Log("Modified damage rate: " + bigFLukeDET.damageInterval);
            }

            orig(self);
        }
        private void FlukenestEnableHook(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);

            // Shaman Stone Sizes
            cursor.TryGotoNext(i => i.MatchLdcR4(0.9f));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(minimum => LS.flukenestShamanStoneFlukeSizeMin);

            cursor.TryGotoNext(i => i.MatchLdcR4(1.2f));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(maximum => LS.flukenestShamanStoneFlukeSizeMax);

            // Shaman Stone Damage
            cursor.TryGotoNext(i => i.MatchLdcI4(5));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(damage => LS.flukenestShamanStoneDamage + (furyActive ? LS.flukenestFotFDamageIncrease : 0));

            // Regular Sizes
            cursor.TryGotoNext(i => i.MatchLdcR4(0.7f));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(minimum => LS.flukenestFlukeSizeMin);

            cursor.TryGotoNext(i => i.MatchLdcR4(0.9f));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(maximum => LS.flukenestFlukeSizeMax);

            // Regular Damage
            cursor.TryGotoNext(i => i.MatchLdcI4(4));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(damage => LS.flukenestDamage + (furyActive ? LS.flukenestFotFDamageIncrease : 0));
        }
        #endregion

//	Fragile Charms
        #region Fragile Charms Changes
		
		//	Fragile Charms Break
        private void FragileCharmsBreak(On.HutongGames.PlayMaker.Actions.PlayerDataBoolTest.orig_OnEnter orig, PlayerDataBoolTest self)
        {
            if (self.Fsm.GameObject.name == "Hero Death" && self.Fsm.Name == "Hero Death Anim" && self.State.Name.StartsWith("Break Glass ") && self.boolName.Value.EndsWith("_unbreakable"))
            {
                self.isFalse = LS.fragileCharmsBreak ? null : FsmEvent.GetFsmEvent("FINISHED");
            }

            orig(self);
        }
		//	Greed Geo Increase
        private void GreedGeoIncrease(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            while(cursor.TryGotoNext(i => i.MatchLdcR4(0.2f)))
            {
                cursor.GotoNext();
                cursor.EmitDelegate<Func<float, float>>(geoScale => LS.greedGeoIncrease / 100f);
            }
        }
        //  Geo Grants Soul
        private void GeoGrantsSoul(On.HeroController.orig_AddGeo orig, HeroController self, int amount)
        {
            if ((PlayerDataAccess.equippedCharm_20 || PlayerDataAccess.equippedCharm_21) && PlayerDataAccess.equippedCharm_24 && !PlayerDataAccess.brokenCharm_24)
            {
                self.AddMPCharge(amount);
                GameManager.instance.StartCoroutine(SoulUpdate());
            }

            orig(self, amount);
        }
        //  Set Heart Extra Masks
        private void SetFragileHeartMasks(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            while (cursor.TryGotoNext(
                i => i.MatchLdcI4(2),
                i => i.MatchAdd(),
                i => i.MatchLdstr("maxHealth")
            ))
            {
                cursor.GotoNext();
                cursor.EmitDelegate<Func<int, int>>(masks => LS.heartMasks);
            }
        }
        //	Strength Nail Damage Increase
        private int StrengthNailDamageIncrease(string name, int orig)
        {
            if (name == "nailDamage") {
                orig = (int)(orig + (PlayerDataAccess.equippedCharm_25 ? LS.strengthNailDamageIncrease : 0));
            }
			
            return orig;
        }
		//	Ignore vanilla Strength damage multiplier
        private void IgnoreVanillaStrengthDamage(On.HutongGames.PlayMaker.Actions.FloatMultiply.orig_OnEnter orig, FloatMultiply self)
        {
            if (self.Fsm.GameObject.name == "Attacks" && self.Fsm.Name == "Set Slash Damage" && self.State.Name == "Glass Attack Modifier") {
				self.multiplyBy.Value = 1f;	//	no multiplier
			}

            orig(self);
        }
        #endregion

//	Fury of the Fallen
        #region Fury of the Fallen Changes
        private void FotFHPRequirements(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, IntCompare self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Fury")
            {
                int bloodiedValue = (int)((PlayerData.instance.maxHealth + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.joniHealthBlue : 0)) * (PlayerDataAccess.overcharmed ? 99 : LS.furyOfTheFallenThreshold));
                if (self.State.Name == "Check HP")
                {
                    self.integer1 = PlayerDataAccess.health + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.healthBlue : 0);
                    self.integer2 = bloodiedValue;
                    self.lessThan = FsmEvent.GetFsmEvent("FURY");
                }
                else if (self.State.Name == "Recheck")
                {
                    self.integer1 = PlayerDataAccess.health + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.healthBlue : 0);
                    self.integer2 = bloodiedValue;
                    self.lessThan = FsmEvent.GetFsmEvent("RETURN");
                }
            }

            orig(self);
        }
        private void FotFActiveOvercharmed(On.HeroController.orig_NearBench orig, HeroController self, bool isNearBench)
        {
            //Log("On.HeroController.NearBench");
            if (LS.furyOfTheFallenOvercharmed && PlayerDataAccess.equippedCharm_6 && PlayerDataAccess.overcharmed)
            {
                //Log("Fury should be active due to overcharm");
                HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject.LocateMyFSM("Fury").SendEvent("HERO DAMAGED");
            }
        }
        private void SetFuryActive(On.NailSlash.orig_SetFury orig, NailSlash self, bool set)
        {
            //Log("set = " + set);
            //Log("furyActive = " + furyActive);

            //  Set nail damage
            if (set != furyActive)
            {
                furyActive = set;
                PlayerData.instance.nailDamage += set ? LS.furyOfTheFallenNailDamageIncrease : -LS.furyOfTheFallenNailDamageIncrease;
                PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                //Log("Nail Damage set to " + PlayerData.instance.nailDamage);
            }

            orig(self, set);
        }
        //	Ignore vanilla nail damage increase
        private void FotFIgnoreVanillaNailScaling(On.HutongGames.PlayMaker.Actions.SetFsmFloat.orig_OnEnter orig, SetFsmFloat self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Fury" && self.State.Name == "Activate") {
                self.setValue.Value = 1f;	//	no multiplier
            }

            orig(self);
        }
		//	Ignore vanilla nail art damage increase
        private void FotFIgnoreVanillaNailArtScaling(On.HutongGames.PlayMaker.Actions.FloatMultiply.orig_OnEnter orig, FloatMultiply self)
        {
            if (self.Fsm.Name == "nailart_damage" && self.State.Name == "Fury?") {
                self.multiplyBy.Value = 1f;	//	no multiplier
            }

            orig(self);
        }
        private void FotFIgnoreJonis(On.HutongGames.PlayMaker.Actions.BoolAllTrue.orig_OnEnter orig, BoolAllTrue self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Fury" && self.State.Name == "Check HP")
            {
                self.sendEvent = null;
            }

            orig(self);
        }
        private void FotFIgnoreJonis2(On.HutongGames.PlayMaker.Actions.PlayerDataBoolTest.orig_OnEnter orig, PlayerDataBoolTest self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Fury" && self.State.Name == "Check HP")
            {
                self.isTrue = FsmEvent.GetFsmEvent("RETURN");
            }

            orig(self);
        }
        #endregion

//	Glowing Womb
        #region Glowing Womb Changes
        private void HatchlingSpawnRequirements(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, IntCompare self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Hatchling Spawn")
            {
                // Spawn Cost
                if (self.State.Name == "Can Hatch?")
                {
                    int spawnCost = LS.glowingWombSpawnCost
                        - (PlayerDataAccess.equippedCharm_1 ? LS.glowingWombGatheringSwarmCostReduction : 0)
                        - (PlayerDataAccess.equippedCharm_40 && PlayerDataAccess.grimmChildLevel == 5 ? LS.glowingWombCarefreeMelodyCostReduction : 0);
                    self.integer2.Value = spawnCost;
                }

                // Spawn Max
                else if (self.State.Name == "Check Count")
                {
                    self.integer2.Value = (PlayerDataAccess.equippedCharm_1 ? LS.glowingWombGatheringSwarmSpawnTotal : LS.glowingWombSpawnTotal);
                }
            }

            orig(self);
        }
        private void HatchlingDamage(On.KnightHatchling.orig_OnEnable orig, KnightHatchling self)
        {
            int damageModifier = (PlayerDataAccess.equippedCharm_1 ? -LS.glowingWombGatheringSwarmDamageReduction : 0)
                + (PlayerDataAccess.equippedCharm_11 ? LS.glowingWombFlukenestDamageIncrease : 0);
            self.normalDetails.damage = Math.Max(LS.glowingWombDamage + damageModifier, 1);
            self.dungDetails.damage = Math.Max(LS.glowingWombDefendersCrestDamage + damageModifier, 1);

            if (LS.glowingWombPiercing)
                self.damageEnemies.attackType = AttackTypes.Spell;
            else
                self.damageEnemies.attackType = AttackTypes.Generic;
            //Log("Hatchling attack type set to " + self.damageEnemies.attackType.ToString());

            orig(self);
        }
        private void GlowingWombDungCloudDamage(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            // Glowing Womb Cloud
            if (self.Fsm.GameObject.name == "Dung Explosion(Clone)" && self.Fsm.Name == "Explosion Control" && self.State.Name == "Explode")
            {
                // Duration & Visuals
                self.time.Value = LS.glowingWombDefendersCrestDuration;
                self.Fsm.GameObject.transform.Find("Particle System").GetComponent<ParticleSystem>().startLifetime = LS.glowingWombDefendersCrestDuration;
                self.Fsm.GameObject.transform.Find("Particle System").GetComponent<ParticleSystem>().maxParticles = 120;
                self.Fsm.GameObject.transform.Find("Particle System").GetComponent<ParticleSystem>().startSpeed = 3.5f * (1f / LS.glowingWombDefendersCrestDuration) * (LS.glowingWombDefendersCrestRadius / 6f);

                // Damage Rate
                self.Fsm.GameObject.GetComponent<DamageEffectTicker>().damageInterval = LS.glowingWombDefendersCrestDamageRate;
                self.Fsm.GameObject.GetComponent<DamageEffectTicker>().extraDamageType = ExtraDamageTypes.Spore;    //  1 damage per tick
            }

            // Hatchling Spawn Rate
            else if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Hatchling Spawn" && self.State.Name == "Equipped")
            {
                self.time.Value = (PlayerDataAccess.equippedCharm_1 ? LS.glowingWombGatheringSwarmSpawnRate : LS.glowingWombSpawnRate);
            }

            orig(self);
        }
        private void GlowingWombDungRadius(On.HutongGames.PlayMaker.Actions.SetScale.orig_OnEnter orig, SetScale self)
        {
            if (self.Fsm.GameObject.name == "Dung Explosion(Clone)" && self.Fsm.Name == "Explosion Control" && self.State.Name == "Explode")
            {
                // Cloud Radius
                self.Fsm.GameObject.GetComponent<UnityEngine.CircleCollider2D>().radius = LS.glowingWombDefendersCrestRadius;
                //Log("Hatchling dung cloud radius set");
            }

            orig(self);
        }
        private void HatchlingFotFSettings(ILContext il)
        {
            // Hatchling HP Requirement
            ILCursor cursor = new ILCursor(il).Goto(0);

            cursor.TryGotoNext
                (
                i => i.MatchLdstr("health"),
                i => i.MatchCallvirt<PlayerData>("GetInt"),
                i => i.MatchLdcI4(1)
                );
            cursor.GotoNext();
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(health => furyActive ? 1 : 0);

            // Hatchling Damage Increase
            cursor.Goto(0);
            cursor.TryGotoNext
                (
                i => i.MatchDup(),
                i => i.MatchLdindI4(),
                i => i.MatchLdcI4(5)
                );
            cursor.GotoNext();
            cursor.GotoNext();
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(HatchlingBonusDamage => LS.glowingWombFotFDamageIncrease);
        }
        #endregion

//	Grimmchild
        #region Grimmchild Changes
        private void GrimmchildDamage(On.HutongGames.PlayMaker.Actions.SetIntValue.orig_OnEnter orig, SetIntValue self)
        {
            if (self.Fsm.GameObject.name == "Grimmchild(Clone)" && self.Fsm.Name == "Control")
            {
                int damageModifier = (PlayerDataAccess.equippedCharm_11 ? LS.grimmchildFlukenestDamageIncrease : 0);
                if (self.State.Name == "Level 2")
                {
                    self.intValue.Value = LS.grimmchildDamage2 + damageModifier;
                }
                else if (self.State.Name == "Level 3")
                {
                    self.intValue.Value = LS.grimmchildDamage3 + damageModifier;
                }
                else if (self.State.Name == "Level 4")
                {
                    self.intValue.Value = LS.grimmchildDamage4 + damageModifier;
                }
            }

            orig(self);
        }

        private void GrimmchildAttackTimer(On.HutongGames.PlayMaker.Actions.SetFloatValue.orig_OnEnter orig, SetFloatValue self)
        {
            if (self.Fsm.GameObject.name == "Grimmchild(Clone)" && self.Fsm.Name == "Control" && (self.State.Name == "Pause" || self.State.Name == "Spawn"))
            {
                self.floatValue.Value = (PlayerDataAccess.equippedCharm_1 ? LS.grimmchildGatheringSwarmAttackTimer : LS.grimmchildAttackTimer);
            }

            orig(self);
        }

        private void GrimmchildAttackTimer2(On.HutongGames.PlayMaker.Actions.RandomFloat.orig_OnEnter orig, RandomFloat self)
        {
            if (self.Fsm.GameObject.name == "Grimmchild(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Antic")
            {
                self.min.Value = (PlayerDataAccess.equippedCharm_1 ? LS.grimmchildGatheringSwarmAttackTimer : LS.grimmchildAttackTimer);
                self.max.Value = (PlayerDataAccess.equippedCharm_1 ? LS.grimmchildGatheringSwarmAttackTimer : LS.grimmchildAttackTimer);
            }

            orig(self);
        }
        private void GrimmchildProjectileSpeed(On.HutongGames.PlayMaker.Actions.FireAtTarget.orig_OnEnter orig, FireAtTarget self)
        {
            //Log(self.Fsm.GameObject.name + self.Fsm.Name + self.State.Name);
            if (self.Fsm.GameObject.name == "Grimmchild(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Shoot")
            {
                self.speed = LS.grimmchildProjectileSpeed;
                self.spread = LS.grimmchildProjectileSpread;
                //Log("FireAtTarget speed: " + self.speed + " and spread: " + self.spread);
            }

            orig(self);
        }
        private void GrimmchildDetectRadius(On.HutongGames.PlayMaker.Actions.CallMethodProper.orig_OnEnter orig, CallMethodProper self)
        {
            if (self.Fsm.GameObject.name == "Grimmchild(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Check For Target")
            {
                //  Get the Enemy Range GameObject from the FSM variables
                GameObject grimmchildEnemyRange = self.Fsm.GetOwnerDefaultTarget(self.gameObject);

                //  Set its localPosition closer to the knight (when grimmchild is idling)
                grimmchildEnemyRange.transform.localPosition = new Vector3(2f, -1.5f, 0f);
            }

            orig(self);
        }
        private void RemoveGrimmballTerrainCollision(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            if (self.Fsm.GameObject.name == "Grimmball(Clone)" && self.Fsm.Name == "Control"
                && LS.grimmchildBypassTerrain)
            {
                self.Fsm.GameObject.RemoveComponent<UnityEngine.CircleCollider2D>();
                //Log("attempting to disable impact collision detection");
            }

            orig(self);
        }
        #endregion

//	Grubberfly's Elegy
        #region Grubberfly's Elegy Changes
        private void GrubberflysShootBeam(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            while (cursor.TryGotoNext
                (
                i => i.MatchLdstr("health"),
                i => i.MatchCallvirt<PlayerData>("GetInt"),
                i => i.MatchLdcI4(1)
                ))
            {
                cursor.GotoNext();
                cursor.GotoNext();
                cursor.EmitDelegate<Func<int, int>>(health => furyActive ? 1 : 0);
            }

            cursor = new ILCursor(il).Goto(0);
            while (cursor.TryGotoNext
                (
                i => i.MatchLdstr("healthBlue"),
                i => i.MatchCallvirt<PlayerData>("GetInt"),
                i => i.MatchLdcI4(1)
                ))
            {
                cursor.GotoNext();
                cursor.GotoNext();
                cursor.GotoNext();
                cursor.EmitDelegate<Func<int, int>>(health => 99);
            }
        }
        private int GrubberflysElegyDamage(string name, int orig)
        {
            if (name == "beamDamage")
            {
                return (int)(PlayerDataAccess.nailDamage * LS.grubberflysElegyDamageScale);
            }
            return orig;
        }
        private void GrubberflysElegyAllowJoniBeam(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, CollisionSide damageSide, int damageAmount, int hazardType)
        {
            int fullHealth = (PlayerData.instance.maxHealth + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.joniHealthBlue - LS.jonisBlessingLifeblood : 0));

            orig(self, go, damageSide, damageAmount, hazardType);

            HeroControllerR.joniBeam = PlayerDataAccess.health + PlayerDataAccess.healthBlue >= fullHealth;
        }
        private void GrubberflyBeamVelocity(On.HutongGames.PlayMaker.Actions.SetVelocity2d.orig_OnEnter orig, SetVelocity2d self)
        {
            Vector2 originalVelocity = self.vector.Value;
            if (self.Fsm.GameObject.name.StartsWith("Grubberfly Beam") && self.Fsm.Name == "Control" && self.State.Name == "Init"
                && LS.grubberflysElegyRangeAffected)
            {
                //  Get square root of nail range to apply as modifier to beam velocity.
                float velocityModifier = Mathf.Sqrt(((100 + (PlayerDataAccess.equippedCharm_18 ? LS.longnailRangeIncrease : 0) 
                    + (PlayerDataAccess.equippedCharm_13 ? LS.markOfPrideRangeIncrease : 0)) / 100f));
                self.vector = Vector2.Scale(originalVelocity, new Vector2(velocityModifier, velocityModifier));
                //Log("Beam velocity set to " + self.vector.Value.ToString());
            }

            orig(self);

            if (self.vector.Value != originalVelocity)
            {
                self.vector = originalVelocity;
            }
        }

        private void GrubberflyBeamDuration(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            float originalTime = self.time.Value;
            if (self.Fsm.GameObject.name.StartsWith("Grubberfly Beam") && self.Fsm.Name == "Control" && self.State.Name.Contains("ctive")
                && LS.grubberflysElegyRangeAffected)
            {
                //  Get square root of nail range to apply as modifier to beam duration.
                float durationModifier = Mathf.Sqrt(((100 + (PlayerDataAccess.equippedCharm_18 ? LS.longnailRangeIncrease : 0)
                    + (PlayerDataAccess.equippedCharm_13 ? LS.markOfPrideRangeIncrease : 0)) / 100f));
                self.time = originalTime * durationModifier;
                //Log("Beam duration set to " + self.time.Value);
            }

            orig(self);

            if (self.time.Value != originalTime)
            {
                self.time = originalTime;
            }
        }
        private void GrubberflysSoulGain(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            if (hitInstance.Source.name.StartsWith("Grubberfly Beam") && (PlayerDataAccess.equippedCharm_20 || PlayerDataAccess.equippedCharm_21))
            {
                int soulGain = (PlayerDataAccess.equippedCharm_20 ? LS.grubberflysElegySoulCatcherSoulGain : 0)
                    + (PlayerDataAccess.equippedCharm_21 ? LS.grubberflysElegySoulEaterSoulGain : 0);
                HeroController.instance.AddMPCharge(soulGain);
            }

            orig(self, hitInstance);
        }
        //	Ignore vanilla FotF beam damage increase
        private void IgnoreVanillaGEFotFDamage(On.HutongGames.PlayMaker.Actions.FloatMultiply.orig_OnEnter orig, FloatMultiply self)
        {
            if (self.Fsm.GameObject.name.StartsWith("Grubberfly Beam") && self.Fsm.Name == "Control" && self.State.Name == "Fury Multiplier")
            {
                self.multiplyBy.Value = 1f;	//	no multiplier
            }

            orig(self);
        }
        //  Ignore vanilla Mark of Pride beam scaling
        private void IgnoreVanillaGEMoPSize(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            while (cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("MANTIS_CHARM_SCALE")))
            {
                cursor.GotoNext();
                cursor.EmitDelegate<Func<float, float>>(scale => 1f);   //  no multiplier
            }
        }
        #endregion

//	Grubsong
        #region Grubsong Changes
        //  Set Grubsong soul gain after changing charms
        private void GrubsongSetSoulGain(PlayerData data, HeroController controller)
        {
            if (PlayerDataAccess.equippedCharm_3)
            {
                grubSoulGain = LS.grubsongSoulGain;
                grubSoulGain += PlayerDataAccess.equippedCharm_35 ? LS.grubsongGrubberflysElegySoulGainIncrease : 0;
                grubSoulGain += PlayerDataAccess.equippedCharm_28 ? LS.grubsongShapeOfUnnSoulGainIncrease : 0;
                //Log("Grubsong soul gain set to " + grubSoulGain);
            }
        }
        private void GrubsongSoulChanges(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("GRUB_SOUL_MP_COMBO"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => grubSoulGain);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("GRUB_SOUL_MP"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => grubSoulGain);
        }
        #endregion

//	Heavy Blow
        #region Heavy Blow Changes
		//	Nail Damage Increase
		private int HeavyBlowNailDamageIncrease(string name, int orig)
        {
            if (name == "nailDamage") {
                orig = (int)(orig + (PlayerDataAccess.equippedCharm_15 ? LS.heavyBlowNailDamageIncrease : 0));
            }

            return orig;
        }
		//	Stagger Improvement
        private void HeavyBlowStagger(On.HutongGames.PlayMaker.Actions.IntOperator.orig_OnEnter orig, IntOperator self)
        {
            if ((self.Fsm.Name == "Stun" || self.Fsm.Name == "Stun Control") && self.State.Name == "Heavy Blow")
            {
                if (self.integer1.Name == "Stun Hit Max") {
                    self.integer2.Value = LS.heavyBlowStagger;
                }
                else {
                    self.integer2.Value = LS.heavyBlowStaggerCombo;
                }
            }

            orig(self);
        }
		//	Combo Stagger Improvement
        private void HeavyBlowComboStaggerFix(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, IntCompare self)
        {
            if ((self.Fsm.Name == "Stun" || self.Fsm.Name == "Stun Control") && self.State.Name == "In Combo") {
                self.greaterThan = FsmEvent.GetFsmEvent("STUN");
            }

            orig(self);
        }
        private void HeavyBlowEnviroHit(On.HutongGames.PlayMaker.Actions.IntOperator.orig_OnEnter orig, IntOperator self)
        {
            if (PlayerDataAccess.equippedCharm_15)
            {
                if ((self.Fsm.GameObject.name.StartsWith("Geo Rock") && self.Fsm.Name.StartsWith("Geo Rock") && self.State.Name == "Hit")
                    || (self.Fsm.Name.StartsWith("breakable_wall") && self.State.Name.StartsWith("Hit "))
                    || (self.Fsm.GameObject.name.StartsWith("Hive Breakable ") && self.State.Name == "Damage"))
                {
                    self.integer2 = LS.heavyBlowEnviroHits;
                    //Log("Heavy Blow damages environment faster.");
                }
            }

            orig(self);
        }
        private void HeavyBlowOneWayHit(On.HutongGames.PlayMaker.Actions.IntSwitch.orig_OnEnter orig, IntSwitch self)
        {
            if (PlayerDataAccess.equippedCharm_15 && self.Fsm.Name.StartsWith("break_floor") && self.State.Name == "Hit")
            {
                if ((self.Fsm.GetFsmInt("Hits").Value + 1) * LS.heavyBlowEnviroHits >= 3)
                {
                    Log("break_floor should be destroyed.");
                    if (self.Fsm.GetState("PlayerData") != null)
                        self.Fsm.SetState("PlayerData");
                    else
                        self.Fsm.SetState("Break");
                }
            }

            orig(self);
        }
        private void HeavyBlowGeoRockFling(On.HutongGames.PlayMaker.Actions.FlingObjectsFromGlobalPool.orig_OnEnter orig, FlingObjectsFromGlobalPool self)
        {
            bool mustReset = false;
            int normalSpawn = 0;
            if (self.Fsm.GameObject.name.StartsWith("Geo Rock") && self.Fsm.Name.StartsWith("Geo Rock") && self.State.Name == "Hit"
                && PlayerDataAccess.equippedCharm_15)
            {
                mustReset = true;
                normalSpawn = self.spawnMax.Value;
                self.spawnMin = normalSpawn * Math.Min(LS.heavyBlowEnviroHits, self.Fsm.GetFsmInt("Hits").Value);
                self.spawnMax = normalSpawn * Math.Min(LS.heavyBlowEnviroHits, self.Fsm.GetFsmInt("Hits").Value);
                //Log("Geo rock dropped additional geo.");
            }

            orig(self);

            if (mustReset)
            {
                self.spawnMin = normalSpawn;
                self.spawnMax = normalSpawn;
            }
        }

        #endregion

//	Hiveblood
        #region Hiveblood Changes
        private void SetHivebloodValues(On.HeroController.orig_MaxHealth orig, HeroController self)
        {
            if (PlayerDataAccess.equippedCharm_29)
            {
                if (PlayerDataAccess.equippedCharm_23 && !PlayerDataAccess.equippedCharm_27)
                {
                    hivebloodRegenLimit = LS.hivebloodRegenLimit + LS.hivebloodFragileHeartLimitIncrease;
                    hivebloodLocalMaxHP = PlayerDataAccess.maxHealth;
                }
                else if (PlayerDataAccess.equippedCharm_27)
                {
                    hivebloodJoniMasks.Clear();

                    hivebloodRegenLimit = LS.hivebloodRegenLimit + (PlayerDataAccess.equippedCharm_8 ? LS.hivebloodLifebloodHeartLimitIncrease : 0);
                    hivebloodLocalMaxHP = PlayerDataAccess.maxHealth + PlayerDataAccess.joniHealthBlue;
                }
                else
                {
                    hivebloodRegenLimit = LS.hivebloodRegenLimit;
                    hivebloodLocalMaxHP = PlayerDataAccess.maxHealth;
                }
                hivebloodRegenMore = false;
                hivebloodRegenTimes = 0;
                hivebloodRegensLeft = 0;

                //Log("Hiveblood regen limit set to " + hivebloodRegenLimit);
                //Log("Hiveblood local max HP set to " + hivebloodLocalMaxHP);
            }

            orig(self);
        }
        private void HivebloodCooldowns(On.HutongGames.PlayMaker.Actions.FloatCompare.orig_OnEnter orig, FloatCompare self)
        {
            if (self.Fsm.GameObject.name == "Health" && self.Fsm.Name == "Hive Health Regen" && self.State.Name.StartsWith("Recover "))
            {
                float regenCooldown = LS.hivebloodCooldown + (hivebloodRegenTimes * LS.hivebloodCooldownDecceleration);
                self.float2.Value = regenCooldown / 2f;
                //if (self.State.Name == "Recover 1") Log("Hiveblood regen will take " + regenCooldown + " seconds.");
            }
            else if (self.Fsm.GameObject.name == "Blue Health Hive(Clone)" && self.Fsm.Name == "blue_health_display" && self.State.Name.StartsWith("Regen "))
            {
                float regenCooldown = LS.hivebloodJonisCooldown + (hivebloodRegenTimes * LS.hivebloodJonisCooldownDecceleration);
                self.float2.Value = regenCooldown / 2f;
                //if (self.State.Name == "Regen 1") Log("Hiveblood regen will take " + regenCooldown + " seconds.");
            }

            orig(self);
        }
        //  Update local hiveblood values after healing or taking damage
        private void HivebloodUpdateOnHeal(On.HeroController.orig_AddHealth orig, HeroController self, int amount)
        {
            orig(self,amount);

            if (hivebloodRegenMore && PlayerDataAccess.equippedCharm_29 && !PlayerDataAccess.equippedCharm_27)
            {
                if (PlayerDataAccess.health < hivebloodLocalMaxHP)
                {
                    hivebloodRegenTimes += amount;
                    hivebloodRegensLeft -= amount;
                    hivebloodRegensLeft = Math.Max(hivebloodRegensLeft, 0);
                    hivebloodRegenMore = hivebloodRegensLeft > 0;
                    //Log("Healed. " + hivebloodRegensLeft + " hiveblood regens remain.");
                }
                else
                {
                    hivebloodRegenMore = false;
                    hivebloodRegenTimes = 0;
                    hivebloodRegensLeft = 0;
                    //Log("Healed to local max HP.");
                }
            }
        }
        private void HivebloodUpdateOnJonisRegen(On.HutongGames.PlayMaker.Actions.SendEventByName.orig_OnEnter orig, SendEventByName self)
        {
            orig(self);

            if (self.Fsm.GameObject.name == "Blue Health Hive(Clone)" && self.Fsm.Name == "blue_health_display" && self.State.Name == "Heal"
                && hivebloodRegenMore && PlayerDataAccess.equippedCharm_29)
            {
                int currentHP = PlayerDataAccess.health + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.healthBlue : 0);

                if (currentHP < hivebloodLocalMaxHP)
                {
                    hivebloodRegenTimes += 1;
                    hivebloodRegensLeft -= 1;
                    hivebloodRegensLeft = Math.Max(hivebloodRegensLeft, 0);
                    hivebloodRegenMore = hivebloodRegensLeft > 0;
                    //Log("Regened. " + hivebloodRegensLeft + " hiveblood regens remain.");

                    //  shout at just the next joniMask to start regen
                    if (currentHP <= hivebloodJoniMasks.Count)
                        hivebloodJoniMasks[currentHP-1].SendEvent("RESTART");
                }
                else
                {
                    hivebloodRegenMore = false;
                    hivebloodRegenTimes = 0;
                    hivebloodRegensLeft = 0;
                    //Log("Regened to local max HP.");
                }
            }
        }
        private void HivebloodUpdateOnDamage(On.PlayerData.orig_TakeHealth orig, PlayerData self, int amount)
        {
            orig(self, amount);

            if (PlayerDataAccess.equippedCharm_29)
            {
                int currentHP = PlayerDataAccess.health + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.healthBlue : 0);
                hivebloodLocalMaxHP = Math.Min(hivebloodLocalMaxHP, currentHP + hivebloodRegenLimit);

                if (currentHP < hivebloodLocalMaxHP)
                {
                    hivebloodRegenMore = true;
                    hivebloodRegenTimes = 0;
                    hivebloodRegensLeft += amount;
                    hivebloodRegensLeft = Math.Min(hivebloodRegensLeft, hivebloodLocalMaxHP - currentHP);
                    hivebloodRegensLeft = Math.Min(hivebloodRegensLeft, hivebloodRegenLimit);

                    //Log("Damaged. " + hivebloodRegensLeft + " hiveblood regens remain.");
                }
                else
                {
                    hivebloodRegenMore = false;
                    hivebloodRegenTimes = 0;
                    hivebloodRegensLeft = 0;
                }
            }
        }
        private void HivebloodCanRegenMore(On.HutongGames.PlayMaker.FsmState.orig_OnEnter orig, FsmState self)
        {
            orig(self);

            if (self.Fsm.GameObject.name == "Health" && self.Fsm.Name == "Hive Health Regen" && self.Name == "Idle" 
                && hivebloodRegenMore && !PlayerDataAccess.equippedCharm_27)
            {
                self.Fsm.SetState("Start Recovery");
            }
            else if (self.Fsm.GameObject.name == "Blue Health Hive(Clone)" && self.Fsm.Name == "blue_health_display" && self.Name == "Empty Wait")
            {
                //Log("Blue Health Hive entered Empty Wait");
                self.Fsm.GameObject.GetComponent<tk2dSpriteAnimator>().Stop();
                self.Fsm.GameObject.GetComponent<tk2dSprite>().color = Color.clear;
            }
            else if (self.Fsm.GameObject.name == "Blue Health Hive(Clone)" && self.Fsm.Name == "blue_health_display" && self.Name == "Regen 1")
            {
                //Log("Blue Health Hive entered Regen 1");
                self.Fsm.GameObject.GetComponent<tk2dSprite>().color = Color.white;
            }
        }
        //  Edit the FSM of each Blue Health Hive game object so it sticks around after being removed
        //  Also add them all to a list to be referenced later.
        private void EditBlueHealthHiveFSM(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.gameObject.name == "Blue Health Hive(Clone)" && self.FsmName == "blue_health_display")
            {
                hivebloodJoniMasks.Add(self);

                FsmState Empty_Wait = self.AddFsmState("Empty Wait");
                //  add action that clears animation
                //Tk2dStopAnimation stopAnim = new();
                //Satchel.FsmUtil.AddAction(Empty_Wait, stopAnim);
                Satchel.FsmUtil.AddTransition(Empty_Wait, "RESTART", "Regen 1");

                FsmState Finish_Anim = self.GetValidState("Finish Anim");
                Satchel.FsmUtil.ChangeTransition(Finish_Anim, "FINISHED", "Empty Wait");

                FsmState Regen_1 = self.GetValidState("Regen 1");
                Satchel.FsmUtil.ChangeTransition(Regen_1, "HERO DAMAGED", "Empty Wait");

                FsmState Regen_2 = self.GetValidState("Regen 2");
                Satchel.FsmUtil.ChangeTransition(Regen_2, "HERO DAMAGED", "Empty Wait");

                //Log("Joni Mask " + hivebloodJoniMasks.Count + " edited and added to list.");
            }
        }
        #endregion

//	Joni's Blessing
        #region Joni's Blessing Changes
        private void JonisBlessingLifeblood(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.GotoNext(i => i.MatchLdcR4(1.4f));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(scale => 0.95f + ((float)(LS.jonisBlessingLifeblood 
                + (PlayerDataAccess.equippedCharm_8 ? LS.lifebloodHeartLifeblood : 0)) / (float)(PlayerDataAccess.maxHealth)));
            //  Joni's Lifeblood (plus Lifeblood Heart) divided by maxHealth to get correct %.
            //  Has to be added to 0.95f instead of 1f so Joni's rounds UP to the expected value.
            //  As a result, jonisHealthBlue in game will always be 1 less than the actual maxHealth including the Joni's masks.
        }
        #endregion

//	Kingsoul / Void Heart
        #region Kingsoul Changes
        private void KingsoulTimer(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "White Charm" && self.State.Name == "Wait")
            {
                self.time.Value = LS.kingsoulRegenTickRate;
            }

            orig(self);
        }
        private void KingsoulSoul(On.HutongGames.PlayMaker.Actions.SendMessageV2.orig_DoSendMessage orig, SendMessageV2 self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "White Charm" && self.State.Name == "Soul UP")
            {
                self.functionCall.IntParameter.Value = LS.kingsoulSoulGain;
            }

            orig(self);
        }
        private void VoidHeartSoulRegen(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.gameObject.name == "Charm Effects" && self.FsmName == "White Charm")
            {
                
                IntCompare intCompare = Satchel.FsmUtil.GetAction<IntCompare>(self, "Check", 2);
                intCompare.greaterThan = FsmEvent.GetFsmEvent("ACTIVE");
            }
        }
        #endregion

//	Lifeblood Heart/Core
        #region Lifeblood Heart/Core Changes
        private int SetBaseLifeblood()
        {
            int baseLifeblood = 0;

            if (PlayerDataAccess.equippedCharm_9)
            {
                baseLifeblood = LS.lifebloodCoreLifeblood 
                    + (PlayerDataAccess.equippedCharm_8 && !(PlayerDataAccess.equippedCharm_27 && PlayerDataAccess.equippedCharm_29) ? LS.lifebloodHeartLifeblood : 0);
                lifebloodCoreCost = (PlayerDataAccess.equippedCharm_14 ? LS.lifebloodCoreSteadyBodyCost : LS.lifebloodCoreCost);
                lifebloodCoreCost = Math.Min(lifebloodCoreCost, (PlayerDataAccess.equippedCharm_33 ? LS.lifebloodCoreSpellTwisterCost : lifebloodCoreCost));
                lifebloodCoreTimer = LS.lifebloodCoreCooldown;
                lifebloodCoreMax = baseLifeblood;
            }
            else if (PlayerDataAccess.equippedCharm_29 && PlayerDataAccess.equippedCharm_27)
            {
                baseLifeblood = 0;
                //if (PlayerDataAccess.equippedCharm_8) Log("Lifeblood Heart will be added to Joni's health.");
            }
            else if (PlayerDataAccess.equippedCharm_4)
            {
                baseLifeblood = 1;
                //Log("Stalwart Shell regen active.");
            }
            else if (PlayerDataAccess.equippedCharm_8)
            {
                baseLifeblood = LS.lifebloodHeartLifeblood;
            }

            return baseLifeblood;
        }
        private void LifebloodCoreRegen()
        {
            if (PlayerDataAccess.equippedCharm_9)
            {
                lifebloodCoreMax = (PlayerDataAccess.healthBlue > lifebloodCoreMax) ? PlayerDataAccess.healthBlue : lifebloodCoreMax;
                lifebloodCoreTimer -= Time.deltaTime;

                if (lifebloodCoreTimer < 0f)
                {
                    if (PlayerDataAccess.healthBlue < lifebloodCoreMax && PlayerDataAccess.MPCharge >= lifebloodCoreCost)
                    {
                        HeroController.instance.TakeMP(lifebloodCoreCost);
                        EventRegister.SendEvent("ADD BLUE HEALTH");
                        lifebloodCoreTimer = LS.lifebloodCoreCooldown;
                        //Log("Lifeblood Core overhealed.");
                    }
                    else
                    {
                        lifebloodCoreTimer = Math.Min(LS.lifebloodCoreCooldown, 2.4f);
                        //if (PlayerDataAccess.healthBlue >= lifebloodCoreMax)
                        //    Log("Lifeblood Core unable to heal; max overheal reached.");
                        //else if (PlayerDataAccess.MPCharge < lifebloodCoreCost)
                        //    Log("Lifeblood Core unable to heal; insufficient soul.");
                    }
                }
            }
        }
        //  Ignore vanilla lifeblood additions
        private void IgnoreVanillaBlueHearts(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdcI4(2));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(Lifeblood => 0);    //  zero lifeblood

            cursor.TryGotoNext(i => i.MatchLdcI4(4));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(Lifeblood => 0);    //  zero lifeblood
        }
        #endregion

//	Longnail / Mark of Pride
        #region Longnail / Mark of Pride Changes
        //	Mark of Pride Nail Damage Increase
        private int MarkOfPrideNailDamageIncrease(string name, int orig)
        {
            if (name == "nailDamage") {
                orig = (int)(orig + (PlayerDataAccess.equippedCharm_13 ? LS.markOfPrideNailDamageIncrease : 0));
            }

            return orig;
        }
        private void NailSlashSizeScale(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);

            // Longnail + Mark of Pride Scaling
            while (cursor.TryGotoNext(i => i.MatchLdcR4(1.4f)))
            {
                cursor.GotoNext();
                cursor.EmitDelegate<Func<float, float>>(scale => (float)(1f + ((LS.markOfPrideRangeIncrease + LS.longnailRangeIncrease) / 100f)));
            }

            // Mark of Pride Scaling
            while (cursor.TryGotoNext(i => i.MatchLdcR4(1.25f)))
            {
                cursor.GotoNext();
                cursor.EmitDelegate<Func<float, float>>(scale => (float)(1f + (LS.markOfPrideRangeIncrease / 100f)));
            }

            // Longnail Scaling
            while (cursor.TryGotoNext(i => i.MatchLdcR4(1.15f)))
            {
                cursor.GotoNext();
                cursor.EmitDelegate<Func<float, float>>(scale => (float)(1f + (LS.longnailRangeIncrease / 100f)));
            }

            //  Set Longnail to use mantis nail swing animation
            cursor.TryGotoNext(i => i.MatchLdfld<NailSlash>("animName"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<string, string>>(anim => (anim + " M"));
        }

        // Wall Slash Application
        private void OnFsmEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.gameObject.name == "Charm Effects" && self.FsmName == "Slash Size Modifiers")
            {
                self.AddFsmAction("Init", new FindChild()
                {
                    gameObject = new FsmOwnerDefault()
                    {
                        OwnerOption = OwnerDefaultOption.SpecifyGameObject,
                        GameObject = self.GetFsmGameObjectVariable("Attacks")
                    },
                    childName = "WallSlash",
                    storeResult = self.GetFsmGameObjectVariable("Wall Slash")
                });

                self.AddFsmAction("Equipped 2", new SendMessage()
                {
                    gameObject = new FsmOwnerDefault()
                    {
                        OwnerOption = OwnerDefaultOption.SpecifyGameObject,
                        GameObject = self.GetGameObjectVariable("Wall Slash")
                    },
                    delivery = 0,
                    options = SendMessageOptions.DontRequireReceiver,
                    functionCall = new FunctionCall()
                    {
                        FunctionName = "SetLongnail",
                        ParameterType = "bool",
                        BoolParameter = new FsmBool(true)
                    }
                });

                self.AddFsmAction("Unequipped 2", new SendMessage()
                {
                    gameObject = new FsmOwnerDefault()
                    {
                        OwnerOption = OwnerDefaultOption.SpecifyGameObject,
                        GameObject = self.GetGameObjectVariable("Wall Slash")
                    },
                    delivery = 0,
                    options = SendMessageOptions.DontRequireReceiver,
                    functionCall = new FunctionCall()
                    {
                        FunctionName = "SetLongnail",
                        ParameterType = "bool",
                        BoolParameter = new FsmBool(false)
                    }
                });

                self.AddFsmAction("Equipped", new SendMessage()
                {
                    gameObject = new FsmOwnerDefault()
                    {
                        OwnerOption = OwnerDefaultOption.SpecifyGameObject,
                        GameObject = self.GetGameObjectVariable("Wall Slash")
                    },
                    delivery = 0,
                    options = SendMessageOptions.DontRequireReceiver,
                    functionCall = new FunctionCall()
                    {
                        FunctionName = "SetMantis",
                        ParameterType = "bool",
                        BoolParameter = new FsmBool(true)
                    }
                });

                self.AddFsmAction("Unequipped", new SendMessage()
                {
                    gameObject = new FsmOwnerDefault()
                    {
                        OwnerOption = OwnerDefaultOption.SpecifyGameObject,
                        GameObject = self.GetGameObjectVariable("Wall Slash")
                    },
                    delivery = 0,
                    options = SendMessageOptions.DontRequireReceiver,
                    functionCall = new FunctionCall()
                    {
                        FunctionName = "SetMantis",
                        ParameterType = "bool",
                        BoolParameter = new FsmBool(false)
                    }
                });
            }
        }
        private void WallSlashSizeScale(On.HutongGames.PlayMaker.Actions.SendMessage.orig_OnEnter orig, SendMessage self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Slash Size Modifiers" && self.gameObject.GameObject.Name == "Wall Slash" && self.State.Name.StartsWith("Equipped"))
            {
                self.functionCall.BoolParameter.Value = LS.regularNailRangeAffectsWallSlash;
            }

            orig(self);
        }
        #endregion

//	Nailmaster's Glory
        #region Nailmaster's Glory Changes
        private void SetNailArtChargeTime(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("NAIL_CHARGE_TIME_CHARM"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => LS.nailmastersGloryChargeTime);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("NAIL_CHARGE_TIME_DEFAULT"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => LS.regularNailArtChargeTime);
        }
        private void NailArtPiercingSoulGain(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            if (hitInstance.AttackType == AttackTypes.Spell)
            {
                int soulGain = LS.regularSoulGain
                    + (PlayerDataAccess.equippedCharm_20 ? LS.soulCatcherSoulGain : 0)
                    + (PlayerDataAccess.equippedCharm_21 ? LS.soulEaterSoulGain : 0);
                if (hitInstance.Source.name == "Great Slash" || hitInstance.Source.name == "Dash Slash")
                {
                    HeroController.instance.AddMPCharge(soulGain);
                    //Log("soul gained");
                }
                else if (hitInstance.Source.name.StartsWith("Hit "))
                {
                    var grandparent = hitInstance.Source.transform.parent.parent;
                    if (grandparent != null)
                    {
                        if (grandparent.name == "Cyclone Slash")
                        {
                            HeroController.instance.AddMPCharge(soulGain);
                            //Log("soul gained");
                        }
                    }
                }
            }

            orig(self, hitInstance);
        }
        #endregion

//  Quick Focus
        #region Quick Focus Changes
        #endregion

//	Quick Slash
        #region Quick Slash Changes
        //  See regular nail changes
        #endregion

//	Shaman Stone
        #region Shaman Stone Changes
        private void ShamanStoneVengefulSpiritScaling(On.HutongGames.PlayMaker.Actions.SetScale.orig_OnEnter orig, SetScale self)
        {
            if (self.Fsm.GameObject.name == "Fireball(Clone)" && self.Fsm.Name == "Fireball Control" && self.State.Name == "Set Damage")
            {
                if (self.State.ActiveActionIndex == 0)
                {
                    self.x.Value = LS.regularVSSizeScaleX;
                    self.y.Value = LS.regularVSSizeScaleY;
                }
                else if (self.State.ActiveActionIndex == 6)
                {
                    self.x.Value = LS.shamanStoneVSSizeScaleX;
                    self.y.Value = LS.shamanStoneVSSizeScaleY;
                }
            }

            // Shade Soul
            else if (self.Fsm.GameObject.name == "Fireball2 Spiral(Clone)" && self.Fsm.Name == "Fireball Control" && self.State.Name == "Set Damage" && self.State.ActiveActionIndex == 0)
            {
                if (self.State.ActiveActionIndex == 0)
                {
                    self.x.Value = PlayerDataAccess.equippedCharm_19 ? 1.8f : LS.regularSSSizeScaleX * 1.8f;
                    self.y.Value = PlayerDataAccess.equippedCharm_19 ? 1.8f : LS.regularSSSizeScaleY * 1.8f;
                }
            }

            orig(self);
        }
        private void ShamanStoneShadeSoulScaling(On.HutongGames.PlayMaker.Actions.FloatMultiply.orig_OnEnter orig, FloatMultiply self)
        {
            if (self.Fsm.GameObject.name == "Fireball2 Spiral(Clone)" && self.Fsm.Name == "Fireball Control" && self.State.Name == "Set Damage")
            {
                if (self.floatVariable.Name == "X Scale")
                {
                    self.multiplyBy.Value = LS.shamanStoneSSSizeScaleX;
                }
                else if (self.floatVariable.Name == "Y Scale")
                {
                    self.multiplyBy.Value = LS.shamanStoneSSSizeScaleY;
                }
            }

            orig(self);
        }
        private void ShamanStoneDamage(On.HutongGames.PlayMaker.Actions.SetFsmInt.orig_OnEnter orig, SetFsmInt self)
        {
            if (self.Fsm.GameObject.name == "Fireball(Clone)" && self.Fsm.Name == "Fireball Control" && self.State.Name == "Set Damage")
            {
                if (self.State.ActiveActionIndex == 2)
                {
                    self.setValue.Value = LS.regularVSDamage + (furyActive ? LS.furyOfTheFallenVSDamageIncrease : 0);
                }
                else if (self.State.ActiveActionIndex == 4)
                {
                    self.setValue.Value = LS.shamanStoneVSDamage + (furyActive ? LS.furyOfTheFallenVSDamageIncrease : 0);
                }
            }
            else if (self.Fsm.GameObject.name == "Fireball2 Spiral(Clone)" && self.Fsm.Name == "Fireball Control" && self.State.Name == "Set Damage")
            {
                if (self.State.ActiveActionIndex == 3)
                {
                    self.setValue.Value = LS.regularSSDamage + (furyActive ? LS.furyOfTheFallenSSDamageIncrease : 0);
                }
                else if (self.State.ActiveActionIndex == 5)
                {
                    self.setValue.Value = LS.shamanStoneSSDamage + (furyActive ? LS.furyOfTheFallenSSDamageIncrease : 0);
                }
            }
            else if (self.Fsm.Name == "Set Damage" && self.State.Name == "Set Damage")
            {
                if (self.Fsm.GameObject.transform.parent.gameObject.name == "Scr Heads")
                {
                    if (self.State.ActiveActionIndex == 0)
                    {
                        self.setValue.Value = LS.regularHWDamage + (furyActive ? LS.furyOfTheFallenHWDamageIncrease : 0);
                    }
                    else if (self.State.ActiveActionIndex == 2)
                    {
                        self.setValue.Value = LS.shamanStoneHWDamage + (furyActive ? LS.furyOfTheFallenHWDamageIncrease : 0);
                    }
                }
                else if (self.Fsm.GameObject.transform.parent.gameObject.name == "Scr Heads 2")
                {
                    if (self.State.ActiveActionIndex == 0)
                    {
                        self.setValue.Value = LS.regularASDamage + (furyActive ? LS.furyOfTheFallenASDamageIncrease : 0);
                    }
                    else if (self.State.ActiveActionIndex == 2)
                    {
                        self.setValue.Value = LS.shamanStoneASDamage + (furyActive ? LS.furyOfTheFallenASDamageIncrease : 0);
                    }
                }
                else if (self.Fsm.GameObject.transform.parent.gameObject.name == "Q Slam")
                {
                    if (self.State.ActiveActionIndex == 0)
                    {
                        self.setValue.Value = LS.regularDDiveDamage + (furyActive ? LS.furyOfTheFallenDDiveDamageIncrease : 0);
                    }
                    else if (self.State.ActiveActionIndex == 2)
                    {
                        self.setValue.Value = LS.shamanStoneDDiveDamage + (furyActive ? LS.furyOfTheFallenDDiveDamageIncrease : 0);
                    }
                }
                else if (self.Fsm.GameObject.transform.parent.gameObject.name == "Q Slam 2")
                {
                    if (self.Fsm.GameObject.name == "Hit L")
                    {
                        if (self.State.ActiveActionIndex == 0)
                        {
                            self.setValue.Value = LS.regularDDarkDamageL + (furyActive ? LS.furyOfTheFallenDDarkDamage1Increase : 0);
                        }
                        else if (self.State.ActiveActionIndex == 2)
                        {
                            self.setValue.Value = LS.shamanStoneDDarkDamageL + (furyActive ? LS.furyOfTheFallenDDarkDamage1Increase : 0);
                        }
                    }
                    else if (self.Fsm.GameObject.name == "Hit R")
                    {
                        if (self.State.ActiveActionIndex == 0)
                        {
                            self.setValue.Value = LS.regularDDarkDamageR + (furyActive ? LS.furyOfTheFallenDDarkDamage1Increase : 0);
                        }
                        else if (self.State.ActiveActionIndex == 2)
                        {
                            self.setValue.Value = LS.shamanStoneDDarkDamageR + (furyActive ? LS.furyOfTheFallenDDarkDamage1Increase : 0);
                        }
                    }
                }
                else if (self.Fsm.GameObject.name == "Q Fall Damage")
                {
                    if (self.State.ActiveActionIndex == 0)
                    {
                        self.setValue.Value = LS.regularDiveDamage + (furyActive ? LS.furyOfTheFallenDiveDamageIncrease : 0);
                    }
                    else if (self.State.ActiveActionIndex == 2)
                    {
                        self.setValue.Value = LS.shamanStoneDiveDamage + (furyActive ? LS.furyOfTheFallenDiveDamageIncrease : 0);
                    }
                }
            }

            orig(self);
        }
        private void ShamanStoneQMegaDamage(On.HutongGames.PlayMaker.Actions.FloatCompare.orig_OnEnter orig, FloatCompare self)
        {
            if (self.Fsm.GameObject.name == "Q Mega" && self.Fsm.Name == "Hit Box Control" && self.State.Name == "Check Scale")
            {
                int ddarkSecondWaveDamage = (PlayerDataAccess.equippedCharm_19 ? LS.shamanStoneDDarkDamageMega : LS.regularDDarkDamageMega) + (furyActive ? LS.furyOfTheFallenDDarkDamage2Increase : 0);
                self.Fsm.GameObject.transform.Find("Hit L").gameObject.LocateMyFSM("damages_enemy").GetFsmIntVariable("damageDealt").Value = ddarkSecondWaveDamage;
                self.Fsm.GameObject.transform.Find("Hit R").gameObject.LocateMyFSM("damages_enemy").GetFsmIntVariable("damageDealt").Value = ddarkSecondWaveDamage;
            }

            orig(self);
        }
        #endregion

//	Shape of Unn
        #region Shape Of Unn Changes
        private void SlugSpeeds(On.HutongGames.PlayMaker.Actions.SetFloatValue.orig_OnEnter orig, SetFloatValue self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control")
            {
                if (self.State.Name == "Slug Speed")
                {
                    if (self.State.ActiveActionIndex == 3)
                    {
                        self.floatValue.Value = -LS.shapeOfUnnSpeed;
                    }
                    else if (self.State.ActiveActionIndex == 4)
                    {
                        self.floatValue.Value = LS.shapeOfUnnSpeed;
                    }
                    else if (self.State.ActiveActionIndex == 6)
                    {
                        self.floatValue.Value = -LS.shapeOfUnnQuickFocusSpeed;
                    }
                    else if (self.State.ActiveActionIndex == 7)
                    {
                        self.floatValue.Value = LS.shapeOfUnnQuickFocusSpeed;
                    }
                }
            }

            orig(self);
        }
        private int ShapeOfUnnReserveIncrease(string name, int orig)
        {
            if (name == "MPReserveMax" && PlayerDataAccess.equippedCharm_28)
            {
                return Math.Min(orig + 33, 132);
            }

            return orig;
        }
        private int ShapeOfUnnVesselGainFix(string name, int orig)
        {
            if (name == "MPReserveMax" && PlayerDataAccess.equippedCharm_28)
            {
                return Math.Min(orig - 33, 132);
            }

            return orig;
        }
        private void ShapeOfUnnUpdateVesselUI(PlayerData data, HeroController controller)
        {
            if (shapeOfUnnVessel != PlayerDataAccess.equippedCharm_28)
            {
                shapeOfUnnVessel = PlayerDataAccess.equippedCharm_28;

                PlayMakerFSM.BroadcastEvent("NEW SOUL ORB");
                if (PlayerDataAccess.MPReserveMax > 132)
                {
                    PlayerDataAccess.MPReserveMax = 132;
                }

                var vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 1").GetComponent<PlayMakerFSM>();
                if (PlayerDataAccess.MPReserveMax <= 0)
                {
                    vessel.SendEvent("UNBIND VESSEL ORB");
                    //Log("Hide vessel 1.");
                }
                if (PlayerDataAccess.MPReserveMax <= 33)
                {
                    vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 2").GetComponent<PlayMakerFSM>();
                    vessel.SendEvent("UNBIND VESSEL ORB");
                    //Log("Hide vessel 2.");
                }
                if (PlayerDataAccess.MPReserveMax <= 66)
                {
                    vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 3").GetComponent<PlayMakerFSM>();
                    vessel.SendEvent("UNBIND VESSEL ORB");
                    //Log("Hide vessel 3.");
                }
                if (PlayerDataAccess.MPReserveMax <= 99)
                {
                    vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 4").GetComponent<PlayMakerFSM>();
                    vessel.SendEvent("UNBIND VESSEL ORB");
                    //Log("Hide vessel 4.");
                }
                if (PlayerDataAccess.MPReserve > PlayerDataAccess.MPReserveMax)
                {
                    PlayerDataAccess.MPReserve = PlayerDataAccess.MPReserveMax;
                }
            }
        }
        #endregion

//	Sharp Shadow
        #region Sharp Shadow Changes
        private void SharpShadowHurtBoxSize(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.gameObject.name == "Sharp Shadow" && self.FsmName == "damages_enemy"
                && LS.sharpShadowHurtBoxSize)
            {
                self.gameObject.GetComponent<UnityEngine.BoxCollider2D>().size = new Vector2(3f, 2.5f);
                //Log("Sharp Shadow hurt box size set.");
            }
        }
        private void SharpShadowDamage(On.HutongGames.PlayMaker.Actions.TakeDamage.orig_OnEnter orig, TakeDamage self)
        {
            if (self.Fsm.GameObject.name == "Sharp Shadow" && self.Fsm.Name == "damages_enemy")
            {
                int nailDamage = PlayerDataAccess.nailDamage;
                float dmgMult = PlayerDataAccess.equippedCharm_31 ? LS.sharpShadowDashmasterDamage : LS.sharpShadowDamage;
                self.DamageDealt = (int)Math.Round(nailDamage * dmgMult, MidpointRounding.AwayFromZero);
            }

            orig(self);
        }
        //  Ignore vanilla dashmaster multiplier
        private void SharpShadowDashMasterDamageScaling(On.HutongGames.PlayMaker.Actions.FloatMultiplyV2.orig_OnEnter orig, FloatMultiplyV2 self)
        {
            if(self.Fsm.GameObject.name == "Attacks" && self.Fsm.Name == "Set Sharp Shadow Damage" && self.State.Name == "Master")
            {
                self.multiplyBy.Value = 1f;
            }

            orig(self);
        }
        private void SetSharpShadowDashSpeed(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("DASH_SPEED_SHARP"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(Speed => (PlayerDataAccess.equippedCharm_37 ? LS.sharpShadowSprintmasterDashSpeed : LS.sharpShadowDashSpeed));
        }
        private void SharpShadowSoulGain(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            if (hitInstance.Source.name == "Shadow Dash" && (PlayerDataAccess.equippedCharm_20 || PlayerDataAccess.equippedCharm_21))
            {
                int soulGain = (PlayerDataAccess.equippedCharm_20 ? LS.sharpShadowSoulCatcherSoulGain : 0)
                    + (PlayerDataAccess.equippedCharm_21 ? LS.sharpShadowSoulEaterSoulGain : 0);
                HeroController.instance.AddMPCharge(soulGain);
            }

            orig(self, hitInstance);
        }
        // Sharp Shadow + Stalwart Shell i-frames
        private void SharpShadowStalwartIFrames(On.HeroController.orig_CancelDash orig, HeroController self)
        {
            if (LS.sharpShadowStalwartShellIFrames > 0f && self.cState.shadowDashing && PlayerDataAccess.equippedCharm_16 && PlayerDataAccess.equippedCharm_4)
            {
                HeroController.instance.StartCoroutine("Invulnerable", LS.sharpShadowStalwartShellIFrames);
            }

            orig(self);
        }
        //  Reduce volume of sharp shadow dash sound effect
        private void SharpShadowVolume(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(
                i => i.MatchLdfld<HeroController>("sharpShadowClip"),
                i => i.MatchLdcR4(1)
            );
            cursor.GotoNext();
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(Volume => LS.sharpShadowVolume);
        }
        #endregion

//	Soul Catcher/Eater
        #region Soul Catcher/Eater Changes
        private void SoulCharmChanges(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);

            // Regular
            cursor.TryGotoNext(i => i.MatchLdcI4(11));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.regularSoulGain);

            // Soul Catcher
            cursor.TryGotoNext(i => i.MatchLdcI4(3));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.soulCatcherSoulGain);

            // Soul Eater
            cursor.TryGotoNext(i => i.MatchLdcI4(8));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.soulEaterSoulGain);

            // Regular Reserves
            cursor.TryGotoNext(i => i.MatchLdcI4(6));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.regularReserveSoulGain);

            // Soul Catcher Reserves
            cursor.TryGotoNext(i => i.MatchLdcI4(2));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.soulCatcherReserveSoulGain);

            // Soul Eater Reserves
            cursor.TryGotoNext(i => i.MatchLdcI4(6));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<int, int>>(soul => LS.soulEaterReserveSoulGain);
        }
        #endregion

//	Spell Twister
        #region Spell Twister Changes
        private void SpellTwisterSpellCosts(On.HutongGames.PlayMaker.Actions.IntCompare.orig_OnEnter orig, IntCompare self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name.StartsWith("Can Cast?"))
            {
                self.integer2.Value = (PlayerDataAccess.equippedCharm_33 ? (PlayerDataAccess.equippedCharm_28 ? LS.spellTwisterShapeOfUnnSpellCost : LS.spellTwisterSpellCost) : LS.regularSpellCost);
            }

            orig(self);
        }
        #endregion

//	Spore Shroom
        #region Spore Shroom Changes
        private void SporeShroomDamageDuration(On.HutongGames.PlayMaker.Actions.Wait.orig_OnEnter orig, Wait self)
        {
            // Spore Shroom Cloud
            if (self.Fsm.GameObject.name == "Knight Spore Cloud(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Wait")
            {
                float upgradeRate = (PlayerDataAccess.MPReserveMax + (11 * PlayerDataAccess.vesselFragments)) / 132f;

                // Duration
                float duration = LS.sporeShroomCloudDurationBase + ((LS.sporeShroomCloudDurationMax - LS.sporeShroomCloudDurationBase) * upgradeRate);
                self.time.Value = duration * (PlayerDataAccess.equippedCharm_34 ? LS.sporeShroomDeepFocusDurationMult : 1f);
                //Log("Spore cloud duration: " + duration);

                // Damage Rate
                float damage = LS.sporeShroomDamageBase + ((LS.sporeShroomDamageMax - LS.sporeShroomDamageBase) * upgradeRate);
                if (PlayerDataAccess.equippedCharm_10)
                    damage = LS.sporeShroomDefendersCrestDamageBase + ((LS.sporeShroomDefendersCrestDamageMax - LS.sporeShroomDefendersCrestDamageBase) * upgradeRate);
                self.Fsm.GameObject.GetComponent<DamageEffectTicker>().damageInterval = duration / (damage + 0.0f); //  +0.9f ensures time works out
                //Log("Spore cloud damage: " + damage);
            }

            //  Spore Shroom + Defender's Crest Cloud
            else if (self.Fsm.GameObject.name == "Knight Dung Cloud(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Wait")
            {
                float upgradeRate = (PlayerDataAccess.MPReserveMax + (11 * PlayerDataAccess.vesselFragments)) / 132f;

                // Duration
                float duration = LS.sporeShroomCloudDurationBase + ((LS.sporeShroomCloudDurationMax - LS.sporeShroomCloudDurationBase) * upgradeRate);
                self.time.Value = duration * (PlayerDataAccess.equippedCharm_34 ? LS.sporeShroomDeepFocusDurationMult : 1f);
                //Log("Spore cloud duration: " + duration);

                // Damage Rate
                float damage = LS.sporeShroomDamageBase + ((LS.sporeShroomDamageMax - LS.sporeShroomDamageBase) * upgradeRate);
                if (PlayerDataAccess.equippedCharm_10)
                    damage = LS.sporeShroomDefendersCrestDamageBase + ((LS.sporeShroomDefendersCrestDamageMax - LS.sporeShroomDefendersCrestDamageBase) * upgradeRate);
                self.Fsm.GameObject.GetComponent<DamageEffectTicker>().damageInterval = duration / (damage + 0.0f); //  +0.9f ensures time works out
                //Log("Spore cloud damage: " + damage);

                var damageTicker = self.Fsm.GameObject.GetComponent<DamageEffectTicker>();
                damageTicker.extraDamageType = ExtraDamageTypes.Dung;
                damageTicker.damageEvent = "DUNG";
            }

            // Spore Shroom Cooldown
            else if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spore Cooldown" && self.State.Name == "Cooldown")
            {
                self.time.Value = LS.sporeShroomCooldown;
            }

            orig(self);
        }
        private void SporeShroomRadius(On.HutongGames.PlayMaker.Actions.SetScale.orig_OnEnter orig, SetScale self)
        {
            //Log(self.Fsm.GameObject.name + self.Fsm.Name + self.State.Name);
            if ((self.Fsm.GameObject.name == "Knight Spore Cloud(Clone)" || self.Fsm.GameObject.name == "Knight Dung Cloud(Clone)")
                && self.Fsm.Name == "Control")
            {
                float upgradeRate = (PlayerDataAccess.MPReserveMax + (11 * PlayerDataAccess.vesselFragments)) / 132f;

                // Radius
                float radius = LS.sporeShroomRadiusBase + ((LS.sporeShroomRadiusMax - LS.sporeShroomRadiusBase) * upgradeRate);
                if (PlayerDataAccess.equippedCharm_34)
                    radius *= LS.sporeShroomDeepFocusRadiusMult;
                //Log("Spore cloud radius: " + radius);

                if (self.State.Name == "Normal")
                {
                    //Log("Normal spore shroom cloud radius set.");
                    self.x = radius;
                    self.y = radius;
                }
                else if (self.State.Name == "Deep")
                {
                    self.x = radius;
                    self.y = radius;
                    //Log("Deep focus spore shroom cloud radius set.");
                }
            }

            orig(self);
        }
        private void SporeShroomVisuals(On.HutongGames.PlayMaker.Actions.ActivateGameObject.orig_OnEnter orig, ActivateGameObject self)
        {
            orig(self);

            if ((self.Fsm.GameObject.name == "Knight Spore Cloud(Clone)" || self.Fsm.GameObject.name == "Knight Dung Cloud(Clone)") && self.Fsm.Name == "Control")
            {
                float upgradeRate = (PlayerDataAccess.MPReserveMax + (11 * PlayerDataAccess.vesselFragments)) / 132f;

                // Duration
                float duration = LS.sporeShroomCloudDurationBase + ((LS.sporeShroomCloudDurationMax - LS.sporeShroomCloudDurationBase) * upgradeRate);
                if (PlayerDataAccess.equippedCharm_34)
                    duration *= LS.sporeShroomDeepFocusDurationMult;

                // Radius
                float radius = LS.sporeShroomRadiusBase + ((LS.sporeShroomRadiusMax - LS.sporeShroomRadiusBase) * upgradeRate);
                if (PlayerDataAccess.equippedCharm_34)
                    radius *= LS.sporeShroomDeepFocusRadiusMult;

                if (self.State.Name == "Normal")
                {
                    self.Fsm.GameObject.transform.Find("Pt Normal").gameObject.GetComponent<ParticleSystem>().startLifetime = duration;
                    self.Fsm.GameObject.transform.Find("Pt Normal").gameObject.GetComponent<ParticleSystem>().startSpeed = 40 * radius;
                }
                else if (self.State.Name == "Deep")
                {
                    self.Fsm.GameObject.transform.Find("Pt Deep").gameObject.GetComponent<ParticleSystem>().startLifetime = duration;
                    self.Fsm.GameObject.transform.Find("Pt Deep").gameObject.GetComponent<ParticleSystem>().startSpeed = 40 * radius;
                }
            }
        }
        private void SporeShroomDisableDungCloud(On.HutongGames.PlayMaker.FsmState.orig_OnEnter orig, FsmState self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.Name.StartsWith("Spore Cloud"))
            {
                Satchel.FsmUtil.DisableAction(self, 2);
            }

            orig(self);
        }
        private void SporeShroomDamageReset(On.HutongGames.PlayMaker.Actions.SetBoolValue.orig_OnEnter orig, SetBoolValue self)
        {
            if (self.Fsm.GameObject.name == "Knight" && self.Fsm.Name == "Spell Control" && self.State.Name == "Cancel All" && self.boolVariable.Name == "Spore Cooldown")
            {
                self.boolValue.Value = self.boolVariable.Value && !LS.sporeShroomDamageResetsCooldown;
            }

            orig(self);
        }
        #endregion

//	Sprintmaster
        #region Sprintmaster Changes
        private void SprintmasterSpeed(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("WALK_SPEED"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(speed => LS.regularWalkSpeed);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("RUN_SPEED_CH_COMBO"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(speed => LS.sprintmasterDashmasterSpeed);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("RUN_SPEED_CH"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(speed => LS.sprintmasterSpeed);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("RUN_SPEED"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(speed => (PlayerDataAccess.equippedCharm_37 ? (PlayerDataAccess.equippedCharm_31 ? LS.sprintmasterDashmasterSpeed : LS.sprintmasterSpeed) : LS.regularSpeed));
        }
        #endregion

//	Stalwart Shell
        #region Stalwart Shell Changes
        private void SetDamageIFrames(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("INVUL_TIME_STAL"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => baldurShellBlocked ? LS.stalwartShellBaldurBlockIFrames : LS.stalwartShellIFrames);

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("INVUL_TIME"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => LS.regularIFrames);
        }
        private void SetParryIFrames(On.HeroController.orig_CharmUpdate orig, HeroController self)
        {
            orig(self);

            HeroController.instance.INVUL_TIME_PARRY = (PlayerDataAccess.equippedCharm_4 ? LS.stalwartShellParryIFrames : LS.regularParryIFrames);
            HeroController.instance.INVUL_TIME_CYCLONE = (PlayerDataAccess.equippedCharm_4 ? LS.stalwartShellParryIFrames : LS.regularParryIFrames);
        }
        private void StalwartShellRegenTimer()
        {
            if (stalwartShellRegen)
            {
                stalwartShellTimer -= Time.deltaTime;

                if (stalwartShellTimer < 0f)
                {
                    if (PlayerDataAccess.healthBlue < LS.stalwartShellLifeblood)
                    {
                        EventRegister.SendEvent("ADD BLUE HEALTH");
                        //Log("Stalwart Shell overhealed.");
                    }
                    stalwartShellRegen = false;
                    stalwartShellTimer = 0f;
                    //Log("Stalwart Shell regen paused.");
                }
            }
        }
        private int StalwartShellReactivate(int hazardType, int damageAmount)
        {
            if (PlayerDataAccess.equippedCharm_4 && !PlayerDataAccess.equippedCharm_27 && !PlayerDataAccess.equippedCharm_9)
            {
                stalwartShellRegen = true;
                stalwartShellTimer = LS.stalwartShellCooldown
                    - (PlayerDataAccess.equippedCharm_8 ? LS.stalwartShellLifebloodHeartCooldownReduction : 0)
                    - (PlayerDataAccess.equippedCharm_10 ? LS.stalwartShellDefendersCrestCooldownReduction : 0);
                //Log("Stalwart Shell timer reset to " + stalwartShellTimer);
            }
            else
            {
                stalwartShellRegen = false;
                stalwartShellTimer = 0f;
            }

            return damageAmount;
        }
        private void StalwartShellJonisDamageReduction(On.PlayerData.orig_TakeHealth orig, PlayerData self, int amount)
        {
            if (LS.stalwartShellJonisDamageReduction && PlayerDataAccess.equippedCharm_4 && PlayerDataAccess.equippedCharm_27 && amount > 1)
            {
                //Log("PlayerData.TakeHealth(amount " + amount + ")");
                amount -= 1;
                //Log("Damage reduced by Stalwart Shell + Joni's Blessing synergy");
            }

            orig(self,amount);
        }
        #endregion

//	Steady Body
        #region Steady Body Changes
        //	Reduce Focus Cost
        private void SteadyBodyFocusCost(PlayerData data, HeroController controller)
        {
            if (PlayerDataAccess.equippedCharm_14) {
                PlayerData.instance.focusMP_amount = PlayerDataAccess.equippedCharm_28 ? LS.steadyBodyShapeOfUnnFocusCost : LS.steadyBodyFocusCost;
            }
            else { PlayerData.instance.focusMP_amount = LS.regularFocusCost; }
        }
        private void SetDamageKnockback(ILContext il)
        {
            ILCursor cursor = new ILCursor(il).Goto(0);
            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("RECOIL_DURATION"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => (PlayerDataAccess.equippedCharm_14 ? LS.steadyBodyKnockback : LS.regularDamageKnockback));

            cursor.TryGotoNext(i => i.MatchLdfld<HeroController>("RECOIL_DURATION_STAL"));
            cursor.GotoNext();
            cursor.EmitDelegate<Func<float, float>>(time => (PlayerDataAccess.equippedCharm_14 ? LS.steadyBodyKnockback : LS.regularDamageKnockback));
        }
        //	Negate Hard Fall
        private bool SteadyBodyNegateHardFall(On.HeroController.orig_ShouldHardLand orig, HeroController self, Collision2D collision)
        {
            if (LS.steadyBodyNegateHardFall && PlayerDataAccess.equippedCharm_14)
            {
                return false;
            }

            // Failsafe to reset meteorDrop bool in case of StopAllCoroutines
            meteorDrop = false;

            return orig(self, collision);
        }
		//	Impact damage while falling
        private void SteadyBodyMeteorDrop(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, CollisionSide damageSide, int damageAmount, int hazardType)
        {
            // Steady Body + Defender's Crest
            if (PlayerDataAccess.equippedCharm_14 && PlayerDataAccess.equippedCharm_10 && self.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 - self.MAX_FALL_VELOCITY && !self.cState.spellQuake && !self.cState.shadowDashing)
            {
                damageAmount = 0;
                if (!meteorDrop)
                {
                    go.GetComponent<HealthManager>().ApplyExtraDamage(LS.steadyBodyDefendersCrestImpactDamage);
                    meteorDrop = true;
                    GameManager.instance.StartCoroutine(MeteorDrop());
                }
            }

            orig(self, go, damageSide, damageAmount, hazardType);
        }
        #endregion

//	Thorns of Agony
//  TODO: synergy w/ flukenest to spray flukes
        #region Thorns of Agony Changes
        private void ThornsOfAgonyDamage(On.HutongGames.PlayMaker.Actions.SetFsmInt.orig_OnEnter orig, SetFsmInt self)
        {
            if (self.Fsm.GameObject.name.StartsWith("Hit ") && self.Fsm.Name == "set_thorn_damage" && self.State.Name == "Set")
            {
                int fullHealth = PlayerData.instance.maxHealth + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.joniHealthBlue : 0);
                int currentHealth = PlayerDataAccess.health + (PlayerDataAccess.equippedCharm_27 ? PlayerDataAccess.healthBlue : 0);
                int missingHealth = Math.Max(fullHealth - currentHealth, 1);

                float damage = LS.thornsOfAgonyBaseDamage - LS.thornsOfAgonyDamageIncrease
                    + (baldurShellBlocked ? LS.thornsOfAgonyBaldurBlockDamage : 0)
                    + (missingHealth * (LS.thornsOfAgonyDamageIncrease
                    + (PlayerDataAccess.equippedCharm_10 ? LS.thornsOfAgonyDefendersCrestDamage : 0)
                    + (PlayerDataAccess.equippedCharm_14 ? LS.thornsOfAgonySteadyBodyDamage : 0)
                    + (furyActive ? LS.thornsOfAgonyFotFDamage : 0)));
                self.setValue.Value = (int)Math.Round(damage, MidpointRounding.AwayFromZero);
                //Log("Thorns damage: " + self.setValue.Value + " (" + damage + ")");
            }

            orig(self);
        }
        private void ThornsOfAgonyRadius(On.HutongGames.PlayMaker.Actions.ActivateGameObject.orig_OnEnter orig, ActivateGameObject self)
        {
            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Thorn Counter" && self.State.Name == "Counter")
            {
                float thornsRadius = LS.thornsOfAgonyRadius * (1.0f
                    + (PlayerDataAccess.equippedCharm_4 ? LS.thornsOfAgonyStalwartShellRadius : 0f)
                    + (PlayerDataAccess.equippedCharm_14 ? LS.thornsOfAgonySteadyBodyRadius : 0f)
                    + (baldurShellBlocked ? LS.thornsOfAgonyBaldurBlockRadius : 0f));
                self.Fsm.Variables.GetFsmGameObject("Thorn Hit").Value.transform.SetScaleX(thornsRadius);
                self.Fsm.Variables.GetFsmGameObject("Thorn Hit").Value.transform.SetScaleY(thornsRadius);
                //Log("Thorns radius: " + thornsRadius);

                //  Animation can't be scaled. Instead figure out how to have a burst of thorn projectiles.
            }

            orig(self);
        }
        #endregion

//	Weaversong
        #region Weaversong Changes
        private void WeaverlingAttack(On.HutongGames.PlayMaker.Actions.IntOperator.orig_OnEnter orig, IntOperator self)
        {
            if (self.Fsm.GameObject.name == "Enemy Damager" && self.Fsm.Name == "Attack" && self.State.Name == "Hit")
            {
                if (self.Fsm.GameObject.transform.parent.name == "Weaverling(Clone)")
                {
                    int weaverlingDamage = LS.weaversongDamage
                        - (PlayerDataAccess.equippedCharm_1 ? LS.weaversongGatheringSwarmDamageReduction : 0)
                        + (PlayerDataAccess.equippedCharm_11 ? LS.weaversongFlukenestDamageIncrease : 0)
                        + (PlayerDataAccess.equippedCharm_40 && PlayerDataAccess.grimmChildLevel == 5 ? LS.weaversongCarefreeMelodyDamageIncrease : 0);
                    self.integer2 = weaverlingDamage;

                    int weaverlingSoulGain = LS.weaversongSoulGain
                        + (PlayerDataAccess.equippedCharm_3 ? LS.weaversongGrubsongSoulGain : 0)
                        + (PlayerDataAccess.equippedCharm_40 && PlayerDataAccess.grimmChildLevel == 5 ? LS.weaversongCarefreeMelodySoulGain : 0);
                    HeroController.instance.AddMPCharge(weaverlingSoulGain);
                }
            }

            orig(self);
        }
        //  Ignore vanilla Weaversong + Grubsong soul gain
        private void IgnoreVanillaWeaversongSoulGain(On.HutongGames.PlayMaker.Actions.CallMethodProper.orig_OnEnter orig, CallMethodProper self)
        {
            if (self.Fsm.GameObject.name == "Enemy Damager" && self.Fsm.Name == "Attack" && self.State.Name == "Grubsong" && self.methodName.Value == "AddMPCharge")
            {
                self.parameters = new FsmVar[1] { new FsmVar(typeof(int)) { intValue = 0 } };   //  no soul gain
            }

            orig(self);
        }
        private void WeaversongSpeeds(On.HutongGames.PlayMaker.Actions.RandomFloat.orig_OnEnter orig, RandomFloat self)
        {
            if (self.Fsm.GameObject.name == "Weaverling(Clone)" && self.Fsm.Name == "Control")
            {
                // Speeds
                if (self.State.Name == "Run L")
                {
                    self.min.Value = -LS.weaversongSpeedMax;
                    self.max.Value = -LS.weaversongSpeedMin;
                }
                else if (self.State.Name == "Run R")
                {
                    self.min.Value = LS.weaversongSpeedMin;
                    self.max.Value = LS.weaversongSpeedMax;
                }
            }

            orig(self);
        }
        private void WeaversongSprintmasterSpeed(On.HutongGames.PlayMaker.Actions.SetFloatValue.orig_OnEnter orig, SetFloatValue self)
        {
            // Sprintmaster Multiplier
            if (self.Fsm.GameObject.name == "Weaverling(Clone)" && self.Fsm.Name == "Control" && self.State.Name == "Sprintmaster" && self.State.ActiveActionIndex == 2)
            {
                self.floatValue.Value = 1f + (float)(LS.weaversongSprintmasterSpeedIncrease / 100f);
            }

            orig(self);
        }
        private void WeaverlingSpawner(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.Fsm.GameObject.name == "Charm Effects" && self.Fsm.Name == "Weaverling Control")
            {
                SpawnObjectFromGlobalPool spawner = self.GetFsmAction<SpawnObjectFromGlobalPool>("Spawn", 0);
                SetAudioClip audio1 = self.GetFsmAction<SetAudioClip>("Spawn", 1);
                SetAudioClip audio2 = self.GetFsmAction<SetAudioClip>("Spawn", 3);
                SetAudioClip audio3 = self.GetFsmAction<SetAudioClip>("Spawn", 5);

                self.GetFsmAction<SpawnObjectFromGlobalPool>("Spawn", 0).Enabled = false;
                self.GetFsmAction<SpawnObjectFromGlobalPool>("Spawn", 2).Enabled = false;
                self.GetFsmAction<SpawnObjectFromGlobalPool>("Spawn", 4).Enabled = false;

                self.GetFsmAction<SetAudioClip>("Spawn", 1).Enabled = false;
                self.GetFsmAction<SetAudioClip>("Spawn", 3).Enabled = false;
                self.GetFsmAction<SetAudioClip>("Spawn", 5).Enabled = false;

                self.AddCustomAction("Spawn", () =>
                {
                    int weaverlingCount = (PlayerDataAccess.equippedCharm_1 ? LS.weaversongGatheringSwarmCount : LS.weaversongCount);
                    for (int i = 1; i <= weaverlingCount; i++)
                    {
                        spawner.OnEnter();

                        if (i % 3 == 0)
                        {
                            audio1.OnEnter();
                        }
                        else if (i % 3 == 1)
                        {
                            audio2.OnEnter();
                        }
                        else
                        {
                            audio3.OnEnter();
                        }
                    }
                });
            }
        }
        #endregion

        #endregion

////////////////////////////////////////////////////////////////
//	COROUTINES
		#region Coroutines
//	Reset Steady Body + Defender's Crest Meteor Drop
        private IEnumerator MeteorDrop()
        {
            yield return new WaitForSeconds(0.5f);
            meteorDrop = false;
        }

//	Animate Soul Gain
        private IEnumerator SoulUpdate()
        {
            yield return new WaitForSeconds(0.5f);
            GameCameras.instance.gameObject.transform.Find("HudCamera/Hud Canvas/Soul Orb").gameObject.LocateMyFSM("Soul Orb Control").SendEvent("MP GAIN");
        }

//	Reset Baldur Shell blocker
        private IEnumerator BaldurShellBlock()
        {
            yield return new WaitForSeconds(0.5f);
            baldurShellBlocked = false;
        }

//	Reset Dreamshield blocker
        private IEnumerator DreamshieldBlocked()
        {
            yield return new WaitForSeconds(0.5f);
            dreamshieldBlock = false;
        }
        #endregion
    }
}
