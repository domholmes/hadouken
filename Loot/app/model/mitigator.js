var calculateMitigation = function (playerStats) {

    var level = 70;

    var dr = playerStats.armour / (playerStats.armour + (50 * level));
    var pr = playerStats.physicalResistance / (playerStats.physicalResistance + (5 * level));
    var cr = 0; // Character reduction 30% for barb/monk
    var mr = playerStats.meleeDamageReductionPercent / 100;

    var mitigation = (1-dr) * (1-pr) * (1-cr) * (1-mr);

    return mitigation;
}