# Charm ReValance

Charm ReValance is an ability and charm overhaul and customization mod for Hollow Knight. It features many changes to both standard abilities and charms with the direct purpose of increasing player choice flexibility when creating builds during randomizer runs. Any charms that I've deemed under utilized have been either modified to be more usable in general or redesigned entirely to help support new play styles that would not otherwise be possible.

This mod's changes do make various parts of the game easier as the player will have access to more diverse options for how to combat any given challenge. However, this mod is not intended purely to make the game easier, and in various ways the dominant strategies have been nerfed to allow other builds and parts of the game shine. In any case, almost all changes made by this mod can be altered via local settings in game to better suit the tastes of the individual player. The local settings default to what I feel is a fun and balanced experience.

While this mod is designed for use with randomizer, I've still kept the gameplay of non-randomized runs in mind when making design decisions, and I believe it will also be a fun addition to an otherwise vanilla playthrough of Hollow Knight.

# Changes

Below is an itemized list of the changes made to each ability and charm. And where I feel it is pertinent, an explanation of my design mentality.

## Abilities

- **Crystal Dash**'s base damage increased to 25.
- Hazards no longer deal damage while the knight is invulnerable.
- In general, modifications to nail damage are additive instead of multiplicative.
- Modifications to nail cooldown have diminishing returns. The second reduction is only half as effective as stated, the third only a third, etc. (The base nail cooldown is unchanged: 0.41s)
- **Nail upgrades** damage increase reduced to +3 (from +4)
- **Nail art damage** is calculated on modified nail damage instead of raw nail damage.
- **Cyclone slash** knockback greatly reduced, making successive hits easier to land.
- **Howling Wraiths** damage per hit reduced to 10 (from 13)
- **Abyss Shriek** damage per hit reduced to 15 (from 20)
- Howling wraiths and abyss shriek knockback greatly reduced, making successive hits easier to land.
- **Desolate Dive** damage reduced to 10 for contact and 15 for shockwave (from 15 and 20, respectively)
- **Descending Dark** damage reduced to 10 for contact and 25 for first shockwave (from 15 and 35/30, respectively)
- The chance for an enemy to grant 1 essence when you've spent more essence than you've gained is increased to 1/20 (from 1/60)
- The height of **Dream Nail**'s hit box increased by 35% 

## Charms

- The order charms appear in the inventory has been changed, placing the charms closer to synergistic charms. This setting is toggleable in the local settings if you don't like it.
- In general, many additional synergies have been added.
- Only changes from vanilla are noted below. Each charm works the same in vanilla except as noted below.

### Baldur Shell

- Number of blocks reduced to 3 (from 4)
- While blocks remain, the knight can overheal by focusing at full health, gaining 1 lifeblood. (Overheal limit 1)
- Enemy knockback from a block is greatly increased.
- Synergy w/ Defender's Cresh: Adds 2 blocks (total 5)
- Synergy w/ Fragile Greed: Blocking a hit causes geo to be dropped from the knight's collected geo. The amount starts at 20 and increases with each subsequent block. As long as the knight carries enough geo to sustain the geo shell, the baldur shell will not break. Sub synergy w/ defender's crest: The amount of geo lost is reduced by 20%.
- Synergy w/ Lifeblood Heart: Baldur shell's overheal limit is increased to 2.

### Carefree Melody

- The chance to block a hit is based on current health instead of number of hits since the last block. The chance starts very low at 8 masks and increases exponentially as current health is reduced until reaching its highest chance of 60% when at 1 mask. The chance is 0% at or above 9 masks.
- Synergy w/ Fragile Heart: There is a chance to heal 1 mask after a successful block. The chance is equal to the chance of a block occuring.
- Summon charms have gained synergies with carefree melody.

### Dashmaster

- Notch cost reduced to 1 (from 2)
- Dashmaster also reduces the cooldown of shadow dash.
- Increases the damage of crystal dash to 40.

### Deep Focus

- Notch cost reduced to 2 (from 4)
- Doubles the damage dealt by crystal dash.
- Increases the charge time of crystal dash by 65%.

### Defender's Crest

- Clouds spawn cooldown reduced to 0.6s (from 0.75s)
- Damage tick rate reduced to 0.24s (from 0.3s)
- Synergy w/ Steady Body: Increases cloud radius by 20% and duration to 1.4s (from 1.1s)
- Synergy w/ Fury of the Fallen: While furious, damage tick rate reduced to 0.18s
- Many other charms have gained synergies with defender's crest.

### Dream Wielder

- Soul gain from dreamnail reduced to 33 (from 66)
- Chance of essence gain from enemies increased to 1/100 and 1/5 (from 1/200 and 1/40, respectively)

### Dreamshield

