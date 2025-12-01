namespace Core.Models.Enums;

public enum Rank{ D, C, B, A, S}
public enum FacilityType{ Basic, Special }
public enum FacilitySize{ Cramped, Roomy, Vast}

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
    Armory, Library, Sanctuary, Stables, Storehouse, Workshop,
    // Rank B
    NavalBastionFittings, GamingHall, Graveyard, Laboratory,
    Sacristy, SiegeWorkshop, TeleportationCircle, Theatre,
    TrainingArea, Museum,
    // Rank A
    Archive, MeditationChamber, Menagerie, Observatory,
    Pub, Reliquary,
    // Rank S
    Demiplane, Guildhall, Sanctum, WarRoom
}