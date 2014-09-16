var calculateMitigation = function (playerStats) {

    var dr = playerStats.damageReductionPercent / 100;
    var pr = playerStats.physicalResistancePercent / 100;
    var cr = 0; // Character reduction 30% for barb/monk
    var mr = playerStats.meleeDamageReductionPercent / 100;

    var mitigation = (1 - dr) * (1 - pr) * (1 - cr) * (1 - mr);

    return mitigation;
}