- Entirely redesigned. The shield now follows directly behind the knight, protecting from attacks from behind. An up or down directional input causes the shield to move to just over the knight's head, allowing it to block projectiles from above.
- The size of the shield is reduced to 75% of its vanilla size.
- When behind the knight, the shield will block incoming attacks from behind, preventing damage but still causing the knight to recoil and gain invulnerability. Blocking a hit causes the shield to be disabled temporarily.
- The shield's contact damage has been decoupled from nail damage. Instead it deals 8 damage plus 1 additional damage for every 100 essence the knight has collected. In addition, the damaged enemy will be knocked away from the knight.
- The shield's reformation time has been increased to 6.4s (from 2s)
- Synergy w/ Defender's Crest: Shield reformation time reduced by 1.2s
- Synergy w/ Dream Wielder: Shield reformation time reduced by 1.6s

### Flukenest

- Notch cost reduced to 2 (from 3)
- Fluke damage reduced to 3 and shaman stone fluke damage reduced to 4 (from 4 and 5, respectively)
- Synergy w/ Fury of the Fallen: While furious, fluke damage increased by 2
- Summon charms have gained synergies with flukenest

#### Flukenest + Defender's Crest

- Volative fluke contact damage increased to 8 for vengeful spirit and 13 for shade soul, or 10 and 18 with shaman stone (from 3, in all cases)
- Volatile fluke cloud duration to 3.6s (from 2.2s) and damage increased to 19 for vengeful spirit and 35 for shade soul (from ~22)
- With shaman stone, the cloud damage is 26 for vengeful spirit and 46 for shade soul.

### Fragile Greed

- Notch cost reduced to 1 (from 2)
- Synergy w/ Soul Catcher or Soul Eater: Gain 1 soul for each geo collected.

### Fragile Heart

- No changes.

### Fragile Strength

- Notch cost reduced to 2 (from 3)
- Increases nail damage by 3 (instead of 50%)

### Fury of the Fallen

- Fury of the Fallen is active while **below 50% health** and/or **while overcharmed**. While fury is active:
- Nail damage increased by 6 (instead of 75%)
- Nail cooldown reduced by 0.1s
- Vengeful spirit and shade soul damage increased by 10
- Howling wraiths and abyss shriek damage per hit increased by 5 and 4, respectively
- Desolate dive and descending damage damage increased by 5 for contact and 5 for shockwave
- Some other damaging charms have gained synergies with fury of the fallen.

### Gathering Swarm

- No changes.
- Summon charms have gained synergies with gathering swarm.

### Glowing Womb

- Hatchlings now damage enemies through shields and armor.
- Soul cost to summon a hatchling increased to 20 (from 8)
- Hatchlings summoned limit reduced to 2 (from 4)
- Hatchling contact damage increased to 15 (from 9)
- Synergy w/ Gathering Swarm: Spawn rate reduced to 2.8s (from 4s), soul cost reduced by 6, summon limit increased to 3
- Synergy w/ Fury of the Fallen: While furious, contact damage increased by 8
- Synergy w/ Flukenest: Contact damage increased by 5
- Synergy w/ Carefree Melody: Soul cost reduced by 6

#### Glowing Womb + Defender's Crest

- Contact damage reduced to 3 (from 4)
- Cloud duration increased to 2.8s and damage to 19 (from 1s and 5, respectively)
- Cloud radius greatly increased
- While furious, cloud duration reduced to 1.1s dealing the same amount of damage

### Grimmchild

- Notch cost increased to 3 (from 2)
- Grimmchild projectile is more accurate and isn't blocked by terrain.
- Grimmchild's detection radius is centered closer to the knight, allowing it to more easily target enemies in front of the knight.
- Attack cooldown increased to 3.6s (from 1.8s)
- Attack damage increased to 11 in phase 2, 18 in phase 3, and 25 in phase 4 (from 5, 8, and 11, respectively)
- Dream nail deals 8 damage plus 1 additional damage for every 100 essence the knight has collected.
- Synergy w/ Gathering Swarm: Attack cooldown reduced to 2.4s
- Synergy w/ Flukenest: Attack damage increased by 7

### Grubberfly's Elegy

