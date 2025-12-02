namespace BastionsOfMaura.Core.Enums
{
    public enum Rank
    {
        D = 0,
        C = 1,
        B = 2,
        A = 3,
        S = 4
    }

    public enum FacilityType
    {
        Basic,
        Special
    }

    public enum FacilitySize
    {
        Cramped,
        Roomy,
        Vast
    }

    public enum BasicFacilityType
    {
        Barracks,
        Bedroom,
        Harvestarium,
        Kitchen,
        Shop,
        WellRoom,
        Battlements,
        DiningRoom,
        HumbleExterior,
        Parlor,
        Storage
    }

    public enum SpecialFacilityType
    {
        // Rank C
        Armory,
        Library,
        Sanctuary,
        Stables,
        Storehouse,
        Workshop,
        
        // Rank B
        NavalBastionFittings,
        GamingHall,
        Graveyard,
        Laboratory,
        Sacristy,
        SiegeWorkshop,
        TeleportationCircle,
        Theatre,
        TrainingArea,
        Museum,
        
        // Rank A
        Archive,
        MeditationChamber,
        Menagerie,
        Observatory,
        Pub,
        Reliquary,
        
        // Rank S
        Demiplane,
        Guildhall,
        Sanctum,
        WarRoom
    }

    public enum MonthlyEventType
    {
        GoldenAge = 1,
        NothingHappens = 2,
        Siege = 20,
        Invasion = 31,
        Drought = 41,
        Marauders = 56,
        Storm = 66,
        Refugees = 73,
        SearchParty = 86,
        Festival = 94,
        AbnormalPhenomenon = 99
    }

    public enum IndividualEventType
    {
        NothingHappens = 1,
        SurpriseAttack = 14,
        AnimalRescue = 20,
        Sellswords = 26,
        RequestForAid = 32,
        AnonymousLetter = 38,
        DuelToTheDeath = 44,
        TragicAccident = 50,
        Plague = 57,
        CriminalHireling = 63,
        VerminInfestation = 69,
        WanderingProfessionals = 75,
        Guest = 81,
        Ronin = 87,
        ExtraordinaryOpportunity = 93,
        Treasure = 99
    }

    public enum ResourceType
    {
        Herbs,
        Woods,
        Metals,
        Stones,
        Leathers,
        Textiles,
        BeastMeat,
        BeastBlood,
        Gold
    }

    public enum HarvestariumSpecialization
    {
        FertileSoil,    // Herbs
        Quarries,       // Stones
        Mines,          // Metals
        HuntersLodge,   // Leathers
        Pasturelands,   // Meat/Blood
        BlackForest     // Woods/Textiles
    }

    public enum SiegeWeaponType
    {
        Ballista,
        Catapult,
        Cannon  // Naval only
    }

    public enum AttackType
    {
        GiantBoars,
        PiratesBandits,
        Bugbears,
        Zombies,
        GoblinsHobgoblins,
        Barbarians,
        OrcWarriorsShamans,
        RabidWolvesDireWolves,
        MadCultists,
        HarpiesQueen,
        Kobolds,
        Skeletons,
        Giants,
        WorgsDireWorgs,
        Ogres,
        CursedOwls,
        GhostsWraiths,
        Vampires,
        Lycanthropes,
        ChromaticDragons
    }
}