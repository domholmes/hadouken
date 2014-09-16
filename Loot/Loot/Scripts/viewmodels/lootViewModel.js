var buildViewModel = function (scope, interval) {

    var p1Stats = {

        name: "Crusader",
        attacksPerSecond: 1.23,
        critChancePercent: 13.5,
        critHitDamagePercent: 102,
        damageReductionPercent: 79.7,
        physicalResistancePercent: 73,
        meleeDamageReductionPercent: 0,
        maxLife: 659975,
        lifePerSecond: 1291,
        lifeOnHit: 5080,
        maxMeter: 130,
        primaryAttack: {
            damage: 677000,
            meterCost: 0,
            meterOnHit: 5
        },
        secondaryAttack: {
            damage: 1387000,
            meterCost: 25,
            meterOnHit: 0
        }
    };

    var p2Stats = {

        name: "Fallen Overseer",
        attacksPerSecond: 1.2,
        critChancePercent: 13.5,
        critHitDamagePercent: 102,
        damageReductionPercent: 79.7,
        physicalResistancePercent: 73,
        meleeDamageReductionPercent: 0,
        maxLife: 500000,
        lifePerSecond: 0,
        lifeOnHit: 0,
        maxMeter: 0,
        primaryAttack: {
            damage: 1800000,
            meterCost: 0,
            meterOnHit: 0
        },
        secondaryAttack: {
            damage: 1,
            meterCost: 1,
            meterOnHit: 0
        }
    };


    
    
    scope.update = function () {

        scope.p1Life = scope.battle.p1.life;
        scope.p1MaxLife = scope.battle.p1.maxLife;
        scope.p1Meter = scope.battle.p1.meter;
        scope.p1MaxMeter = scope.battle.p1.maxMeter;
        scope.p1Status = scope.battle.p1.status;

        scope.p2Life = scope.battle.p2.life;
        scope.p2MaxLife = scope.battle.p2.maxLife;
        scope.p2Meter = scope.battle.p2.meter;
        scope.p2MaxMeter = scope.battle.p2.maxMeter;
        scope.p2Status = scope.battle.p2.status;
        
        scope.battleStatus = scope.battle.status;
    };

    scope.intervalLengthInMillis = 16.66;
    scope.battle = new Battle(p1Stats, p2Stats);
    scope.update();
    scope.battle.begin();
    scope.p1Name = scope.battle.p1.name;
    scope.p2Name = scope.battle.p2.name;

    interval(scope.update, scope.intervalLengthInMillis);
}