- Beam damage increased to 100% of nail damage, including modifications (from 50% nail damage without modifications)
- Synergy w/ Long Nail and Mark of Pride: Beam range and velocity increased by nail range modifications, resulting in a total range increase equal to the percentage nail range increase provided by long nail and mark of pride
- Synergy w/ Joni's Blessing: Elegy is active while blue health is at or above the knight's normal max health without Joni's blessing.
- Synergy w/ Fury of the Fallen: Elegy is active while furious. (This is true in vanilla, but fury's activation is different here.)
- Synergy w/ Soul Catcher: Striking an enemy with the beam generates 3 soul
- Synergy w/ Soul Eater: Striking an enemy with the beam generates 8 soul

### Grubsong

- Soul gain from taking damage reduced to 11 (from 15)
- Synergy w/ Grubberfly's Elegy: Soul gain increases by 16 (instead of by 10)
- Synergy w/ Shape of Unn: Soul gain increases by 6
- Synergy w/ Carefree Melody: A blocked hit still generates soul

### Heavy Blow

- Entirely redesigned. No longer increases knockback of nail strikes.
- Increases nail damage by 5. Increases base nail damage of nail arts by an additional 3.
- Increases nail cooldown to 0.57s (from 0.41s base nail cooldown)
- Additional boss stagger increased to 3 or 2 during combo (from 1 in both cases)
- Nail strikes destroy geo rocks and destructible walls three times faster.
- Knockback of great slash and dash slash doubled
- Synergy w/ Steady Body: Nail cooldown reduced by 0.1s

### Hiveblood

- Notch cost reduced to 3 (from 4)
- Regen cooldown reduced to 8s (from 10s)
- Synergy w/ Fragile Heart: Hiveblood can regen up to 3 masks. Each subsequent mask takes 4.8s longer to regen. This synergy does not apply if Joni's blessing is also equipped.

#### Hiveblood + Joni's Blessing

- Regen cooldown reduced to 9.6s (from 20s)
- Synergy w/ Lifeblood Heart: Hiveblood can regen up to 3 lifeblood. Each subsequent lifeblood takes 6.4s longer to regen.

### Joni's Blessing

- Notch cost reduced to 2 (from 4)
- Adds +3 lifeblood (instead of +40% of max health)

### Kingsoul/Void Heart

- Notch cost reduced to 2 (from 5)
- Soul regen reduced to 3 soul every 3 seconds, or 60 soul per minute (from 4 soul every 2 seconds, or 120 soul per minute)
- Soul regen remains active after kingsoul becomes void heart

### Lifeblood Core

- Entirely redesigned. Lifeblood reduced to 3 (from 4). Every 9.6s, if the knight has less lifeblood than the current overheal limit, lifeblood core consumes 33 soul to add 1 lifeblood. The overheal limit is equal to the maximum number of lifeblood the knight has had since last sitting at a bench, regardless of the source of the lifeblood.
- Synergy w/ Steady Body and/or Spell Twister: Soul cost reduced to 24

### Lifeblood Heart

- No changes

### Long Nail

- Nail range increased by 25% of base nail range (instead of 15%)

### Mark of Pride

- Nail range increased by 15% of base nail range (instead of 25%)
- Nail damage increased by 2
- Nail cooldown reduced by 0.08s

### Nailmaster's Glory

- Nail art damage increased by 10%
- Synergy w/ Mark of Pride: Nail arts bypass shields and armor

### Quick Focus

- Crystal dash charge time reduced to 60%

### Quick Slash

- Notch cost reduced to 2 (from 3)
- Nail cooldown reduced by 0.12s (instead of 0.16s)

### Shaman Stone

- Shade soul damage reduced to 35 (from 40)
- Howling wraiths damage per hit reduced to 14 (from 20)
- Abyss shriek damage per hit reduce to 18 (from 30)
- Desolate dive damage reduced to 15 for contact and 20 for shockwave (from 23 and 30, respectively)
- Descending dark damage reduced to 15 for contact, 30 for shockwave (from 23 and 50, respectively)

### Shape of Unn

- Adds a reserve soul vessel

### Sharp Shadow

- Notch cost increased to 3 (from 2)
- Dash damage increased to 180% of nail damage, including modifications (from 100% without modifications)
- Hurt box size increased to 300% width and 250% height
- Synergy w/ Dashmaster: Damage increased to 240%
- Synegry w/ Sprintmaster: Dash speed (and thus dash length) increased to 36 (normal dash speed is 20, normal sharp shadow dash speed is 28)
- Synergy w/ Soul Catcher: Striking an enemy with sharp shadow grants 8 soul
- Synergy w/ Soul Eater: Striking an enemy with sharp shadow grants 14 soul
- Synergy w/ Stalwart Shell: Gain additional 0.25s of invulnerability

### Soul Catcher

- Notch cost reduced to 1 (from 2)

### Soul Eater

- Notch cost reduced to 3 (from 4)

### Spell Twister

- Increases dream nail range by 25%
- Synergy w/ Shape of Unn: Spell cost further reduced to 22 (from 24 without)

### Spore Shroom

- There is no cooldown, allowing multiple clouds to be created and overlap.
- Spore cloud radius, duration, and damage tick rate improve with the knight's maximum soul reserve. (max 4 reserve soul vessels, 1 from shape of unn)
- Spore cloud radius increases by 10% per reserve vessel (3.33% per vessel fragment)
- Spore cloud duration increases from 4.1s to 8.1s (instead of always 4.1s)
- Spore cloud total damage increases from 15 to 60 (instead of always ~27)
- Synergy w/ Deep Focus: Radius increased by 35%, duration and total damage increased by 20%
- Synergy w/ Defender's Crest: Cloud total damage increased to 20-80

### Sprintmaster

- Sprint speed increased to 10.8 (from 10)
- Airspeed matches grounded speed (airspeed in vanilla is always normal movement speed: 8.3)
- Synergy w/ Dashmaster: Sprint speed increased to 12 (up from 11.5)

### Stalwart Shell

- Vanilla recoil reduction has been removed
- Increases parry invulnerability to 0.4s (from 0.25s)
- Grants 1 lifeblood on bench. 18 seconds after taking damage, regens 1 lifeblood (overheal limit 1)
- Synergy w/ Baldur Shell: When baldur shell blocks a hit, invulnerability time increased to 3.25s (instead of 1.75s)
- Synergy w/ Defender's Crest: Regen cooldown reduced by 3s
- Synergy w/ Lifeblood Heart: Regen cooldown reduced by 7s, but only 1 lifeblood is gained at bench
- Synergy w/ Joni's Blessing: Regen is disabled, but incoming damage is reduced by 1 mask (minimum 1 mask)

### Steady Body

- Notch cost increased to 2 (from 1)
- Entirely redesigned. No longer reduces nail recoil.
- Focus cost reduced to 24 soul (from 33)
- Recoil when taking damage reduced to 0.08 (from 0.2)
- Negates hard fall stagger time
- Synergy w/ Defender's Crest: While falling, become invulnerable and deal 25 contact damage to enemies.
- Synergy w/ Shape of Unn: Focus cost further reduced to 22 soul

### Thorns of Agony

- Damage has been decoupled from nail damage and instead scales with current health, dealing more damage the more damage the knight has taken.
- Base damage starts at 8 and increases by ~1.9 per additional mask lost.
- Damage radius reduced to 80%
- Synergy w/ Baldur Shell: When baldur shell blocks a hit, thorns damage increases by ~10 and radius by 30%
- Synergy w/ Defender's Crest: Damage increases by ~1 per lost mask.
- Syengry w/ Fury of the Fallen: While furious, damage increases by ~2.7 per lost mask.
- Synergy w/ Stalwart Shell: Radius increased by 30%.
- Synergy w/ Steady Body: Damage increases by ~0.7 per lost mask. Radius increased by 15%.
- Synergy w/ Flukenest: Causes a spray of flukes in all directions.

### Wayward Compass

- Notch cost reduced to 0 (from 1)

### Weaversong

- As vanilla, summons 3 weaverlings that deal 2 damage per hit.
- Weaverlings steal 2 soul from each enemy they strike.
- Synergy w/ Gathering Swarm: Increase number of weaverlings to 5 but reduce damage by 1
- Synergy w/ Flukenest: Increase damage by 3 but reduces soul steal by 1
- Synergy w/ Grubsong: Increase soul steal by 1 (down from 3 in vanilla)
- Synergy w/ Carefree Melody: Increase damage and soul steal by 1 each


# Version Change Log

### 1.25.10.07

- Local settings changed to global settings
- Multiple damage ticker effects allowed per frame so overlapping damage tick effects don't interfere with each other
- Increased dream nail hit box height by 40%
- Move dream wielder + spell twister dream nail range increase synergy to just spell twister
- Increased dreamshield notch cost back to 3
- Removed dreamshield's overheal effective
- Increased dreamshield reform time
- Added synergy with dreamshield + dream wielder to reduce shield reform time
- Fixed crystal hunter projectiles so they're blocked by dreamshield
- Fixed flukenest + defender's crest volatile fluke cloud duration
- Rebalanced volatile fluke to deal more damage with cloud than contact
- Fury of the fallen now active under 50% hp instead of under 40%
- Fury nail cooldown reduction increased to 0.1s from 0.08s
- Glowing womb soul cost reduced to 20 from 24
- Glowing womb + defender's crest + fury of the fallen cloud duration is reduced with increased damage rate
- Grimmchild now causes dream nail to deal damage
- Heavy blow + steady body nail cooldown reduction synergy increased to 0.1s (from 0.08s)
- Added missing destructible objects to heavy blow's faster break times
- Thorns of agony + flukenest now causes a spray of flukes when taking damage
- Weaverlings deal less damage but steal more soul. Flukenest counters these changes, increasing damage and reducing soul gain

